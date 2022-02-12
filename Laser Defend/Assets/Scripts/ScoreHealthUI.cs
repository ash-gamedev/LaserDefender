using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScoreHealthUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] Slider healthSlider;

        // private variables
        ScoreKeeper scoreKeeper;
        Health playerHealth;

        void Awake()
        {
            // find score keeper
            scoreKeeper = FindObjectOfType<ScoreKeeper>();

            // find player health
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

            healthSlider.maxValue = playerHealth.GetHealth();
            healthSlider.value = healthSlider.maxValue;
        }

        private void Update()
        {
            UpdateScoreText();
            UpdateHealthSlider();
        }

        public void UpdateScoreText()
        {
            this.scoreText.text = scoreKeeper.GetScore().ToString("D9");
        }

        public void UpdateHealthSlider()
        {
            this.healthSlider.value = playerHealth.GetHealth();
        }
    }
}