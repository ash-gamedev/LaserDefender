using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class MisslePowerUp : MonoBehaviour
    {
        [SerializeField] GameObject missleProjectilePrefab;
        GameObject originalProjectilePrefab;

        Shooter playerShooter;

        void Start()
        {
            // find player shooter
            playerShooter = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooter>();

            // get original projectile prefab
            originalProjectilePrefab = playerShooter.GetProjectile();

            // update projectile to missle
            playerShooter.SetProjectile(missleProjectilePrefab);
        }

        private void OnDestroy()
        {
            // reset projectile
            playerShooter.SetProjectile(originalProjectilePrefab);
        }
    }
}