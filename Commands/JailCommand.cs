using Lafalafa.JailPlugin.Helpers;
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
    class AddJail : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "jail";

        public string Help => "Commands for jail plugin";

        public string Syntax => "/jail |add|remove|tp|all (name) <raiuds>";

        public List<string> Aliases => new List<string>() { "jaila"};

        public List<string> Permissions => new List<string>() { "jailplugin.jail"};

        public void Execute(IRocketPlayer caller, string[] command)
        {

            UnturnedPlayer player = (UnturnedPlayer)caller;

            if (command.Length <= 3)
            {
               

                switch (command[0])
                {

                    case "add":
                        if (command.Length < 2 )
                        {

                            ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Syntax}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                            return;
                        }
                        if (JailModel.getJailFromName(command[1]) == null)
                        {
                            var radius = Jail.instance.Configuration.Instance.defaultRadio;

                            if (command.Length == 3)
                            {
                                if (Int32.TryParse(command[2], out int result))
                                {
                                    radius = result;
                                    JailModel.addNewJail(command[1], result, player.Position);
                                }
                            }
                            else {
                                JailModel.addNewJail(command[1], radius, player.Position);
                            }
                            StoreData.writeObject();
                            //TODO Carcel añadida Arg = nombre, radio jail_add
                            ChatManager.serverSendMessage(string.Format(Jail.instance.Translations.Instance.Translate("jail_add", command[1], radius).Replace('(', '<').Replace(')', '>')), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                        }
                        else
                        {

                            //TODO Carcel existente Arg = nombre jail_exist
                            ChatManager.serverSendMessage(string.Format(Jail.instance.Translations.Instance.Translate("jail_exist", command[1]).Replace('(', '<').Replace(')', '>')), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                        }
                        break;
                    case "remove":
                        if (command.Length != 2)
                        {

                            ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Syntax}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                            return;
                        }
                        if (JailModel.getJailFromName(command[1]) != null)
                        {

                            JailModel.removeJail(command[1]);
                            //TODO Carcel removida Arg = nombre jail_removed
                            ChatManager.serverSendMessage(string.Format(Jail.instance.Translations.Instance.Translate("jail_removed", command[1]).Replace('(', '<').Replace(')', '>')), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                        }
                        else
                        {

                        
                            ChatManager.serverSendMessage(string.Format(Jail.instance.Translations.Instance.Translate("jail_not_exist", command[1]).Replace('(', '<').Replace(')', '>')), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                        }
                        break;
                    case "tp":
                        if (command.Length != 2)
                        {

                            ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Syntax}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                            return;
                        }
                        JailModel jail = JailModel.getJailFromName(command[1]);
                        if (jail != null)
                        {

                            player.Player.teleportToLocation(new Vector3(jail.x, jail.y, jail.z), player.Player.look.yaw);

                        }
                        else
                        {
                            //TODO Mensaje no existe (Args = nombre) jail_not_exist
                            ChatManager.serverSendMessage(string.Format(Jail.instance.Translations.Instance.Translate("jail_not_exist",command[1]).Replace('(', '<').Replace(')', '>')), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                            
                        }
                        break;
                    case "all":
                        //TODO Mensaje todas las carceles disponibles 0 Args available_jails
                        ChatManager.serverSendMessage(string.Format(Jail.instance.Translations.Instance.Translate("available_jails").Replace('(', '<').Replace(')', '>')), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);

                        foreach (JailModel jailModel in JailModel.getJails())
                        {
                            ChatManager.serverSendMessage(string.Format(jailModel.name), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                        }
                        break;
                    default:

                        ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Syntax}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);

                        break;
                }

            }
            else
            { 
                ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Syntax}"), Color.white, null, player.SteamPlayer(), EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
            }
        }
    }
}
