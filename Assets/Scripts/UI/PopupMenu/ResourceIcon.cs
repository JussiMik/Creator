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
    public string name;
    public Sprite faithSprite;
    public Sprite devotionSprite;
    public Sprite quarrySprite;
    public Sprite workshopSprite;
    public Sprite foodSprite;

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
        name = clickedObject.GetComponent<Structure>().name;
        if (type == "Faith")
        {
            image.sprite = faithSprite;
        }
        if (type == "Devotion")
        {
            image.sprite = devotionSprite;
        }
        if(name == "Quarry")
        {
            image.sprite = quarrySprite;
        }
        if(name == "Wood workshop")
        {
            image.sprite = workshopSprite;
        }
        if (type == "Food")
        {
            image.sprite = foodSprite;
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
