using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace PickupMeleeWeapons
{
    public static class PickupMeleeWeaponsHelper
    {
        public static bool HasSameTypeOfMeleeWeapon(Agent agent, WeaponClass weaponClass, bool isOnSpawn)
        {
            for (EquipmentIndex index = EquipmentIndex.WeaponItemBeginSlot; index < EquipmentIndex.ExtraWeaponSlot; index++)
            {
                if (isOnSpawn)
                {
                    if (!agent.SpawnEquipment[index].IsEmpty && agent.SpawnEquipment[index].Item.PrimaryWeapon.IsMeleeWeapon && agent.SpawnEquipment[index].Item.PrimaryWeapon.WeaponClass == weaponClass)
                    {
                        return true;
                    }
                }
                else
                {
                    if (!agent.Equipment[index].IsEmpty && agent.Equipment[index].Item.PrimaryWeapon.IsMeleeWeapon && agent.Equipment[index].Item.PrimaryWeapon.WeaponClass == weaponClass)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool HasLostMeleeWeapon(Agent agent)
        {
            int difference = 0;

            for (EquipmentIndex index = EquipmentIndex.WeaponItemBeginSlot; index < EquipmentIndex.ExtraWeaponSlot; index++)
            {
                if (!agent.SpawnEquipment[index].IsEmpty && agent.SpawnEquipment[index].Item.PrimaryWeapon.IsMeleeWeapon)
                {
                    difference++;
                }

                if (!agent.Equipment[index].IsEmpty && agent.Equipment[index].Item.PrimaryWeapon.IsMeleeWeapon)
                {
                    difference--;
                }
            }

            return difference > 0;
        }
    }
}
