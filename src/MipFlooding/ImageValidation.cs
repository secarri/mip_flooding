using System;


namespace ImageProcessingLibrary
{
    public class ImageValidation
    {
        public static bool ValidateInputs(int colorWidth, int colorHeight, int alphaWidth, int alphaHeight, Logger logger)
        {
            // Check if resolutions match
            if (colorWidth != alphaWidth || colorHeight != alphaHeight)
            {
                logger.LogError($"--- Inputs do not match in resolution. Skipping...");
                return false;
            }

            // Check if images are power of two
            if ((colorWidth & (colorWidth - 1)) != 0 || (colorHeight & (colorHeight - 1)) != 0)
            {
                logger.LogError($"--- Input is not a power of two image. Skipping...");
                return false;
            }

            return true;
        }

    }
}
