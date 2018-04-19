using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceIcon : MonoBehaviour
{
    public GameObject canvas;
    GameObject clickedObject;
    Image image;
    public string type;
    public Sprite faithImage;
    public Sprite devotionImage;

    void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("PopupMenuCanvas");
        image = gameObject.GetComponent<Image>();
    }
    void Start()
    {

    }
    void OnEnable()
    {
        clickedObject = canvas.GetComponent<PopupMenu>().clickedObject;
        type = clickedObject.GetComponent<Structure>().type;
        if (type == "Faith")
        {
            image.sprite = faithImage;
        }
        if (type == "Devotion")
        {
            image.sprite = devotionImage;
        }
        if (type == null)
        {
            image.sprite = null;
        }

    }

    void Update()
    {

    }
}
