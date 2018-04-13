using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
//[RequireComponent(typeof(SpriteRenderer))]

public class BuildingMenu : MonoBehaviour
{
    Image srend;
    private Sprite button;
    public Sprite pressedButton;
    public int structCount;
    public GameObject[] structs;

    // Use this for initialization
    void Awake()
    {
        srend = GetComponent<Image>();
        button = srend.sprite;
    }

    private void OnMouseDown()
    {
        Debug.Log(gameObject);
        srend.sprite = pressedButton;
    }
    private void OnMouseUp()
    {
        Debug.Log(gameObject);
        srend.sprite = button;
    }
}
