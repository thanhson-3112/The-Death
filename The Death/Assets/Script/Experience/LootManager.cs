using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class LootManager : ScriptableObject
{
    public Sprite lootSprite;
    public string lootName;
    public int dropChance;

    public void Loot(string lootName, int dropChance)
    {
        this.lootName = lootName;
        this.dropChance = dropChance;
    }
}
