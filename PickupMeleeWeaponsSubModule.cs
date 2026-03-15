using System;
using System.Linq;
using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace PickupMeleeWeapons
{
	// This mod makes troops pick up dropped melee weapons.
	public class PickupMeleeWeaponsSubModule : MBSubModuleBase
	{
		private Harmony _harmony;
		private Type _typeofStanceLogic;

		protected override void OnSubModuleLoad()
		{
			_harmony = new Harmony("mod.bannerlord.pickupmeleeweapons");
			_harmony.PatchAll();
		}

		protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
		{
			gameStarterObject.AddModel(new PickupMeleeWeaponsModel((ItemPickupModel)gameStarterObject.Models.Last(model => model is ItemPickupModel)));

			_typeofStanceLogic = AccessTools.TypeByName("RBMAI.StanceLogic");

			// Check whether RBM is loaded.
			if (_typeofStanceLogic != null)
			{
				_harmony.Patch(AccessTools.Method(AccessTools.Inner(_typeofStanceLogic, "CreateMeleeBlowPatch"), "TryToDropWeapon"), transpiler: new HarmonyMethod(AccessTools.Method(typeof(PickupMeleeWeaponsStanceLogic), "Transpiler")));
				_harmony.Patch(AccessTools.Method(_typeofStanceLogic, "forceTiredAnimation"), prefix: new HarmonyMethod(AccessTools.Method(typeof(PickupMeleeWeaponsStanceLogic), "Prefix")));
			}
		}

		public override void OnGameEnd(Game game)
		{
			if (_typeofStanceLogic != null)
			{
				_harmony.Unpatch(AccessTools.Method(AccessTools.Inner(_typeofStanceLogic, "CreateMeleeBlowPatch"), "TryToDropWeapon"), AccessTools.Method(typeof(PickupMeleeWeaponsStanceLogic), "Transpiler"));
				_harmony.Unpatch(AccessTools.Method(_typeofStanceLogic, "forceTiredAnimation"), AccessTools.Method(typeof(PickupMeleeWeaponsStanceLogic), "Prefix"));
			}
		}
	}
}
