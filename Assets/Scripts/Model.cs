using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{

    [SerializeField] DungeonGenerator dungeonGenerator;
   

    private int[,] currentDungeon;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3Int GetDungeonStartPos()
    {
        return dungeonGenerator.GetDungeonStartPos();
    }

    public DungeonData GenerateDungeon(DungeonParams dungeonParams)
    {
        
        return dungeonGenerator.GenerateDungeon(dungeonParams);
    }

}
