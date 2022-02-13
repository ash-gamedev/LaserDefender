using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PowerUpManager : MonoBehaviour
    {

        [SerializeField] List<GameObject> powerUps;
        [SerializeField] float timeBetweenPowerUps = 15f;

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
            do
            {
                foreach (GameObject power in powerUps)
                {
                    Instantiate(power,  // what object to instantiate
                        Camera.main.transform.position, // where to spawn the object
                        Quaternion.identity); // need to specify rotation

                    yield return new WaitForSeconds(timeBetweenPowerUps);
                }
            } while (true);
        }
        #endregion
    }
}