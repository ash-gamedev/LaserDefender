using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] List<AudioClip> soundEffects;

        // static persists through all instances of a class
        static AudioPlayer instance;

        private void Awake()
        {
            ManageSingleton();
        }

        void ManageSingleton()
        {
            if(instance != null)
            {
                // need to disable this so other objects don't try to access
                gameObject.SetActive(false);

                // now destroy
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void PlaySoundEffect(Enum.Sounds soundEffect, float volume = 1f)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioClip audioClip = soundEffects[(int)soundEffect];
            AudioSource.PlayClipAtPoint(audioClip, cameraPos, volume);
        }
    }
}