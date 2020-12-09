﻿using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;

namespace Lafalafa.JailPlugin
{
    public class Prisioner
    {
        //aTimer = new System.Timers.Timer(2000);
        //// Hook up the Elapsed event for the timer. 
        //aTimer.Elapsed += OnTimedEvent;
        //aTimer.AutoReset = true;
        //aTimer.Enabled = true;


        Timer timer;
        public UnturnedPlayer prisioner;
        public bool online { get; set; }
        private UnturnedPlayer judge;
        private JailModel jail;

        public Prisioner(UnturnedPlayer prisioner, UnturnedPlayer judge, int time, string jailName)
        {
            this.prisioner = prisioner;
            this.judge = judge;
            timer =  new Timer(time*1000);
            jail = JailModel.getJailFromName(jailName);

            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = false;
            timer.Enabled = true;
            timer.Start();
        }
        public void stopTimer() => timer.Stop();
        public void startTimer() => timer.Start();

        public void release()
        {

            timer.Stop();                
            timer.Dispose();
            timer.Elapsed -= Timer_Elapsed;

        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //TODO liberar
           
        }
    }
}
