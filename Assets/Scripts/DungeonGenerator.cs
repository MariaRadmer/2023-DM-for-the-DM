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



    public Vector3Int GetDungeonStartPos()
    {
       

        return diggerAlgo.getStartPosition();
    }

    public DungeonData GenerateDungeon(DungeonParams dungeonParams)
    {
        int tileMapSize = dungeonParams.tileMapSize; 
        DungeonData dungeonData = diggerAlgo.GenerateDungeon(dungeonParams);



        return dungeonData;
    }
}


