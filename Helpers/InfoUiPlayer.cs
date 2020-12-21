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
        public int jail;
        public List<JailModel> jails;
        public JailModel jailSelected;
        public List<Prisoner> prisoners { get; set; }
        public List<SteamPlayer> onlinePlayers;

        public UnturnedPlayer prisoner;

        public InfoUiPlayer()
        {
            


        }

    }
}
