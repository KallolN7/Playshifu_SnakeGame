using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PlayShifu
{
    /// <summary>
    ///  Manager class that keeps track of score, food spawn, and tiles remaining
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("GameData ScriptableObject")]
        [SerializeField]
        private GameData gameData;

        [Header("FoodItemController Prefab")]
        [SerializeField]
        private FoodItemController foodPrefab;

        private FoodItemController food;
        private int tilesCount;
        private int score;
        private bool canSpawnFood;



        #region Private methods

        /// <summary>
        /// Resetting Config variables on Start
        /// </summary>
        private void Start()
        {
            Config.ResetConfig();
            canSpawnFood = true;
        }

        /// <summary>
        /// Spawns food prefabs at random non-green tiles 
        /// </summary>
        private void SpawnFood()
        {
            if (canSpawnFood)
            {
                int random = Random.Range(0, Config.tilesLeft);  //Getting random number 

                if (Config.tileDict.Count != 0)  //Config.tileDict.ElementAt(random).Value != null && 
                {
                    food = Instantiate(foodPrefab, Config.tileDict.ElementAt(random).Value.transform);
                    food.SetData(gameData.GetFoodStayTime());
                }
            }
        }

        /// <summary>
        /// decrements the tile count. Then triggers Event_TileCalculationDone event for the tile count data to be shown in UI
        /// Checks for tile count. if count <= 0 then triggers Event_OnGameOver event
        /// </summary>
        public void OnTileRemoved()
        {
            tilesCount = Config.tilesLeft;
            tilesCount--;
            Config.tilesLeft = tilesCount;
            EventManager.TriggerEvent(EventID.Event_TileCalculationDone);
            if (tilesCount <= 0) //Checks for tile count
            {
                EventManager.TriggerEvent(EventID.Event_OnGameOver);
            }
        }


        /// <summary>
        /// increments the score. Then triggers Event_FoodCalculationDone event after score calculation
        /// </summary>
        private void UpdateScore()
        {
            score++;
            Config.currentScore = score;
            EventManager.TriggerEvent(EventID.Event_FoodCalculationDone);
        }

        /// <summary>
        /// Updates timescale based on current timeScale value 
        /// </summary>
        private void UpdateTimeScale()
        {
            if (Time.timeScale == 0)
                Time.timeScale = 1;
            else
                Time.timeScale = 0;
        }

        #endregion


        #region EventsHandler


        /// <summary>
        /// Subsribing methods to events
        /// </summary>
        private void OnEnable()
        {
            EventManager.AddListener(EventID.Event_OnGameStart, EventOnGameStart);
            EventManager.AddListener(EventID.Event_OnGamePaused, EventOnGamePaused);
            EventManager.AddListener(EventID.Event_OnGameResume, EventOnGameResume);
            EventManager.AddListener(EventID.Event_OnGameOver, EventOnGameOver);
            EventManager.AddListener(EventID.Event_OnTileConsumed, EventOnTileConsumed);
            EventManager.AddListener(EventID.Event_FoodTimerUp, EventOnFoodTimerUp);
            EventManager.AddListener(EventID.Event_OnFoodConsumed, EventOnFoodConsumed);
        }

        /// <summary>
        /// Un-subsribing methods from events
        /// </summary>
        private void OnDisable()
        {
            EventManager.RemoveListener(EventID.Event_OnGameStart, EventOnGameStart);
            EventManager.RemoveListener(EventID.Event_OnGamePaused, EventOnGamePaused);
            EventManager.RemoveListener(EventID.Event_OnGameResume, EventOnGameResume);
            EventManager.RemoveListener(EventID.Event_OnGameOver, EventOnGameOver);
            EventManager.RemoveListener(EventID.Event_OnTileConsumed, EventOnTileConsumed);
            EventManager.RemoveListener(EventID.Event_FoodTimerUp, EventOnFoodTimerUp);
            EventManager.RemoveListener(EventID.Event_OnFoodConsumed, EventOnFoodConsumed);
        }

        /// <summary>
        ///  Methos subsribed to Event_OnGameStart event
        /// </summary>
        /// <param name="obj"></param>
        private void EventOnGameStart(object obj)
        {
            SpawnFood();
        }

        /// <summary>
        /// Methos subsribed to Event_OnGamePaused event
        /// </summary>
        /// <param name="obj"></param>
        private void EventOnGamePaused(object obj)
        {
            UpdateTimeScale();
        }

        /// <summary>
        /// Methos subsribed to Event_OnGameResume event
        /// </summary>
        /// <param name="obj"></param>
        private void EventOnGameResume(object obj)
        {
            UpdateTimeScale();
        }

        /// <summary>
        /// Methos subsribed to Event_OnGameOver event
        /// </summary>
        /// <param name="obj"></param>
        private void EventOnGameOver(object obj)
        {
            canSpawnFood = true;
        }

        /// <summary>
        /// Methos subsribed to Event_OnTileConsumed event
        /// </summary>
        /// <param name="obj"></param>
        private void EventOnTileConsumed(object obj)
        {
            OnTileRemoved();
        }

        /// <summary>
        /// Methos subsribed to Event_FoodTimerUp event
        /// </summary>
        /// <param name="obj"></param>
        private void EventOnFoodConsumed(object obj)
        {
            UpdateScore();
            SpawnFood();
        }

        /// <summary>
        /// Methos subsribed to Event_OnFoodConsumed event
        /// </summary>
        /// <param name="obj"></param>
        private void EventOnFoodTimerUp(object obj)
        {
            SpawnFood();
        }

        #endregion
    }
}

