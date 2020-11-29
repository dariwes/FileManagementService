using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher
{
    public class EtlOptions
    {
        public StorageOptions StorageOptions { get; set; }
        public ArchiveOptions ArchiveOptions { get; set; }
        public CryptingOptions CryptingOptions { get; set; }
    }

    public class StorageOptions
    {
        public string SourseFileName { get; set; }
        public string TargetDirectory { get; set; }
    }

    public class ArchiveOptions
    {
        public string ZipName { get; set; }
    }

    public class CryptingOptions
    {
        public string EncryptionKey { get; set; }
    }
}
