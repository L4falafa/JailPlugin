using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lafalafa.JailPlugin
{
    public class PrisionerComponent : MonoBehaviour
    {

        public UnturnedPlayer prisioner;
        UnturnedPlayer judge;
        string reason;
        int time;
        JailModel jail;

        public PrisionerComponent(UnturnedPlayer judge, string reason, int time, string jailName)
        { 
        
               

        }
        void Awake()
        {

            prisioner = GetComponent<UnturnedPlayer>();

        }
            void Update()
        {

            if(Vector3.Distance(prisioner.Position, jail.loc) > jail.radius )
            {



            }

        }
    }
}
