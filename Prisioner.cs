using Rocket.API;
using Rocket.Core.Utils;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
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
    public class Prisoner
    {
        //aTimer = new System.Timers.Timer(2000);
        //// Hook up the Elapsed event for the timer. 
        //aTimer.Elapsed += OnTimedEvent;
        //aTimer.AutoReset = true;
        //aTimer.Enabled = true;


        public Timer timer { get; private set; }
        public UnturnedPlayer prisioner;
        public bool online { get; set; }
        private Stopwatch _elapsedTime;
        public bool reviving { get; set; }
        public CSteamID judge { get; private set; }
        public JailModel jail { get; private    set; }
        public string reason { get; private set; }

        public Prisoner(UnturnedPlayer prisioner, UnturnedPlayer judge, int time, string jailName)
        {
            this.prisioner = prisioner;
            this.judge = judge.CSteamID;

            if (Jail.instance.Configuration.Instance.God) prisioner.Features.GodMode = true;
            timer =  new Timer(time*1000);
            online = true;
            reason = "Not Reason";
            jail = JailModel.getJailFromName(jailName);
            _elapsedTime = new Stopwatch();
            _elapsedTime.Start();
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = false;
            timer.Enabled = true;
            timer.Start();
        }

        public Prisoner(UnturnedPlayer prisioner, UnturnedPlayer judge, int time, string jailName, string reason)
        {
            this.prisioner = prisioner;
            this.judge = judge.CSteamID;
            this.reason = reason;
            if (Jail.instance.Configuration.Instance.God) prisioner.Features.GodMode = true;
            timer = new Timer(time * 1000);
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
            _elapsedTime.Stop();
            

        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.jail.removePrisonerJail(this.prisioner.CSteamID);
            if (Jail.instance.Configuration.Instance.teleportToSpawn)
            {
                
            }
            UnturnedPlayer uclient;           
            // Code to run on the main thread
            TaskDispatcher.QueueOnMainThread(() =>
            {
                prisioner.Player.teleportToLocationUnsafe(Jail.instance.Configuration.Instance.spawn, prisioner.Player.look.yaw);
                if (Jail.instance.Configuration.Instance.broadcastRelease)
                {

                    Provider.clients.ForEach(client =>
                    {

                    
                    ChatManager.serverSendMessage(string.Format(Jail.instance.Translations.Instance.Translate("release_time", prisioner.DisplayName, this.jail.name, this._elapsedTime.ElapsedMilliseconds / 1000).Replace('(', '<').Replace(')', '>')), Color.white, null, client, EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                    });
                }
                else
                {

                    Provider.clients.ForEach(client =>
                    {
                        uclient = UnturnedPlayer.FromSteamPlayer(client);

                        if (uclient.HasPermission("jailplugin.police"))
                        {

                            ChatManager.serverSendMessage(string.Format($"{Jail.namePluginChat}{Jail.instance.Translations.Instance.Translate("release_time", prisioner.DisplayName, this.jail.name, this._elapsedTime.ElapsedMilliseconds / 1000).Replace('(', '<').Replace(')', '>')}"), Color.white, null, client, EChatMode.WELCOME, Jail.instance.Configuration.Instance.imageUrl, true);
                        }
                    });

                }
            });
        }
    }
}
