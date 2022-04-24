using Sh.Items;
using UnityEngine;

// Sits on slots, which are used for weapon item equipment
namespace Sh.Inventory
{
    public class WeaponEquipmentPanel : EquipmentPanel
    {
        public WeaponTypes allowedWeaponType;

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