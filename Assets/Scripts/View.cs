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

    [SerializeField] TMP_InputField steps;
    [SerializeField] TMP_InputField size;

    [SerializeField] TMP_InputField save_input;
    [SerializeField] TMP_InputField load_input;

    public List<CustomTile> allTiles = new List<CustomTile>();
 

    int tileMapSize = 10;
    int nbrOfStepsDungeons = 10;

   

    string folderPath;
    string saveFolderPath;


    const int maxValueSteps = 5000;
    const int maxValueSize = 10000;

    const int minValue = 2;

    // Start is called before the first frame update
    void Start()
    {
        folderPath = Application.dataPath + "Dungeons/";
        DirectoryInfo di = Directory.CreateDirectory(folderPath);

        saveFolderPath = Application.dataPath + "Saves/";
        DirectoryInfo saveFolder = Directory.CreateDirectory(saveFolderPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnSizeChanged(string input) {

        try
        {

            int result = Int32.Parse(input);

            if (result >= minValue && result < maxValueSize)
            {
                tileMapSize = result;
            } 
            else if (result < minValue)  
            {
                tileMapSize = minValue;
                size.text = tileMapSize.ToString();
            } 
            else if (result <= maxValueSize)
            {
                tileMapSize = result;
            }
            else if (result > maxValueSize)
            {
                Debug.Log("Size over max ");
                tileMapSize = maxValueSize;
                size.text = tileMapSize.ToString();
            }

        }
        catch (OverflowException)
        {
            tileMapSize = maxValueSize;
            size.text = tileMapSize.ToString();
        }
        catch (FormatException)
        {
            size.text = tileMapSize.ToString();
            Debug.Log($"Unable to parse '{input}'");
        }

    }


    public void OnStepsChanged(string input)
    { 
        try
        {
            int result = Int32.Parse(input);


            if (result >= minValue && result < maxValueSteps)
            {
                nbrOfStepsDungeons = result;
            } 
            else if (result <= minValue)
            {
                nbrOfStepsDungeons = minValue;
                steps.text = nbrOfStepsDungeons.ToString();
            }
            
            else if (result <= maxValueSteps)
            {
                nbrOfStepsDungeons = result;
            }
            else if (result > maxValueSteps) 
            {
                nbrOfStepsDungeons = maxValueSteps;
                steps.text = nbrOfStepsDungeons.ToString();
            }
            
            
        }
        catch (OverflowException)
        {
            nbrOfStepsDungeons = maxValueSteps;
            steps.text = nbrOfStepsDungeons.ToString();
        }
        catch (FormatException)
        {
            steps.text = nbrOfStepsDungeons.ToString();
            Debug.Log($"Unable to parse '{input}'");
        }

    }

    public void OnSave()
    {
        // TODO: also have the endpos, right now it is 0,0,0

        Vector3Int endPos = controller.GetEndPosition();

        translater.SaveDungeon(save_input.text, endPos, tilemap, saveFolderPath, allTiles);
    }

    public void OnLoad()
    {
        Vector3Int endPos = translater.LoadLevel(load_input.text,tilemap, saveFolderPath,allTiles);
        cameraSystem.UpdateCameraPosition(endPos);


    }

    public void OnClickGenerateDungeon()
    {
        

        DungeonParams dungeonParams = new DungeonParams(tileMapSize, nbrOfStepsDungeons);


        DungeonData dungeonData =controller.GenerateNewDungeon(dungeonParams);

        cameraSystem.UpdateParameters(dungeonParams);

        // move cam to dungeon
        Vector3Int camSatartPos = dungeonData.endPosition;
      

        cameraSystem.UpdateCameraPosition(camSatartPos);

        
        translater.UpdateDungeonLayout(dungeonData, dungeonParams, tilemap, tile);
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
