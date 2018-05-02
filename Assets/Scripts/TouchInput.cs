using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    public LayerMask touchInputMask;
    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchesOld;

    private RaycastHit2D hit;

    Vector3 movement = Vector3.zero;
    public bool worldPos;
    public float zoomAmountPerStep;
    public float cameraSizeMax = 20f;
    public float cameraSizeMin = 1f;
    public float orthoZoomSpeed = 0.5f;
    float scrollSpeed = 0.5f;

    public Vector2 minXValue;
    public Vector2 maxXValue;
    public Vector2 minYValue;
    public Vector2 maxYValue;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        // if unity editor


        LimitCameraMovement();
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            CameraZoom(Camera.main.ScreenToWorldPoint(Input.mousePosition), zoomAmountPerStep);
        }

        //Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            CameraZoom(Camera.main.ScreenToWorldPoint(Input.mousePosition), -zoomAmountPerStep);
        }

        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            touchesOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchesOld);
            touchList.Clear();

            if (worldPos)
            {
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, touchInputMask);
            }
            else
            {
                hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero, Mathf.Infinity, touchInputMask);
            }

            if (hit.collider != null)
            {
                GameObject recipient = hit.transform.gameObject;
                touchList.Add(recipient);

                if (Input.GetMouseButtonDown(0))
                {
                    recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                }
                if (Input.GetMouseButton(0))
                {
                    recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }

            foreach (GameObject g in touchesOld)
            {
                if (!touchList.Contains(g))
                {
                    g.SendMessage("onTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }
        }

#endif

        if (Input.touchCount > 0)
        {
            touchesOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchesOld);
            touchList.Clear();

            foreach (Touch touch in Input.touches)
            {
                if (worldPos)
                {
                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, touchInputMask);
                }
                else
                {
                    hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero, Mathf.Infinity, touchInputMask);
                }

                if (hit.collider != null)
                {
                    GameObject recipient = hit.transform.gameObject;
                    //touchList.Add (recipient);

                    if (touch.phase == TouchPhase.Began)
                    {
                        recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver); //orignal
                    }
                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Canceled)
                    {
                        recipient.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }

            foreach (GameObject g in touchesOld)
            {
                if (!touchList.Contains(g))
                {
                    g.SendMessage("onTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        if(Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            gameObject.GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

            gameObject.GetComponent<Camera>().orthographicSize = Mathf.Clamp(gameObject.GetComponent<Camera>().orthographicSize, cameraSizeMin, cameraSizeMax);
        }
    }


    void CameraZoom(Vector3 zoomTowards, float amount)
    {
        float multiplier = (1.0f / gameObject.GetComponent<Camera>().orthographicSize * amount);
        transform.position += (zoomTowards - transform.position) * multiplier;
        gameObject.GetComponent<Camera>().orthographicSize -= amount;
        gameObject.GetComponent<Camera>().orthographicSize = Mathf.Clamp(gameObject.GetComponent<Camera>().orthographicSize, cameraSizeMin, cameraSizeMax);
    }
    void LimitCameraMovement()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minXValue.x, maxXValue.x), Mathf.Clamp(transform.position.y, minYValue.y, maxYValue.y), transform.position.z);
    }
}