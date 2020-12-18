using Lafalafa.JailPlugin.Helpers;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lafalafa.JailPlugin
{
    class JailUiManager : MonoBehaviour
    {
        public static Dictionary<CSteamID, InfoUiPlayer> infoPlayer = new Dictionary<CSteamID,InfoUiPlayer>();
       
        public static void sendUiOnlinePlayers(CSteamID player, short key)
        {

            List<SteamPlayer> playersOnline = Provider.clients;
            //if (playersOnline.Count <= 12) playersOnline = Provider.clients.GetRange(0, playersOnline.Count);
            //else if (playersOnline.Count <= 24) playersOnline = Provider.clients.GetRange(12, playersOnline.Count - 12);
            //else if (playersOnline.Count <= 36) playersOnline = Provider.clients.GetRange(24, playersOnline.Count - 24);
            //else if (playersOnline.Count <= 48) playersOnline = Provider.clients.GetRange(36, playersOnline.Count - 36);
            //else { return; }


            switch (infoPlayer[player].onlinePlayersPage)
            {
                case 0:
                    playersOnline = Provider.clients.GetRange(0, playersOnline.Count);
                    break;
                case 1:
                    playersOnline = Provider.clients.GetRange(12, playersOnline.Count - 12);
                    break;
                case 2:
                    playersOnline = Provider.clients.GetRange(24, playersOnline.Count - 24);
                    break;
               case 3:
                    playersOnline = Provider.clients.GetRange(36, playersOnline.Count - 36);
                    break;

            }
            int count = 1;

            foreach (SteamPlayer steamPlayer in playersOnline)
            {
                UnturnedPlayer unturnedPlayer = UnturnedPlayer.FromSteamPlayer(steamPlayer);
                EffectManager.sendUIEffectImageURL(key, player, true, $"JailPlugin_Admin_Players_Logo_{count}", unturnedPlayer.SteamProfile.AvatarFull.OriginalString);
                EffectManager.sendUIEffectText(key, player, true, $"JailPlugin_Admin_Players_Name_{count}", unturnedPlayer.CharacterName);
                    count++;

            }
            for (int i = count; i <= 12; i++)
            {
                EffectManager.sendUIEffectVisibility(key, player, true, $"JailPlugin_Admin_Player{count}", false);       
            }

        }
        public void Awake()
        {

            EffectManager.onEffectButtonClicked += onEffectButtonClicked;
            EffectManager.onEffectTextCommitted += onEffectTextCommitted;
        }

        private void onEffectTextCommitted(Player player, string buttonName, string text)
        {

        }

        private void onEffectButtonClicked(Player player, string buttonName)
        {
           
        }

        public void Update()
        {
            


        }

        public void OnDestroy()
        {
                

        }



    }
}
