using Rocket.API;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lafalafa.JailPlugin.Commands
{
    class Prisioners : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "prisioners";

        public string Help => "See availible prisioners and can check one";

        public string Syntax => "/prisioners | /prisioners (name)";

        public List<string> Aliases => new List<string>() {"prisioner" };

        public List<string> Permissions => new List<string>() { "jailplugin.prisioners" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length > 1) ;//TODO send syntax
            
            if (command.Length == 0)
            {

                foreach (JailModel jailModel in JailModel.getJails())
                {

                    //TODO todas las carceles con los jugadores:
                    /*
                     Carcel UNO:
                     Pepe
                     Jose 
                     Ramiro
                     Juan
                     */
                    //Carcel Uno mensaje
                    foreach (Prisioner prisioner in jailModel.prisioners)
                    {
                        


                    }

                }

            }
            else {

                UnturnedPlayer player = UnturnedPlayer.FromName(command[0]);
                if (player == null)
                {

                    //TODO no se encontro jugador
                    return;
                }
                else { 
                
                    /*Nombre: 
                      SteamID:
                      Tiempo:
                        
                     */

                }

            }
        }
    }
}
