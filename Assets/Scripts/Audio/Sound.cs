using UnityEngine.Audio;
using UnityEngine;

//Definiert die Eigenschaften der Audios vom AudioManager
namespace AngryChief.Audio
{
    [System.Serializable]
    public class Sound
    {
        public string name;

        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;

        [Range(.1f, 2f)]
        public float pitch;

        public bool loop;

        [HideInInspector]
        public AudioSource source;

        public bool isMusic;
        
        public bool isSpatialSound;
    }
}