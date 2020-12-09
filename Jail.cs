using Lafalafa.JailPlugin.Helpers;
using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using SDG.Unturned;
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
        protected override void Load()
        {
            instance = this;
            string pluginDirec = this.Directory;
            pluginDirec += @"\JailsData.xml";
            StoreData.path = pluginDirec;
            if (!File.Exists(pluginDirec))
            {

                StoreData.createFile();

            }
            StoreData.loadJails();
            U.Events.OnPlayerConnected += Events_OnPlayerConnected;
            U.Events.OnPlayerDisconnected += Events_OnPlayerDisconnected;

            Console.WriteLine($"Total jails loaded {JailModel.getJails().Count }");
           
            

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

                    foreach (Prisioner prisioner in jailModel.prisioners)
                    {
                        if (prisioner.online)
                        {
                            if (Vector3.Distance(prisioner.prisioner.Position, new Vector3(jailModel.x, jailModel.y, jailModel.z)) > jailModel.radius)
                            {

                                jailModel.removePrisionerJail(prisioner.prisioner.CSteamID);
                                
                            }
                        }
                    }

                }
                yield return new WaitForSeconds(1);

            }

        }

        #region mensajeSendToAll
        //public void sendMessageToAll(string translate)
        //{

           

        //}
        //public static void sendMessageToAll(string translate, string arg1)
        //{

        //}
        //public static void sendMessageToAll(string translate, string arg1, string arg2)
        //{

        //}

        //public static void sendMessageToAll(string translate, string arg1, string arg2, string arg3)
        //{

        //}

        #endregion

        #region traduccion
        public override TranslationList DefaultTranslations => new TranslationList()
        {
            {"VICTIM_FROZED"," You has been tased, must wait {0} second to be realesed"},
            {"KILLER_FROZED"," You tased a player"}
        };
        #endregion

    }
}
