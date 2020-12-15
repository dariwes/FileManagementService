using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.XmlGeneratorService
{
    interface IGenerator<T>
    {
        void Generate(string path, List<T> list);
    }
}
