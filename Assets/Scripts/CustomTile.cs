using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName ="new Customtile",menuName ="DungeonMaker/Tile")]
public class CustomTile : ScriptableObject
{
    public TileBase tile;
    public string id;
}
