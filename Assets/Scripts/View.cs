using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] Controller controller;
    [SerializeField] DungeonTranslater translater;

    [SerializeField] int tileMapSize = 32;
    [SerializeField] int nbrOfStepsDungeons = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickGenerateDungeon()
    {
        
        

        DungeonParams dungeonParams = new DungeonParams(tileMapSize, nbrOfStepsDungeons);

        int[,] dungeonArr=controller.GenerateNewDungeon(dungeonParams);
        
        translater.UpdateDungeonLayout(dungeonArr, dungeonParams);
    }
}
