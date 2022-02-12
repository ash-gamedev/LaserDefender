using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ScoreKeeper : MonoBehaviour
    {
        int score = 0;

        static ScoreKeeper instance;

        #region Awake

        private void Awake()
        {
            ManageSingleton();
        }
        #endregion

        #region public functions

        public int GetScore()
        {
            return score;
        }

        public void AddToScore(int points)
        {
            score += points;
            Mathf.Clamp(score, 0, int.MaxValue);
        }

        public void ResetScore()
        {
            score = 0;
        }

        #endregion

        #region private functions
        void ManageSingleton()
        {
            if (instance != null)
            {
                // need to disable this so other objects don't try to access
                gameObject.SetActive(false);

                // now destroy
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        #endregion
    }
}