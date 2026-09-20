using EFT;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;

namespace KillsAndHitAndDeathSounds.Patches
{
    internal class DeathPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(Player), nameof(Player.OnDead));
        }

        [PatchPostfix]
        static void Postfix(Player __instance)
        {
            if (!Utils.InRaid || Utils.LocalPlayer == null)
                return;

            if (!Plugin.EnableDeathSound.Value)
                return;

            if (__instance != Utils.LocalPlayer)
                return;

            SoundPlayer.PlayAsync("death.mp3", Plugin.DeathSoundVolume.Value);
        }
    }
}