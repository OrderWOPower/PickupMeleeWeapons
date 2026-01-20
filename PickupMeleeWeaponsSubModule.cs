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
        private Type _typeofPostureLogic;

        protected override void OnSubModuleLoad()
        {
            _harmony = new Harmony("mod.bannerlord.pickupmeleeweapons");
            _harmony.PatchAll();
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            gameStarterObject.AddModel(new PickupMeleeWeaponsModel((ItemPickupModel)gameStarterObject.Models.Last(model => model is ItemPickupModel)));

            _typeofPostureLogic = AccessTools.TypeByName("RBMAI.PostureLogic+CreateMeleeBlowPatch");

            // Check whether RBM is loaded.
            if (_typeofPostureLogic != null)
            {
                _harmony.Patch(AccessTools.Method(_typeofPostureLogic, "TryToDropWeapon"), transpiler: new HarmonyMethod(AccessTools.Method(typeof(PickupMeleeWeaponsPostureLogic), "Transpiler")));
                _harmony.Patch(AccessTools.Method(_typeofPostureLogic, "forceTiredAnimation"), prefix: new HarmonyMethod(AccessTools.Method(typeof(PickupMeleeWeaponsPostureLogic), "Prefix")));
            }

            _harmony.Unpatch(AccessTools.Method(typeof(HumanAIComponent), "OnTick"), HarmonyPatchType.All, "com.rbmai");
        }

        public override void OnGameEnd(Game game)
        {
            if (_typeofPostureLogic != null)
            {
                _harmony.Unpatch(AccessTools.Method(_typeofPostureLogic, "TryToDropWeapon"), AccessTools.Method(typeof(PickupMeleeWeaponsPostureLogic), "Transpiler"));
                _harmony.Unpatch(AccessTools.Method(_typeofPostureLogic, "forceTiredAnimation"), AccessTools.Method(typeof(PickupMeleeWeaponsPostureLogic), "Prefix"));
            }
        }
    }
}
