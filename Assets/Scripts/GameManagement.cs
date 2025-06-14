using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public void RestartOne()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void ContinueOne()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
