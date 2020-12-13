using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Stopwatch _elapsedTime;
        private UnturnedPlayer judge;
        public JailModel jail { get; private    set; }
        

        public Prisioner(UnturnedPlayer prisioner, UnturnedPlayer judge, int time, string jailName)
        {
            this.prisioner = prisioner;
            this.judge = judge;
            timer =  new Timer(time*1000);
            online = true;
            jail = JailModel.getJailFromName(jailName);
            _elapsedTime = new Stopwatch();
            _elapsedTime.Start();
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = false;
            timer.Enabled = true;
            timer.Start();
        }
        public void stopTimer()
        {
            timer.Stop();
            _elapsedTime.Stop();
        }
        public void startTimer()
        {
            timer.Start();
            _elapsedTime.Start();
        }

        public Stopwatch elapsedTime()
        {

            return _elapsedTime;

        }
        


        public void release()
        {

            timer.Stop();                
            timer.Dispose();
            timer.Elapsed -= Timer_Elapsed;

        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //TODO liberar !IMPORTANTE!
            Console.WriteLine(_elapsedTime.ElapsedMilliseconds / 1000);
            this.jail.removePrisionerJail(this.prisioner.CSteamID);
            //persona liberada mensaje
        }
    }
}
