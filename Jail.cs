using Lafalafa.JailPlugin.Helpers;
using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core;
using Rocket.Core.Plugins;
using Rocket.Unturned;
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

            StartCoroutine(checkPrisioner());

        }

   

        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= Events_OnPlayerConnected;
            U.Events.OnPlayerDisconnected -= Events_OnPlayerDisconnected;
        }

        private void Events_OnPlayerDisconnected(UnturnedPlayer player)
        {
            Prisioner prisioner = JailModel.getPlayer(player.CSteamID);
            if (prisioner != null)
            {

                prisioner.online = false;
                prisioner.stopTimer();

            }
        }

        private void Events_OnPlayerConnected(UnturnedPlayer player)
        {

            Prisioner prisioner = JailModel.getPlayer(player.CSteamID);
            if (prisioner != null)
            {
               
                prisioner.online = true;
                prisioner.startTimer();

            }

        }


        private IEnumerator checkPrisioner()
        {

            while (this.enabled)
            {
        
                foreach (JailModel jailModel in JailModel.getJails())
                {
             
                    var toRemove = new List<CSteamID>();
                    foreach (Prisioner prisioner in jailModel.prisioners)
                    {
                        if (prisioner.online)
                        {

                            if (Vector3.Distance(prisioner.prisioner.Position, new Vector3(jailModel.x, jailModel.y, jailModel.z)) > jailModel.radius)
                            {

                                toRemove.Add(prisioner.prisioner.CSteamID);
                                UnturnedPlayer uclient;
                                Provider.clients.ForEach(client =>
                                {
                                    uclient = UnturnedPlayer.FromSteamPlayer(client);

                                    if (uclient.HasPermission("jailplugin.police"))
                                    {
                                        //TODO PlayerEscapes Args = nombre, jailname, time   player_escape_police
                                        ChatManager.serverSendMessage(string.Format(Jail.instance.Translations.Instance.Translate("player_escape_police", prisioner.prisioner.DisplayName, prisioner.jail.name, (prisioner.elapsedTime().ElapsedMilliseconds / 1000)).Replace('(', '<').Replace(')', '>')), Color.white, null, client, EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                                    }
                                });
                                Provider.clients.ForEach(client =>
                                { 
                                    //TODO PlayerEscapes civilian Args = nombre, jailname, time   player_escape_civilian
                                    ChatManager.serverSendMessage(string.Format(Jail.instance.Translations.Instance.Translate("player_escape_civilian", prisioner.prisioner.DisplayName, prisioner.jail.name, (prisioner.elapsedTime().ElapsedMilliseconds / 1000)).Replace('(', '<').Replace(')', '>')), Color.white, null, client, EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                                });

                    }
                        }
                    }
                    foreach (CSteamID prisioner1 in toRemove)
                    {
                        jailModel.removePrisionerJail(prisioner1);
                    }

                }
                yield return new WaitForSeconds(1);

            }

        }

        #region traduccion
        public override TranslationList DefaultTranslations => new TranslationList()
        {
            {"VICTIM_FROZED"," You has been tased, must wait {0} second to be realesed"},
            {"KILLER_FROZED"," You tased a player"}
        };
        #endregion

    }
}
