using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] int damage = 10;

        public int GetDamage()
        {
            return damage;
        }

        public void Hit()
        {
            Destroy(gameObject);
        }
    }
}