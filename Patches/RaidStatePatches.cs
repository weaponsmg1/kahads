using EFT;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;

namespace KillsAndHitAndDeathSounds.Patches
{
    internal class GameWorldAwakePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(GameWorld), nameof(GameWorld.Awake));
        }

        [PatchPostfix]
        static void Postfix(GameWorld __instance)
        {
            Utils.InRaid = false;
            Utils.World = __instance;
        }
    }

    internal class GameWorldStartedPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(GameWorld), nameof(GameWorld.OnGameStarted));
        }

        [PatchPostfix]
        static void Postfix(GameWorld __instance)
        {
            Utils.World = __instance;
            Utils.InRaid = true;
            Utils.LocalPlayer = null;
            Plugin.LogSource.LogInfo("[KAHAD] Raid started");
        }
    }

    internal class GameWorldDisposePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(GameWorld), nameof(GameWorld.Dispose));
        }

        [PatchPostfix]
        static void Postfix(GameWorld __instance)
        {
            if (!ReferenceEquals(Utils.World, __instance))
                return;

            Utils.InRaid = false;
            Utils.World = null;
            Utils.LocalPlayer = null;
            AudioLoader.ClearCache();
            Plugin.LogSource.LogInfo("[KAHAD] Raid ended");
        }
    }
}