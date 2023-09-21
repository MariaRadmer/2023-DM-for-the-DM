using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;


// Source https://lukashermann.dev/writing/unity-highlight-tile-in-tilemap-on-mousever/ 
public class GridController : MonoBehaviour
{
    private Grid grid;

    // Should only et in the DT
    [SerializeField] private Tilemap interactiveMap = null;
    [SerializeField] private Tilemap hoverMap = null;
    [SerializeField] private Tilemap selectMap = null;
    [SerializeField] private Tile hoverTile = null;
    [SerializeField] private RuleTile hoverRuleTile = null;
    [SerializeField] private RuleTile pathTile = null;

    private Vector3Int previousMousePos = new Vector3Int();

    private Vector3Int startMousePos = new Vector3Int();

    private Vector3Int prevMousePosMakeRoom = new Vector3Int();


    private bool hasChangedFillBox = false;
    private bool hasChangedFillBoxDelete = false;

    Vector3Int startTarget = Vector3Int.zero;
    Vector3Int endTarget = Vector3Int.zero;


    private bool deleteRoom = false;
    private bool printMode = false;



    void Start()
    {

        grid = gameObject.GetComponent<Grid>();

    }




    void Update()
    {

        Vector3Int mousePos = GetMousePosition();
        if (!mousePos.Equals(previousMousePos))
        {
            selectMap.SetTile(previousMousePos, null); // Remove old hoverTile
            selectMap.SetTile(mousePos, hoverTile);
            previousMousePos = mousePos;
        }



      


        if (Input.GetMouseButton(0))
        {
            
            MakeRoom(mousePos);
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            
            startMousePos = Vector3Int.zero;
            hoverMap.ClearAllTiles();
            

            if (!deleteRoom)
            {
                interactiveMap.BoxFill(mousePos, pathTile, startTarget.x, startTarget.y, endTarget.x, endTarget.y);
            }
            else
            {
                interactiveMap.BoxFill(mousePos, null, startTarget.x, startTarget.y, endTarget.x, endTarget.y);
            }

            
            
        }

    }






    public void EraseMode()
    {
        deleteRoom= true;
        
    }


    public void PenMode()
    {
        deleteRoom = false;
    }





    void MakeRoom(Vector3Int mousePos)
    {

        // have both startMousePos and endMousePos in begining of call?
        // then see if x or y has become smaller ?

        if (startMousePos == Vector3Int.zero)
        {
            startMousePos = GetMousePosition();
        }

        startTarget = startMousePos;
        endTarget = mousePos;

        if (startTarget.x > endTarget.x)
        {
            int temp = startTarget.x;
            startTarget.x = endTarget.x;
            endTarget.x = temp;
            hasChangedFillBox = true;

        }


        if (startTarget.y > endTarget.y)
        {
            int temp = startTarget.y;
            startTarget.y = endTarget.y;
            endTarget.y = temp;
            hasChangedFillBox = true;

        }

        
        
        


        if (hasChangedFillBox)
        {
            hoverMap.BoxFill(mousePos, hoverRuleTile, startTarget.x, startTarget.y, endTarget.x, endTarget.y);
        }
        
            
           
        

    }

    Vector3Int GetMousePosition()
    {

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }
    
}
