using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ProductionBar : MonoBehaviour
{
    //  public GameObject enemyHealthBar;
    // GameObject enemyHealth;
    public Vector3 offset;
    Transform target;

    GameObject building;
    GameObject foreground;
    GameObject background;
    public GameObject foregroundObject;
    public GameObject backgroundObject;
    public Image progressBarForegroundImage;
    Image progressBarBackgroundImage;

    public bool buildingDone;
    public bool checkForBuildingDone;
    public float percent;
    public float faithMaximumTime;
    public float faithTimer;

    // Use this for initialization
    void Start()
    {
        checkForBuildingDone = true;

        faithMaximumTime = gameObject.GetComponentInParent<Structure>().originalFaithTargetTime;
        background = Instantiate(backgroundObject);
        foreground = Instantiate(foregroundObject);
        progressBarForegroundImage = foreground.GetComponent<Image>();
        progressBarBackgroundImage = backgroundObject.GetComponent<Image>();

        foreground.transform.SetParent(gameObject.transform, false);
        background.transform.SetParent(gameObject.transform, false);
        gameObject.GetComponentInChildren<OriginalParent>().originalParent = gameObject;

        foreground.transform.SetParent(GameObject.FindGameObjectWithTag("ProductionCycleCanvas").transform, false);
        background.transform.SetParent(GameObject.FindGameObjectWithTag("ProductionCycleCanvas").transform, false);

    }

    private void Awake()
    {

        target = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(checkForBuildingDone == true)
        {
            if (buildingDone = gameObject.GetComponent<Structure>().constructingDone == true)
            {
                checkForBuildingDone = false;
            }
        }
        background.transform.position = Camera.main.WorldToScreenPoint(target.position) + offset;
        foreground.transform.position = Camera.main.WorldToScreenPoint(target.position) + offset;

        faithTimer = gameObject.GetComponent<Structure>().productionCycleLength; // Sue me. 

        percent = faithTimer / faithMaximumTime;
        progressBarForegroundImage.fillAmount = Mathf.Lerp(1, 0, percent);
        if (faithTimer == 10 && checkForBuildingDone == false)
        {
            progressBarForegroundImage.fillAmount = 1;
        }

    }
}
