namespace GeekyTool.Services
{
    public interface ISettingsService
    {
        void Save<T>(string key, T value);
        T Retrieve<T>(string key);
        void Clear();
    }
}