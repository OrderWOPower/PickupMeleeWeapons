using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace PickupMeleeWeapons
{
	public static class PickupMeleeWeaponsHelper
	{
		public static bool HadSameTypeOfMeleeWeaponOnSpawn(Agent agent, WeaponClass weaponClass)
		{
			for (EquipmentIndex index = EquipmentIndex.WeaponItemBeginSlot; index < EquipmentIndex.ExtraWeaponSlot; index++)
			{
				if (!agent.SpawnEquipment[index].IsEmpty && agent.SpawnEquipment[index].Item.PrimaryWeapon.IsMeleeWeapon && agent.SpawnEquipment[index].Item.PrimaryWeapon.WeaponClass == weaponClass)
				{
					return true;
				}
			}

			return false;
		}

		public static bool HasSameTypeOfMeleeWeaponCurrently(Agent agent, WeaponComponentData otherWeapon)
		{
			for (EquipmentIndex index = EquipmentIndex.WeaponItemBeginSlot; index < EquipmentIndex.ExtraWeaponSlot; index++)
			{
				if (!agent.Equipment[index].IsEmpty)
				{
					WeaponComponentData equippedWeapon = agent.Equipment[index].Item.PrimaryWeapon;

					if (equippedWeapon.IsMeleeWeapon && ((equippedWeapon.IsOneHanded && otherWeapon.IsOneHanded) || (equippedWeapon.IsTwoHanded && otherWeapon.IsTwoHanded) || (equippedWeapon.IsPolearm && otherWeapon.IsPolearm)))
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
