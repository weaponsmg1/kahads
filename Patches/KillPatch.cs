using EFT;
using EFT.Ballistics;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;

namespace KillsAndHitAndDeathSounds.Patches
{
    internal class KillPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(Player), nameof(Player.OnBeenKilledByAggressor));
        }

        [PatchPostfix]
        static void Postfix(Player __instance, IPlayer aggressor, DamageInfo damageInfo, EBodyPart bodyPart)
        {
            if (!Utils.InRaid || Utils.LocalPlayer == null)
                return;

            if (!Plugin.EnableKillSound.Value)
                return;

            if (!ReferenceEquals(aggressor, Utils.LocalPlayer))
                return;

            SoundPlayer.PlayAsync("kill.mp3", Plugin.KillSoundVolume.Value);
        }
    }
}