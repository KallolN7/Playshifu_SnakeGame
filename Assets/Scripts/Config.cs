using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayShifu
{
    /// <summary>
    ///  Static clas that acts as a bridge between classes, exchanging data. 
    /// </summary>
    public static class Config
    {
        public static int currentScore;
        public static int tilesLeft;
        public static int tileToRemoveId;
        public static float steeringDirection;
        public static Dictionary<int, GameObject> tileDict = new Dictionary<int, GameObject>();

        public static void ResetConfig()
        {
            currentScore = 0;
            tilesLeft = 0;
            tileToRemoveId = 0;
            steeringDirection = 0;
            tileDict.Clear();
        }

    }
}

