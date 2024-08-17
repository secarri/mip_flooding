import os
import sys
import subprocess
from pathlib import Path

import clr


def unblock_library(path, lib_name):
    dll = path / lib_name
    if os.name == 'nt':
        command = f"Unblock-File -Path '{dll}'"
        new_command = f'powershell -Command "{command}"'
        try:
            subprocess.check_call(new_command, shell=True)
            print(f"{dll} has been unblocked.")
        except subprocess.CalledProcessError as e:
            print(f"Failed to unblock {dll}: {e}")
    else:
        print("Unblocking is not necessary on this OS.")


dll_name = Path("ImageProcessingLibrary.dll")
dll_path = Path(__file__).parent.parent / Path("net4.7.2")

unblock_library(dll_path, dll_name)

sys.path.append(str(dll_path))
clr.AddReference(str(dll_name.stem))

# noinspection PyUnresolvedReferences
from ImageProcessingLibrary import MipFlooding as mip_flooding

if __name__ == "__main__":
    print(dll_path)
