using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ComeTheDeath : MonoBehaviour
{
    public GameObject loadingPanel;
    public Slider loadingSlider;
    public TextMeshProUGUI loadingText;

    public float currentProgress = 0f;
    private float progressSpeed = 0.5f;
    private bool isDone = true;

    public void Start()
    {
        loadingPanel.SetActive(false);
        currentProgress = 0f;
        loadingSlider.value = 0f;
        loadingText.text = "0%";
    }

    public void Update()
    {
        if (currentProgress < 1f && !isDone)
        {
            currentProgress += Time.fixedDeltaTime * progressSpeed;

            currentProgress = Mathf.Clamp01(currentProgress);
            loadingSlider.value = currentProgress;
            loadingText.text = (currentProgress * 100f).ToString("F0") + "%";

            if (currentProgress >= 1f)
            {
                SceneManager.LoadScene(3);
                isDone = true;
            }
        }
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            yield return new WaitForSeconds(1f);
            loadingPanel.SetActive(true);
            ResetProgress();
        }
    }

    private void ResetProgress()
    {
        currentProgress = 0f;
        loadingSlider.value = 0f;
        loadingText.text = "0%";
        isDone = false;
    }
}
