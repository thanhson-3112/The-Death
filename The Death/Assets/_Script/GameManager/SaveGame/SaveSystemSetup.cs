﻿using UnityEngine;
using System.Collections;

public class SaveSystemSetup : MonoBehaviour {

	[SerializeField] private string fileName = "Profile.bin"; // file to save with the specified resolution
    [SerializeField] private bool dontDestroyOnLoad; // the object will move from one scene to another (you only need to add it once)

    void Awake()
	{
		SaveSystem.Initialize(fileName);
        if (dontDestroyOnLoad)
        {
            if (transform.parent != null)
            {
                transform.parent = null;
            }
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnApplicationQuit()
	{
		SaveSystem.SaveToDisk();
	}
}
