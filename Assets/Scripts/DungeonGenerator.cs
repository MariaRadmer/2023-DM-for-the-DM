using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{

    DiggerAlgo diggerAlgo = new DiggerAlgo();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int[,] GenerateDungeon(DungeonParams dungeonParams)
    {
        int tileMapSize = dungeonParams.tileMapSize; 
        int[,] dungeonArr = diggerAlgo.GenerateDungeon(dungeonParams);



        return dungeonArr;
    }
}


