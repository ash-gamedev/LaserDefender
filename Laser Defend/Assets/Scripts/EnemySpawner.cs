using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        // Serializavle variables
        [SerializeField] List<WaveConfigSO> waveConfigs;
        [SerializeField] float timeBetweenWaves = 0f;
        [SerializeField] bool isLooping = true;

        // private variables
        WaveConfigSO currentWave;
        
        #region Start
        void Start()
        {
            StartCoroutine(SpawnEnemyWaves());
        }
        #endregion

        #region public functions
        public WaveConfigSO GetWaveConfigSO()
        {
            return currentWave;
        }
        #endregion

        #region private functions
        /// <summary>
        /// Loops through each wave in list, and spawns enemies for the wave. Wait x time between waves.
        /// </summary>
        /// <returns>Nothing. IEnumerator used for "WaitForSeconds"</returns>
        IEnumerator SpawnEnemyWaves()
        {
            do
            {
                foreach (WaveConfigSO wave in waveConfigs)
                {
                    currentWave = wave;
                    int numEnemies = currentWave.GetEnemyCount();
                    for (int i = 0; i < numEnemies; i++)
                    {
                        Instantiate(currentWave.GetEnemyPrefab(i),  // what object to instantiate
                        currentWave.GetStartingWaypoint().position, // where to spawn the object
                        Quaternion.identity, // need to specify rotation
                        transform); // this will place the enemies under the Enemy Spawner object as a child

                        yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                    }

                    yield return new WaitForSeconds(timeBetweenWaves);
                }
            } while (isLooping);
        }
        #endregion
    }
}