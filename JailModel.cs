﻿using Lafalafa.JailPlugin.Helpers;
using Steamworks;
using System;
using System.Collections.Generic;
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
        public List<Prisioner> prisioners;
        public string name { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public int radius { get; set; }

        public JailModel()
        {
            prisioners = new List<Prisioner>();
            

        }

        public static void addNewJail(string name, int radius, Vector3 loc)
        {

            //TODO json
            JailModel jail = new JailModel();
            jail.name = name;
            jail.radius = radius;
            jail.x = loc.x;
            jail.y = loc.y;
            jail.z = loc.z;
            jails.Add(jail);

            StoreData.writeObject();
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

        public void escapePlayer(CSteamID steamID)
        { 
        
            //TODO Mensaje Escape


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
