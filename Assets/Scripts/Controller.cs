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

    public int[,] GenerateNewDungeon()
    {
        Debug.Log("Generate in Controller!");
        return model.GenerateDungeon();
    }
}
