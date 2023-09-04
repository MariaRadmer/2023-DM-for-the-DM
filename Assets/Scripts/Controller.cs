using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    [SerializeField] View view;
    [SerializeField] Model model;
    [SerializeField] CameraSystem cameraSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int[,] GenerateNewDungeon(DungeonParams dungeonParams)
    {
        cameraSystem.UpdateParameters(dungeonParams);
        return model.GenerateDungeon(dungeonParams);
    }
}
