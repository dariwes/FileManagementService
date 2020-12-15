using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher.FileOperations
{
    public static class Compression
    {
        public static void Compress(string sourceFile, string compressedFile)
        {
            try
            {
                using (var sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
                {
                    using (var targetStream = File.Create(compressedFile))
                    {
                        using (var compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                        {
                            sourceStream.CopyTo(compressionStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (var streamWriter = new StreamWriter(
                       Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "errorMessage.txt"),
                       true, Encoding.Default))
                {
                    streamWriter.WriteLine("File compression error: " + ex.Message);
                }
            }
        }

        public static void Decompress(string compressedFile, string targetFile)
        {
            try
            {
                using (var sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
                {
                    using (var targetStream = File.Create(targetFile))
                    {
                        using (var decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(targetStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                using (var streamWriter = new StreamWriter(
                       Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "errorMessage.txt"),
                       true, Encoding.Default))
                {
                    streamWriter.WriteLine("File decompression error: " + ex.Message);
                }
            }
        }
    }
}
