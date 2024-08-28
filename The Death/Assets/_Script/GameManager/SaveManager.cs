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

        PlayerGold.instance.FromJson(jsonString1);
        PlayerPower.instance.FromJson(jsonString2);
        ShopManager.instance.FromJson(jsonString3);

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
}
