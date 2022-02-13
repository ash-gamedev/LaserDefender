using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PowerUpSpawner : MonoBehaviour
    {

        [SerializeField] List<GameObject> powerUps;
        [SerializeField] float timeBetweenPowerUps = 15f;

        GameObject currentPowerUp = null;

        #region Start
        void Start()
        {
            StartCoroutine(SpawnPowerUps());
        }
        #endregion

        #region private functions
        /// <summary>
        /// Loops through each power up in list, and spawns power up. Wait x time betweenpower ups.
        /// </summary>
        /// <returns>Nothing. IEnumerator used for "WaitForSeconds"</returns>
        IEnumerator SpawnPowerUps()
        {
            int powerUpIndex = 0;
            yield return new WaitForSeconds(timeBetweenPowerUps);
            do
            {
                float waitTime = timeBetweenPowerUps;

                if (currentPowerUp == null)
                {
                    GameObject power = powerUps[powerUpIndex];

                    currentPowerUp = Instantiate(power,  // what object to instantiate
                        power.transform.position, // where to spawn the object
                        Quaternion.identity); // need to specify rotation

                    // switch to next power (works for only 2 powers)
                    powerUpIndex = (powerUpIndex == 0) ? 1 : 0;

                    waitTime += power.GetComponent<PowerUp>().GetPowerUpDuration();
                }

                yield return new WaitForSeconds(waitTime);

            } while (true);
        }
        #endregion
    }
}