using UnityEngine;
using Sh.Items;

// Sits on slots, which are used for armor equipment
namespace Sh.Inventory
{
    public class ArmorEquipmentPanel : EquipmentPanel
    {
        public ArmorTypes allowedArmorType;

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