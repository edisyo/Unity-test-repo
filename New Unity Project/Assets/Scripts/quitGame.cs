﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitGame : MonoBehaviour
{
    public void quit_Game(){
        FindObjectOfType<audioManager>().Play("Menu Button Press");
        Application.Quit();
    }
}
