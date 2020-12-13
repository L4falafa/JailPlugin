using Lafalafa.JailPluginJailPlugin.Helpers;
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
    class PrisonerAddCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "prisoneradd";

        public string Help => "Arrest a player";

        public string Syntax => "/prisoneradd (!playername!) <jail> <time>";

        public List<string> Aliases => new List<string>() { "arrest" };

        public List<string> Permissions => new List<string>() { "jailplugin.prisoner.add" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            if (command.Length < 1 ) 
            {
                ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Syntax}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);

                return; 
            }
            
            UnturnedPlayer target = UnturnedPlayer.FromName(command[0]);
            if (target == null)
            {

                //TODO Player not found     player_not_found
                ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("player_not_found").Replace('(', '<').Replace(')', '>')}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                return;

            }
            Prisoner prisioner = JailModel.getPlayer(target.CSteamID);
            if (prisioner != null)
            {
                //TODO El player ya esta en la carcel Args = nombrecarcel    player_already_at_jail
                ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("player_already_at_jail", prisioner.jail.name).Replace('(', '<').Replace(')', '>')}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                return;

            }
            JailModel jail = null;
            if (command.Length > 1)
            {
                jail = JailModel.getJailFromName(command[1]);
            }
            switch (command.Length)
            {

                case 2:


                    if (jail != null)
                    {

                   

                            jail.addPrisonerJail(new Prisoner(target, player, Jail.instance.Configuration.Instance.defaultTime, command[1]));
                            target.Player.teleportToLocation(new UnityEngine.Vector3(jail.x, jail.y, jail.z), player.Player.look.yaw);
                            if (Jail.instance.Configuration.Instance.removeInventory) InventoryHelp.ClearPlayerInventory(PlayerTool.getPlayer(target.CSteamID));
                            ItemTool.tryForceGiveItem(target.Player, (ushort)Jail.instance.Configuration.Instance.ShirtID, 1);
                            ItemTool.tryForceGiveItem(target.Player, (ushort)Jail.instance.Configuration.Instance.PantsID, 1);
                            if (target.Player.clothing.isVisual) target.Player.clothing.tellVisualToggle(target.CSteamID, 0,false);
                    

                            ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("player_send_you_to_jail", player.DisplayName,command[1], Jail.instance.Configuration.Instance.defaultTime).Replace('(', '<').Replace(')', '>')}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                            
                            ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("player_send_other_to_jail", player.DisplayName, jail.name, Jail.instance.Configuration.Instance.defaultTime).Replace('(', '<').Replace(')', '>')}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);



                    }
                    else
                    {

                        ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("jail_not_exist", command[1]).Replace('(', '<').Replace(')', '>')}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);

                    }

                    break;
                case 3:



                    if (jail != null)
                    {


                     
                            int radio;
                            var flag = Int32.TryParse(command[2], out int x) ? radio = x : radio = Jail.instance.Configuration.Instance.defaultTime;

                            jail.addPrisonerJail(new Prisoner(target, player, radio, command[1]));
                            target.Player.teleportToLocation(new UnityEngine.Vector3(jail.x, jail.y, jail.z), player.Player.look.yaw);

                            if (Jail.instance.Configuration.Instance.removeInventory) InventoryHelp.ClearPlayerInventory(PlayerTool.getPlayer(target.CSteamID));
                            ItemTool.tryForceGiveItem(target.Player, (ushort)Jail.instance.Configuration.Instance.ShirtID, 1);
                            ItemTool.tryForceGiveItem(target.Player, (ushort)Jail.instance.Configuration.Instance.PantsID, 1);
                            target.Player.clothing.askVisualToggle(target.CSteamID, 0);
                            target.Player.clothing.tellVisualToggle(target.CSteamID, 0, false);
                            
                        //TODO Te enviaron a x carcel Args = nombrejuez, jailname, time     player_send_you_to_jail
                        ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("player_send_you_to_jail", player.DisplayName, jail.name, radio).Replace('(', '<').Replace(')', '>')}"), Color.white, null, target.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                            //TODO Enviaste a x a p carcel Args = nombretarget, jailname, time     player_send_other_to_jail
                            ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("player_send_other_to_jail", player.DisplayName, jail.name, radio).Replace('(', '<').Replace(')', '>')}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);

                    }
                    else
                    {

                        ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("jail_not_exist", command[1]).Replace('(', '<').Replace(')', '>')}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);

                    }

                    break;
                default:
                    ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Syntax}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);

                    break;

            }

            
        }
    }
}

