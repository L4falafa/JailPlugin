using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lafalafa.JailPlugin.Commands
{
    class AddJail : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "addJail";

        public string Help => "Add a new jail with 'x' radio.";

        public string Syntax => "/addJail name radio";

        public List<string> Aliases => new List<string>() { "ajail"};

        public List<string> Permissions => new List<string>() { "jailplugin.add"};

        public void Execute(IRocketPlayer caller, string[] command)
        {

            if (JailModel.getJailFromName(command[0]) == null) return;
            if (int.TryParse(command[1], out int result))
            {

                //JailModel.addNewJail(command[0], result);

            }
            else
            {
                //numero plis
            }
        }
    }
}
