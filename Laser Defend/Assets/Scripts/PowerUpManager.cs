using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class PowerUpManager : MonoBehaviour
    {
        [SerializeField] float duration = 10f; // 10 seconds
        [SerializeField] GameObject shieldPowerUp;
        [SerializeField] GameObject misslePowerUp;
        [SerializeField] TextMeshProUGUI textShieldCount;
        [SerializeField] TextMeshProUGUI textMissleCount;
        [SerializeField] Image shieldImage;
        [SerializeField] Image missleImage;

        bool isPowerUpActive = false;
        int shieldPowerUpsCount = 0;
        int misslePowerUpsCount = 0;

        AudioPlayer audioPlayer;
        SpriteRenderer powerUpSpriteRenderer;
        GameObject powerUpInstance;

        Color transparentShieldColor;
        Color transparentMissleColor;

        private void Awake()
        {
            audioPlayer = FindObjectOfType<AudioPlayer>();
        }

        private void Start()
        {
            transparentShieldColor = shieldImage.color;
            transparentMissleColor = missleImage.color;

            UpdateUI();
        }

        #region public functions 
        public void AddShieldPowerUp()
        {
            shieldPowerUpsCount++;
            UpdateUI();
        }

        public void AddMisslePowerUp()
        {
            misslePowerUpsCount++;
            UpdateUI();
        }
        
        public int GetShieldPowerUpCount()
        {
            return shieldPowerUpsCount;
        }
        public int GetMisslePowerUpCount()
        {
            return misslePowerUpsCount;
        }
        #endregion

        #region private functions
        void UpdateUI()
        {
            // text
            textShieldCount.text = shieldPowerUpsCount == 0 ? "" : shieldPowerUpsCount.ToString();
            textMissleCount.text = misslePowerUpsCount == 0 ? "" : misslePowerUpsCount.ToString();

            // images
            shieldImage.color = shieldPowerUpsCount == 0 ? transparentShieldColor : new Color(shieldImage.color.r, shieldImage.color.g, shieldImage.color.b, 1f);
            missleImage.color = misslePowerUpsCount == 0 ? transparentMissleColor : new Color(missleImage.color.r, missleImage.color.g, missleImage.color.b, 1f);
        }
        void OnActivateShieldPowerUp(InputValue value)
        {
            if (shieldPowerUpsCount > 0 && !isPowerUpActive)
            {
                shieldPowerUpsCount--;
                UpdateUI();
                ActivatePowerUp(shieldPowerUp);
            }
        }

        void OnActivateMisslePowerUp(InputValue value)
        {
            if (misslePowerUpsCount > 0 && !isPowerUpActive)
            {
                misslePowerUpsCount--;
                UpdateUI();
                ActivatePowerUp(misslePowerUp);
            }
        }

        void ActivatePowerUp(GameObject powerUp)
        {
            isPowerUpActive = true;

            // play sound
            audioPlayer.PlaySoundEffect(Enum.Sounds.PowerUpActivated);

            // instantiate object
            powerUpInstance = Instantiate(powerUp,  // what object to instantiate
                    Camera.main.transform.position, // where to spawn the object
                    Quaternion.identity); // need to specify rotation

            // get sprite
            powerUpSpriteRenderer = powerUpInstance.GetComponent<SpriteRenderer>();

            // wait to destroy
            StartCoroutine(WaitAndDestroy(powerUp));
        }
        
        IEnumerator WaitAndDestroy(GameObject powerUp)
        {
            // wait fraction of duration, then flash sprite
            yield return new WaitForSeconds(duration * 0.8f);

            // flash before power down
            float maxTime = duration * 0.2f;
            float currentTime = 0f;

            while (currentTime < maxTime)
            {
                powerUpSpriteRenderer.enabled = false;
                yield return new WaitForSeconds(.1f);
                powerUpSpriteRenderer.enabled = true;
                yield return new WaitForSeconds(.1f);
                currentTime += 0.2f;
            }

            // play sound effect on powerup debuff
            audioPlayer.PlaySoundEffect(Enum.Sounds.PowerUpLost);

            isPowerUpActive = false;
            Destroy(powerUpInstance);
        }

        #endregion
    }
}