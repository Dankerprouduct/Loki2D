using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Loki2D.Core.Utilities
{
    public static class JsonHelper
    {
        
        public static void Save(object data, string path)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(json);
            }
        }

        public static T Load<T>(string path)
        {
            var obj = JsonConvert.DeserializeObject<T>(path);
            return obj;
        }
    }
}
