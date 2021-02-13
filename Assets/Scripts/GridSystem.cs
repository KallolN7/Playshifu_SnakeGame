using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayShifu
{

    /// <summary>
    /// Creates custom grid. Takes care of grid related functions- insertion and deletion from grid dictionary
    /// </summary>
    public class GridSystem : MonoBehaviour
    {
        [Header("GameData ScriptableObject")]
        [SerializeField]
        private GameData gameData;
        [Header("TileController prefab")]
        [SerializeField]
        private TileController referenceTile;
        [Header("GameObject border")]
        [SerializeField]
        private GameObject border;

        private int totalTileCount;
        private Dictionary<int, GameObject> tileDict = new Dictionary<int, GameObject>();


        #region Private Methods

        /// <summary>
        /// Creating custom grid at Start 
        /// </summary>
        private void Start()
        {
            CreateGrid();
        }


        /// <summary>
        ///  Creating grid based on GameData rows, column and cellSize data 
        /// </summary>
        private void CreateGrid()
        {

            for (int i = 0; i < gameData.GetRows(); i++) // Looping through rows
            {
                for (int j = 0; j < gameData.GetColumns(); j++) // Looping through rows
                {
                    TileController tile = Instantiate(referenceTile, transform); //Instantiating each cell. Parenting them to this gameobject
                    float posX = j * gameData.GetCellSize();
                    float posY = i * -gameData.GetCellSize();
                    tile.transform.position = new Vector2(posX, posY); //Positioning each cell based on row-column combination
                    tile.transform.localScale = new Vector3(gameData.GetCellSize(), gameData.GetCellSize(), gameData.GetCellSize()); // Setting each cell size based on GameData
                    tile.SetTileData(totalTileCount);
                    tileDict.Add(totalTileCount, tile.gameObject); //Adding each cell to tileDict dictionary
                    totalTileCount++;   //Incrementing tileCount
                    tile.gameObject.name = "row" + i + " | column" + j;
                }
            }

            float gridH = gameData.GetRows() * gameData.GetCellSize();  // Calculating total grid height
            float gridW = gameData.GetColumns() * gameData.GetCellSize(); // Calculating total grid width
            float cellsizeFactor = gameData.GetCellSize() / 2;  // Calculating cell size factor
            transform.position = new Vector2((-gridW / 2) + cellsizeFactor, (gridH / 2) - cellsizeFactor);  //Centering grid position
            border.transform.localScale = new Vector3(gridW + 0.5f, gridH + 0.5f, 1); //Setting bounary scale based on grid size. adding 0.5f offset to keep border size bigger than grid
            Config.tileDict = tileDict; //Setting this tileDict to config class tileDict 
            Config.tilesLeft = totalTileCount; //Setting this totalTilesCount to config class tilesLeft 
            //Config.totalTilesCount = totalTileCount; //Setting this totalTilesCount to config class totalTilesCount
        }

        /// <summary>
        /// Removes tile from tileDict dictionary based on Config.tileToRemoveId key
        /// </summary>
        private void RemoveTileFromDictionary()
        {
            tileDict.Remove(Config.tileToRemoveId); //Removing tiles from config tileDict
            //Config.tileDict.Clear();
            //Config.tileDict = tileDict; 
            totalTileCount = tileDict.Count;
            Config.tilesLeft = totalTileCount; //Setting this totalTilesCount to config class tilesLeft 
            EventManager.TriggerEvent(EventID.Event_TileCalculationDone); //Triggering Event_TileCalculationDone event on tiles count calculation done
        }


        #endregion





        #region EventsHandler

        /// <summary>
        /// Subsribing methods to events
        /// </summary>
        private void OnEnable()
        {
            EventManager.AddListener(EventID.Event_OnTileConsumed, EventOnTileConsumed);
        }

        /// <summary>
        /// Un-subsribing methods from events
        /// </summary>
        private void OnDisable()
        {
            EventManager.RemoveListener(EventID.Event_OnTileConsumed, EventOnTileConsumed);
        }

        /// <summary>
        /// Methos subsribed to Event_OnTileConsumed event
        /// </summary>
        /// <param name="obj"></param>
        private void EventOnTileConsumed(object obj)
        {
            RemoveTileFromDictionary();
        }

        #endregion
    }
}


