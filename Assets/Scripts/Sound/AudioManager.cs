using Core;
using UnityEngine;

namespace Sound
{
    public class AudioManager : Singleton<AudioManager>
    {
        public AudioSource soundsAudioSource;
        public AudioSource musicAudioSource;
        public AudioSource ambientAudioSource;
        
        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public void PlaySound(string soundName)
        {
            soundsAudioSource.PlayOneShot(AudioLoader.LoadSound(soundName));
        }

        public void PlayMusic(string musicName)
        {
            if (musicAudioSource.isPlaying)
                StopMusic();

            musicAudioSource.clip = AudioLoader.LoadMusic(musicName);
            
            musicAudioSource.Play();
        }

        public void StopMusic()
        {
            musicAudioSource.Stop();
        }

        public void PlayAmbient(string trackName)
        {
            if(ambientAudioSource.isPlaying)
                StopAmbient();

            ambientAudioSource.clip = AudioLoader.LoadAmbient(trackName);
            
            ambientAudioSource.Play();
        }

        public void StopAmbient()
        {
            ambientAudioSource.Stop();
        }
    }
}