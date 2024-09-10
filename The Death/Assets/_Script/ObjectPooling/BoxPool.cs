using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPool : MonoBehaviour
{
    public static BoxPool Instance;

    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private int poolSize = 10;

    // Danh s�ch c�c v? tr� spawn
    [SerializeField] private List<Transform> spawnPositions;

    private List<GameObject> pool;
    private Dictionary<GameObject, Transform> boxSpawnPositions; // L?u tr? v? tr� c?a t?ng h?p

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pool = new List<GameObject>();
        boxSpawnPositions = new Dictionary<GameObject, Transform>();

        // ??m b?o t?o t?t c? c�c h?p t?i c�c v? tr� spawn v� l?u l?i v? tr� spawn c?a t?ng h?p
        for (int i = 0; i < poolSize; i++)
        {
            GameObject box = Instantiate(boxPrefab, spawnPositions[i].position, Quaternion.identity);
            box.SetActive(true); // ??m b?o h?p xu?t hi?n ngay t? ??u
            pool.Add(box);

            // G�n v? tr� spawn cho t?ng h?p
            boxSpawnPositions[box] = spawnPositions[i];
        }
    }

    public void ReturnBox(GameObject box)
    {
        // ?n h?p khi n� b? ph� h?y
        box.SetActive(false);

        // G?i h�m ?? t�i xu?t hi?n h?p sau 10 gi�y
        StartCoroutine(ReappearBoxAfterDelay(box, 30f));
    }

    private IEnumerator ReappearBoxAfterDelay(GameObject box, float delay)
    {
        yield return new WaitForSeconds(delay);

        // ??a h?p v? v? tr� spawn ban ??u v� k�ch ho?t l?i n�
        box.transform.position = boxSpawnPositions[box].position;
        box.SetActive(true);
    }
}
