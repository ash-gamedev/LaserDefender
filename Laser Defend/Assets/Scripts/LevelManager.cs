﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] float sceneLoadDelay = 2f;
        public void LoadMainMenu()
        {
            // in case returning after pause
            Time.timeScale = 1f;
            FindObjectOfType<ScoreKeeper>()?.ResetScore();

            SceneManager.LoadScene("MainMenu");
        }
        public void LoadGame()
        {
            SceneManager.LoadScene("Game");
        }

        public void LoadControls()
        {
            SceneManager.LoadScene("Controls");
        }

        public void LoadGameOver()
        {
            StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay));
        }
        public void LoadCredits()
        {
            SceneManager.LoadScene("Credits");
        }
        public void QuitGame()
        {
            Debug.Log("Quitting");
            Application.Quit();
        }

        IEnumerator WaitAndLoad(string sceneName, float delay)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(sceneName);
        }
    }
}