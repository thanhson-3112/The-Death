using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class ExperienceSO : ScriptableObject
{
    public Sprite exSprite;
    public string exName;
    public int exChance;
    public int exAmount;

    public void Loot(string exName, int exChance, int exAmount)
    {
        this.exName = exName;
        this.exChance = exChance;
        this.exAmount = exAmount;
    }

}
