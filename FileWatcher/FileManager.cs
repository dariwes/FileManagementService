using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using ConfigurationProvider;

namespace FileWatcher
{
    class FileManager
    {
        private readonly EtlOptions configOptions;
        private bool isEnabled = true;

        public FileManager()                                                                                                               
        {
            var optionsManager = new OptionsManager(AppDomain.CurrentDomain.BaseDirectory);
            configOptions = optionsManager.GetOptions<EtlOptions>();
        }

        public void Start()
        {
            if (configOptions != null)
            {
                FileTransfer();
            }

            while (isEnabled)
            {
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            isEnabled = false;
        }

        private void FileTransfer()
        {
            var control = new object();

            lock (control)
            {
                var path = Path.GetDirectoryName(configOptions.StorageOptions.SourseFileName);
                var dirInfo = new DirectoryInfo(path);
                var fileName = Path.GetFileName(configOptions.StorageOptions.SourseFileName);
                var date = DateTime.Now;
                var subPath = $"{date.ToString("yyyy", DateTimeFormatInfo.InvariantInfo)}\\" +
                   $"{date.ToString("MM", DateTimeFormatInfo.InvariantInfo)}\\" +
                   $"{date.ToString("dd", DateTimeFormatInfo.InvariantInfo)}";
                var newPath = path +
                   $"\\{date.ToString("yyyy", DateTimeFormatInfo.InvariantInfo)}\\" +
                   $"{date.ToString("MM", DateTimeFormatInfo.InvariantInfo)}\\" +
                   $"{date.ToString("dd", DateTimeFormatInfo.InvariantInfo)}\\" +
                   $"{Path.GetFileNameWithoutExtension(fileName)}_" +
                   $"{date.ToString(@"yyyy_MM_dd_HH_mm_ss", DateTimeFormatInfo.InvariantInfo)}" +
                   $"{Path.GetExtension(fileName)}";
                var compressedPath = Path.ChangeExtension(newPath, "gz");
                var newCompressedPath = Path.Combine(configOptions.StorageOptions.TargetDirectory,
                    Path.GetFileName(compressedPath));
                var decompressedPath = Path.ChangeExtension(newCompressedPath, "txt");

                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }

                dirInfo.CreateSubdirectory(subPath);
                File.Move(configOptions.StorageOptions.SourseFileName, newPath);
                FileOperations.EncryptFile(newPath, newPath, 
                    configOptions.CryptingOptions.EncryptionKey);
                FileOperations.Compress(newPath, compressedPath);
                File.Move(compressedPath, newCompressedPath);
                FileOperations.Decompress(newCompressedPath, decompressedPath);
                FileOperations.DecryptFile(decompressedPath, decompressedPath, 
                    configOptions.CryptingOptions.EncryptionKey);
                FileOperations.AddToArchive(decompressedPath, 
                    configOptions.ArchiveOptions.ZipName);
                File.Delete(newPath);
                File.Delete(newCompressedPath);
                File.Delete(decompressedPath);
            }
        }
    }
}
