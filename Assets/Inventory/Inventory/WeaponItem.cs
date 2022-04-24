using UnityEngine;

/// Used for creating weapon type items
namespace Sh.Items
{
    public class WeaponItem : Item
    {
        public WeaponTypes weaponType;
        public Sprite weaponInventorySprite;
        public int weaponDamage;
        public int weaponLevel;
    }
}
