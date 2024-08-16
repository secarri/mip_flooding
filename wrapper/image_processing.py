from dll_loader import *
import image_format as imgf


def run_mip_flooding(color_path: str, mask_path: str, output_path: str, format: str) -> None:
    """
    Perform Mipmap Flooding on input color and mask textures to optimize for disk storage.

    Args:
        color_path (str): The absolute path to the color texture image.
        mask_path (str): The absolute path to the mask texture image.
        output_path (str): The absolute path for the output image.
        format (str): The image format of the output image, use ImageFormat to call supported formats.
    Example:
        run_mip_flooding('input_color.png', 'input_mask.png', 'output_texture.png', ImageFormat.PNG)
    """
    run_main = mip_flooding.RunMipFlooding(color.__str__(), mask.__str__(), out.__str__(), format)


if __name__ == "__main__":
    wrapper_path = Path(__file__).parent.parent
    print(wrapper_path)
    color = wrapper_path / Path("source\\MipFlooding\\tests\\Albedo_4K__wfdgcg0p2.png")
    mask = wrapper_path / Path("source\\MipFlooding\\tests\\Opacity_4K__wfdgcg0p2.png")
    out = wrapper_path / Path("source\\MipFlooding\\tests\\outs\\output.png")

    run_mip_flooding(str(color), str(mask), str(out), imgf.ImageFormat.PNG)
