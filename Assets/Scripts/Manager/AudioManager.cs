using UnityEngine.Audio;
using System;
using UnityEngine;
﻿using UnityEngine.Audio;
using AngryChief.Audio;

namespace AngryChief.Manager
{
    /// <summary>
    /// Maintains a library of sound clips and plays from them
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
        public Sound[] sounds;
        Sound lastStartedSound;
        
        void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else
                if (Instance != this)
                Destroy(gameObject);
        }
        
        private void Start()
        {
            foreach (Sound s in sounds)
            {
                if (s.isMusic)
                    s.volume = PlayerPrefs.GetFloat("musicVolume");
                else
                    s.volume = PlayerPrefs.GetFloat("soundVolume");
                
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }

        public void ChangeMusicVolume(float volume)
        {
            foreach (Sound s in sounds)
            {
                if (s.isMusic)
                {
                    s.source.volume = volume;
                    s.volume = volume;
                }
            }       
        }
    
        public void ChangeSoundVolume(float volume)
        {
            foreach (Sound s in sounds)
            {
                if (!s.isMusic)
                {
                    s.source.volume = volume;
                    s.volume = volume;
                }
            }
            
            AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
            
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.volume = volume;
            }
        }
        
        /// <summary>
        /// Start continuous playback
        /// </summary>
        /// <param name="name">Sound to play</param>
        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " nicht gefunden");
                return;
            }

            s.source.Play();
            /*
            if (lastStartedSound != null)
            {
                Stop(lastStartedSound.name);
                s.source.Play();
            }
            else
            {
                s.source.Play();
            }

            lastStartedSound = s;*/
        }

        /// <summary>
        /// Stop playback
        /// </summary>
        /// <param name="name"></param>
        public void Stop(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " nicht gefunden");
                return;
            }
            s.source.Stop();
        }

        /// <summary>
        /// Gibt die AudioSource wieder
        /// </summary>
        /// <param name="name">Sound to play</param>
        public AudioSource Find(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " nicht gefunden");
                return null;
            }

            return s.source;
        }
    }
}