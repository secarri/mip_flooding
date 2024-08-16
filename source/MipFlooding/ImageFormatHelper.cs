using System;
using System.Collections.Generic;
using System.Drawing.Imaging;


namespace ImageProcessingLibrary
{
    public static class ImageFormatHelper
    {
        private static readonly Dictionary<string, ImageFormat> ImageFormats = new Dictionary<string, ImageFormat>(StringComparer.OrdinalIgnoreCase)
    {

        { "jpg", ImageFormat.Jpeg },
        { "jpeg", ImageFormat.Jpeg },
        { "png", ImageFormat.Png },
        { "tiff", ImageFormat.Tiff },
        { "bmp", ImageFormat.Bmp },
        { "gif", ImageFormat.Gif },
        { "tif", ImageFormat.Tiff },
    };

        public static ImageFormat GetImageFormat(string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                throw new ArgumentNullException(nameof(format), "Format string cannot be null or empty.");
            }

            if (ImageFormats.TryGetValue(format.Trim(), out ImageFormat imageFormat))
            {
                return imageFormat;
            }

            throw new ArgumentException("Unknown image format: " + format, nameof(format));
        }
    }
}
