using Rocket.API;

namespace Lafalafa.JailPlugin
{
    public class JailConfig : IRocketPluginConfiguration
    {
        public int defaultTime;
        public string imageUrl;
        public bool anounceAll;
        public void LoadDefaults()
        {
            anounceAll = false;
            defaultTime = 300;
            imageUrl = "";

        }
    }
}