using System;
using System.Collections.Generic;
using Windows.Storage;

namespace GeekyTool.Helpers
{
    public class Settings
    {
        public static void Save<T>(string key, T value, bool roamSetting = default(bool))
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

        public static T Retrieve<T>(string key, bool roamSetting = default(bool))
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException();

            var settingsContainer = roamSetting
                ? ApplicationData.Current.RoamingSettings
                : ApplicationData.Current.LocalSettings;

            if (settingsContainer.Values.ContainsKey(key))
                return (T)settingsContainer.Values[key];
            return default(T);
        }

        public static void Clear(bool roamSetting = default(bool))
        {
            var settingsContainer = roamSetting
                ? ApplicationData.Current.RoamingSettings
                : ApplicationData.Current.LocalSettings;

            settingsContainer.Values.Clear();
        }
    }
}