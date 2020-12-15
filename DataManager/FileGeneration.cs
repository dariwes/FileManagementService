using DataAccessLayer.Repository;
using Models;
using ServiceLayer.XmlGeneratorService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
    public class FileGeneration
    {
        private readonly EtlOptions configOptions;

        public FileGeneration(EtlOptions configOptions)
        {
            this.configOptions = configOptions;
        }

        public void GenerateXmlFile()
        {
            var access = new PersonRepository(configOptions.DataBaseOptions.ConnectionString);
            access.GetPerson();
            var xmlGenerator = new XmlGenerator<Person>();
            xmlGenerator.Generate(configOptions.StorageOptions.SourseFileName, access.People);
        }
    }
}
