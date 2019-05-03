using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* 
    Triggera a morte do bixin, pra que ela tenha um comportamento uniforme independente do hazard que o matou
       Autores: Krauss, (Vinny)
*/

//combinar melhor com o Vinny essa divisão de tarefas aqui

[RequireComponent(typeof(Animator))]
public class PlayerDeath : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void KillPlayer()
    {
        anim.SetBool("Death", true);
    }
}
