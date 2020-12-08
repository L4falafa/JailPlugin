using Rocket.API;

namespace Lafalafa.JailPlugin
{
    public class JailConfig : IRocketPluginConfiguration
    {
        public int defaultTime;
        string imageUrl;
        public void LoadDefaults()
        {

            defaultTime = 300;
            imageUrl = "";

        }
    }
}