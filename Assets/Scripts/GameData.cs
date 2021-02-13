using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayShifu
{/// <summary>
/// ScriptableObject to store all general game data
/// </summary>
    [CreateAssetMenu(fileName = "GameData", menuName = "GameData")]

    public class GameData : ScriptableObject
    {
        [Header("Grid Data")]
        [SerializeField]
        private int rows;
        [SerializeField]
        private int columns;
        [SerializeField]
        private float cellsize;

        [Header("food lifespan")]
        [SerializeField]
        private float foodStayTime;

        [Header("Snake Physics Data")]
        [SerializeField]
        private float snakeSpeed;
        [SerializeField]
        private float snakeSteering;
        [SerializeField]
        private float driftForceMagnitude;
        [SerializeField]
        private float turnRateFactor;

        [Header("Steering wheel data")]
        [SerializeField]
        private float maxSteeringAngle;
        [SerializeField]
        private float releaseSpeed;

        /// <summary>
        /// Returns number rows in grid
        /// </summary>
        /// <returns></returns>
        public int GetRows()
        {
            return rows;
        }

        /// <summary>
        /// Returns number columns in grid
        /// </summary>
        /// <returns></returns>
        public int GetColumns()
        {
            return columns;
        }

        /// <summary>
        /// Returns size of each cell in grid
        /// </summary>
        /// <returns></returns>
        public float GetCellSize()
        {
            return cellsize;
        }

        /// <summary>
        /// Returns lifespan of each food after they are spawned 
        /// </summary>
        /// <returns></returns>
        public float GetFoodStayTime()
        {
            return foodStayTime;
        }

        /// <summary>
        /// Returns speed of snake movement
        /// </summary>
        /// <returns></returns>
        public float GetSnakeSpeed()
        {
            return snakeSpeed;
        }

        /// <summary>
        /// Returns max steering angle of steering wheel
        /// </summary>
        /// <returns></returns>
        public float GetMaxSteeringAngle()
        {
            return maxSteeringAngle;
        }

        /// <summary>
        /// Returns speed at which steering wheel sets to default position
        /// </summary>
        /// <returns></returns>
        public float GetReleaseSpeed()
        {
            return releaseSpeed;
        }

        /// <summary>
        /// Returns snake steering speed
        /// </summary>
        /// <returns></returns>
        public float GetSnakeSteering()
        {
            return snakeSteering;
        }

        /// <summary>
        /// Returns snake driftForceMagnitude
        /// <returns></returns>
        public float GetDriftForce()
        {
            return driftForceMagnitude;
        }

        /// <summary>
        /// Returns snake turnRateFactor
        /// <returns></returns>
        public float GetTurnRateFactor()
        {
            return turnRateFactor;
        }
    }
}

