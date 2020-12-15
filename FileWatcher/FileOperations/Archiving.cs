using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher.FileOperations
{
    public static class Archiving
    {
        public static void AddToArchive(string filePath, string archive)
        {
            try
            {
                using (var zipArchive = ZipFile.Open(archive, ZipArchiveMode.Update))
                {
                    zipArchive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                }
            }
            catch (Exception ex)
            {

                using (var streamWriter = new StreamWriter(
                       Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "errorMessage.txt"),
                       true, Encoding.Default))
                {
                    streamWriter.WriteLine("File archiving error: " + ex.Message);
                }
            }
        }
    }
}
