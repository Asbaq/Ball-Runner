﻿using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LevelEvents : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Level");
    }
    public void QuitGame()
    {
        Application.Quit();
    }


}
