using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lafalafa.JailPlugin.Commands
{
    class PrisionersCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "prisioners";

        public string Help => "See availible prisioners and can check one";

        public string Syntax => "/prisioners";

        public List<string> Aliases => new List<string>() {"pall" };

        public List<string> Permissions => new List<string>() { "jailplugin.prisioner.all" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            if (command.Length > 0)

            {
                ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Syntax}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                return;
            }
            
      

                foreach (JailModel jailModel in JailModel.getJails())
                {

                ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}Jail: {jailModel.name}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);

                foreach (Prisioner prisioner in jailModel.prisioners)
                    {


                    ChatManager.serverSendMessage(string.Format($"Prisioner: {prisioner.prisioner.DisplayName}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);

                    }



            }
          
        }
    }
}
