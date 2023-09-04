using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DiggerAlgo
{
    
    float ChanceDiggerDir = 5f;
    float ChanceRoom = 5f;

    
 

    // TODO put in dungon params?
    int roomWidthMin = 3;
    int roomWidthMax = 7;
    int roomLengthMin = 3;
    int roomLengthMax = 7;
    



    DIRECTION diggerDir;
    (int, int) diggerPos;
    Dictionary<DIRECTION, (int, int)> dirToXY = new Dictionary<DIRECTION, (int, int)>();

    int[,] dungeonArr;

    int dungeonID = 0;
    int dungeonCol;
    int dungeonRow;

    (int, int) starPos;
    (int, int) endPos;

    public DiggerAlgo() {
        dirToXY.Add(DIRECTION.UP, (1, 0));
        dirToXY.Add(DIRECTION.DOWN, (-1, 0));
        dirToXY.Add(DIRECTION.RIGHT, (0, 1));
        dirToXY.Add(DIRECTION.LEFT, (0, -1));
    }




    // skal laves om til hasmap (Right (0,1))
    enum DIRECTION{
        UP=0,
        DOWN=1,
        RIGHT=2,
        LEFT=3
    }






    public int[,] GenerateDungeon(DungeonParams dungeonParams)
    {
        

        dungeonInit(dungeonParams.tileMapSize);

        diggerPos = (UnityEngine.Random.Range(0, dungeonParams.tileMapSize), UnityEngine.Random.Range(0, dungeonParams.tileMapSize));
        starPos = diggerPos;
        diggerDir = randomValidDirection();


        dungeonGenerate(dungeonParams.nbrOfStepsDungeons);

        return dungeonArr;
    }





    public void dungeonPrintToFile()
    {

        string path = Application.dataPath + "/Dungeons" + "/Dungeon" + dungeonID + ".txt";
        StreamWriter writer = new StreamWriter(path, false);


        for (int i = 0; i < dungeonArr.GetLength(0); i++)
        {
            for (int j = 0; j < dungeonArr.GetLength(1); j++)
            {
                String tmp = dungeonArr[i, j].ToString();
                
                writer.Write(tmp);
            }

           
            writer.Write("\n");

        }

        writer.Close();
        

    }


    void dungeonInit(int tilemapSize)
    {
        

        dungeonArr = new int[tilemapSize, tilemapSize];
        dungeonCol = dungeonArr.GetLength(0);
        dungeonRow = dungeonArr.GetLength(1);

        for (int i = 0; i < dungeonArr.GetLength(0); i++)
        {
            for (int j = 0; j < dungeonArr.GetLength(1); j++)
            {
                
                dungeonArr[i,j] = 0;
            }
        }
    }


    List<DIRECTION> validDirections()
    {
        List<DIRECTION> allDirections = new List<DIRECTION>() { DIRECTION.UP, DIRECTION.DOWN, DIRECTION.RIGHT, DIRECTION.LEFT };
        List<DIRECTION> valid = new List<DIRECTION>();

        foreach (DIRECTION dir in allDirections)
        {
            (int, int) dirXY = dirToXY[dir];
            int newDirX = diggerPos.Item1 + dirXY.Item1;
            int newDirY = diggerPos.Item2 + dirXY.Item2;

            if (isInDungeonSize(newDirX,newDirY))
            {
               
                valid.Add(dir);
                
            }

        }

        
        return valid;
    }

    bool isInDungeonSize(int x,int y)
    {
        if ((x < dungeonCol) && (y < dungeonRow))
        {
            if ((x >= 0) && (y >= 0))
            {
                return true;
            }
        }

        return false;
    }

    bool validDirection(DIRECTION dirrection)
    {
        List<DIRECTION> valid = validDirections();

        foreach (DIRECTION dir in valid)
        {
            if((dir ==dirrection) ) {
                return true; 
            }
        }

        return false;
    }

    DIRECTION randomValidDirection()
    {

        List<DIRECTION> valid = validDirections();

        int random = 0;

        

        if(valid.Count> 1)
        {
            random = UnityEngine.Random.Range(0, valid.Count);
            DIRECTION randomValidDir = valid.ToArray()[random];

            return randomValidDir;
        }
        else
        {
            return valid.ToArray()[0];
        }

        


    }


    void createRoom(int width, int length)
    {

        int halfWidth = (int)Math.Floor(width/2.0);
        int halfLength = (int)Math.Floor(length / 2.0);

        (int, int) startPos = (diggerPos.Item1 - halfWidth, diggerPos.Item2 - halfLength);


        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                (int,int) newPos = (i+ startPos.Item1, j+ startPos.Item2);

                if (isInDungeonSize(newPos.Item1, newPos.Item2))
                {
                    dungeonArr[newPos.Item1, newPos.Item2] = 1;
                }
            }
        }
    }

    void Dig()
    {
        (int,int) dir = dirToXY[diggerDir];
        
        if (validDirection(diggerDir))
        {
            diggerPos = (diggerPos.Item1 + dir.Item1, diggerPos.Item2 + dir.Item2);
            dungeonArr[diggerPos.Item1, diggerPos.Item2] = 1;
           
        }
        else
        {
            diggerDir = randomValidDirection();
        }         
       
    }


    void dungeonGenerate(int steps)
    {

        if(steps <= 0)
        {
            endPos = diggerPos;
            return;
        }
        else
        {
            Dig();
            newDiggerDirection();
            newRoom();

            dungeonGenerate(steps - 1);
        }

        
    }

    void newDiggerDirection()
    {
        int randomNbrDir = UnityEngine.Random.Range(0, 100);
        if (randomNbrDir < ChanceDiggerDir)
        {
            diggerDir = randomValidDirection(); 
            ChanceDiggerDir = 0;
        }
        else
        {
            ChanceDiggerDir += 5;
        }
    }

    void newRoom()
    {
        int randomNbrRoom = UnityEngine.Random.Range(0, 100);
        if (randomNbrRoom < ChanceRoom)
        {
            int roomWidth = UnityEngine.Random.Range(roomWidthMin, roomWidthMax);
            int roomLength = UnityEngine.Random.Range(roomLengthMin, roomLengthMax);

            createRoom(roomWidth, roomLength);
            ChanceRoom = 0;

        }
        else
        {
            ChanceRoom += 5;
        }
    }


}


