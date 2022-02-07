using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI healthText;

        // private variables
        ScoreKeeper scoreKeeper;
        Health playerHealth;

        void Awake()
        {
            // only want one game session
            int numGameSessions = FindObjectsOfType<GameSession>().Length;
            if (numGameSessions > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }

            // find score keeper
            scoreKeeper = FindObjectOfType<ScoreKeeper>();

            // find player health
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            this.scoreText.text = "Score: " + scoreKeeper.GetScore().ToString();
            this.healthText.text = "Health: " + playerHealth.GetHealth().ToString();
        }
    }
}