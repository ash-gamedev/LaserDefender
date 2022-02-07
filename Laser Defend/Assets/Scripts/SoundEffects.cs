using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class SoundEffects : MonoBehaviour
    {
        [SerializeField] List<AudioClip> soundEffects;

        AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();    
        }

        public void PlaySoundEffect(Enum.Sounds soundEffect, float volume = 1f)
        {
            audioSource.PlayOneShot(soundEffects[(int)soundEffect], volume);
        }
    }
}