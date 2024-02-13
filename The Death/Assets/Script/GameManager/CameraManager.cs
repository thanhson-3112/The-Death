using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target; // Reference ??n transform c?a nhân v?t

    // C?p nh?t v? trí c?a camera m?i frame
    void Update()
    {
        if (target != null) // ??m b?o target t?n t?i
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
            
        }
    }
}
