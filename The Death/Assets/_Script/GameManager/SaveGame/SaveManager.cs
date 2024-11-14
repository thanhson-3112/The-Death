using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private const string SAVE_1 = "save_1";
    private const string SAVE_2 = "save_2";
    private const string SAVE_3 = "save_3";

    private void Awake()
    {
        if (SaveManager.instance != null) Debug.LogError("Only 1 SaveManager allow");
        SaveManager.instance = this;
    }

    private void Start()
    {
        this.LoadSaveGame();
    }

    private void OnApplicationQuit()
    {
        this.SaveGame();
    }

    protected virtual string GetSaveName1()
    {
        return SaveManager.SAVE_1;
    }

    protected virtual string GetSaveName2()
    {
        return SaveManager.SAVE_2;
    }

    protected virtual string GetSaveName3()
    {
        return SaveManager.SAVE_3;
    }


    public virtual void LoadSaveGame()
    {
        string jsonString1 = SaveSystem.GetString(this.GetSaveName1());
        string jsonString2 = SaveSystem.GetString(this.GetSaveName2());
        string jsonString3 = SaveSystem.GetString(this.GetSaveName3());

        if (PlayerGold.instance != null && !string.IsNullOrEmpty(jsonString1))
        {
            PlayerGold.instance.FromJson(jsonString1);
        }

        if (PlayerPower.instance != null && !string.IsNullOrEmpty(jsonString2))
        {
            PlayerPower.instance.FromJson(jsonString2);
        }

        if (ShopManager.instance != null && !string.IsNullOrEmpty(jsonString3))
        {
            ShopManager.instance.FromJson(jsonString3);
        }

        Debug.Log("loadSaveGame" + jsonString1);
        Debug.Log("loadSaveGame" + jsonString2);
        Debug.Log("loadSaveGame" + jsonString3);

    }

    public virtual void SaveGame()
    {
        Debug.Log("SaveGame");
        string jsonString1 = JsonUtility.ToJson(PlayerGold.instance);
        string jsonString2 = JsonUtility.ToJson(PlayerPower.instance);
        string jsonString3 = JsonUtility.ToJson(ShopManager.instance);

        SaveSystem.SetString(this.GetSaveName1(), jsonString1);
        SaveSystem.SetString(this.GetSaveName2(), jsonString2);
        SaveSystem.SetString(this.GetSaveName3(), jsonString3);    
    }

    public virtual void NewSaveGame()
    {
        Debug.Log("New Save Game");

        PlayerGold.instance.goldTotal = 0;

        PlayerPower.instance.playerBaseDamage = 100f;
        PlayerPower.instance.playerBaseArmor = 10f;
        PlayerPower.instance.playerBaseMaxHealth = 30f;
        PlayerPower.instance.playerBaseHealthRegen = 0.1f;
        PlayerPower.instance.playerBaseSpeed = 15f;
        PlayerPower.instance.playerBasePickRadius = 5f;
        PlayerPower.instance.playerBaseCritChance = 10f;
        PlayerPower.instance.playerBaseAbilityHaste = 0.5f;
        PlayerPower.instance.playerBaseExperienceBonus = 10;
        PlayerPower.instance.playerBaseProjectiles = 1;
        PlayerPower.instance.playerBaseGoldBonus = 1;

        ShopManager.instance.currentDamageLevel = 0;
        ShopManager.instance.currentArmorLevel = 0;
        ShopManager.instance.currentMaxHealthLevel = 0;
        ShopManager.instance.currentHealthRegenLevel = 0;
        ShopManager.instance.currentSpeedLevel = 0;
        ShopManager.instance.currentPickRadiusLevel = 0;
        ShopManager.instance.currentCritChanceLevel = 0;
        ShopManager.instance.currentAbilityHasteLevel = 0;
        ShopManager.instance.currentExperienceBonusLevel = 0;
        ShopManager.instance.currentProjectilesLevel = 0;
        ShopManager.instance.currentGoldBonusLevel = 0;
        
        string jsonString1 = JsonUtility.ToJson(PlayerGold.instance);
        string jsonString2 = JsonUtility.ToJson(PlayerPower.instance);
        string jsonString3 = JsonUtility.ToJson(ShopManager.instance);


        SaveSystem.SetString(this.GetSaveName1(), jsonString1);
        SaveSystem.SetString(this.GetSaveName2(), jsonString2);
        SaveSystem.SetString(this.GetSaveName3(), jsonString3);

    }
}
