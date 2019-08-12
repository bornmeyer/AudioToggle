using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.Models
{
    public class DefaultConfigurationReader : IConfigurationReader
    {
        // Fields

        private readonly IConfigFilePathLocator _configFilePathLocator = null;

        // Constructors

        public DefaultConfigurationReader(IConfigFilePathLocator configFilePathLocator)
        {
            _configFilePathLocator = configFilePathLocator;         
        }

        // Methods

        public Configuration Read()
        {
            Configuration result = null;

            var filePath = _configFilePathLocator.Locate();

            using (StreamReader fileReader = File.OpenText(filePath.FullName))
            {
                JsonSerializer serializer = new JsonSerializer();
                result = (Configuration)serializer.Deserialize(fileReader, typeof(Configuration));
            }

            return result;
        }
    }
}
