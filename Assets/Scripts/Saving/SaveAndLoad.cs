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
        PlayerPrefs.SetInt("ObjectCount", SaveableObjects.Count);

        for (int i = 0; i < SaveableObjects.Count; i++)
        {
            SaveableObjects[i].Save(i);
        }

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
        foreach (SaveableObject obj in SaveableObjects)
        {
            if (obj != null)
            {
                Destroy(obj.gameObject);
            }
        }

        SaveableObjects.Clear();

        int objectCount = PlayerPrefs.GetInt("ObjectCount");

        for (int i = 0; i < objectCount; i++)
        {
            string[] value = PlayerPrefs.GetString(i.ToString()).Split('_');
            GameObject loadableObject = null;

            switch (value[0])
            {
                /*
                case "Monk":
                    loadableObject = Instantiate(Resources.Load("Monk1") as GameObject);
                    break;
                    */
                case "TreeTile":
                    loadableObject = Instantiate(Resources.Load("TreeGO") as GameObject);
                    break;
                case "RockTile":
                    loadableObject = Instantiate(Resources.Load("RockGO") as GameObject);
                    break;
            }

            if (loadableObject != null)
            {
                loadableObject.GetComponent<SaveableObject>().Load(value);
            }
            
            Debug.Log(value); 
        }
    }

    private void UpdateSaveData(string saveFile)
    {
        lastSaveFile = saveFile;
        lastSaveTime = DateTime.Now.ToBinary();
        savedScene = SceneManager.GetActiveScene().name;
    }

    //Converts string to Vector3
    public Vector3 StringToVector(string value)
    {
        value = value.Trim(new char[] { '(', ')' });

        value = value.Replace(" ", "");

        string[] pos = value.Split(',');

        return new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));
    }

    public Quaternion StringToQuaternion(string value)
    {
        value = value.Trim(new char[] { '(', ')' });

        value = value.Replace(" ", "");

        string[] pos = value.Split(',');

        return new Quaternion(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]), float.Parse(pos[3]));
    }
}
