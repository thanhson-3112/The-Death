using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private List<ExperienceSO> lootList = new List<ExperienceSO>();
    /*[SerializeField] */private int expAmount;

    public int _expAmount { get => expAmount; }
    
    protected virtual void Update()
    {
    }

    public ExperienceSO GetDroppedItem()
    {
        // random ti le roi ra 1% - 100%
        int randomNumber = Random.Range(1, 101);
        List<ExperienceSO> PossibleItems = new List<ExperienceSO>();
        foreach(ExperienceSO item in lootList)
        {
            if (randomNumber <= item.exChance)
            {
                PossibleItems.Add(item); 
            }
        }
        if (PossibleItems.Count > 0)
            {
                ExperienceSO droppedItem = PossibleItems[Random.Range(0, PossibleItems.Count)];
                return droppedItem;
            }
        Debug.Log("No loot dropped");
        return null;
    }

    // Roi ra tu quai
    public void InstantiateLoot(Vector3 spawnPosition)
    {
        ExperienceSO droppedItem = GetDroppedItem();
        if(droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            //truyen hinh anh cho item
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.exSprite;
            // truyen gia tri cho item
            lootGameObject.GetComponent<ExperienceItem>().expAmount = droppedItem.exAmount;

            float dropForce = 10f;
            Vector2 dropDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDir * dropForce, ForceMode2D.Impulse);
        }
    } 
}
