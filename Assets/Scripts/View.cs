using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] Controller controller;
    [SerializeField] DungeonTranslater translater;
    [SerializeField] CameraSystem cameraSystem;
    [SerializeField] Tilemap tilemap;
    [SerializeField] TileBase tile;

    [SerializeField] Camera printCamera;
 

    int tileMapSize = 10;
    int nbrOfStepsDungeons = 10;

    string folderPath;



    // Start is called before the first frame update
    void Start()
    {
        folderPath = Application.dataPath + "Dungeons/";
        DirectoryInfo di = Directory.CreateDirectory(folderPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnSizeChanged(string input) {

        try
        {
           
            int result = Int32.Parse(input);

            if (result > 10)
            {
                tileMapSize = result;
            }
            
        }
        catch (FormatException)
        {
            Debug.Log($"Unable to parse '{input}'");
        }

    }


    public void OnStepsChanged(string input)
    { 
        try
        {
            int result = Int32.Parse(input);

            if (result > 10)
            {
                nbrOfStepsDungeons = result;
            }
            
        }
        catch (FormatException)
        {
            Debug.Log($"Unable to parse '{input}'");
        }

    }


    public void OnClickGenerateDungeon()
    {
        
        

        DungeonParams dungeonParams = new DungeonParams(tileMapSize, nbrOfStepsDungeons);

        dungeonParams.DebugPrintDungeonParams();

        int[,] dungeonArr=controller.GenerateNewDungeon(dungeonParams);
        cameraSystem.UpdateParameters(dungeonParams);

        // move cam to dungeon
        Vector3Int camSatartPos = controller.GetDungeonStartPos();
        
        //Vector3 position = tilemap.CellToWorld(camSatartPos);
        Debug.Log("Cam position "+ camSatartPos);

        cameraSystem.UpdateCameraPosition(camSatartPos);


        translater.UpdateDungeonLayout(dungeonArr, dungeonParams, tilemap, tile);
    }






    //Soruce https://gamedevbeginner.com/how-to-capture-the-screen-in-unity-3-methods/ 
    public void OnPrintDungeon()
    {
        //StartCoroutine(takeScreenShot());

        printCamera.enabled = true;
        
        RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
        printCamera.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;
        printCamera.Render();
        Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
        renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        RenderTexture.active = null;
        byte[] byteArray = renderedTexture.EncodeToPNG();

        string currentTime = System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)");

        printCamera.enabled = false;
        System.IO.File.WriteAllBytes(folderPath + currentTime+".png", byteArray);
        
    }


    public IEnumerator takeScreenShot()
    {
        yield return new WaitForEndOfFrame();

        //tilemap.CompressBounds();
        Vector3Int size = tilemap.size;

        Vector2 temp = tilemap.transform.position;
        //var startX = temp.x - size.x / 2;
        //var startY = temp.y - size.y / 2;

        var tex = new Texture2D(size.x, size.y, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(size.x, size.y, size.x, size.y), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        var bytes = tex.EncodeToPNG();
        Destroy(tex);

        File.WriteAllBytes(Application.dataPath + "ScreenShot.png", bytes);

    }

}
