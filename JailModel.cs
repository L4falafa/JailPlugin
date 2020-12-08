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

        private List<Prisioner> prisioners;
        public string name;
        public Vector3 loc;
        public int radius;

        public JailModel(string name, Vector3 loc, int radius)
        {

            this.radius = radius;
            this.name = name;
            this.loc = loc;
            

        }
        public static void addNewJail(string name, int radius)
        { 
        
            //TODO json

        }
        public static List<JailModel> getJails()
        {

            return jails;

        }
        public static JailModel getJailFromName(string name)
        {
            foreach (JailModel jail in jails)
            {


                if (jail.name.ToLower() == name.ToLower())
                {

                    return jail;
                }
               
            }

            return null;

        }
        public void removePrisionerJail(CSteamID steamID)
        {

            foreach (Prisioner player in prisioners)
            {

                if (player.prisioner.CSteamID == steamID)
                {

                    prisioners.Remove(player);

                    break;
                }

            }

        }
        
        public void addPrisionerJail(Prisioner prisioner)
        {

            prisioners.Add(prisioner);

        }

        

    }

}
