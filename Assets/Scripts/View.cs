using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] Controller controller;
    [SerializeField] DungeonTranslater translater;

    int tileMapSize = 10;
    int nbrOfStepsDungeons = 10;

    MouseState mouseState = MouseState.EDIT;

    enum MouseState
    {
        ERASE,
        EDIT,
    }




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
        
        translater.UpdateDungeonLayout(dungeonArr, dungeonParams);
    }
}
