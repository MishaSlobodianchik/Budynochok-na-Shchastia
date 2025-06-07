// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;
// using JetBrains.Annotations;

// public class ResultController : MonoBehaviour
// {
//     [SerializeField] private int loadStars;
//     [SerializeField] private Text starText;
//     [SerializeField] private GameObject resultPanel;
//     [SerializeField] private GameObject gameInterface;
//     [SerializeField] private GameObject[] interfaceButtons;
//     private float currentLevelTime;

//     private int levelIndex;
    
//     public void LoadStars()
//     {
//         loadStars = PlayerPrefs.GetInt($"Level{levelIndex}");
//     }

//     public void SaveResult()
//     {
//         int stars = 0;
//         if (currentLevelTime <= timeToThreeStars)
//         {
//             stars = 3;
//         }
//         else if (score)
//         {
//             stars = 2;
//         }
//         else
//         {
//             stars = 1;
//         }
//         resultPanel.SetActive(true);
//         gameInterface.SetActive(false);
//         allTimeText.text = string.Format("Time: {0:N1} s", currentLevelTime);
//         starText.text = stars.ToString();
//         if (stars>loadStars)
//         {
//             PlayerPrefs.SetInt($"Level{levelIndex}", stars);
//         }
//         if (stars != 3)
//         {
//             interfaceButtons[1].GetComponent<Button>().interactable = false;
//         }
//     }

//         public void Restart()
//         {
//             SceneManager.LoadScene(levelIndex);
//         }

//         public void Continue()
//         {
//             if(levelIndex < SceneManager.sceneCountInBuildSettings)
//             {
//                 SceneManager.LoadScene(levelIndex + 1);
//             }
//             else
//             {
//                 SceneManager.LoadScene(0);
//             }
//         }

//         public void Menu()
//         {
//                 SceneManager.LoadScene(0);
//         }

//     }


