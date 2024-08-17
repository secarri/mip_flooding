from dataclasses import dataclass


@dataclass(frozen=True)
class ImageFormat:
    """
    A class representing various image formats as attributes.
    This class can be used to access standardized strings representing
    image formats, which can be used for image processing, loading,
    saving, or any related tasks.

    Attributes:
    JPG (str): Joint Photographic Experts Group, a commonly used method of lossy compression for digital images.
    JPEG (str): Joint Photographic Experts Group, a commonly used method of lossy compression for digital images.
    PNG (str): Portable Network Graphics, a raster-graphics file-format that supports lossless data compression.
    TIFF (str): Tagged Image File Format, a file format for storing raster graphics images.
    BMP (str): Bitmap image file format.
    GIF (str): Graphics Interchange Format, a bitmap image format.
    Default (None): An attribute to handle unspecified or unknown formats.
    """
    JPG: str = "JPG"
    JPEG: str = "JPEG"
    PNG: str = "PNG"
    TIFF: str = "TIFF"
    BMP: str = "BMP"
    GIF: str = "GIF"
    Default: None = None
