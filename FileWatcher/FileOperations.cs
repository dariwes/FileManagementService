using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher
{
    public static class FileOperations
    {
        public static void EncryptFile(string inputFile, string outputFile, string encryptionKey)
        {
            var UE = new UnicodeEncoding();
            var key = UE.GetBytes(encryptionKey);
            var fileContents = File.ReadAllBytes(inputFile);

            try
            {
                using (var fileStream = new FileStream(outputFile, FileMode.Create))
                {
                    var rijndaelManaged = new RijndaelManaged();

                    using (var cryptoStream = new CryptoStream(fileStream,
                        rijndaelManaged.CreateEncryptor(key, key), CryptoStreamMode.Write))
                    {
                        foreach (var symbol in fileContents)
                        {
                            cryptoStream.WriteByte(symbol);
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
                    streamWriter.WriteLine("File encryption error: "+ ex.Message);
                }
            }
        }

        public static void DecryptFile(string inputFile, string outputFile, string encryptionKey)
        {
            try 
            {
                var UE = new UnicodeEncoding();
                var key = UE.GetBytes(encryptionKey);
                var content = new List<byte>();

                using (var fileStream = new FileStream(inputFile, FileMode.Open))
                {
                    var rijndaelManaged = new RijndaelManaged();

                    using (var cryptoStream = new CryptoStream(fileStream, 
                        rijndaelManaged.CreateDecryptor(key, key), CryptoStreamMode.Read))
                    {
                        int data;
                        while ((data = cryptoStream.ReadByte()) != -1)
                        {
                            content.Add((byte)data);
                        }
                    }
                }
                using (var fileStreamOut = new FileStream(outputFile, FileMode.Create))
                {
                    foreach (var symbol in content)
                    {
                        fileStreamOut.WriteByte(symbol);
                    }
                }
            }
            catch (Exception ex)
            {

                using (var streamWriter = new StreamWriter(
                       Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "errorMessage.txt"),
                       true, Encoding.Default))
                {
                    streamWriter.WriteLine("File dencryption error: " + ex.Message);
                }
            }
        }

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

        public static void AddToArchive(string filePath, string archive)
        {
            try
            {
                using (ZipArchive zipArchive = ZipFile.Open(archive, ZipArchiveMode.Update))
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
