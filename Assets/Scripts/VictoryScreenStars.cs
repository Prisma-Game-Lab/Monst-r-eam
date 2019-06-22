using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreenStars : MonoBehaviour
{
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    public Text victoryText1;
    public Text victoryText2;

    // Start is called before the first frame update
    void Start()
    {
        Star1.SetActive(false);
        Star2.SetActive(false);
        Star3.SetActive(false);
        if(Vitoria.ZeVit) {
            Star1.SetActive(true);
        }
        if(Vitoria.DedeVit) {
            Star2.SetActive(true);
        }
        if(Vitoria.ManeVit) {
            Star3.SetActive(true);
        }
        Debug.Log(Vitoria.WinCount);
        Vitoria.WinCount = 0; // pra nao bugar
        Morte.DeathCount = 0; // pra nao bugar

        //seta texto com numero da fase
        string str = (SaveSystem.GetInstance().currLevelPage + 1).ToString() + "-" + (SaveSystem.GetInstance().currLevelNumber + 1).ToString();
        if(victoryText1 != null)
        {
            victoryText1.text = str;
        }
        if(victoryText2 != null)
        {
            victoryText2.text = str;
        }
    }
}
