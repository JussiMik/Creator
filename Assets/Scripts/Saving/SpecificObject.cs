using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificObject : SaveableObject
{
    public float playerLevel;
    [Space(10)]
    public float woodAmount;
    public float stoneAmount;

    // Update is called once per frame
    void Update()
    {

    }

    public override void Save(int id)
    {
        saveStats = playerLevel.ToString() + "_" + woodAmount.ToString() + "_" + stoneAmount.ToString();
        base.Save(id);
    }

    public override void Load(string[] values)
    {
        playerLevel = float.Parse(values[4]);
        woodAmount = float.Parse(values[5]);
        stoneAmount = float.Parse(values[6]);
        base.Load(values);
    }
}
