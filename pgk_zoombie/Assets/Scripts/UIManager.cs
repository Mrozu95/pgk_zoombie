﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    GameObject[] pauseObjects;
    public static bool pauseState = false;
    public Text tekstSkill1;
    public Text tekstSkill2;
    public Text tekstSkill3;
    public Lvl_1_player_movment player;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
    }
	
	// Update is called once per frame
	void Update () {
        setSkillsLetters();
        if (Input.GetKeyDown(KeyCode.Escape) && player.canPause == true)
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pauseState = true;
                showPaused();
            }
            else if (Time.timeScale == 0 && player.canPause == true)
            {
                Debug.Log("high");
                Time.timeScale = 1;
                hidePaused();
                pauseState = false;
            }
        }
    }

    public void Reload()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    //controls the pausing of the scene
    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseState = false;
            hidePaused();
        }
    }

    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //loads inputted level
    public void LoadLevel(string level)
    {
        Application.LoadLevel(level);
    }

    public void setSterowanie(string wybor)
    {
        if(wybor=="strzalki")
        {
            Lvl_1_player_movment.sterowanie = true;
        }
        else if(wybor == "wsad")
        {
            Lvl_1_player_movment.sterowanie = false;
        }
    }

    public void setSkillsLetters()
    {
        if(Lvl_1_player_movment.sterowanie == true)
        {
            tekstSkill1.text = "Z";
            tekstSkill2.text = "X";
            tekstSkill3.text = "C";
        }
        else
        {
            tekstSkill1.text = "<";
            tekstSkill2.text = ">"; 
            tekstSkill3.text = "?"; 
        }
    }
}
