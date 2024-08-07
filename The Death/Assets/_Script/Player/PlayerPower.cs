using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPower : MonoBehaviour
{
    public float baseDamage = 70f;
    public float currentDamage;

    void Start()
    {
        currentDamage = baseDamage;
    }

    void Update()
    {
        
    }

    public void PlayerDamageUpgrade()
    {
        currentDamage += 2;
    }

    public void PlayerDamageUpgrade2()
    {
        currentDamage += 5;
    }
}
