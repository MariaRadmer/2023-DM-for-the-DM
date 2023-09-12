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
    [SerializeField] private Tile hoverTile = null;
    [SerializeField] private RuleTile hoverRuleTile = null;
    [SerializeField] private RuleTile pathTile = null;

    private Vector3Int previousMousePos = new Vector3Int();

    private Vector3Int startMousePos = new Vector3Int();


    private bool hasChangedFillBox = false;
    private bool hasChangedFillBoxDelete = false;

    Vector3Int startTarget = Vector3Int.zero;
    Vector3Int endTarget = Vector3Int.zero;


    private bool deleteRoom = false;



    void Start()
    {

        grid = gameObject.GetComponent<Grid>();

    }

    void Update()
    {

        Vector3Int mousePos = GetMousePosition();
        if (!mousePos.Equals(previousMousePos))
        {
            hoverMap.SetTile(previousMousePos, null); // Remove old hoverTile
            hoverMap.SetTile(mousePos, hoverTile);
            previousMousePos = mousePos;
        }

      


        if (Input.GetMouseButton(0))
        {
            
            MakeRoom(mousePos);
            
            
            
        }

        // Have to have an eraser function
        if (Input.GetMouseButton(1))
        {
            //interactiveMap.SetTile(mousePos, null);
            
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

            
            Debug.Log($" Fill {startTarget} to {endTarget}");
        }

    }






    public void EraseCalled()
    {
        deleteRoom= !deleteRoom;
        Debug.Log("Delete room bool " + deleteRoom);
    }




    void MakeRoom(Vector3Int mousePos)
    {
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
            
            interactiveMap.BoxFill(mousePos, hoverRuleTile, startTarget.x, startTarget.y, endTarget.x, endTarget.y);

           
        }

    }

    Vector3Int GetMousePosition()
    {

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }
    
}
