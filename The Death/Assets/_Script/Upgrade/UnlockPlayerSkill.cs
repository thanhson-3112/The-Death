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


    void Start()
    {
    }

    public void UnlockSkillMeteo()
    {
        meteo.gameObject.SetActive(true);
    }

    public void UnlockSkillFireSword()
    {
        fireSword.gameObject.SetActive(true);
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
