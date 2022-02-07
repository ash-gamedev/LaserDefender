using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int health = 50;
        [SerializeField] int scorePoints = 50;
        [SerializeField] ParticleSystem hitEffect;
        [SerializeField] bool applyCameraShake;

        // private variables
        CameraShake cameraShake;
        SoundEffects soundEffects;
        ScoreKeeper scoreKeeper;

        #region Awake
        private void Awake()
        {
            this.scoreKeeper = FindObjectOfType<ScoreKeeper>();
            this.soundEffects = FindObjectOfType<SoundEffects>();
            this.cameraShake = Camera.main.GetComponent<CameraShake>();
        }
        #endregion

        #region public functions
        public int GetHealth() 
        {
            return health;
        }
        #endregion

        #region private functions
        void OnTriggerEnter2D(Collider2D collision)
        {
            // if collider is damage dealer, take damage
            DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

            if(damageDealer != null)
            {
                //Take damage
                TakeDamage(damageDealer.GetDamage());

                //Play hit effect
                PlayHitEffect();
                ShakeCamera();

                //Tell damage dealer it hit something
                damageDealer.Hit();
            }
        }

        private void ShakeCamera()
        {
            if (cameraShake != null && applyCameraShake)
            {
                cameraShake.Play();
            }
        }

        /// <summary>
        /// Deals damage to health. If health <= 0, destory object.
        /// </summary>
        /// <param name="damage"></param>
        void TakeDamage(int damage)
        {
            health -= damage;

            if (health <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            // Destroyed Sound Effect
            soundEffects.PlaySoundEffect(Enum.Sounds.Destroyed, 0.5f);

            if (gameObject.CompareTag("Enemy"))
            {
                // if enemy was destroyed add points to score
                scoreKeeper.AddToScore(scorePoints);
            }
            else
            {
                // if the player was destroyed, reset score
                scoreKeeper.ResetScore();
            }

            Destroy(gameObject);
        }

        void PlayHitEffect()
        {
            if (hitEffect != null)
            {
                // Damage Sound Effect
                soundEffects.PlaySoundEffect(Enum.Sounds.Damage);

                // Particles
                ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
            }
        }
        #endregion
    }
}