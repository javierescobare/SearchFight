using System;
using Microsoft.Extensions.Configuration;

namespace Searchfight.Client
{
    public class ConfigurationManager
    {
        private readonly IConfiguration config;

        public ConfigurationManager()
        {
            config = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();
        }

        public string GetSettingByKey(string key)
        {
            var setting = config.GetSection(key);
            if (setting == null)
            {
                throw new Exception($"Could not retrieve setting with key {key}");
            }
            return setting.Value;
        }
    }
}
