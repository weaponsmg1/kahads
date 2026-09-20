using System.Threading.Tasks;
using UnityEngine;

namespace KillsAndHitAndDeathSounds
{
    public static class SoundPlayer
    {
        private static AudioSource _source;

        public static void Init()
        {
            _source = Plugin.Instance.gameObject.GetComponent<AudioSource>() ??
                       Plugin.Instance.gameObject.AddComponent<AudioSource>();

            _source.spatialBlend = 0f;
            _source.playOnAwake = false;
        }

        public static void Play(AudioClip clip, float volume)
        {
            if (clip == null || _source == null)
                return;

            _source.PlayOneShot(clip, Mathf.Clamp01(volume));
        }

        public static async void PlayAsync(string file, float volume)
        {
            if (string.IsNullOrEmpty(file))
                return;

            var clip = await AudioLoader.LoadAudioAsync(file);

            if (clip == null)
                return;

            Play(clip, volume);
        }
    }
}