using Microsoft.Win32;
using System;
using System.Collections.Concurrent;
using WRC.Service.Interfaces;
using WRC.Service.Models;

namespace WRC.Service.Services
{
    public class RegistryOptionsProvider : IOptionsProvider
    {
        private readonly ConcurrentDictionary<string, string> _configuration = new ConcurrentDictionary<string, string>();
        
        public RegistryOptionsProvider()
        {
            InitFromRegistry(Constants.RegistryServerUrlKey);
        }

        public string Get(string key) => _configuration[key];

        private void InitFromRegistry(string key)
        {
            using var regKey = Registry.LocalMachine.OpenSubKey(Constants.RegistryKey);
            var value = regKey?.GetValue(key);

            if (value == default || !_configuration.TryAdd(key, value.ToString()))
            {
                throw new ArgumentException($"'{key}' value is not configured");
            }
        }

    }
}
