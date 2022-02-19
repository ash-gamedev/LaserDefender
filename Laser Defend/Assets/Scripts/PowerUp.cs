using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PowerUp : MonoBehaviour
    {
        [SerializeField] GameObject powerUp;

        bool powerUpActive = false;

        AudioPlayer audioPlayer;
        Animator animator;

        PowerUpManager powerUpManager;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            audioPlayer = FindObjectOfType<AudioPlayer>();
        }

        private void Start()
        {
            powerUpManager = FindObjectOfType<PowerUpManager>();

            // play sound effect on appear
            audioPlayer.PlaySoundEffect(Enum.Sounds.PowerUpAppear);
        }

        public bool IsPowerUpActive()
        {
            return powerUpActive;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && powerUpActive == false)
            {
                powerUpActive = true;

                // hide sprite + play sound effect
                audioPlayer.PlaySoundEffect(Enum.Sounds.PowerUpGained);
                ChangeAnimationState("PowerUp_Dissapear");

                if (powerUp.CompareTag("Shield"))
                    powerUpManager.AddShieldPowerUp();
                else
                    powerUpManager.AddMisslePowerUp();

                Destroy(gameObject);
            }
        }

        void ChangeAnimationState(string animationName)
        {
            Debug.Log("Playing: " + animationName);

            //play the animation
            animator.Play(animationName);
        }

    }
}