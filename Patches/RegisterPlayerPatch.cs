using EFT;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;

namespace KillsAndHitAndDeathSounds.Patches
{
    internal class RegisterPlayerPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(GameWorld), nameof(GameWorld.RegisterPlayer));
        }

        [PatchPostfix]
        static void Postfix(IPlayer iPlayer)
        {
            if (iPlayer == null || !iPlayer.IsYourPlayer)
                return;

            if (iPlayer is Player player)
            {
                Utils.LocalPlayer = player;
                Plugin.LogSource.LogDebug($"[KAHAD] LocalPlayer registered: {player.Profile?.Nickname}");
            }
        }
    }
}