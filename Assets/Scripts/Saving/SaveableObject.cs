using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ObjectType {Monk, Tile, TreeTile, RockTile}

public abstract class SaveableObject : MonoBehaviour
{
    protected string saveStats;

    [SerializeField]
    private ObjectType objectType;

    private void Start()
    {
        SaveAndLoad.Instance.SaveableObjects.Add(this);
    }

    public virtual void Save(int id)
    {
        PlayerPrefs.SetString(id.ToString(), objectType + "_" + transform.position.ToString() + "_" + transform.localScale);
    }

    public virtual void Load(string[] values)
    {
        transform.localPosition = SaveAndLoad.Instance.StringToVector(values[1]);
        transform.localScale = SaveAndLoad.Instance.StringToVector(values[2]);
        //transform.localRotation = SaveAndLoad.Instance.StringToQuaternion(values[3]);
    }

    public void DestroySaveable()
    {

    }
}
