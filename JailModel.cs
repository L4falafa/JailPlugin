using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lafalafa.JailPlugin
{

    class JailModel
    {

        private static List<JailModel> jails;

        private List<PrisionerComponent> prisioners;
        private string name;
        private int defaultTime;
        public Vector3 loc;
        public int radius;

        public JailModel(string name, int defaultTime, Vector3 loc, int radius)
        {

            this.radius = radius;
            this.name = name;
            this.loc = loc;
            this.defaultTime = defaultTime;


        }

        public static List<JailModel> getJails()
        {

            return jails;

        }

        public void removePrisionerJail(CSteamID steamID)
        {

            foreach (PrisionerComponent player in prisioners)
            {

                if (player.prisioner.CSteamID == steamID)
                {

                    prisioners.Remove(player);

                    break;
                }

            }

        }
        
        public void addPrisionerJail()
        {



        }

        

    }

}
