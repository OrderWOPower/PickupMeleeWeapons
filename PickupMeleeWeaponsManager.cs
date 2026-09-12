using System.Collections.Generic;
using TaleWorlds.MountAndBlade;

namespace PickupMeleeWeapons
{
	public class PickupMeleeWeaponsManager
	{
		private static readonly PickupMeleeWeaponsManager _pickupMeleeWeaponsManager = new PickupMeleeWeaponsManager();

		public static PickupMeleeWeaponsManager Current => _pickupMeleeWeaponsManager;

		public HashSet<Agent> ItemPickupQueueEven { get; set; }
		public HashSet<Agent> ItemPickupQueueOdd { get; set; }

		public PickupMeleeWeaponsManager()
		{
			ItemPickupQueueEven = new HashSet<Agent>();
			ItemPickupQueueOdd = new HashSet<Agent>();
		}
	}
}
