using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveTracker : MonoBehaviour
{
    GameManager gameManager;
    ObjectiveManager objectiveManager;
    public GameObject panel;
    public Transform verticalPosition;
    public Text textField;
    public Image checkmark;


    bool objectiveComplete;
    float fillSpeed;
    public float fadeSpeed;
    public float maxTime;
    float dividerTime = 2f;

    // Use this for initialization
    void Start()
    {
        objectiveManager = GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        panel = gameObject.transform.GetChild(0).gameObject;
        textField = panel.transform.GetChild(0).GetComponent<Text>();
        
        checkmark = panel.transform.Find("Checkmark").GetComponent<Image>();
        UpdateObjectiveText();
    }

    public void UpdateObjectiveText()
    {
        if (objectiveManager.objectiveObject.name == "ConvertMonksChallenge")
        {
            ConvertMonks challenge = objectiveManager.objectiveObject.GetComponent<ConvertMonks>();
            if (challenge.challengeDone)
            {
                objectiveComplete = true;

            }
            textField.text = objectiveManager.objectiveText + gameManager.totalMonksConverted + " / " + challenge.requiredMonks;
        }
        if(objectiveManager.objectiveObject.name == "BuildTempleChallenge")
        {
            BuildTemple challenge = objectiveManager.objectiveObject.GetComponent<BuildTemple>();
            if(challenge.challengeDone)
            {
                objectiveComplete = true;
            }
            textField.text = objectiveManager.objectiveText + objectiveManager.templeList.Count + " / " + challenge.requiredTemples;
        }
        if(objectiveManager.objectiveObject.name == "BuildShrineChallenge")
        {
            BuildShrines challenge = objectiveManager.objectiveObject.GetComponent<BuildShrines>();
            if (challenge.challengeDone)
            {
                objectiveComplete = true;
            }
            textField.text = objectiveManager.objectiveText + objectiveManager.shrineList.Count + " / " + challenge.requiredShrines;
        }
        if(objectiveManager.objectiveObject.name == "UpgradeMysticPlace")
        {
            UpgradeMysticPlace challenge = objectiveManager.objectiveObject.GetComponent<UpgradeMysticPlace>();
            if(challenge.challengeDone)
            {
                textField.text = objectiveManager.objectiveText + "1" + " / " + " 1 ";
                objectiveComplete = true;
            }
            if(!challenge.challengeDone)
            {
                textField.text = objectiveManager.objectiveText + "0" + " / " + " 1 ";
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (objectiveComplete)
        {
            ObjectiveComplete();
        }
    }

    void ObjectiveComplete()
    {
        fillSpeed = dividerTime / maxTime;
        dividerTime -= Time.deltaTime;
        checkmark.fillAmount = Mathf.Lerp(1, 0, fillSpeed);
        textField.color = new Color(textField.color.r, textField.color.g, textField.color.b, 75f);
    }

}
