using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Text LevelText;
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;
    public bool Completed;
    public bool CurrentLevel;
    public int CountStars;


    private void Start()
    {
        if(CountStars == 1)
        {
            Star1.SetActive(true);
        }
        if (CountStars == 2)
        {
            Star1.SetActive(true);
            Star2.SetActive(true);
        }
        if (CountStars == 3)
        {
            Star1.SetActive(true);
            Star2.SetActive(true);
            Star3.SetActive(true);
        }
    }
    public void CurrentLevelOn(string name)
    {
       
    }
}
