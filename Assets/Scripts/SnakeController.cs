using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayShifu
{
    /// <summary>
    /// Controls the movement of snake based on steering wheel input
    /// </summary>
    public class SnakeController : MonoBehaviour
    {
        [Header("GameData ScriptableObject")]
        [SerializeField]
        private GameData gameData;
        [Header("Rigidbody2D rb")]
        [SerializeField]
        private Rigidbody2D rb;
        [Header("Transform headTransform")]
        [SerializeField]
        private Transform headTransform;

        Quaternion currentRotation;
        Vector3 currentEulerAngles;
        private bool canMove = false;


        /// <summary>
        ///  Physics related calculations done in FixedUpdate
        /// </summary>
        void FixedUpdate()
        {
            if (!canMove) //Checing if game is On or not
                return;

            Vector2 speed = transform.up * gameData.GetSnakeSpeed() * Time.deltaTime;
            rb.AddForce(speed);


            float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));

            currentEulerAngles = new Vector3(headTransform.rotation.x, headTransform.rotation.y, direction);

            //moving the value of the Vector3 into Quanternion.eulerAngle format
            currentRotation.eulerAngles = currentEulerAngles;

            //apply the Quaternion.eulerAngles change to the gameObject
            headTransform.rotation = currentRotation;


            if (direction >= 0.0f)
            {
                rb.rotation += Config.steeringDirection * gameData.GetSnakeSteering() * (rb.velocity.magnitude / gameData.GetTurnRateFactor());
            }
            else
            {
                rb.rotation -= Config.steeringDirection * gameData.GetSnakeSteering() * (rb.velocity.magnitude / gameData.GetTurnRateFactor());
            }

            Vector2 forward = new Vector2(0.0f, 0.5f);
            float steeringRightAngle;
            if (rb.angularVelocity > 0)
            {
                steeringRightAngle = -90;
            }
            else
            {
                steeringRightAngle = 90;
            }

            // Find a Vector2 that is 90 degrees relative to the local forward direction 
            Vector2 rightAngleFromForward = Quaternion.AngleAxis(steeringRightAngle, Vector3.forward) * forward;

            // Calculate an opposite force to the drift and apply this to generate sideways traction
            float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(rightAngleFromForward.normalized));

            // Calculate an opposite force to the drift and apply this to generate sideways traction 
            Vector2 relativeForce = (rightAngleFromForward.normalized * -1.0f) * (driftForce * gameData.GetDriftForce());

            rb.AddForce(rb.GetRelativeVector(relativeForce));
        }
       

        #region EventsHandler

        /// <summary>
        /// Subsribing methods to events
        /// </summary>
        private void OnEnable()
        {
            EventManager.AddListener(EventID.Event_OnGameStart, EventOnGameStart);
        }

        /// <summary>
        /// Un-subsribing methods from events
        /// </summary>
        private void OnDisable()
        {
            EventManager.RemoveListener(EventID.Event_OnGameStart, EventOnGameStart);
        }

        /// <summary>
        ///  Methos subsribed to Event_OnGameStart event
        /// </summary>
        /// <param name="obj"></param>
        private void EventOnGameStart(object obj)
        {
            canMove = true;
        }

        #endregion

    }
}

