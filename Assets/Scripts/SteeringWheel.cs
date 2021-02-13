using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayShifu
{
    /// <summary>
    /// Controls the steering wheel UI. Returns the direction based on steering angle
    /// </summary>
    public class SteeringWheel : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [Header("GameData ScriptableObject")]
        [SerializeField]
        private GameData gameData;
        [Header("RectTransform Wheel")]
        [SerializeField]
        private RectTransform Wheel;

        private float WheelAngle = 0f;
        private float LastWheelAngle = 0f;
        private Vector2 center;
        private float OutPut;
        private bool Wheelbeingheld = false;

        #region Private Methods

        void Update()
        {
            if (!Wheelbeingheld && WheelAngle != 0f)   //Reverting steering wheel back to default position once input is removed
            {
                float DeltaAngle = gameData.GetReleaseSpeed() * Time.deltaTime;    //Calculating angle the wheel has to rotate back
                if (Mathf.Abs(DeltaAngle) > Mathf.Abs(WheelAngle))
                    WheelAngle = 0f;
                else if (WheelAngle > 0f)
                    WheelAngle -= DeltaAngle;
                else
                    WheelAngle += DeltaAngle;
            }
            Wheel.localEulerAngles = new Vector3(0, 0, -gameData.GetMaxSteeringAngle() * OutPut);
            OutPut = WheelAngle / gameData.GetMaxSteeringAngle();  // Calculating output of wheel rotaion in terms of direction

            Config.steeringDirection = -OutPut;  //Settings snakeController steering direction as output angle
        }

        #endregion

        #region Input Methods

        /// <summary>
        /// Registers PointerEvent data On Pointer Down
        /// </summary>
        /// <param name="data"></param>
        public void OnPointerDown(PointerEventData data)
        {
            Wheelbeingheld = true;
            center = RectTransformUtility.WorldToScreenPoint(data.pressEventCamera, Wheel.position);   //calculating centre of wheel based on WorldToScreenPoint
            LastWheelAngle = Vector2.Angle(Vector2.up, data.position - center);
        }

        /// <summary>
        /// Registers PointerEvent data On Pointer Drag
        /// </summary>
        /// <param name="data"></param>
        public void OnDrag(PointerEventData data)
        {
            float NewAngle = Vector2.Angle(Vector2.up, data.position - center);
            if ((data.position - center).sqrMagnitude >= gameData.GetMaxSteeringAngle()*2)   //Checking the squareMagnitude of vectors data.position and centre
            {
                if (data.position.x > center.x)
                    WheelAngle += NewAngle - LastWheelAngle;
                else
                    WheelAngle -= NewAngle - LastWheelAngle;
            }
            WheelAngle = Mathf.Clamp(WheelAngle, -gameData.GetMaxSteeringAngle(), gameData.GetMaxSteeringAngle());                  //Setting the max wheel rotaion to be same as an actual car. maxSteerAngle = 540= 1.5 turns
            LastWheelAngle = NewAngle;
        }

        /// <summary>
        /// Registers PointerEvent data On Pointer Up
        /// </summary>
        /// <param name="data"></param>
        public void OnPointerUp(PointerEventData data)
        {
            OnDrag(data);   //Registering LastWheelAngle onPointerUp
            Wheelbeingheld = false;
        }

        #endregion


    }
}

