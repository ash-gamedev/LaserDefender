using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ScoreKeeper : MonoBehaviour
    {
        int score = 0;

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
    }
}