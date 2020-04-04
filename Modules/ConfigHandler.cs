using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace VeryCoolBot
{
    public class ConfigHandler
    {
        private Config conf;
        private string configPath, line;

        struct Config
        {
            public string token;
        }

        public ConfigHandler()
        {
            conf = new Config()
            {
                token = ""
            };
        }

        public async Task PopulateConfig()
        {
            configPath = Path.Combine(Directory.GetCurrentDirectory(), "config.json").Replace(@"\", @"\\");
            Console.WriteLine(configPath);

            if (!File.Exists(configPath))//Create the new config file to be filled out
            {

                using (StreamWriter sw = File.AppendText(configPath))
                {
                    sw.WriteLine(JsonConvert.SerializeObject(conf));
                }
                Console.WriteLine("WARNING! New Config initialized! Need to fill in values before running commands!");
                throw new Exception("NO CONFIG AVAILABLE! Go to executable path and fill out newly created file!");
            }

            using (StreamReader reader = new StreamReader(configPath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    conf = JsonConvert.DeserializeObject<Config>(line);
                }
            }

            await Task.CompletedTask;
        }

        public string GetToken()
        {
            return conf.token;
        }
    }
}
