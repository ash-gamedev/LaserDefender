using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        // Serializavle variables
        [SerializeField] List<WaveConfigSO> waveConfigs;
        [SerializeField] float timeBetweenWaves = 0f;
        [SerializeField] bool isLooping = true;
        [SerializeField] TextMeshProUGUI textWaveCount;

        // private variables
        WaveConfigSO currentWave;
        float waveTimeVariance = 1f;
        int waveNumber = 0;
        
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
            int waveLoop = 0;
            do
            {
                foreach (WaveConfigSO wave in waveConfigs)
                {
                    waveNumber++;
                    textWaveCount.text = (waveNumber > 9999 ? "W #" : "Wave #") + waveNumber.ToString();
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

                    // increase enemies through each loop of waves
                    int extraWaves = waveLoop;
                    int enemyType = 0;
                    for (int j = 0; j < extraWaves; j++)
                    {
                        Instantiate(currentWave.GetEnemyPrefab(enemyType),  // what object to instantiate
                            currentWave.GetStartingWaypoint().position, // where to spawn the object
                            Quaternion.identity, // need to specify rotation
                            transform); // this will place the enemies under the Enemy Spawner object as a child

                        yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());

                        enemyType = enemyType == 0 ? 1 : 0;
                    }

                    yield return new WaitForSeconds(timeBetweenWaves*waveTimeVariance);
                }
                // decrease the time each loop through of waves to increase difficulty
                waveTimeVariance *= 0.8f;

                waveLoop++;
            } while (isLooping);
        }
        #endregion
    }
}