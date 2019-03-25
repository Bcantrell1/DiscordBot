using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace DiscordBot
{
    class Utilities
    {
        private static Dictionary<string, string> alerts;

        static Utilities()
        {
            //System IO to read file known as our JSON
            string json = File.ReadAllText("SystemLan/alerts.json");
            //Converting from JSON to readable format and putting into text format.
            var data = JsonConvert.DeserializeObject<dynamic>(json);

            alerts = data.ToObject<Dictionary<string, string>>();
        }

        public static string GetAlert(string key)
        {
            if (alerts.ContainsKey(key)) return alerts[key];
            return "WWWRRRONNGG";
        }
    }
}
