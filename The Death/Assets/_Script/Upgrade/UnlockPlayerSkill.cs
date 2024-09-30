using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPlayerSkill : MonoBehaviour
{
    public static UnlockPlayerSkill Instance;

    public GameObject meteo;
    public GameObject fireSword;
    public GameObject lightning;
    public GameObject fireGun;
    public FireSwordController fireSwordController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UnlockSkillMeteo()
    {
        meteo.gameObject.SetActive(true);
    }

    public void UnlockSkillFireSword()
    {
        fireSword.gameObject.SetActive(true);
        fireSwordController.AddSword();
    }

    public void UnlockSkillLightning()
    {
        lightning.gameObject.SetActive(true);
    }

    public void UnlockSkillFireGun()
    {
        fireGun.gameObject.SetActive(true);
    }


}
