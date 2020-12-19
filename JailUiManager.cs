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
                    infoPlayer[player].onlinePlayers = playersOnline;
                    break;
                case 1:
                    playersOnline = Provider.clients.GetRange(12, playersOnline.Count - 12);
                    infoPlayer[player].onlinePlayers = playersOnline;
                    
                    break;
                case 2:
                    playersOnline = Provider.clients.GetRange(24, playersOnline.Count - 24);
                    infoPlayer[player].onlinePlayers = playersOnline;
                    break;
               case 3:
                    playersOnline = Provider.clients.GetRange(36, playersOnline.Count - 36);
                    infoPlayer[player].onlinePlayers = playersOnline;
                    break;

            }
            int count = 1;

            foreach (SteamPlayer steamPlayer in playersOnline)
            {
                UnturnedPlayer unturnedPlayer = UnturnedPlayer.FromSteamPlayer(steamPlayer);
                EffectManager.sendUIEffectVisibility(key, player, true, $"JailPlugin_Admin_Player{count}", true);
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

        private void onEffectTextCommitted(Player pplayer, string buttonName, string text)
        {
            UnturnedPlayer player = UnturnedPlayer.FromPlayer(pplayer);
            if (infoPlayer.ContainsKey(player.CSteamID))
            {

                switch (buttonName)
                {
                    case "jailInput":
                        

                            infoPlayer[player.CSteamID].jailName = text;

                  
                        break;
                    case "TimeInput":
                            if (Int64.TryParse(text, out long result)) infoPlayer[player.CSteamID].jailTime = (int)result;   
                        break;
                    case "ReasonInput":
                        infoPlayer[player.CSteamID].reason = text;
                        break;
                }

            }

        }

        private void onEffectButtonClicked(Player pplayer, string buttonName)
        {
            //JailPlugin_Admin_Players_ButtonArrest_3
            UnturnedPlayer player = UnturnedPlayer.FromPlayer(pplayer);

            string button = buttonName.Remove(buttonName.Length - 2, 2);
            if (infoPlayer.ContainsKey(player.CSteamID))
            {
                InfoUiPlayer infoUi = infoPlayer[player.CSteamID];
                switch (button)
                {
                    case "JailPlugin_Admin_Players_ButtonArrest":

                        infoPlayer[player.CSteamID].prisoner = UnturnedPlayer.FromSteamPlayer(infoPlayer[player.CSteamID].onlinePlayers[Int32.Parse(buttonName[buttonName.Length - 1].ToString())]);

                        break;
                    case "JailPlugin_Admin_Players_ButtonRight":

                        if (Provider.clients.Count <= 12)
                        {
                            infoUi.onlinePlayersPage = 1;
                            return;

                        }
                              
                        else if (Provider.clients.Count <= 24)
                        {
                            infoUi.onlinePlayersPage = 2;
                            return;
                        }

                        else if (Provider.clients.Count <= 36) {
                            infoUi.onlinePlayersPage = 3;
                            return;
                        }
                        else if (Provider.clients.Count <= 48) {
                            infoUi.onlinePlayersPage = 3;
                            return;
                        }

                     


                        break;
                    case "JailPlugin_Admin_Players_ButtonLeft":

                        switch(infoUi.onlinePlayersPage)
                        if (Provider.clients.Count <= 12)
                        {
                            infoUi.onlinePlayersPage = 0;
                            return;

                        }

                        else if (Provider.clients.Count <= 24)
                        {
                            infoUi.onlinePlayersPage = 1;
                            return;
                        }

                        else if (Provider.clients.Count <= 36)
                        {
                            infoUi.onlinePlayersPage = 3;
                            return;
                        }
                        else if (Provider.clients.Count <= 48)
                        {
                            infoUi.onlinePlayersPage = 3;
                            return;
                        }


                        break;



                }                
            }
        }

        public void Update()
        {
            


        }

        public void OnDestroy()
        {
                

        }



    }
}
