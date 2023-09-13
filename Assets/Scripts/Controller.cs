using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    [SerializeField] View view;
    [SerializeField] Model model;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public (Vector3Int, Vector3Int) GetTopBottom()
    {
        return model.GetTopBottom();
    }
    public Vector3Int GetDungeonStartPos()
    {
        return model.GetDungeonStartPos();
    }

    public int[,] GenerateNewDungeon(DungeonParams dungeonParams)
    {
        

        int[,] dungeon = model.GenerateDungeon(dungeonParams);
        

        return dungeon;
    }
}
