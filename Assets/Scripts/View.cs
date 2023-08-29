using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] Controller controller;
    [SerializeField] DungeonTranslater translater;

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
        
        Debug.Log("Generate in view!");
        int[,] dungeonArr=controller.GenerateNewDungeon();
        translater.UpdateDungeonLayout(dungeonArr);
    }
}
