using System;
using System.Collections.Generic;
using Windows.Storage;

namespace GeekyTool.Services
{
    public class SettingsService : ISettingsService
    {
        public void Save<T>(string key, T value)
        {
            if (key == null || value == null) throw new ArgumentNullException();

            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey(key))
                localSettings.Values[key] = value;
            else
                localSettings.Values.Add(new KeyValuePair<string, object>(key, value));
        }

        public T Retrieve<T>(string key)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey(key))
                return (T) localSettings.Values[key];
            else
                return default(T);
        }

        public void Clear()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values.Clear();
        }
    }
}