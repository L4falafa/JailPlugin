using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lafalafa.JailPlugin
{
    public class Prisioner
    {

        public UnturnedPlayer prisioner;
        UnturnedPlayer judge;
        bool online;
        int time;
        JailModel jail;

        public Prisioner(UnturnedPlayer prisioner, UnturnedPlayer judge, int time, string jailName)
        {
            this.prisioner = prisioner;
            this.judge = judge;
            this.time = time;
            jail = JailModel.getJailFromName(jailName);
        }
       
    }
}
