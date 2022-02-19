using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class MisslePowerUp : MonoBehaviour
    {
        [SerializeField] GameObject missleProjectilePrefab;
        GameObject originalProjectilePrefab;
        Enum.Sounds originalShootSound;

        Shooter playerShooter;

        void Start()
        {
            // find player shooter
            playerShooter = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooter>();

            // get original projectile prefab
            originalProjectilePrefab = playerShooter.GetProjectile();
            originalShootSound = playerShooter.playerShootingSound;

            // update projectile to missle
            playerShooter.SetProjectile(missleProjectilePrefab);
            playerShooter.playerShootingSound = Enum.Sounds.MissleSound;
        }

        private void Update()
        {
            FollowPlayer();
        }

        private void FollowPlayer()
        {
            if(playerShooter != null)
                this.transform.position = new Vector2(playerShooter.transform.position.x, playerShooter.transform.position.y + 0.5f);
        }

        private void OnDestroy()
        {
            // reset projectile
            playerShooter.SetProjectile(originalProjectilePrefab);
            playerShooter.playerShootingSound = originalShootSound;
        }
    }
}