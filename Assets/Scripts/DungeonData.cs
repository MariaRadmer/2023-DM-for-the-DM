using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonData
{
    public List<TileBase> tiles;
    public List<Vector3Int> tilePositions;
    public Vector3Int endPosition;

    public DungeonData() { 
    
        tiles = new List<TileBase>();
        tilePositions = new List<Vector3Int>();
        endPosition = new Vector3Int();
    }
}
