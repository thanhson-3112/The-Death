using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerGold : MonoBehaviour
{
    public static PlayerGold instance;

    [SerializeField] public int goldTotal;
    public TextMeshProUGUI goldText;

    private PlayerPower playerPower;

    [Header("Sound Settings")]
    public AudioClip goldCollectSound;

    private void Awake()
    {
        if (PlayerGold.instance != null) Debug.LogError("Only 1 ScoreManager allow");
        PlayerGold.instance = this;
    }

    protected virtual void Start()
    {
        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();

    }

    protected virtual void Update()
    {
        goldText.text = "Gold: " + goldTotal.ToString();

    }

    protected virtual void OnEnable()
    {
        if (LootManager.Instance != null)
        {
            LootManager.Instance.OnGoldChange += HandleGold;
        }
    }

    protected virtual void OnDisable()
    {
        if (LootManager.Instance != null)
        {
            LootManager.Instance.OnGoldChange -= HandleGold;
        }
    }

    protected virtual void HandleGold(int newGold)
    {
        goldTotal += newGold + playerPower.playerCurrentGoldBonus;
        goldText.text = "Gold: " + goldTotal.ToString();
        SoundFxManager.instance.PlaySoundFXClip(goldCollectSound, transform, 1f);
    }

    // save game
    public virtual void FromJson(string jsonString)
    {
        GameData obj = JsonUtility.FromJson<GameData>(jsonString);
        if (obj == null) return;
        this.goldTotal = obj.goldTotal;
    }
}
