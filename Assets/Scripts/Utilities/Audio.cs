using System.Collections;
using UnityEngine;

namespace Utilities {
    
    public class Audio : MonoBehaviour{

        public static void PlayAudio(AudioSource audioSource, AudioClip audioClip, float volume = 1f, bool loop = false, float pitch = 1f) {
            audioSource.clip = audioClip;
            // audioSource.volume = volume;
            audioSource.loop = loop;
            audioSource.pitch = pitch;
            audioSource.Play();
        }
        
    }
}