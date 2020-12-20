using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lafalafa.JailPlugin.Helpers
{
    public class InfoUiPlayer
    {
        public int onlinePlayersPage;
        public int jailsPage;
        public int prisonersPage;
        public string jailName;
        public int jailTime;
        public string reason;
        public List<JailModel> jails;
        public List<SteamPlayer> onlinePlayers;

        public UnturnedPlayer prisoner;

        public InfoUiPlayer()
        {
            


        }

    }
}
