namespace GeekyTool
{
    public class AdaptiveImageSource
    {
        public AdaptiveImageSource(string hiResSource, string lowResSource)
        {
            HiResSource = hiResSource;
            LowResSource = lowResSource;
        }

        public string HiResSource { get; private set; }

        public string LowResSource { get; private set; }
    }
}