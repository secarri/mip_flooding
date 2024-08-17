using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;


namespace ImageProcessingLibrary
{
    public class MipFlooding
    {
        private static Bitmap StackMipLevels(Bitmap background, int mipLevels, Bitmap color, Bitmap alpha, int originalWidth, int originalHeight)
        {
            // This takes 60% of the time, maybe it can be optimized even more. 
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Start iterating over the mip levels
            Bitmap maskedColor = ImageProcessor.ApplyAlpha(color, alpha);

            for (int mipLevel = mipLevels - 1; mipLevel >= 0; mipLevel--)
            {
                int tempWidth = (int)Math.Pow(2, mipLevel + 1);
                int tempHeight = ImageProcessor.CalculateImageHeight(tempWidth, color);
                Bitmap resizedColor = ImageProcessor.ResizeImage(maskedColor, originalWidth / tempWidth, originalHeight / tempHeight, System.Drawing.Drawing2D.InterpolationMode.Bilinear);
                Bitmap resizedAlpha = ImageProcessor.ResizeImage(alpha, originalWidth / tempWidth, originalHeight / tempHeight, System.Drawing.Drawing2D.InterpolationMode.Bilinear);

                // Re-normalize color using the resized alpha
                Bitmap normalizedColor = ImageProcessor.NormalizeColor(resizedColor, resizedAlpha);

                // Combine color and alpha
                Bitmap combinedColor = ImageProcessor.CombineColorAndAlpha(normalizedColor, resizedAlpha);

                Bitmap colorToStack = ImageProcessor.ResizeImage(combinedColor, originalWidth, originalHeight, System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor);

                using (Graphics g = Graphics.FromImage(background))
                {
                    g.DrawImage(colorToStack, 0, 0, colorToStack.Width, colorToStack.Height);
                }
                resizedColor.Dispose();
                resizedAlpha.Dispose();
                normalizedColor.Dispose();
            }
            color.Dispose();
            alpha.Dispose();
            maskedColor.Dispose();
            
            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;
            Console.WriteLine($"--- StackMipLevels Time: {elapsedTime.TotalSeconds:F6} seconds.");
            
            return background;
        }

        public static void RunMipFlooding(string inTexColorAbsPath, string inTexAlphaAbsPath, string outAbsPath, string format)
        {
            // Start Mip Flooding
            Console.WriteLine("- Starting mip flooding algorithm...");
            Stopwatch stopwatch = Stopwatch.StartNew();
            Bitmap color = new Bitmap(inTexColorAbsPath);
            Bitmap alpha = new Bitmap(inTexAlphaAbsPath);

            // Get output format
            ImageFormat outFormat = ImageFormatHelper.GetImageFormat(format);

            // Caching resolutions
            int colorWidth = color.Width;
            int colorHeight = color.Height;
            int alphaWidth = alpha.Width;
            int alphaHeight = alpha.Height;

            // Run validation on inputs
            if (ImageValidation.ValidateInputs(colorWidth, colorHeight, alphaWidth, alphaHeight) == false)
            {
                return;
            }

            // Get mip levels
            int getMipLevels = ImageProcessor.GetMipLevels(colorWidth, colorHeight);

            // Generate the background
            Bitmap background_img = ImageProcessor.GenerateAverageColorImage(color);

            // Run stacking process
            StackMipLevels(background_img, getMipLevels, color, alpha, colorWidth, colorHeight).Save(outAbsPath, outFormat);
            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;

            // Calculate improvement
            long oldFileSize = FileUtilities.GetFileSize(inTexColorAbsPath);
            long newFileSize = FileUtilities.GetFileSize(outAbsPath);
            long optimizedPercentage = (newFileSize - oldFileSize) * 100 / oldFileSize;
            Console.WriteLine($"--- Final image is {optimizedPercentage}% smaller in disk.");
            Console.WriteLine($"- Mip Flooding Time: {elapsedTime.TotalSeconds:F6} seconds.");
        }
    }
}
