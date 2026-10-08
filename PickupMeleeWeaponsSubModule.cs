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
		private Type _typeofAgentAi;

		protected override void OnSubModuleLoad()
		{
			_harmony = new Harmony("mod.bannerlord.pickupmeleeweapons");
			_harmony.PatchAll();
		}

		protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
		{
			gameStarterObject.AddModel(new PickupMeleeWeaponsModel((ItemPickupModel)gameStarterObject.Models.Last(model => model is ItemPickupModel)));

			_typeofAgentAi = AccessTools.TypeByName("RBMAI.AgentAi");

			// Check whether RBM is loaded.
			if (_typeofAgentAi != null)
			{
				_harmony.Patch(AccessTools.Method(AccessTools.Inner(_typeofAgentAi, "OnTickPatch"), "TrySeekMeleeWeapon"), prefix: new HarmonyMethod(AccessTools.Method(typeof(PickupMeleeWeaponsAgentAi), "Prefix")));
			}
		}

		public override void OnBeforeMissionBehaviorInitialize(Mission mission) => mission.AddMissionBehavior(new PickupMeleeWeaponsMissionBehavior());

		public override void OnGameEnd(Game game)
		{
			if (_typeofAgentAi != null)
			{
				_harmony.Unpatch(AccessTools.Method(_typeofAgentAi, "TrySeekMeleeWeapon"), AccessTools.Method(typeof(PickupMeleeWeaponsAgentAi), "Prefix"));
			}
		}
	}
}
