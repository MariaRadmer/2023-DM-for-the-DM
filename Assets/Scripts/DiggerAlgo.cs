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
    Vector3Int diggerPos;
    Dictionary<DIRECTION, Vector3Int> dirToXY = new Dictionary<DIRECTION, Vector3Int>();
    
    int tileMapSize = 0;

    
    Vector3Int endPos;

    

    public DiggerAlgo() {
        dirToXY.Add(DIRECTION.UP, new Vector3Int(1, 0,0));
        dirToXY.Add(DIRECTION.DOWN, new Vector3Int(-1, 0,0));
        dirToXY.Add(DIRECTION.RIGHT, new Vector3Int(0, 1,0));
        dirToXY.Add(DIRECTION.LEFT, new Vector3Int(0, -1,0));
    }

    public Vector3Int getStartPosition()
    {
        Debug.Log("End pos " + endPos);
        return endPos;
    }


    // skal laves om til hasmap (Right (0,1))
    enum DIRECTION{
        UP=0,
        DOWN=1,
        RIGHT=2,
        LEFT=3
    }






    public DungeonData GenerateDungeon(DungeonParams dungeonParams)
    {
        

        DungeonData dungeonData = new DungeonData();

        diggerPos =new Vector3Int (UnityEngine.Random.Range(0, dungeonParams.tileMapSize), UnityEngine.Random.Range(0, dungeonParams.tileMapSize),0);
        tileMapSize = dungeonParams.tileMapSize;
        diggerDir = randomValidDirection();


        dungeonGenerate(dungeonParams.nbrOfStepsDungeons, dungeonData);

        return dungeonData;
    }


    DIRECTION randomValidDirection()
    {

        List<DIRECTION> valid = validDirections();

        int random = 0;



        if (valid.Count > 1)
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


    List<DIRECTION> validDirections()
    {
        List<DIRECTION> allDirections = new List<DIRECTION>() { DIRECTION.UP, DIRECTION.DOWN, DIRECTION.RIGHT, DIRECTION.LEFT };
        List<DIRECTION> valid = new List<DIRECTION>();

        foreach (DIRECTION dir in allDirections)
        {
            Vector3Int dirXY = dirToXY[dir];
            Vector3Int newDirection = diggerPos + dirXY;
            

            if (isInDungeonSize(newDirection))
            {
                valid.Add(dir);
            }
        }
        return valid;
    }

    bool isInDungeonSize(Vector3Int pos)
    {
        if ((pos.x < tileMapSize) && (pos.y < tileMapSize))
        {
            if ((pos.x >= 0) && (pos.y >= 0))
            {
                return true;
            }
        }

        return false;
    }

    void dungeonGenerate(int steps, DungeonData dungeonData)
    {

        if (steps <= 0)
        {
            dungeonData.endPosition = diggerPos;
            endPos = diggerPos;
            return;
        }
        else
        {
            Dig(dungeonData);
            newDiggerDirection();
            newRoom(dungeonData);

            dungeonGenerate(steps - 1, dungeonData);
        }
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



    void createRoom(int width, int length,DungeonData dungeonData)
    {

        int halfWidth = (int)Math.Floor(width/2.0);
        int halfLength = (int)Math.Floor(length / 2.0);

        Vector3Int startPos = new Vector3Int (diggerPos.x - halfWidth, diggerPos.y - halfLength);


        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                Vector3Int newPos = new Vector3Int (i+ startPos.x, j+ startPos.y,0);

                if (isInDungeonSize(newPos))
                {
                    dungeonData.tilePositions.Add (newPos);
                   
                    
                }
            }
        }
    }

    void Dig(DungeonData dungeonData)
    {
        Vector3Int dir = dirToXY[diggerDir];
        
        if (validDirection(diggerDir))
        {
            diggerPos = new Vector3Int(diggerPos.x + dir.x, diggerPos.y + dir.y,0);
            //dungeonArr[diggerPos.Item1, diggerPos.Item2] = 1;

            dungeonData.tilePositions.Add(diggerPos);
        }
        else
        {
            diggerDir = randomValidDirection();
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

    void newRoom(DungeonData dungeonData)
    {
        int randomNbrRoom = UnityEngine.Random.Range(0, 100);
        if (randomNbrRoom < ChanceRoom)
        {
            int roomWidth = UnityEngine.Random.Range(roomWidthMin, roomWidthMax);
            int roomLength = UnityEngine.Random.Range(roomLengthMin, roomLengthMax);

            createRoom(roomWidth, roomLength, dungeonData);
            ChanceRoom = 0;

        }
        else
        {
            ChanceRoom += 5;
        }
    }


 

}


