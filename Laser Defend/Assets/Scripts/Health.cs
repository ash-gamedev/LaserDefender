using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int health = 50;

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

                //Tell damage dealer it hit something
                damageDealer.Hit();
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
                Destroy(gameObject);
            }
        }
        #endregion
    }
}