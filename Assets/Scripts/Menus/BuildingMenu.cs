//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//    [DisallowMultipleComponent]
//    //[RequireComponent(typeof(SpriteRenderer))]

//public class BuildingMenu : MonoBehaviour
//{
//    SpriteRenderer srend;
//    private Sprite button;
//    public Sprite pressedButton;
//    public int structCount;
//    public GameObject[] structs;

//    // Use this for initialization
//    void Awake ()
//    {
//        srend = GetComponent<SpriteRenderer>();
//        button = srend.sprite;


//	}

//    private void OnMouseDown()
//    {
//        Debug.Log(gameObject);
//        srend.sprite = pressedButton;
//    }
//    private void OnMouseUp()
//    {
//        Debug.Log(gameObject);
//        srend.sprite = button;
//    }
//}
