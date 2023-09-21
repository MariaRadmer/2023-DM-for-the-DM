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


    public void SaveDungeon(string fileName, Vector3Int endPos, Tilemap tilemap, string folderPath, List<CustomTile> allTiles)
    {
        BoundsInt bounds = tilemap.cellBounds;
        DungeonData dungeonData = new DungeonData();
        dungeonData.endPosition = endPos;

        for(int x = bounds.min.x; x < bounds.max.x;x++)
        {
            for(int y = bounds.min.y; y < bounds.max.y; y++)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(x,y,0));

                CustomTile customTile = allTiles.Find(t => t.tile == tile);

                if(customTile != null)
                {
                    dungeonData.tiles.Add(customTile.id);
                    dungeonData.tilePositions.Add(new Vector3Int(x,y,0));
                }
            }
        }

        string json = JsonUtility.ToJson(dungeonData,true);
        //string currentTime = System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)");
       
        File.WriteAllText(folderPath + fileName + ".json", json);


    }


    public Vector3Int LoadLevel (string fileName, Tilemap tilemap,string folderPath, List<CustomTile> allTiles)
    {
        string json = File.ReadAllText(folderPath + fileName + ".json");
        DungeonData data = JsonUtility.FromJson<DungeonData>(json);

        tilemap.ClearAllTiles();
        for (int i = 0; i<data.tilePositions.Count; i++)
        {
 
            TileBase tileBase = allTiles.Find(t => t.name == data.tiles[i]).tile;

            tilemap.SetTile(data.tilePositions[i], tileBase);
        }

        return data.endPosition;
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
