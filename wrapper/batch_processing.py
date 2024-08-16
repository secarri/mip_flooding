import os
from concurrent.futures import ThreadPoolExecutor
from pathlib import Path
from typing import List

from . import image_processing


def _match_mask(image_path: str, color_name_pattern: str, mask_name_pattern: str) -> str | None:
    mask_path = image_path.replace(color_name_pattern, mask_name_pattern)
    return mask_path if Path(mask_path).exists() else None


def _mip_flooding_task(file: str, mask: str, output: str) -> None:
    """Function to run the mip flooding algorithm on a single file, used as a task for the ThreadPoolExecutor."""
    image_processing.run_mip_flooding(file, mask, output)


def run_batch_mip_flood(files: List[str], output_dir: str, input_color_pattern: str = "_C",
                        input_mask_pattern: str = "_A", output_pattern: str = "_C",
                        max_workers: int | None = None) -> None:
    """
    Function to process all relevant files in parallel.
    From doc: ""If max_workers is None or not given, it will default to the number of processors on the machine,
    multiplied by 5": https://docs.python.org/3.10/library/concurrent.futures.html#concurrent.futures.ThreadPoolExecutor
    """
    out_log_file = Path(os.path.join(output_dir, 'batch_mipmap_flooding.txt'))

    def _run_batch_mip_flood() -> None:
        with ThreadPoolExecutor(max_workers=max_workers) as executor:
            futures = []
            for file in files:
                mask = _match_mask(file, input_color_pattern, input_mask_pattern)
                if mask is None:
                    continue
                file_name = file.replace(input_color_pattern, output_pattern)
                output = os.path.join(output_dir, Path(file_name).name)
                futures.append(executor.submit(_mip_flooding_task, file, mask, output))
            for future in futures:
                future.result()

    _run_batch_mip_flood()
