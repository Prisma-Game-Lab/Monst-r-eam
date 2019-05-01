using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStars : MonoBehaviour
{
    public List<LevelButton> lb;

    public void CheckStars()
    {
        if(Morte.DeathCount == 3) // E F I C I E N C I A
        {
            return;
        }
        foreach(LevelButton Lbutton in lb) // Procura o level que esta sendo jogado no momento
        {
            if(Lbutton.CurrentLevel) // Achei
            {
                if(!Lbutton.Completed) // Se ele nunca foi completado antes
                {
                    if(Vitoria.WinCount == 1)
                    {
                        Lbutton.Star1.SetActive(true);
                        Lbutton.CountStars = 1;
                    }
                    if (Vitoria.WinCount == 2)
                    {
                        Lbutton.Star1.SetActive(true);
                        Lbutton.Star2.SetActive(true);
                        Lbutton.CountStars = 2;
                    }
                    if (Vitoria.WinCount == 3)
                    {
                        Lbutton.Star1.SetActive(true);
                        Lbutton.Star2.SetActive(true);
                        Lbutton.Star3.SetActive(true);
                        Lbutton.CountStars = 3;
                    }
                    Lbutton.Completed = true;
                }
                else
                {
                    if(Vitoria.WinCount > Lbutton.CountStars) // Checa se o numero de vitoria atual eh melhor que o anteriror
                    {
                        if (Vitoria.WinCount == 2)
                        {
                            Lbutton.Star1.SetActive(true);
                            Lbutton.Star2.SetActive(true);
                            Lbutton.CountStars = 2;
                        }
                        if (Vitoria.WinCount == 3)
                        {
                            Lbutton.Star1.SetActive(true);
                            Lbutton.Star2.SetActive(true);
                            Lbutton.Star3.SetActive(true);
                            Lbutton.CountStars = 3;
                        }
                    }
                }
                Lbutton.CurrentLevel = false;
            }
        }
    }
}
