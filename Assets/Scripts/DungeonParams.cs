using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonParams
{

    public int tileMapSize {get; }
    public int nbrOfStepsDungeons { get; }

    public DungeonParams (int tileMapSize, int nbrOfStepsDungeons)
    {
        this.tileMapSize = tileMapSize;
        this.nbrOfStepsDungeons = nbrOfStepsDungeons;
    }
}
