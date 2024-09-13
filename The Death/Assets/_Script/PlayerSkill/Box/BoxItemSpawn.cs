using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxItemSpawn : MonoBehaviour
{
    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private List<BoxItemSO> lootList = new List<BoxItemSO>();

    public BoxItemSO GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<BoxItemSO> PossibleItems = new List<BoxItemSO>();
        foreach (BoxItemSO item in lootList)
        {
            if (randomNumber <= item.boxItemChance)
            {
                PossibleItems.Add(item);
            }
        }
        if (PossibleItems.Count > 0)
        {
            BoxItemSO droppedItem = PossibleItems[Random.Range(0, PossibleItems.Count)];
            return droppedItem;
        }
        Debug.Log("No loot dropped");
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        BoxItemSO droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.boxItemSprite;

            BoxItem boxItem = lootGameObject.GetComponent<BoxItem>();
            boxItem.itemData = droppedItem;  // Truy?n d? li?u item

            Animator animator = lootGameObject.GetComponent<Animator>();
            if (animator != null && droppedItem.itemAnimatorController != null)
            {
                animator.runtimeAnimatorController = droppedItem.itemAnimatorController;
            }

            float dropForce = 10f;
            Vector2 dropDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDir * dropForce, ForceMode2D.Impulse);
        }
    }
}
