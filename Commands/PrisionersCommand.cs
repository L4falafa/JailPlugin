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
    class PrisonersCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "prisoners";

        public string Help => "See availible prisoners and can check one";

        public string Syntax => "/prisoners";

        public List<string> Aliases => new List<string>() {"pall" };

        public List<string> Permissions => new List<string>() { "jailplugin.prisoner.all" };

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
                string final = "";
                foreach (Prisoner prisioner in jailModel.prisioners)
                    {

                    final += $"Prisoner: {prisioner.prisioner.DisplayName}";

                    }
                if(final != "") ChatManager.serverSendMessage(string.Format(final), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);



            }
          
        }
    }
}
