using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestProject.Classes
{
    public static class GenericExtensions
    {
        private static JsonSerializerSettings SettingsDefault()
        {
            return new JsonSerializerSettings { Formatting = Formatting.Indented };
        }

        /// <summary>
        /// Write list to json file
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="list"></param>
        /// <param name="fileName"></param>
        public static void ModeListToJson<TModel>(this List<TModel> list, string fileName)
        {

            JsonConvert.DefaultSettings = SettingsDefault;

            var json = JsonConvert.SerializeObject(list);

            File.WriteAllText(fileName, json);

        }
	}
}
