using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JailPlugin.Commands
{
    public class PrisionerRemove : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "prisioneremove";

        public string Help => "Release a player from their jail";

        public string Syntax => "/prisionremove (playername)";

        public List<string> Aliases => throw new NotImplementedException();

        public List<string> Permissions => throw new NotImplementedException();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            throw new NotImplementedException();
        }
    }
}
