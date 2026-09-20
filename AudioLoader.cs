using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace KillsAndHitAndDeathSounds
{
    public static class AudioLoader
    {
        private static readonly Dictionary<string, AudioClip> AudioCache = new();
        private static string Directory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static async Task<AudioClip> LoadAudioAsync(string file)
        {
            if (!file.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
                file += ".mp3";

            if (AudioCache.TryGetValue(file, out var cacheclip) && cacheclip)
                return cacheclip;

            if (AudioCache.ContainsKey(file))
                AudioCache.Remove(file);

            string path = Path.Combine(Directory, file);

            if (!File.Exists(path))
            {
                Plugin.LogSource.LogError($"[KAHAD] Audio file not found: {path}");
                return null;
            }

            string url = new Uri(path).AbsoluteUri;

            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
            {
                var operation = www.SendWebRequest();

                while (!operation.isDone)
                {
                    await Task.Yield();
                }

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Plugin.LogSource.LogError($"[KAHAD] Error loading audio: {www.error}");
                    return null;
                }

                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                if (clip)
                {
                    clip.name = file;
                    AudioCache[file] = clip;
                }

                return clip;
            }
        }

        public static void ClearCache()
        {
            AudioCache.Clear();
        }
    }
}