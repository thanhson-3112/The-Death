using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockMeteo : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void UnlockSkillMeteo()
    {
        gameObject.SetActive(true);
    }
}
