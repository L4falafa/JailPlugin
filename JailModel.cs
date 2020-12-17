using Lafalafa.JailPlugin.Helpers;
using Rocket.Unturned.Player;
using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace Lafalafa.JailPlugin
{

    public class JailModel
    {
        //aTimer = new System.Timers.Timer(2000);
        //// Hook up the Elapsed event for the timer. 
        //aTimer.Elapsed += OnTimedEvent;
        //aTimer.AutoReset = true;
        //aTimer.Enabled = true;
        [XmlIgnore]
        private static List<JailModel> jails = new List<JailModel>();
        [XmlIgnore]
        public List<Prisoner> prisioners;
        public string name { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public int radius { get; set; }

        public JailModel()
        {
            prisioners = new List<Prisoner>();
            

        }

        public static void addNewJail(string name, int radius, Vector3 loc)
        {

            if (JailModel.getJailFromName(name) != null) return;
            JailModel jail = new JailModel();
            jail.name = name;
            jail.radius = radius;
            jail.x = loc.x;
            jail.y = loc.y;
            jail.z = loc.z;
            jails.Add(jail);
        }
        public static List<JailModel> getJails()
        {

            return jails;

        }

        public static void removeJail(string name)
        {
            JailModel jail = JailModel.getJailFromName(name);

            if (jail != null)
            {
                if (JailModel.getJails().Count == 0)
                {
                    JailModel.addNewJail("Tes1", 10, new Vector3(10, 3, 6));
                }
                jails.Remove(jail);
               

            }

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
        public static Prisoner getPlayer(CSteamID steamID)
        {

            foreach(JailModel jail in jails)
            {

                foreach (Prisoner prisioner in jail.prisioners)
                {

                    if (prisioner.prisioner.CSteamID.m_SteamID == steamID.m_SteamID)
                    {

                        return prisioner;
                      
                    }

                }

            }
            return null;
        } 





        public static void unloadJails()
        {
            File.Delete(StoreData.path);
            StoreData.writeObject();
            jails.Clear();

        }
        public void removePrisonerJail(CSteamID steamID)
        {

            foreach (Prisoner player in prisioners)
            {

                if (player.prisioner.CSteamID == steamID)
                {
                    player.prisioner.Features.GodMode = false;
                    player.release();
                    prisioners.Remove(player);
                    
                    break;
                }

            }

        }
        
        public void addPrisonerJail(Prisoner prisioner)
        {

            prisioners.Add(prisioner);

        }

        

    }

}
