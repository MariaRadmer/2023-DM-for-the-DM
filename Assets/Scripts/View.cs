using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] Controller controller;
    [SerializeField] DungeonTranslater translater;
    [SerializeField] CameraSystem cameraSystem;
    [SerializeField] Tilemap tilemap;
    [SerializeField] TileBase tile;

    int tileMapSize = 10;
    int nbrOfStepsDungeons = 10;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnSizeChanged(string input) {

        try
        {
           
            int result = Int32.Parse(input);

            if (result > 10)
            {
                tileMapSize = result;
            }
            
        }
        catch (FormatException)
        {
            Debug.Log($"Unable to parse '{input}'");
        }

    }


    public void OnStepsChanged(string input)
    { 
        try
        {
            int result = Int32.Parse(input);

            if (result > 10)
            {
                nbrOfStepsDungeons = result;
            }
            
        }
        catch (FormatException)
        {
            Debug.Log($"Unable to parse '{input}'");
        }

    }


    public void OnClickGenerateDungeon()
    {
        
        

        DungeonParams dungeonParams = new DungeonParams(tileMapSize, nbrOfStepsDungeons);

        dungeonParams.DebugPrintDungeonParams();

        int[,] dungeonArr=controller.GenerateNewDungeon(dungeonParams);
        cameraSystem.UpdateParameters(dungeonParams);
        Vector3Int camSatartPos = controller.GetDungeonStartPos();
        
        Vector3Int position = tilemap.WorldToCell(camSatartPos);
        // use the grid insted of tilemap ? grid.WorldToCell(mouseWorldPos);
        Debug.Log("Cam position "+ position);

        cameraSystem.UpdateCameraPosition(position);

        translater.UpdateDungeonLayout(dungeonArr, dungeonParams, tilemap, tile);
    }
}
