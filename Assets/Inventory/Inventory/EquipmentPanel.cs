using Sh.Items;
using UnityEngine;

// Sits on slots, which are used for item equipment
namespace Sh.Inventory 
{
    public class EquipmentPanel : MonoBehaviour
    {
        [HideInInspector] public GridSlot mainSlot;
        [HideInInspector] public int width, height;
        public Item equipedItem;
        
        private Item lastItem;  
        
        public ItemTypes allowedItemType;
        public Item LastItem { get => lastItem; set => lastItem = value; }

        private void Update()
        {
            if(equipedItem != null && lastItem == null)
            {
                lastItem = equipedItem;
            }

            if(equipedItem == null && lastItem != null)
            {
                    lastItem = null;
            }
        }
    }
}