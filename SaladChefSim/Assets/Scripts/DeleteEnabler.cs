﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteEnabler : MonoBehaviour
{

    private Button deleteButton;

    private void Awake()
    {
        deleteButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SaveSystem.CheckForSave())
        {
            deleteButton.interactable = true;
        }
        else
        {
            deleteButton.interactable = false;
        }
    }
}
