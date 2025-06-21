using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject WindowOne;
    public GameObject WindowTwo;

    void Start()
    {
        WindowTwo.SetActive(false);
    }
    public void GratyButton()
    {
        WindowTwo.SetActive(true);
        WindowOne.SetActive(false);
    }

    public void ReturnButton()
    {
        WindowTwo.SetActive(false);
        WindowOne.SetActive(true);
    }

    public void LevelOne()
    {
        SceneManager.LoadScene(1);
    }
    public void LevelTwo()
    {
        SceneManager.LoadScene(2);
    }public void LevelThree()
    {
        SceneManager.LoadScene(3);
    }

}
