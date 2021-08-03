using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataLibraryCore.Helpers
{
    public class JsonHelpers
    {
        /// <summary>
        /// Serialize a list of T (model) with formatting
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static string Serialize<T>(List<T> sender) => JsonConvert.SerializeObject(sender, Formatting.Indented);
    }
}
