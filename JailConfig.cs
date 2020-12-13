using Rocket.API;

namespace Lafalafa.JailPlugin
{
    public class JailConfig : IRocketPluginConfiguration
    {
        public int defaultTime;
        public string imageUrl;
        public bool anounceAll;
        public int defaultRadio;
        public bool removeInventory;
        public int ShirtID;
        public int PantsID;
        public void LoadDefaults()
        {
            ShirtID = 303;
            PantsID = 304;
            anounceAll = false;
            defaultTime = 300;
            defaultRadio = 10;
            imageUrl = "";
            removeInventory = true;

        }
    }
}