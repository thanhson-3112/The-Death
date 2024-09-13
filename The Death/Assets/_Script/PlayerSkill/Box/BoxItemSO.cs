using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxItemType
{
    Health,
    MaxPickRadius,
    Speed
}

[CreateAssetMenu]
public class BoxItemSO : ScriptableObject
{
    public Sprite boxItemSprite;
    public string boxItemName;
    public int boxItemChance;
    public BoxItemType boxItemType;
    public RuntimeAnimatorController itemAnimatorController;  

    public void Loot(string boxItemName, int boxItemChance, BoxItemType boxItemType, RuntimeAnimatorController itemAnimatorController)
    {
        this.boxItemName = boxItemName;
        this.boxItemChance = boxItemChance;
        this.boxItemType = boxItemType;
        this.itemAnimatorController = itemAnimatorController;
    }
}
