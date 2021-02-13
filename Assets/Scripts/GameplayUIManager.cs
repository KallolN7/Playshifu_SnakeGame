using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PlayShifu
{
    /// <summary>
    /// Controls all the gameply UI functionalities
    /// </summary>
    public class GameplayUIManager : MonoBehaviour
    {
        [Header("score TextMeshProText")]
        [SerializeField]
        private TextMeshProUGUI scoreText;
        [Header("tilesLeft TextMeshProText")]
        [SerializeField]
        private TextMeshProUGUI tilesLeft;
        [Header("commentText TextMeshProText")]
        [SerializeField]
        private TextMeshProUGUI commentText;



        #region Private Method


        /// <summary>
        /// Update score UI text  based on current score
        /// </summary>
        private void UpdateScore()
        {
            scoreText.text = Config.currentScore.ToString();
        }

        /// <summary>
        /// Update tileCount UI text  based on current tileCount
        /// </summary>
        private void UpdateTilesCount()
        {
            tilesLeft.text = Config.tilesLeft.ToString();
        }

        /// <summary>
        /// Resets UI for new game session
        /// </summary>
        private void ResetUI()
        {
            scoreText.text = "00";
            tilesLeft.text = Config.tilesLeft.ToString();
            commentText.text = "";
        }

        /// <summary>
        /// Shows comment text on food consumed. Then destroys after "interval" seconds
        /// /// </summary>
        private void ShowComment()
        {
            CancelInvoke();
            commentText.text = "Yum!";
            Invoke(nameof(ClearComment), 2);
        }

        /// <summary>
        /// Clearing comment text field
        /// </summary>
        private void ClearComment()
        {
            commentText.text = "";
        }

        #endregion




        #region EventsHandler

        /// <summary>
        /// Subsribing methods to events
        /// </summary>
        private void OnEnable()
        {
            EventManager.AddListener(EventID.Event_OnGameStart, EventOnGameStart);
            EventManager.AddListener(EventID.Event_TileCalculationDone, EventOnTileCalculationDone);
            EventManager.AddListener(EventID.Event_FoodCalculationDone, EventOnFoodCalculationDone);
        }

        /// <summary>
        /// Un-subsribing methods from events
        /// </summary>
        private void OnDisable()
        {
            EventManager.RemoveListener(EventID.Event_OnGameStart, EventOnGameStart);
            EventManager.RemoveListener(EventID.Event_TileCalculationDone, EventOnTileCalculationDone);
            EventManager.RemoveListener(EventID.Event_FoodCalculationDone, EventOnFoodCalculationDone);
        }

        /// <summary>
        ///  Methos subsribed to Event_OnGameStart event
        /// </summary>
        /// <param name="obj"></param>
        private void EventOnGameStart(object obj)
        {
            ResetUI();
        }

        /// <summary>
        ///  Methos subsribed to Event_TileCalculationDone event
        /// </summary>
        /// <param name="obj"></param>
        private void EventOnTileCalculationDone(object obj)
        {
            UpdateTilesCount();
        }

        /// <summary>
        ///  Methos subsribed to Event_FoodCalculationDone event
        /// </summary>
        /// <param name="obj"></param>
        private void EventOnFoodCalculationDone(object obj)
        {
            UpdateScore();
            ShowComment();
        }

        #endregion
    }
}

