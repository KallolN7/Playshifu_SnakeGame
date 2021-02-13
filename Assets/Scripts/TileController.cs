using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayShifu
{
    /// <summary>
    /// Detects the collision with Snake gameobject  and triggers Event_OnTileConsumed event for individual tiles
    /// </summary>
    public class TileController : MonoBehaviour
    {
        [Header("BoxCollider2D boxCollider")]
        [SerializeField]
        private BoxCollider2D boxCollider;
        [Header("SpriteRenderer spriteRenderer")]
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [Header("Sprite  greenSprite")]
        [SerializeField]
        private Sprite greenSprite;

        private int tileID;
        private bool isTileConsumed;
        private bool isGAmeOn;

        #region Public Methods

        /// <summary>
        /// Sets tileId data on tile spawn
        /// </summary>
        /// <param name="id"></param>
        public void SetTileData(int id)
        {
            tileID = id;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Detects the collision with Snake gameobject  and triggers Event_OnTileConsumed event
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Snake" && !isTileConsumed && isGAmeOn)
            {
                isTileConsumed = true;
                spriteRenderer.sprite = greenSprite;
                Config.tileToRemoveId = tileID;
                EventManager.TriggerEvent(EventID.Event_OnTileConsumed);
            }
        }

        #endregion




        #region EventsHandler


        /// <summary>
        /// Subsribing methods to events
        /// </summary>
        private void OnEnable()
        {
            EventManager.AddListener(EventID.Event_OnGameStart, EventOnGameStart);
            EventManager.AddListener(EventID.Event_OnGameOver, EventOnGameOver);
        }

        /// <summary>
        /// Un-subsribing methods from events
        /// </summary>
        private void OnDisable()
        {
            EventManager.RemoveListener(EventID.Event_OnGameStart, EventOnGameStart);
            EventManager.RemoveListener(EventID.Event_OnGameOver, EventOnGameOver);
        }

        /// <summary>
        ///  Methos subsribed to Event_OnGameStart event. Setting isGameOn= true on GameStart
        /// </summary>
        /// <param name="obj"></param>
        private void EventOnGameStart(object obj)
        {
            isGAmeOn = true;
        }


        /// <summary>
        /// Methos subsribed to Event_OnGameOver event. Setting isGameOn= false on GameOver
        /// </summary>
        /// <param name="obj"></param>
        private void EventOnGameOver(object obj)
        {
            isGAmeOn = false;
        }

        #endregion
    }
}

