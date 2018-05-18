using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ObjectType {Monk, Tile, TreeTile, RockTile}

public abstract class SaveableObject : MonoBehaviour
{
    protected string save;
    [SerializeField]
    private ObjectType objectType;

    private void Start()
    {
        SaveAndLoad.Instance.SaveableObjects.Add(this);
        
    }

    public virtual void Save(int id)
    {
        PlayerPrefs.SetString(id.ToString(), objectType + "_" + transform.position.ToString());
    }

    public virtual void Load(string[] values)
    {
        transform.localPosition = SaveAndLoad.Instance.StringToVector(values[1]);
    }

    public void DestroySaveable()
    {

    }
}
