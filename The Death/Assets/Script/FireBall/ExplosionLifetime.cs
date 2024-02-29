using System.Collections;
using UnityEngine;

public class ExplosionLifetime : MonoBehaviour
{
    [SerializeField] private float lifetime = 1f; 

    void Start()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
