using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootExperience : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<LootManager> lootList = new List<LootManager>();

    LootManager GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<LootManager> PossibleItems = new List<LootManager>();
        foreach(LootManager item in lootList)
        {
            if(randomNumber <= item.dropChance)
            {
                PossibleItems.Add(item); 
            }
        }
        if (PossibleItems.Count > 0)
            {
                LootManager droppedItem = PossibleItems[Random.Range(0, PossibleItems.Count)];
                return droppedItem;
            }
        Debug.Log("No loot dropped");
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        LootManager droppedItem = GetDroppedItem();
        if(droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;

            float dropForce = 10f;
            Vector2 dropDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDir * dropForce, ForceMode2D.Impulse);
        }
    } 
}
