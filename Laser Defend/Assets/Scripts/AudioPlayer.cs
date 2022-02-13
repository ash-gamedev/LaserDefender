using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class AudioPlayer : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] AudioClip damageTaken;
        [SerializeField] float damageTakenVolume;

        [SerializeField] AudioClip destroyed;
        [SerializeField] float destroyedVolume;

        [Header("Player")]
        [SerializeField] AudioClip playerProjectile;
        [SerializeField] float playerProjectileVolume;

        [Header("Enemy")]
        [SerializeField] AudioClip enemyProjectile;
        [SerializeField] float enemyProjectileVolume;

        [Header("PowerUps")]
        [SerializeField] AudioClip shieldHit;
        [SerializeField] float shieldHitVolume;

        [SerializeField] AudioClip powerUpAppear;
        [SerializeField] float powerUpAppearVolume;

        [SerializeField] AudioClip powerUpGained;
        [SerializeField] float powerUpGainedVolume;

        [SerializeField] AudioClip powerUpDisappear;
        [SerializeField] float powerUpDisppearVolume;

        // disctionary
        Dictionary<Enum.Sounds, (AudioClip, float)> soundEffects;

        // static persists through all instances of a class
        static AudioPlayer instance;

        private void Awake()
        {
            ManageSingleton();

            soundEffects = new Dictionary<Enum.Sounds, (AudioClip, float)>
            {
                { Enum.Sounds.Damage, (damageTaken, damageTakenVolume) },
                { Enum.Sounds.Destroyed, (destroyed, destroyedVolume) },
                { Enum.Sounds.EnemyShot, (enemyProjectile, enemyProjectileVolume) },
                { Enum.Sounds.PlayerShot, (playerProjectile, playerProjectileVolume) },
                { Enum.Sounds.ShieldHit, (shieldHit, shieldHitVolume) },
                { Enum.Sounds.PowerUpAppear, (powerUpAppear, powerUpAppearVolume) },
                { Enum.Sounds.PowerUpGained, (powerUpGained, powerUpGainedVolume) },
                { Enum.Sounds.PowerUpLost, (powerUpDisappear, powerUpDisppearVolume) }
            };
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

        public void PlaySoundEffect(Enum.Sounds soundEffectName)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            (AudioClip, float) soundEffect = soundEffects[soundEffectName];
            AudioClip audioClip = soundEffect.Item1;
            float volume = soundEffect.Item2;

            AudioSource.PlayClipAtPoint(audioClip, cameraPos, volume);
        }
    }
}