using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace PlayShifu
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        private Image pauseButtonImg;
        [SerializeField]
        private Sprite[] pauseButtonSprites;
        [SerializeField]
        private GameObject uiHolder;
        [SerializeField]
        private TextMeshProUGUI scoreText;
        [SerializeField]
        private GameObject[] gameStateObjects;     //0= Play state, 1= GameOver state

        private bool isPaused;


        #region OnClickButtonMethods

        /// <summary>
        /// Attached to Play Button. Triggers Event_OnGameStart event on click
        /// </summary>
        public void Play()
        {
            EventManager.TriggerEvent(EventID.Event_OnGameStart);
            HideUI();
        }

        /// <summary>
        /// Attached to Pause/Unpause Button. Triggers Event_OnGamePaused/Event_OnGameResume event on click
        /// </summary>
        public void Pause()
        {
            if (isPaused)
            {
                isPaused = false;
                pauseButtonImg.sprite = pauseButtonSprites[0];
                EventManager.TriggerEvent(EventID.Event_OnGameResume);
            }
            else
            {
                isPaused = true;
                pauseButtonImg.sprite = pauseButtonSprites[1];
                EventManager.TriggerEvent(EventID.Event_OnGamePaused);
            }
        }

        /// <summary>
        /// Attached to Retry Button. Reloads scene on click
        /// </summary>
        public void Retry()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// hides Play UI state
        /// </summary>
        private void HideUI()
        {
            uiHolder.SetActive(false);
            gameStateObjects[0].SetActive(false);
        }

        /// <summary>
        /// Show GameOver UI state
        /// </summary>
        private void ShowGameOverUI()
        {
            uiHolder.SetActive(true);
            gameStateObjects[1].SetActive(true);
            scoreText.text = Config.currentScore.ToString();
        }


        #endregion



        #region EventsHandler

        /// <summary>
        /// Subsribing methods to events
        /// </summary>
        private void OnEnable()
        {
            EventManager.AddListener(EventID.Event_OnGameOver, EventOnGameOver);
        }

        /// <summary>
        /// Un-subsribing methods from events
        /// </summary>
        private void OnDisable()
        {
            EventManager.RemoveListener(EventID.Event_OnGameOver, EventOnGameOver);
        }

        /// <summary>
        /// Methos subsribed to Event_OnGameOver event
        /// </summary>
        /// <param name="obj"></param>
        private void EventOnGameOver(object obj)
        {
            ShowGameOverUI();
        }

        #endregion

    }
}

