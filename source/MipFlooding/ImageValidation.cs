using System;


namespace ImageProcessingLibrary
{
    public class ImageValidation
    {
        public static bool ValidateInputs(int colorWidth, int colorHeight, int alphaWidth, int alphaHeight)
        {
            // Check if resolutions match
            if (colorWidth != alphaWidth || colorHeight != alphaHeight)
            {
                return false;
            }

            // Check if images are power of two
            if ((colorWidth & (colorWidth - 1)) != 0 || (colorHeight & (colorHeight - 1)) != 0)
            {
                return false;
            }

            return true;
        }

    }
}
