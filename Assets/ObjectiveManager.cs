using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    ObjectiveTracker objectiveTracker;
    GameManager gameManager;
    MysticPlaceCS mysticPlace;
    public Text textField;
    public GameObject objectiveObject;
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

    List<bool> selectedObjectives = new List<bool>();

    [Space(10)]
 
    bool useMonkConvertChallenge;
    bool useFourShrinesChallenge;
    bool useMysticPlaceChallenge;
    bool useBuildThreeTemplesChallenge;

    bool monkTextUsed;
    bool shrineTextUsed;

    string convertMonkText;
    string upgradeMysticPlaceText;
    string buildShrinesText;

    
    public string buildTempleText;

    public string objectiveText;

    void Awake()
    {
        objectiveTracker = GameObject.Find("ObjectiveTrackerCanvas").GetComponent<ObjectiveTracker>();
        objectiveText = objectiveObject.GetComponent<ChallengeBase>().objectiveText;

    }

    public void CheckForCompletedObjectives()
    {
        objectiveObject.GetComponent<ChallengeBase>().Objective();
        objectiveTracker.UpdateObjectiveText();
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
