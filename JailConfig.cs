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
        public ushort adminPlayersOnlineEffectID;
        public ushort jailsEffectID;
        public ushort addPrisonerEffectID;
        public ushort infoPrisonerEffectID;
        public ushort jailAdminEffectID;

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
            //TODO  jailsEffectID = 29111 key = 29211
            jailsEffectID = 29111;
            //TODO adminPlayersOnlineEffectID = 29110 key = 29210
            adminPlayersOnlineEffectID = 29110;
            //TODO addPrisonerEffectID = 29112 key = 29212
            addPrisonerEffectID = 29112;
            //TODO infoPrisonerEffectID = 29113 key = 29213
            infoPrisonerEffectID = 29113;
            //TODO jailAdminEffectID = 29114; key = 29214
            jailAdminEffectID = 29114;

            imageUrl = "https://cdn.discordapp.com/attachments/382220784040673281/787627393283915816/prison.png";
            removeInventory = true;
            spawn = new Vector3();
        }
    }
}