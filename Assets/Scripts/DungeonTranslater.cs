using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Tilemaps;

public class DungeonTranslater : MonoBehaviour
{

    [SerializeField]
    private int borderFactor = 4;



    

    int ran = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
       
        
    }

    // Update is called once per frame
    void Update()
    {


    }




  


    void toTilePosition(Vector3Int pos, Tilemap tilemap, TileBase grasstile)
    {

        tilemap.SetTile(pos, grasstile);
    }

    public void UpdateDungeonLayout(DungeonData dungeonData, DungeonParams dungeonParams, Tilemap tilemap, TileBase grasstile)
    {
        tilemap.ClearAllTiles();
        tilemap.origin = new Vector3Int(0, 0, 0);


        foreach(Vector3Int pos in dungeonData.tilePositions)
        {
            toTilePosition(pos, tilemap, grasstile);
        }



        
    }
}
