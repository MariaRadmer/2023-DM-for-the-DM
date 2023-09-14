using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonData
{
    public List<string> tiles;
    public List<Vector3Int> tilePositions;
    public Vector3Int endPosition;

    public DungeonData() { 
    
        tiles = new List<string>();
        tilePositions = new List<Vector3Int>();
        
    }
}
