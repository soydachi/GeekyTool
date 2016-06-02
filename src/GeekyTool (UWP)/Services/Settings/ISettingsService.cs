namespace GeekyTool.Services
{
    public interface ISettingsService
    {
        void Save<T>(string key, T value, bool roamSetting = default(bool));
        T Retrieve<T>(string key, bool roamSettings = default(bool));
        void Clear(bool roamSettings = default(bool));
    }
}