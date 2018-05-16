using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndLoad : MonoBehaviour
{
    //https://www.youtube.com/watch?v=WJQuMHH0JPM
    //https://www.youtube.com/watch?v=EoXBU0rvho8
    //https://www.sitepoint.com/saving-data-between-scenes-in-unity/

    public static SaveAndLoad Instance;

    public List<SaveableObject> SaveableObjects { get; private set; }

    [Space(10)]
    public float playerLevel;
    [Space(10)]
    public float woodAmount;
    public float stoneAmount;

    
    public string lastSaveFile;
    public long lastSaveTime;
    

    public string savedScene;

    public Scene openScene;

    private string savePath;
    public string SavePath
    {
        get
        {
            if (savePath != null)
                return savePath;
            else
            {
                savePath = Application.persistentDataPath + "/SavedGames/";
                return savePath;
            }
        }
    }

    private const string FILE_EXTENSION = ".xml";

    private string saveFile;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        SaveableObjects = new List<SaveableObject>();
    }

    public void SaveTheGame(string saveFile)
    {
        /*
        //CheckDirectory();

        //Update saveFile name
        if (saveFile == null)
        {
            //saveFile = GenerateNewSaveName();
        }
        */

        UpdateSaveData(saveFile);

        /*
        string fullSavePath = SavePath + saveFile + FILE_EXTENSION;
        

        playerLevel = 

        woodAmount;
        stoneAmount;

        lastSaveFile;
        lastSaveTime;

        savedScene;
        */
    }

    public void LoadTheGame(string gameName)
    {

    }

    private void UpdateSaveData(string saveFile)
    {
        lastSaveFile = saveFile;
        lastSaveTime = DateTime.Now.ToBinary();
        savedScene = SceneManager.GetActiveScene().name;
    }

    public Vector3 StringToVector(string value)
    {
        return Vector3.zero;
    }
}
