import os
import time
from pathlib import Path

from python.mipflooding.wrapper import image_processing
from python.mipflooding.wrapper import batch_processing

# Variables for single thread test
wrapper_path = Path(__file__).parent
color = wrapper_path / Path("src\\MipFlooding\\tests\\poppy_C.jpg")
mask = wrapper_path / Path("src\\MipFlooding\\tests\\poppy_A.jpg")
out = wrapper_path / Path("src\\MipFlooding\\tests\\outs\\output_bug.png")

# Variables for multi thread test
directory = wrapper_path / Path("src\\MipFlooding\\tests")
output_dir = wrapper_path / Path("src\\MipFlooding\\tests\\outs")


def get_files(path, pattern="_C"):
    files = os.listdir(path)
    return [os.path.join(path, file) for file in files if pattern in file]


def run_single_test():
    start_time = time.perf_counter()
    image_processing.run_mip_flooding(str(color), str(mask), str(out))
    end_time = time.perf_counter()
    print(f"Single thread time: {end_time - start_time:,.2f} sec.")


def run_multi_test():
    start_time = time.perf_counter()
    batch_processing.run_batch_mip_flood(get_files(directory), str(output_dir))
    end_time = time.perf_counter()
    print(f"Multi thread time: {end_time - start_time:,.2f} sec.")


if __name__ == "__main__":
    # Single Thread Mip Flooding
    run_single_test()
    # Multi Thread Mip Flooding
    run_multi_test()
