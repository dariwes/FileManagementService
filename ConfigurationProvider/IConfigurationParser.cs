using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationProvider
{
    public interface IConfigurationParser<out T>
    {
        T Parse();
    }
}
