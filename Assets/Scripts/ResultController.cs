using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class ResultController : MonoBehaviour
{
    [SerializeField] private int loadStars;
    [SerializeField] private int HorilkaToThreeStars;
    [SerializeField] private int CurrentHorilka;
    [SerializeField] private Text starText;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject[] interfaceButtons;
    private int levelIndex;
    void Start()
    {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        LoadStars();
    }
    public void LoadStars()
    {
        loadStars = PlayerPrefs.GetInt($"Level{levelIndex}");
    }

    public void SaveResult()
    {
        int stars = 0;
        if (CurrentHorilka <= HorilkaToThreeStars)
        {
            stars = 3;
        }
        else if (CurrentHorilka + 1 == HorilkaToThreeStars/2)
        {
            stars = 2;
        }
        else
        {
            stars = 1;
        }
        GameOverPanel.SetActive(true);
        starText.text = stars.ToString();
        if (stars>loadStars)
        {
            PlayerPrefs.SetInt($"Level{levelIndex}", stars);
        }
        if (stars != 3)
        {
            interfaceButtons[1].GetComponent<Button>().interactable = false;
        }
    }

        public void Restart()
        {
            SceneManager.LoadScene(levelIndex);
        }

        public void Continue()
        {
            if(levelIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(levelIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }

        public void Menu()
        {
                SceneManager.LoadScene(0);
        }

    }
