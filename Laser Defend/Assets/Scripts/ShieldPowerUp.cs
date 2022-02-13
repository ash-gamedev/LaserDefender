using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ShieldPowerUp : MonoBehaviour
    {
        [SerializeField] ParticleSystem hitEffect;
        [SerializeField] float rotationTime = 100f;
        private AudioPlayer soundEffects;
        private Player player;

        private void Awake()
        {
            soundEffects = FindObjectOfType<AudioPlayer>();
            player = FindObjectOfType<Player>();
        }

        private void Update()
        {
            FollowPlayer();
            Rotate();
        }

        private void FollowPlayer()
        {
            this.transform.position = player.transform.position;
        }

        private void Rotate()
        {
            transform.Rotate(new Vector3(0f, 0f, rotationTime) * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                Health collisionHealth = collision.GetComponent<Health>();

                // if enemy, kill. if projectile, destroy.
                if (collisionHealth != null)
                    collisionHealth.Kill();
                else // projectile
                {
                    Destroy(collision.gameObject);

                    // Sound
                    soundEffects.PlaySoundEffect(Enum.Sounds.ShieldHit);

                    // Particles
                    ParticleSystem instance = Instantiate(hitEffect, collision.transform.position, Quaternion.identity);
                    Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
                }                    
            }
        }
    }
}