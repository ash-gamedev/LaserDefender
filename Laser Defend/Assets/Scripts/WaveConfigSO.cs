using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
    public class WaveConfigSO : ScriptableObject
    {
        // Serializable variables
        [SerializeField] List<GameObject> enemyPrefabs;
        [SerializeField] Transform pathPrefab;
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float timeBetweenEnemySpawns = 1f;
        [SerializeField] float spawnTimeVariance = 0f;
        [SerializeField] float minimumSpawnTime = 0.2f;

        #region public functions
        /// <summary>
        /// Get the starting waypoint position of the path prefab
        /// </summary>
        /// <returns>Transform - the position of the starting waypoint</returns>
        public Transform GetStartingWaypoint()
        {
            return pathPrefab.GetChild(0);
        }

        /// <summary>
        /// Get a list of all the waypoints in the prefab path
        /// </summary>
        /// <returns>A list of transform (positions) of the points in the prefav path</returns>
        public List<Transform> GetWaypoints()
        {
            List<Transform> waypoints = new List<Transform>();
            foreach(Transform child in pathPrefab)
            {
                waypoints.Add(child);
            }

            return waypoints;
        }

        /// <summary>
        /// Get the set move speed of the enemy
        /// </summary>
        /// <returns>The set move speed</returns>
        public float GetMoveSpeed()
        {
            return moveSpeed;
        }

        /// <summary>
        /// Get the number of enemies is the list.
        /// </summary>
        /// <returns>The number of enemies in the wave</returns>
        public int GetEnemyCount()
        {
            return enemyPrefabs.Count;
        }

        /// <summary>
        /// Get the enemy at the given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The enemy game object at index</returns>
        public GameObject GetEnemyPrefab(int index)
        {
            return enemyPrefabs[index];
        }

        /// <summary>
        /// Get a random spawn time for enemies. 
        /// </summary>
        /// <returns>A float with the spawntime</returns>
        public float GetRandomSpawnTime()
        {
            float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance,
                                            timeBetweenEnemySpawns + spawnTimeVariance);

            return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
        }
        #endregion
    }
}