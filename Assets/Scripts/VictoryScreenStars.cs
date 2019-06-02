using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreenStars : MonoBehaviour
{
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;
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
    }
}
