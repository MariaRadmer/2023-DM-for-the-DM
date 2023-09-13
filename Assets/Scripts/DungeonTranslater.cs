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




  


    void toTilePosition(int x,int y, Tilemap tilemap, TileBase grasstile)
    {

        //Vector3Int pos = new Vector3Int(y, x * -1, 0);
        Vector3Int pos = new Vector3Int(x,y,0);

        tilemap.SetTile(pos, grasstile);
    }

    public void UpdateDungeonLayout(int[,] dungeonArr, DungeonParams dungeonParams, Tilemap tilemap, TileBase grasstile)
    {
        tilemap.ClearAllTiles();
        int width = dungeonArr.GetLength(0);
        int height = dungeonArr.GetLength(1);


        tilemap.origin = new Vector3Int(0, 0, 0);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (dungeonArr[i, j] == 1)
                {
                    toTilePosition(i, j, tilemap, grasstile);

                }

            }
        }

        
    }
}
