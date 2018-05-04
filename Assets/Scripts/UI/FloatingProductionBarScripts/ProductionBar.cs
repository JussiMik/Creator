using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ProductionBar : MonoBehaviour
{

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
    public bool startTimer;
    public bool checkForFaithCollected;
    public float percent;
    public float faithMaximumTime;
    public float faithTimer;


    private void Awake()
    {
        target = gameObject.transform;
    }

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

        gameObject.GetComponentInChildren<ForegroundBar>().originalParent = gameObject;
        gameObject.GetComponentInChildren<BackgroundBar>().originalParent = gameObject;

        foreground.transform.SetParent(GameObject.FindGameObjectWithTag("ProductionCycleCanvas").transform, false);
        background.transform.SetParent(GameObject.FindGameObjectWithTag("ProductionCycleCanvas").transform, false);
    }

    // Update is called once per frame
    void Update()
    {

        if (checkForBuildingDone == true)
        {
            if (buildingDone = gameObject.GetComponent<Structure>().constructingDone == true)
            {
                checkForBuildingDone = false;
                checkForFaithCollected = true;
            }
        }
        if (checkForFaithCollected == true)
        {
            if (gameObject.GetComponent<Structure>().faithCollected == true && startTimer == false)
            {
                faithTimer = gameObject.GetComponent<Structure>().productionCycleLength; // Sue me. 
                startTimer = true;
                checkForFaithCollected = false;
            }
        }
        if (startTimer == true)
        {
            ProductionTimer();
        }

        percent = faithTimer / faithMaximumTime;

        /*  if (percent == 0)
          {
              progressBarForegroundImage.fillAmount = 0;
          }*/
        if (faithTimer == 10 && checkForBuildingDone == false)
        {
            progressBarForegroundImage.fillAmount = 1;
        }
    }

    void ProductionTimer()
    {
        faithTimer -= Time.deltaTime;
        progressBarForegroundImage.fillAmount = Mathf.Lerp(1, 0, percent);
        if (faithTimer <= 0)
        {
            faithTimer = 0;
            startTimer = false;
            checkForFaithCollected = true;
        }
    }
}
