using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;

namespace PickupMeleeWeapons
{
	public class PickupMeleeWeaponsStanceLogic
	{
		public static bool Prefix() => false;

		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = instructions.ToList();
			int startIndex = 0, endIndex = 0;

			for (int i = 0; i < codes.Count; i++)
			{
				if (codes[i].opcode == OpCodes.Cgt)
				{
					startIndex = i - 1;
					endIndex = i;
				}
			}

			// Remove the restriction on dropping the last melee weapon.
			codes.RemoveRange(startIndex, endIndex - startIndex + 1);

			return codes;
		}
	}
}
