using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    [SerializeField] public int goldTotal ;


    protected virtual void Update()
    {

    }

    protected virtual void OnEnable()
    {
        LootManager.Instance.OnGoldChange += HandleGold;
    }

    protected virtual void OnDisable()
    {
        LootManager.Instance.OnGoldChange -= HandleGold;
    }

    protected virtual void HandleGold(int newGold)
    {
        goldTotal += newGold;
        Debug.Log("Vang duoc cong" + goldTotal);
    }
}
