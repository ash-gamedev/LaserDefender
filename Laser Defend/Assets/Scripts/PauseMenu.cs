using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool GameIsPaused = false;
        public GameObject pauseMenuUi;
        [SerializeField] List<GameObject> panels;

        private void Start()
        {
            PauseMenu.GameIsPaused = false;
        }

        private void OnPauseButtonPress(InputValue value)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        public void Resume()
        {
            pauseMenuUi.SetActive(false);
            DisplayPanels(true);
            Time.timeScale = 1f; //normal rate
            GameIsPaused = false;
        }

        void Pause()
        {
            DisplayPanels(false);
            pauseMenuUi.SetActive(true);
            Time.timeScale = 0f; //freezes fame
            GameIsPaused = true;
        }

        void DisplayPanels(bool displayPanel)
        {
            foreach(GameObject panel in panels)
            {
                panel.SetActive(displayPanel);
            }
        }
    }
}