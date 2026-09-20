using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using KillsAndHitAndDeathSounds.Patches;

namespace KillsAndHitAndDeathSounds
{
    [BepInPlugin("com.weaponsmg1.kahads", "KillsAndHitAndDeathSounds", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance;

        public static ManualLogSource LogSource;

        public static ConfigEntry<bool> EnableHitSound;
        public static ConfigEntry<bool> EnableKillSound;
        public static ConfigEntry<bool> EnableDeathSound;

        public static ConfigEntry<float> HitSoundVolume;
        public static ConfigEntry<float> KillSoundVolume;
        public static ConfigEntry<float> DeathSoundVolume;

        private void Awake()
        {
            Instance = this;
            LogSource = Logger;

            EnableHitSound = Config.Bind("General", "Enable HitSound", true);
            EnableKillSound = Config.Bind("General", "Enable KillSound", true);
            EnableDeathSound = Config.Bind("General", "Enable DeathSound", true);

            HitSoundVolume = Config.Bind("General", "HitSound Volume", 1.0f);
            KillSoundVolume = Config.Bind("General", "KillSound Volume", 1.0f);
            DeathSoundVolume = Config.Bind("General", "DeathSound Volume", 1.0f);

            SoundPlayer.Init();
        }

        private void Start()
        {
            new RegisterPlayerPatch().Enable();

            new GameWorldAwakePatch().Enable();
            new GameWorldStartedPatch().Enable();
            new GameWorldDisposePatch().Enable();

            new HitPatch().Enable();
            new KillPatch().Enable();
            new DeathPatch().Enable();
        }
    }
}