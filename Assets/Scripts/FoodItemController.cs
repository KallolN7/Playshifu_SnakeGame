using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayShifu
{
    /// <summary>
    /// Controls the functionalities of individual food items
    /// </summary>
    public class FoodItemController : MonoBehaviour
    {

        /// <summary>
        /// Setting lifespan time of this FoodItemController 
        /// </summary>
        /// <param name="time"></param>
        public void SetData(float time)
        {
            Invoke(nameof(DestroyFood), time);
        }

        /// <summary>
        /// Checking the collision with "Snake" tagged object
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "SnakeHead")
            {
                CancelInvoke();
                EventManager.TriggerEvent(EventID.Event_OnFoodConsumed);  //Triggering Event_OnFoodConsumed event on collision detection
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        ///  Destroying this gameobject
        /// </summary>
        private void DestroyFood()
        {
            EventManager.TriggerEvent(EventID.Event_FoodTimerUp);
            Destroy(this.gameObject);
        }


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
            Destroy(this.gameObject);
        }


        #endregion
    }
}

