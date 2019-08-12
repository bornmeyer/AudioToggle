using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.Models
{
    public class DefaultConfigurationWriter : IConfigurationWriter
    {
        // Fields

        private readonly IConfigFilePathLocator _configFilePathLocator = null;


        // Constructors

        public DefaultConfigurationWriter(IConfigFilePathLocator configFilePathLocator)
        {
            _configFilePathLocator = configFilePathLocator;
        }

        public void Write(Configuration configuration)
        {
            var filePath = _configFilePathLocator.Locate();
            using (StreamWriter file = File.CreateText(filePath.FullName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, configuration);
            }
        }
    }
}
