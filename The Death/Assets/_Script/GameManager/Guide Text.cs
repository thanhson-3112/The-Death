using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideText : MonoBehaviour
{


    void Start()
    {
        gameObject.SetActive(true);
        StartCoroutine(HideGuideText());
    }


    IEnumerator HideGuideText()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);

    }
}
