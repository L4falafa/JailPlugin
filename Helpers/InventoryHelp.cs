using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lafalafa.JailPluginJailPlugin.Helpers
{
    public class InventoryHelp
    {
        public static readonly byte[] EMPTY_BYTE_ARRAY = new byte[0];
        public static void ClearPlayerInventory(Player player)
        {
            var playerInv = player.inventory;

        
            player.channel.send("tellSlot", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER,
                (byte)0, (byte)0, new byte[0]);
            player.channel.send("tellSlot", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER,
                (byte)1, (byte)0, new byte[0]);

      
            for (byte page = 0; page < PlayerInventory.PAGES; page++)
            {
                if (page == PlayerInventory.AREA)
                    continue;

                var count = playerInv.getItemCount(page);

                for (byte index = 0; index < count; index++)
                {
                    playerInv.removeItem(page, 0);
                }
            }


            System.Action removeUnequipped = () => {
                for (byte i = 0; i < playerInv.getItemCount(2); i++)
                {
                    playerInv.removeItem(2, 0);
                }
            };

            player.clothing.askWearBackpack(0, 0, EMPTY_BYTE_ARRAY, true);
            removeUnequipped();

            player.clothing.askWearGlasses(0, 0, EMPTY_BYTE_ARRAY, true);
            removeUnequipped();

            player.clothing.askWearHat(0, 0, EMPTY_BYTE_ARRAY, true);
            removeUnequipped();

            player.clothing.askWearPants(0, 0, EMPTY_BYTE_ARRAY, true);
            removeUnequipped();

            player.clothing.askWearMask(0, 0, EMPTY_BYTE_ARRAY, true);
            removeUnequipped();

            player.clothing.askWearShirt(0, 0, EMPTY_BYTE_ARRAY, true);
            removeUnequipped();

            player.clothing.askWearVest(0, 0, EMPTY_BYTE_ARRAY, true);
            removeUnequipped();


        }
    }
}