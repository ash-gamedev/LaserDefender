using System.Collections;
using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    public class UIGameOver : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;
        ScoreKeeper scoreKeeper;

        private void Awake()
        {
            scoreKeeper = FindObjectOfType<ScoreKeeper>();
        }
        void Start()
        {
            scoreText.text = "Score: " + scoreKeeper.GetScore().ToString("D8");
            scoreKeeper.ResetScore();
        }
    }
}