using Lafalafa.JailPlugin;
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
    public class PrisonerRemove : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "prisonerremove";

        public string Help => "Release a player from their jail";

        public string Syntax => "/prisonerremove (playername)";

        public List<string> Aliases => new List<string> { "release"};

        public List<string> Permissions => new List<string> { "jailplugin.prisoner.remove" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            if (command.Length != 1)
            {

                ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Syntax}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                return;

            }
            UnturnedPlayer target = UnturnedPlayer.FromName(command[0]);

            if (target == null)
            {
                ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("player_not_found").Replace('(', '<').Replace(')', '>')}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                return;
            }
            Prisoner prisioner = JailModel.getPlayer(target.CSteamID);
            if (prisioner == null)
            {

                
                ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("not_in_jail", target.DisplayName).Replace('(', '<').Replace(')', '>')}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                return;
            }
            prisioner.prisioner.Player.teleportToLocationUnsafe(Jail.instance.Configuration.Instance.spawn, prisioner.prisioner.Player.look.yaw);
            prisioner.jail.removePrisonerJail(target.CSteamID);
            
            ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("to_released_player", player.DisplayName,(prisioner.elapsedTime().ElapsedMilliseconds/1000)).Replace('(', '<').Replace(')', '>')}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
           
            ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("releases_player", target.DisplayName, prisioner.jail.name,(prisioner.elapsedTime().ElapsedMilliseconds / 1000)).Replace('(', '<').Replace(')', '>')}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);


        }
    }
}
