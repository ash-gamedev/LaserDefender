using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Shooter : MonoBehaviour
    {
        // Serializable fields
        [Header("General")]
        [SerializeField] GameObject projectilePrefab;
        [SerializeField] float baseFiringRate = 0.5f;
        [SerializeField] float projectileSpeed = 10f;
        [SerializeField] float projectileLifetime = 5f;

        [Header("AI")]
        [SerializeField] bool useAI;
        [SerializeField] float minimumFiringRate = 0.1f;
        [SerializeField] float firingRateVariance = 0.2f;
        
        // public fields
        [HideInInspector] public bool isFiring;

        // private fields
        private Coroutine firingCoroutine;
        private AudioPlayer soundEffects;

        #region Start, Update
        void Start()
        {
            soundEffects = FindObjectOfType<AudioPlayer>();
            if (useAI)
                isFiring = true;
        }

        void Update()
        {
            Fire();
        }
        #endregion

        #region public functions
        public GameObject GetProjectile()
        {
            return projectilePrefab;
        }

        public void SetProjectile(GameObject newProjectile)
        {
            projectilePrefab = newProjectile;
        }

        #endregion

        #region private functions
        /// <summary>
        /// Begin firing projectiles
        /// </summary>
        void Fire()
        {
            if (isFiring && firingCoroutine == null)
            {
                firingCoroutine = StartCoroutine(FireContinuously());
            }
            else if (!isFiring && firingCoroutine != null)
            {
                //option 1 (hammer option): StopAllCoroutines();
                //option 2: StopCoroutine()
                StopCoroutine(firingCoroutine);
                firingCoroutine = null;
            }
        }

        /// <summary>
        /// Couroutine to continuously fires projectiles
        /// </summary>
        /// <returns></returns>
        IEnumerator FireContinuously()
        {
            while (true)
            {
                GameObject instance = Instantiate(projectilePrefab,  // what object to instantiate
                        transform.position, // where to spawn the object
                        Quaternion.identity // need to specify rotation
                        );

                Rigidbody2D instanceRb = instance.GetComponent<Rigidbody2D>();
                if (instanceRb != null)
                    instanceRb.velocity = (useAI ? -transform.up : transform.up) * projectileSpeed;

                // play sound
                Enum.Sounds firingSound = useAI ? Enum.Sounds.EnemyShot : Enum.Sounds.PlayerShot;
                soundEffects.PlaySoundEffect(firingSound);

                Destroy(instance, projectileLifetime);

                float timeTillNextProjectile;
                if (!useAI) 
                { 
                    timeTillNextProjectile = baseFiringRate;
                }
                else
                {
                    float newFiringRate = UnityEngine.Random.Range( baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance );
                    timeTillNextProjectile = Mathf.Clamp(newFiringRate, minimumFiringRate, float.MaxValue);
                }

                yield return new WaitForSeconds(timeTillNextProjectile);
            }
        }
        #endregion
    }
}