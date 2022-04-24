using UnityEngine;
using Sh.Items;

// Sits on slots, which are used for trinket item equipment
namespace Sh.Inventory
{
    public class TrinketEquipmentPanel : EquipmentPanel
    {
        private void Update()
        {
            if (equipedItem != null && LastItem == null)
            {
                LastItem = equipedItem;
            }

            if (equipedItem == null && LastItem != null)
            {
                LastItem = null;
            }
        }
    }
}