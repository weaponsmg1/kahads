using EFT;
using EFT.Ballistics;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;

namespace KillsAndHitAndDeathSounds.Patches
{
    internal class HitPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(Player), nameof(Player.ApplyDamageInfo));
        }

        [PatchPostfix]
        static void Postfix(Player __instance, DamageInfo damageInfo, EBodyPart bodyPartType)
        {
            if (!Utils.InRaid || Utils.LocalPlayer == null)
                return;

            if (!Plugin.EnableHitSound.Value)
                return;

            if ((Player)damageInfo.Player?.iPlayer != Utils.LocalPlayer)
                return;

            SoundPlayer.PlayAsync("hit.mp3", Plugin.HitSoundVolume.Value);
        }
    }
}