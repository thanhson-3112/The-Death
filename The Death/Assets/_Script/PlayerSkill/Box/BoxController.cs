using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour, IDamageAble
{
    private Animator anim;
    public float boxHealth = 10;
    private bool isBroken = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        gameObject.SetActive(true);
        isBroken = false;
    }

    void Update()
    {

    }

    public virtual void TakePlayerDamage(float damage)
    {
        if (isBroken) return;

        if (boxHealth > 0)
        {
            boxHealth -= damage;
        }

        if (boxHealth <= 0)
        {
            BoxBroken();
        }
    }

    public void BoxBroken()
    {
        if (isBroken) return;
        isBroken = true;

        GetComponent<BoxItemSpawn>().InstantiateLoot(transform.position);

        Invoke("Disappear", 1.5f);
        anim.SetTrigger("BoxBreak");
    }

    public void Disappear()
    {
        // Tr? h?p v? pool
        BoxPool.Instance.ReturnBox(gameObject);
        boxHealth = 10;
        isBroken = false;
    }

}
