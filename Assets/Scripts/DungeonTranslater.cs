using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Tilemaps;

public class DungeonTranslater : MonoBehaviour
{

    [SerializeField]
    Tilemap TilemapGround;
    [SerializeField]
    TileBase grasstile;



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

    void readFromTextFile()
    {

        string path = Application.dataPath + "/Dungeons" + "/Dungeon"+ ".txt";

        int[,] dungeonArr = convertToArray(path);

        int width = dungeonArr.GetLength(0);
        int height = dungeonArr.GetLength(1);

        int offsetWidth = -width / 2;
        int offsetHeight = -height / 2;


        TilemapGround.origin = new Vector3Int(offsetWidth, offsetHeight, 0);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (dungeonArr[i,j] == 1)
                {
                    toTilePosition(i+ offsetWidth, j+offsetHeight);
                    
                }
                
            }
        }

       

    }



    void resetBorders(Tilemap map, int width,int height)
    {
        map.origin = new Vector3Int(-width / 2 * borderFactor, -height / 2 * borderFactor, 0);
        map.size = new Vector3Int(width * borderFactor, height * borderFactor);
        map.ResizeBounds();

    }

    int[,] convertToArray(string path)
    {

        StreamReader reader = new StreamReader(path);
        int lineLength = reader.ReadLine().Length;

        int[,] dungeonArr = new int[lineLength,lineLength];
        List<string> stringArr = convertToList(path);

        

        int i = 0;

        foreach(string s in stringArr) {

           
            char[] arr = s.ToCharArray();
            

            for (int j = 0; j < arr.Length; j++)
            {
                if (isInDungeonSize(i, j, lineLength))
                {

                    dungeonArr[i, j] = convertCharToInt(arr[j]);

                }

            }
            i++;
       }


        reader.Close();

        return dungeonArr;
    }


    List<string> convertToList(string path)
    {
        List<string> list = new List<string>();
        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                list.Add(line); 
                
            }
        }
        return list;
    }

    int convertCharToInt(char c)
    {
        int bar;
        if (!int.TryParse(c.ToString(), out bar))
        {
            bar = 0;
        }
        return bar;
    }

    void printSingleArray(char[] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
                print(array[i]);
        }
        
    }

    void printArray(int [,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                print(""+ array[i,j]);

            }
        }

        print("Array print done");
       
    }

    bool isInDungeonSize(int x, int y, int size)
    {
        if ((x < size) && (y < size))
        {
            if ((x >= 0) && (y >= 0))
            {
                return true;
            }
        }

        return false;
    }


    void toTilePosition(int x,int y)
    {

        Vector3Int pos = new Vector3Int(y, x * -1, 0);

        TilemapGround.SetTile(pos, grasstile);


    }

    public void UpdateDungeonLayout(int[,] dungeonArr, DungeonParams dungeonParams)
    {
        TilemapGround.ClearAllTiles();
        int width = dungeonArr.GetLength(0);
        int height = dungeonArr.GetLength(1);

        int offsetWidth = -width / 2;
        int offsetHeight = -height / 2;


        TilemapGround.origin = new Vector3Int(offsetWidth, offsetHeight, 0);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (dungeonArr[i, j] == 1)
                {
                    toTilePosition(i + offsetWidth, j + offsetHeight);

                }

            }
        }

        
    }
}
