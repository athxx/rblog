using System.IO.Compression;

namespace Core.Util.File;

public static class Zipper
{
    public static void Zip(string sourceFilePath, string zipFilePath)
    {
        using (FileStream sourceStream = new FileStream(sourceFilePath, FileMode.Open))
        using (FileStream zipStream = new FileStream(zipFilePath, FileMode.Create))
        using (GZipStream gzipStream = new GZipStream(zipStream, CompressionMode.Compress))
        {
            sourceStream.CopyTo(gzipStream);
        }
    }

    public static void Unzip(string zipFilePath, string destinationFilePath)
    {
        using (FileStream zipStream = new FileStream(zipFilePath, FileMode.Open))
        using (GZipStream gzipStream = new GZipStream(zipStream, CompressionMode.Decompress))
        using (FileStream destinationStream = new FileStream(destinationFilePath, FileMode.Create))
        {
            gzipStream.CopyTo(destinationStream);
        }
    }
}