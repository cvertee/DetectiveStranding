using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public static class AudioLoader
    {
        private const string SoundPath = "sounds";
        private const string AmbientPath = "sounds/ambient";
        private const string MusicPath = "music";
        
        private static readonly Dictionary<string, AudioClip> CachedSounds = new Dictionary<string, AudioClip>();
        private static readonly Dictionary<string, AudioClip> CachedMusic = new Dictionary<string, AudioClip>();
        private static readonly Dictionary<string, AudioClip> CachedAmbient = new Dictionary<string, AudioClip>();
        
        public static AudioClip LoadSound(string soundName)
        {
            if (CachedSounds.TryGetValue(soundName, out var audioClip))
                return audioClip;

            audioClip = Resources.Load<AudioClip>($"{SoundPath}/{soundName}");

            if (audioClip == null)
                Debug.LogWarning($"Can't find sound {soundName}, will return null");
            else
                CachedSounds.Add(soundName, audioClip);

            return audioClip;
        }

        public static AudioClip LoadMusic(string musicName)
        {
            if (CachedMusic.TryGetValue(musicName, out var audioClip))
                return audioClip;

            audioClip = Resources.Load<AudioClip>($"{MusicPath}/{musicName}");

            if (audioClip == null)
                Debug.LogWarning($"Can't find music {musicName}, will return null");
            else
                CachedMusic.Add(musicName, audioClip);

            return audioClip;
        }

        public static AudioClip LoadAmbient(string trackName)
        {
            if (CachedAmbient.TryGetValue(trackName, out var audioClip))
                return audioClip;

            audioClip = Resources.Load<AudioClip>($"{AmbientPath}/{trackName}");

            if (audioClip == null)
                Debug.LogWarning($"Can't find ambient {trackName}, will return null");
            else
                CachedAmbient.Add(trackName, audioClip);

            return audioClip;
        } 
    }
}