using System.IO;


namespace ImageProcessingLibrary
{
    public class FileUtilities
    {
        public static long GetFileSize(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Length;
        }
    }
}
