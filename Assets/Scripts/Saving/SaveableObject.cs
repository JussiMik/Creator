using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ObjectType {Monk, Tile}

public abstract class SaveableObject : MonoBehaviour
{
    protected string save;

    private ObjectType objectType;

    private void Start()
    {
        SaveAndLoad.Instance.SaveableObjects.Add(this);
    }

    public virtual void Save(int id)
    {

    }

    public virtual void Load(string[] values)
    {

    }

    public void DestroySaveable()
    {

    }
}
