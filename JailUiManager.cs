using Lafalafa.JailPlugin.Helpers;
using Lafalafa.JailPluginJailPlugin.Helpers;
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
                case 1:
                    playersOnline = Provider.clients.GetRange(0, playersOnline.Count);
                    infoPlayer[player].onlinePlayers = playersOnline;
                    break;
                case 2:
                    playersOnline = Provider.clients.GetRange(12, playersOnline.Count - 12);
                    infoPlayer[player].onlinePlayers = playersOnline;
                    
                    break;
                case 3:
                    playersOnline = Provider.clients.GetRange(24, playersOnline.Count - 24);
                    infoPlayer[player].onlinePlayers = playersOnline;
                    break;
               case 4:
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
                        int time;
                        var flag = Int32.TryParse(text, out int x) ? time = x : time= Jail.instance.Configuration.Instance.defaultTime;
                        infoPlayer[player.CSteamID].jailTime = time;
                        break;
                    case "ReasonInput":
                        infoPlayer[player.CSteamID].reason = text;
                        break;
                   
                }

            }

        }
        private void sendPrisonerJailsUi(CSteamID player, int index, short key)
        {
            JailModel jail = JailModel.getJails()[index];
            InfoUiPlayer infoUi = infoPlayer[player];
            //comenzar desde el cero
            EffectManager.sendUIEffectText(key, player, true, "JailPlugin_Jail_Admin_Players_Name", jail.name);
            EffectManager.sendUIEffectText(key, player, true, "JailPlugin_Jail_Admin_Players_TotalPrisoners", jail.prisioners.Count.ToString());
            EffectManager.sendUIEffectText(key, player, true, "JailPlugin_Jail_Admin_Players_Radious", jail.radius.ToString());
            infoPlayer[player].prisoners =jail.prisioners.GetRange(infoUi.prisonersPage * 6, JailModel.getJails()[index].prisioners.Count - (infoUi.prisonersPage * 6));
            int count = 1;
            foreach (Prisoner prisoner in infoUi.prisoners)
            {
                
                EffectManager.sendUIEffectVisibility(key,player,true, $"JailPlugin_Jail_Admin_Player{count}", true);
                EffectManager.sendUIEffectImageURL(key,player,true, $"JailPlugin_Jail_Admin_Players_Logo_{count}", prisoner.prisioner.SteamProfile.AvatarFull.OriginalString);
                EffectManager.sendUIEffectText(key,player,true, $"JailPlugin_Jail_Admin_Players_Name_{count}",prisoner.prisioner.CharacterName);
                count++;
            }
            for (int i = count; i <= 6; i++)
            {
                EffectManager.sendUIEffectVisibility(key, player, true, $"JailPlugin_Jail_Admin_Player{count}", false);
            }


        }
        private void sendJailsUi(CSteamID player, short key)
        {
            List<JailModel> jails = JailModel.getJails();
            switch (infoPlayer[player].onlinePlayersPage)
            {
                case 1:
                    jails = JailModel.getJails().GetRange(0, jails.Count);
                    infoPlayer[player].jails = jails;
                    break;
                case 2:
                    jails = JailModel.getJails().GetRange(6, jails.Count - 6);
                    infoPlayer[player].jails = jails;

                    break;
                case 3:
                    jails = JailModel.getJails().GetRange(12, jails.Count - 12);
                    infoPlayer[player].jails = jails;
                    break;
                case 4:
                    jails = JailModel.getJails().GetRange(18, jails.Count - 18);
                    infoPlayer[player].jails = jails;
                    break;
                case 5:
                    jails = JailModel.getJails().GetRange(24, jails.Count - 24);
                    infoPlayer[player].jails = jails;
                    break;

            }
            int count = 1;
                
            foreach (JailModel jail in jails)
            {
               
                EffectManager.sendUIEffectVisibility(key, player, true, $"JailPlugin__AllJails_Jail{count}", true);
                EffectManager.sendUIEffectImageURL(key, player, true, $"JailPlugin__AllJails_Name_Jail_{count}", jail.name);
                count++;

            }
            for (int i = count; i <= 6; i++)
            {
                EffectManager.sendUIEffectVisibility(key, player, true, $"JailPlugin__AllJails_Jail{count}", false);
            }


        }
        private void onEffectButtonClicked(Player pplayer, string buttonName)
        {
            //JailPlugin_Admin_Players_ButtonArrest_3
            if (!buttonName.StartsWith("JailPlugin")) return;
            UnturnedPlayer player = UnturnedPlayer.FromPlayer(pplayer);
            string button = string.Empty;
            if (buttonName.Contains("JailPlugin_Admin_Players_ButtonArrest")|| buttonName.Contains("JailPlugin_Jail_Admin_Players_Button_Info")|| buttonName.Contains("JailPlugin_Jail_Admin_Players_ButtonArrest")|| buttonName.Contains("JailPlugin__AllJails_ButtonRemove_Jail") || buttonName.Contains("JailPlugin__AllJails_ButtonRemove_Jail") || buttonName.Contains("JailPlugin__AllJails_ButtonTeleport_Jail")) button = buttonName.Remove(buttonName.Length - 2, 2);
            if (infoPlayer.ContainsKey(player.CSteamID))
            {
                InfoUiPlayer infoUi = infoPlayer[player.CSteamID];
                switch (button)
                {
                    case "JailPlugin_Admin_Players_ButtonArrest":
                        UnturnedPlayer unturnedPlayerPrisoner = UnturnedPlayer.FromSteamPlayer(infoPlayer[player.CSteamID].onlinePlayers[Int32.Parse(buttonName[buttonName.Length - 1].ToString())]);
                        if (JailModel.getPlayer(unturnedPlayerPrisoner.CSteamID) != null)
                        {
                            ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("player_already_at_jail", JailModel.getPlayer(unturnedPlayerPrisoner.CSteamID).jail.name).Replace('(', '<').Replace(')', '>')}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                            return;

                        }
                        else
                        {
                            infoPlayer[player.CSteamID].prisoner = unturnedPlayerPrisoner;
                        }
                        break;
                    case "JailPlugin_Admin_Players_ButtonRight":


                        switch (infoUi.onlinePlayersPage)
                        {
                            case 1:
                             if (Provider.clients.Count <= 24 && Provider.clients.Count > 12)
                             {
                                 infoUi.onlinePlayersPage = 2;
                             }
                                break;
                            case 2:
                            if (Provider.clients.Count <= 36 && Provider.clients.Count > 24)
                            {
                            infoUi.onlinePlayersPage = 3;
                            }
                                break;
                            case 3:
                                if (Provider.clients.Count <= 48 && Provider.clients.Count > 36)
                                {
                                    infoUi.onlinePlayersPage = 4;
                                 
                                }
                                break;
                           
                                
                       
                      
                        }
                        //TODO Key Effec Online Player Ui 29210
                        sendUiOnlinePlayers(player.CSteamID, 29210);


                        break;
                    case "JailPlugin_Admin_Players_ButtonLeft":


                        switch (infoUi.onlinePlayersPage)
                        {
                                                          
                            case 2:
                                if (Provider.clients.Count <= 12)
                                {
                                    infoUi.onlinePlayersPage = 1;
                                }
                                break;
                            case 3:
                                if (Provider.clients.Count <= 24 && Provider.clients.Count > 12)
                                {
                                    infoUi.onlinePlayersPage = 2;
                              
                                }
                                break;
                            case 4:
                                if (Provider.clients.Count <= 36 && Provider.clients.Count > 24)
                                {
                                    infoUi.onlinePlayersPage = 3;
                               
                                }
                                break;



                        }
 
                        sendUiOnlinePlayers(player.CSteamID, 29210);


                        break;
                    case "JailPlugin_ButtonClose":

                        EffectManager.askEffectClearByID(Jail.instance.Configuration.Instance.infoPrisonerEffectID, player.CSteamID);
                        EffectManager.askEffectClearByID(Jail.instance.Configuration.Instance.jailAdminEffectID, player.CSteamID);
                        EffectManager.askEffectClearByID(Jail.instance.Configuration.Instance.jailsEffectID, player.CSteamID);
                        EffectManager.askEffectClearByID(Jail.instance.Configuration.Instance.infoPrisonerEffectID, player.CSteamID);
                        EffectManager.askEffectClearByID(Jail.instance.Configuration.Instance.addPrisonerEffectID, player.CSteamID);
                        EffectManager.askEffectClearByID(Jail.instance.Configuration.Instance.adminPlayersOnlineEffectID, player.CSteamID);
                        infoPlayer.Remove(player.CSteamID);
                        break;

                    case "JailPlugin_AddPrisoner_ButtonConfirm":

                        JailModel jail = JailModel.getJailFromName(infoUi.jailName);
                        if (jail == null)
                        {
                            ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("jail_not_exist", infoUi.jailName).Replace('(', '<').Replace(')', '>')}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                            return;

                        }


                        jail.addPrisonerJail(new Prisoner(infoUi.prisoner, player, infoUi.jailTime, infoUi.jailName, infoUi.reason));
                        infoUi.prisoner.Player.teleportToLocation(new UnityEngine.Vector3(jail.x, jail.y, jail.z), player.Player.look.yaw);

                        if (Jail.instance.Configuration.Instance.removeInventory) InventoryHelp.ClearPlayerInventory(PlayerTool.getPlayer(infoUi.prisoner.CSteamID));
                        ItemTool.tryForceGiveItem(infoUi.prisoner.Player, (ushort)Jail.instance.Configuration.Instance.ShirtID, 1);
                        ItemTool.tryForceGiveItem(infoUi.prisoner.Player, (ushort)Jail.instance.Configuration.Instance.PantsID, 1);
                        infoUi.prisoner.Player.clothing.tellVisualToggle(infoUi.prisoner.CSteamID, 0, false);

                        ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("player_send_you_to_jail", player.DisplayName, jail.name, infoUi.jailTime).Replace('(', '<').Replace(')', '>')}"), Color.white, null, infoUi.prisoner.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                        ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("player_send_other_to_jail", player.DisplayName, jail.name, infoUi.jailTime).Replace('(', '<').Replace(')', '>')}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                        
                        break;
                    case "JailPlugin_Jail_Admin_Players_ButtonRight":

                        int div = System.Math.DivRem(infoUi.jailSelected.prisioners.Count, 6, out int result);
                        if (result != 0) div++;
                        if (infoUi.prisonersPage++ > div) return;
                        infoUi.prisonersPage++;
                        break;
                    case "JailPlugin_Jail_Admin_Players_ButtonLeft":                        
                        if ((infoUi.prisonersPage-1)<0 ) return;
                        infoUi.prisonersPage--;
                        break;
                    case "JailPlugin_Jail_Admin_Players_Button_Info":
                        break;
                    case "JailPlugin__AllJails_ButtonInfo_Jail":
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
