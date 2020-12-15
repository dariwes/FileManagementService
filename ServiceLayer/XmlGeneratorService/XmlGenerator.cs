using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ServiceLayer.XmlGeneratorService
{
    public class XmlGenerator<T> : IGenerator<T>
    {
        public void Generate(string path, List<T> list)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<T>));

            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fileStream, list);
            }

            GenerateXsd(path);
        }

        private void GenerateXsd(string path)
        {
            XmlSchemaInference infer = new XmlSchemaInference();
            XmlSchemaSet schemaSet = infer.InferSchema(new XmlTextReader(path));

            using (var xmlWriter = XmlWriter.Create(Path.ChangeExtension(path, ".xsd")))
            {
                foreach (XmlSchema schema in schemaSet.Schemas())
                {
                    schema.Write(xmlWriter);
                }
            }
        }
    }
}
