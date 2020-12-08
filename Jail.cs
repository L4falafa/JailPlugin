using Lafalafa.JailPlugin.Helpers;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
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



            Console.WriteLine($"Total jails loaded {JailModel.getJails().Count }");
           
            

            StartCoroutine(checkPrisioner());

        }
        protected override void Unload()
        {

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

                                jailModel.escapePlayer(prisioner.prisioner.CSteamID);

                            }
                        }
                    }

                }
                yield return new WaitForSeconds(1);

            }

        }

        #region mensajeSendToAll
        public void sendMessageToAll(string translate)
        { 
        
            Provider.clients.ForEach(client=>
            { 
            
         

            });

        }
        public static void sendMessageToAll(string translate, string arg1)
        {

        }
        public static void sendMessageToAll(string translate, string arg1, string arg2)
        {

        }

        public static void sendMessageToAll(string translate, string arg1, string arg2, string arg3)
        {

        }

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
