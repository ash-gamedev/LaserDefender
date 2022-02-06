using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PathFinder : MonoBehaviour
    {
        [SerializeField] WaveConfigSO waveConfig;
        List<Transform> waypoints;
        int waypointIndex = 0;

        void Start()
        {
            waypoints = waveConfig.GetWaypoints();

            // get first waypoint and set position
            transform.position = waypoints[waypointIndex].position;
        }

        void Update()
        {
            FollowPath();
        }

        /// <summary>
        /// Follow path moves (transform) closer to the next waypoint in the list
        /// </summary>
        void FollowPath()
        {
            // if we haven't reached the end of the path yet
            if (waypointIndex < waypoints.Count)
            {
                Vector3 targetPosition = waypoints[waypointIndex].position;

                // multiply by delta time to be framerate independent
                float delta = waveConfig.GetMoveSpeed() * Time.deltaTime; 

                // move from current position towards target position at speed delta
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);

                // if current position is the target position, increment waypoint index to target next position
                if(transform.position == targetPosition)
                {
                    waypointIndex++;
                }
            }
            // if reaches the end of path, destroy
            else
            {
                Destroy(gameObject);
            }
        }
    }
}