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
    public class PrisonerCheck : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "prisonercheck";

        public string Help => "Get info about a prisioner";

        public string Syntax => "/prisonercheck (name)";

        public List<string> Aliases => new List<string>() { "pcheck" };

        public List<string> Permissions => new List<string>() { "jailplugin.check"};

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

                //TODO No esta arrestado arg= name   not_in_jail
                ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("not_in_jail", target.DisplayName).Replace('(', '<').Replace(')', '>')}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                return;
            }
            //TODO Prisoner info args= prisionername,judgesteamid,jailname,time, remaining   info_prisioner
            ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("info_prisioner", prisioner.prisioner.DisplayName, prisioner.judge, prisioner.jail.name, (prisioner.elapsedTime().ElapsedMilliseconds/1000), (int)(prisioner.timer.Interval - prisioner.elapsedTime().ElapsedMilliseconds)/1000).Replace('(', '<').Replace(')', '>')}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);

        }
    }
}
