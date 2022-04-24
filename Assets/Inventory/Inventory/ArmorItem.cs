using UnityEngine;

/// Used for creating armor type items
namespace Sh.Items
{
    public class ArmorItem : Item
    {
        public ArmorTypes armorType;
        public Sprite armorInventorySprite;
        public int armorValue;
        public int armorLevel;
    }
}
