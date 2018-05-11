using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveManager : MonoBehaviour
{
    GameManager gameManager;
    MysticPlaceCS mysticPlace;
    [Space(10)]
    [Header("Meditation rooms and Gardens")]
    public float requiredAmountOfMeditationRooms;
    public float requiredAmountOfGardens;

    [Space(10)]
    [Header("Amount of resources")]
    public float requiredResourceAmount;
    [Space(10)]
    public bool checkForCompletedObjectives;

    public List<GameObject> shrineList = new List<GameObject>();
    public List<GameObject> gardenList = new List<GameObject>();
    public List<GameObject> meditationRoomList = new List<GameObject>();
    public List<GameObject> templeList = new List<GameObject>();

    //BOOLS BITCH
    bool monkConversionDone;
    bool meditationRoomAndGardensDone;
    bool buildFourShrinesDone;
    bool buildOneTempleDone;
    bool buildThreeTemplesDone;
    bool mysticPlaceLevelTwoDone;
    bool haveAmountOfWoodDone;
    bool haveAmountOfStoneDone;

    public List<bool> selectedObjectives = new List<bool>();

    [Space(10)]
    public bool useMonkConvertChallenge;
    public bool useFourShrinesChallenge;
    public bool useMysticPlaceChallenge;
    public bool useBuildThreeTemplesChallenge;

    public bool monkTextUsed;
    public bool shrineTextUsed;

    public string convertMonkText;
    public string upgradeMysticPlaceText;
    public string buildShrinesText;
    public string buildTempleText;

    public string tutturuuVittu;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        CheckSelectedObjectives();
    }

    void CheckSelectedObjectives()
    {
        selectedObjectives.Add(useMonkConvertChallenge);
        selectedObjectives.Add(useFourShrinesChallenge);
        selectedObjectives.Add(useMysticPlaceChallenge);
        selectedObjectives.Add(useBuildThreeTemplesChallenge);
    }

    public void CheckForCompletedObjectives()
    {

        ConvertMonks();
        BuildFourShrines();
        UpgradeMysticPlaceToLevelTwo();
        BuildThreeTemples();

        /* MeditationRoomAndGardens();
         BuildOneTemple();
         HaveAmountOfWood();
         HaveAmountOfStone();
         */
    }
    void ConvertMonks()
    {
        if (useMonkConvertChallenge == true)
        {
            tutturuuVittu = convertMonkText;
            if (gameManager.totalMonksConverted == 10 && monkConversionDone == false)
            {
                Debug.Log("Monjes convertidos");
                monkTextUsed = true;
                monkConversionDone = true;
            }
        }
    }

    void BuildFourShrines()
    {
        if (useFourShrinesChallenge == true)
        {
            if (shrineList.Count == 4 && buildFourShrinesDone == false)
            {
                Debug.Log("Cuatro santuarios");
                shrineTextUsed = true;
                buildFourShrinesDone = true;
            }
        }
    }

    void MeditationRoomAndGardens()
    {
        if (meditationRoomList.Count >= requiredAmountOfMeditationRooms && gardenList.Count >= requiredAmountOfGardens && meditationRoomAndGardensDone == false)
        {
            Debug.Log("Meditatziuun and gäärdens");
            meditationRoomAndGardensDone = true;
        }
    }

    void BuildOneTemple()
    {
        if (templeList.Count == 1 && buildOneTempleDone == false)
        {
            Debug.Log("Uno templo");
            buildOneTempleDone = true;
        }
    }

    void BuildThreeTemples()
    {
        if (useBuildThreeTemplesChallenge == true)
        {
            if (templeList.Count == 3 && buildThreeTemplesDone == false)
            {
                Debug.Log("Tres templos");
                buildThreeTemplesDone = true;
            }
        }
    }

    void UpgradeMysticPlaceToLevelTwo()
    {
        if (useMysticPlaceChallenge == true)
        {
            mysticPlace = GameObject.FindGameObjectWithTag("MysticPlace").GetComponent<MysticPlaceCS>();

            if (mysticPlace.level == 2 && mysticPlaceLevelTwoDone == false)
            {
                Debug.Log("Mystic place level TWOOOO");
                mysticPlaceLevelTwoDone = true;
            }
        }
    }

    void HaveAmountOfWood()
    {
        if (gameManager.wood >= requiredResourceAmount && haveAmountOfWoodDone == false)
        {
            Debug.Log("Puuta ON");
            haveAmountOfWoodDone = false;
        }
    }

    void HaveAmountOfStone()
    {
        if (gameManager.stone >= requiredResourceAmount && haveAmountOfStoneDone == false)
        {
            Debug.Log("Kiveä ON");
            haveAmountOfStoneDone = false;
        }
    }

}
