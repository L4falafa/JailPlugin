using Rocket.API.Collections;
using Rocket.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lafalafa.JailPlugin
{
    public class Jail : RocketPlugin<JailConfig>
    {
        private static List<JailModel> jails;
        protected override void Load()
        {
            
            

        }
        protected override void Unload()
        {

        }

        #region traduccion
        public override TranslationList DefaultTranslations => new TranslationList()
        {
            {"VICTIM_FROZED"," You has been tased, must wait {0} second to be realesed"},
            {"KILLER_FROZED"," You tased a player"}
        };
        #endregion

    }
}
