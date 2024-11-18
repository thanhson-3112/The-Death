using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByDis : MonoBehaviour
{
    public Transform player;
    public float currentDis = 0f;
    public float limitDis = 180f;

    protected void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        this.GetDistance();
        this.Despawning();
    }

    protected void Despawning()
    {
        if (this.currentDis < this.limitDis) return;
        Destroy(gameObject);
        /*        MapPoolManager.Instance.ReturnMap(gameObject);
        */
    }

    protected virtual void GetDistance()
    {
        this.currentDis = Vector3.Distance(this.player.position, transform.position);
    }
}
