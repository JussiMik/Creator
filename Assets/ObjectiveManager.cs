using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    GameManager gameManager;
    public MysticPlaceCS mysticPlace;
    public bool checkForCompletedObjectives;

    public List<GameObject> shrineList = new List<GameObject>();
    public List<GameObject> gardenList = new List<GameObject>();
    public List<GameObject> meditationRoomList = new List<GameObject>();
    public List<GameObject> templeList = new List<GameObject>();

    //BOOLS BITCH
    public bool buildFourShrinesDone;
    public bool buildOneTempleDone;
    public bool buildThreeTemplesDone;
    public bool mysticPlaceLevelTwoDone;


    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void ChooseObjective()
    {

    }

    public void CheckForCompletedObjectives()
    {
        BuildFourShrines();
        BuildOneTemple();
        BuildThreeTemples();
        UpgradeMysticPlaceToLevelTwo();
    }
    void BuildFourShrines()
    {

        if (shrineList.Count == 4 && buildFourShrinesDone == false)
        {
            Debug.Log("Cuatro santuarios");
            buildFourShrinesDone = true;
        }
    }
    void BuildOneTemple()
    {
        if(templeList.Count == 1 && buildOneTempleDone == false)
        {
            Debug.Log("Uno templo");
            buildOneTempleDone = true;
        }
    }
    void BuildThreeTemples()
    {
        if(templeList.Count == 3 && buildThreeTemplesDone == false)
        {
            Debug.Log("Tres templos");
            buildThreeTemplesDone = true;
        }
    }
    void UpgradeMysticPlaceToLevelTwo()
    {
        mysticPlace = GameObject.FindGameObjectWithTag("MysticPlace").GetComponent<MysticPlaceCS>();
        
        if(mysticPlace.level == 2 && mysticPlaceLevelTwoDone == false)
        {
            Debug.Log("Mystic place level TWOOOO");
            mysticPlaceLevelTwoDone = true;
        }
    }
    void HaveAmountOfResource()
    {

    }
}
