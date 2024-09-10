using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxItemType
{
    Health,
    Armor,
    Speed
}

[CreateAssetMenu]
public class BoxItemSO : ScriptableObject
{
    public Sprite boxItemSprite;
    public string boxItemName;
    public int boxItemChance;
    public BoxItemType boxItemType;  // Lo?i v?t ph?m

    public void Loot(string boxItemName, int boxItemChance, BoxItemType boxItemType)
    {
        this.boxItemName = boxItemName;
        this.boxItemChance = boxItemChance;
        this.boxItemType = boxItemType;
    }
}
