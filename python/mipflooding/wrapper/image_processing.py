from python.mipflooding.bin.loader import *
from . import image_format as imgf


def run_mip_flooding(color_path: str, mask_path: str, output_path: str, img_format: str = imgf.ImageFormat.PNG) -> None:
    """
    Perform Mipmap Flooding on input color and mask textures to optimize for disk storage.

    Args:
        color_path (str): The absolute path to the color texture image.
        mask_path (str): The absolute path to the mask texture image.
        output_path (str): The absolute path for the output image.
        img_format (str): The image format of the output image, use ImageFormat to call supported formats.
    Example:
        run_mip_flooding('input_color.png', 'input_mask.png', 'output_texture.png', ImageFormat.PNG)
    """
    mip_flooding.RunMipFlooding(color_path, mask_path, output_path, img_format)
