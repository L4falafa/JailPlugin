using Lafalafa.JailPlugin.Helpers;
using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lafalafa.JailPlugin
{
    public class Jail : RocketPlugin<JailConfig>
    {
        public static Jail instance;
        public static string namePluginChat = "<color=red>Jail</color>: ";
        protected override void Load()
        {

            instance = this;
            string pluginDirec = this.Directory;
            pluginDirec += @"\JailsData.xml";
            StoreData.path = pluginDirec;
            if (!File.Exists(pluginDirec))
            {

                StoreData.createFile();
;
            }
            StoreData.loadJails();
            U.Events.OnPlayerConnected += Events_OnPlayerConnected;
            U.Events.OnPlayerDisconnected += Events_OnPlayerDisconnected;
            UnturnedPlayerEvents.OnPlayerDead += UnturnedPlayerEvents_OnPlayerDead;
            UnturnedPlayerEvents.OnPlayerRevive += UnturnedPlayerEvents_OnPlayerRevive;
            StartCoroutine(checkPrisoner());

        }


        private void UnturnedPlayerEvents_OnPlayerRevive(UnturnedPlayer player, Vector3 position, byte angle)
        {
            Prisoner prisoner = JailModel.getPlayer(player.CSteamID);
            if (prisoner != null)
            {
                
                
                StartCoroutine(reviveLate(prisoner));
           
            }
        }

        private void UnturnedPlayerEvents_OnPlayerDead(UnturnedPlayer player, Vector3 position)
        {
            
            Prisoner prisoner = JailModel.getPlayer(player.CSteamID);
            if (prisoner != null)
            {
                prisoner.stopTimer();
                prisoner.reviving = true;
            }

        }

        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= Events_OnPlayerConnected;
            U.Events.OnPlayerDisconnected -= Events_OnPlayerDisconnected;
          
            JailModel.unloadJails();
        }

        private void Events_OnPlayerDisconnected(UnturnedPlayer player)
        {
            Prisoner prisioner = JailModel.getPlayer(player.CSteamID);
            if (prisioner != null)
            {

                prisioner.online = false;
                prisioner.stopTimer();

            }
        }
        
        private void Events_OnPlayerConnected(UnturnedPlayer player)
        {
            
            Prisoner prisoner = JailModel.getPlayer(player.CSteamID);
            var x = 10;
            foreach (JailModel jail in JailModel.getJails())
            {
                x =+ 1;
                player.Player.quests.tellSetMarker(new CSteamID((ulong)x), true, new Vector3(jail.x, jail.y, jail.z), $"Jail: {jail.name}");
               
            }
            if (prisoner != null)
            {
                prisoner.prisioner = player;
                prisoner.online = true;
                prisoner.startTimer();
               
            }

        }

        private IEnumerator reviveLate(Prisoner prisoner)
        {
            
            prisoner.startTimer();
            yield return new WaitForSeconds((float)0.5);
            prisoner.prisioner.Teleport(new UnityEngine.Vector3(prisoner.jail.x, prisoner.jail.y, prisoner.jail.z), prisoner.prisioner.Player.look.yaw);
            prisoner.reviving = false;
        }
        private IEnumerator checkPrisoner()
        {

            while (this.enabled)
            {
        
                foreach (JailModel jailModel in JailModel.getJails())
                {
             
                    var toRemove = new List<CSteamID>();
                    foreach (Prisoner prisioner in jailModel.prisioners)
                    {
                        if (prisioner.online)
                        {
                 
                            
                            if (prisioner.reviving == false)
                            {
                              
                                if (Vector3.Distance(prisioner.prisioner.Position, new Vector3(jailModel.x, jailModel.y, jailModel.z)) > jailModel.radius)
                                {
                                  
                                    toRemove.Add(prisioner.prisioner.CSteamID);
                                    UnturnedPlayer uclient;

                                    if (Configuration.Instance.broadcastEscape)
                                    {
                                        Provider.clients.ForEach(client =>
                                        {

                                            
                                            ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("player_escape_civilian", prisioner.prisioner.DisplayName, prisioner.jail.name).Replace('(', '<').Replace(')', '>')}"), Color.white, null, client, EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                                        });
                                    }
                                    else
                                    {

                                        Provider.clients.ForEach(client =>
                                        {
                                            uclient = UnturnedPlayer.FromSteamPlayer(client);

                                            if (uclient.HasPermission("jailplugin.police"))
                                            {
                                               
                                                ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("player_escape_police", prisioner.prisioner.DisplayName, prisioner.jail.name).Replace('(', '<').Replace(')', '>')}"), Color.white, null, client, EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                                            }
                                        });

                                    }
                                }
                            }
                            
                          
                        }
                    }
                    foreach (CSteamID prisioner1 in toRemove)
                    {
                        jailModel.removePrisonerJail(prisioner1);
                    }

                }
                yield return new WaitForSeconds(1);

            }

        }

        #region traduccion
        public override TranslationList DefaultTranslations => new TranslationList()
        {
            {"jail_add","You added the jail with name {0} and radio {1}"},
            {"jail_exist","There is already an existing jail with the name {0}"},
            {"jail_removed","The jail with the name {0} was removed"},
            {"jail_not_exist","The jail with the name {0} does not exist."},
            {"available_jails","The jails available are:"},
            {"player_not_found","Can't find the player"},
            {"player_already_at_jail","The player is already in jail: {0}"},
            {"player_send_you_to_jail","{0} send you to prison (color=red){1}(/color) for (color=orange){2}s(/color)"},
            {"player_send_other_to_jail","You sent {0} to jail (color=red){1}(/color) ​​for (color=orange){2}s(/color)"},
            {"not_in_jail","{0} is not arrested."},
            {"info_prisioner","Name: {0} | Judge: {1} | Jail: {2} | Time in Jail: {3} | Time Remaining: {4}s"},
            {"to_released_player","You were released by {0} and spent {1}s in jail."},
            {"releases_player","You freed {0} from {1} and he spent {2}s in jail."},
            {"player_escape_police","{0} is now a fugitive, escaped from jail {1}. You must stop it!"},
            {"player_escape_civilian","{0} is a fugitive, from the law, escaped from jail {1}. Any relation with him will be taken as an accomplice."},
            {"release_time","{0} was released from jail {1} ​​after being {2}s in it."}
        };
        #endregion

    }
}
