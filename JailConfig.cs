using Rocket.API;
using UnityEngine;

namespace Lafalafa.JailPlugin
{
    public class JailConfig : IRocketPluginConfiguration
    {
        public int defaultTime;
        public string imageUrl;
        public bool broadcastEscape;
        public bool broadcastRelease;
        public int defaultRadio;
        public bool removeInventory;
        public int ShirtID;
        public int PantsID;
        public bool God;
        public bool teleportToSpawn;
        public Vector3 spawn;
        public void LoadDefaults()
        {
            God = false;
            ShirtID = 303;
            PantsID = 304;
            broadcastEscape = false;
            broadcastEscape = false;
            defaultTime = 300;
            defaultRadio = 10;
            teleportToSpawn = false;
            imageUrl = "https://cdn.discordapp.com/attachments/382220784040673281/787627393283915816/prison.png";
            removeInventory = true;
            spawn = new Vector3();
        }
    }
}