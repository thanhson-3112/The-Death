using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSwordController : MonoBehaviour
{
    public GameObject swordPrefab; // Prefab c?a thanh ki?m
    public float swordRadius = 10f; // Bán kính quay quanh ng??i ch?i
    public float rotateSpeed = 50f; // T?c ?? xoay
    private List<GameObject> swords = new List<GameObject>(); // Danh sách các ki?m ?ã sinh ra

    void Start()
    {
        // B?t ??u v?i 0 ki?m, không c?n làm gì ? ?ây
    }

    void Update()
    {
        RotateSwords();

        if (Input.GetKeyDown(KeyCode.F))
        {
            AddSword();
        }
    }

    public void AddSword()
    {
        // T?o m?t thanh ki?m m?i
        GameObject sword = Instantiate(swordPrefab, transform.position, Quaternion.identity);
        sword.transform.SetParent(transform); // Gán thanh ki?m làm con c?a ng??i ch?i
        swords.Add(sword);
    }

    void RotateSwords()
    {
        int swordCount = swords.Count;

        if (swordCount == 0)
            return; // Không c?n xoay n?u không có ki?m

        for (int i = 0; i < swordCount; i++)
        {
            float angle = i * (360f / swordCount) + Time.time * rotateSpeed;
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * swordRadius;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * swordRadius;

            // ??t v? trí c?a ki?m
            swords[i].transform.localPosition = new Vector3(x, y, 0f);

            // Tính toán góc quay sao cho m?i ki?m h??ng ra ngoài
            Vector3 directionFromCenter = (swords[i].transform.position - transform.position).normalized;
            float angleFromCenter = Mathf.Atan2(directionFromCenter.y, directionFromCenter.x) * Mathf.Rad2Deg;

            // ??t rotation sao cho m?i ki?m h??ng ra ngoài
            swords[i].transform.rotation = Quaternion.Euler(0, 0, angleFromCenter);
        }
    }


}
