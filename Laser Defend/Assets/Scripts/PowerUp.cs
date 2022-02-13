using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PowerUp : MonoBehaviour
    {
        [SerializeField] float duration = 10f; // 10 seconds
        [SerializeField] GameObject powerUp;

        GameObject powerUpInstance;
        bool powerUpActive = false;

        SpriteRenderer spriteRenderer;
        SpriteRenderer powerUpSpriteRenderer;

        Animator animator;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        public bool IsPowerUpActive()
        {
            return powerUpActive;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && powerUpActive == false)
            {
                powerUpActive = true;

                // hide sprite
                animator.Play("ShieldPowerUpDissappear");
                //spriteRenderer.enabled = false;

                // instantiate object
                powerUpInstance = Instantiate(powerUp,  // what object to instantiate
                        Camera.main.transform.position, // where to spawn the object
                        Quaternion.identity); // need to specify rotation

                // get sprite
                powerUpSpriteRenderer = powerUpInstance.GetComponent<SpriteRenderer>();

                // wait to destroy
                StartCoroutine(WaitAndDestroy());
            }
        }

        IEnumerator WaitAndDestroy()
        {
            if (powerUp.CompareTag("Shield"))
            {
                // wait fraction of duration, then flash sprite
                yield return new WaitForSeconds(duration * 0.8f);

                // flash shield before power down
                float maxTime = duration * 0.2f;
                float currentTime = 0f;

                while(currentTime < maxTime)
                {
                    powerUpSpriteRenderer.enabled = false;
                    yield return new WaitForSeconds(.1f);
                    powerUpSpriteRenderer.enabled = true;
                    yield return new WaitForSeconds(.1f);
                    currentTime += 0.2f;
                }
            }
            else
            {
                // wait full duration
                yield return new WaitForSeconds(duration);
            }
            
            powerUpActive = false;
            Destroy(gameObject);
            Destroy(powerUpInstance);
        }

    }
}