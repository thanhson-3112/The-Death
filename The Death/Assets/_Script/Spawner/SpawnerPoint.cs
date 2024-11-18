using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public static SpawnPoint instance;

    [SerializeField] protected List<Transform> points;

    public void Awake()
    {
        instance = this;
    }

    protected virtual void Update()
    {
        this.LoadPoints();
    }

    protected virtual void LoadPoints()
    {
        if (this.points.Count > 0) return;
        foreach (Transform point in transform)
        {
            this.points.Add(point);
        }
        Debug.Log(transform.name + ": LoadPoints", gameObject);
    }

    public virtual Transform GetRandomPoint()
    {
        if (points.Count == 0)
        {
            Debug.LogWarning("No spawn points available!");
            return null;
        }

        int randIndex = Random.Range(0, points.Count);
        return points[randIndex];
    }

    public void StopSpawnEnemy()
    {
        points.Clear();
    }
}
