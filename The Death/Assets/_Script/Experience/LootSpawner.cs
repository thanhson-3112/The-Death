using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<LootItemManager> lootList = new List<LootItemManager>();

    LootItemManager GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<LootItemManager> PossibleItems = new List<LootItemManager>();
        foreach(LootItemManager item in lootList)
        {
            if(randomNumber <= item.dropChance)
            {
                PossibleItems.Add(item); 
            }
        }
        if (PossibleItems.Count > 0)
            {
                LootItemManager droppedItem = PossibleItems[Random.Range(0, PossibleItems.Count)];
                return droppedItem;
            }
        Debug.Log("No loot dropped");
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        LootItemManager droppedItem = GetDroppedItem();
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
