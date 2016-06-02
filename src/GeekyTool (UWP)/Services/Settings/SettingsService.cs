using System;
using System.Collections.Generic;
using Windows.Storage;

namespace GeekyTool.Services
{
    public class SettingsService : ISettingsService
    {
        public void Save<T>(string key, T value, bool roamSetting)
        {
            if (string.IsNullOrEmpty(key) || value == null) throw new ArgumentNullException();

            var settingsContainer = roamSetting 
                ? ApplicationData.Current.RoamingSettings 
                : ApplicationData.Current.LocalSettings;

            if (settingsContainer.Values.ContainsKey(key))
                settingsContainer.Values[key] = value;
            else
                settingsContainer.Values.Add(new KeyValuePair<string, object>(key, value));
        }

        public T Retrieve<T>(string key, bool roamSetting)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException();

            var settingsContainer = roamSetting 
                ? ApplicationData.Current.RoamingSettings 
                : ApplicationData.Current.LocalSettings;

            if (settingsContainer.Values.ContainsKey(key))
                return (T)settingsContainer.Values[key];
            return default(T);
        }

        public void Clear(bool roamSetting)
        {
            var settingsContainer = roamSetting
                ? ApplicationData.Current.RoamingSettings
                : ApplicationData.Current.LocalSettings;

            settingsContainer.Values.Clear();
        }
    }
}