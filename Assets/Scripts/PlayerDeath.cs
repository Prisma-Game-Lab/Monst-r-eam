using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* 
    Triggera a morte do bixin, pra que ela tenha um comportamento uniforme independente do hazard que o matou
       Autores: Krauss, (Vinny)
*/

//combinar melhor com o Vinny essa divisão de tarefas aqui
//haha, isso só foi ser útil na última semana do projeto, só pra partícula de morte. 

[RequireComponent(typeof(Animator))]
public class PlayerDeath : MonoBehaviour
{

    public ParticleSystem deathParticle;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void KillPlayer()
    {
        Debug.Log("Kill Player!");
        anim.SetBool("Death", true);
        SoundSystem.PlaySound("morte");
        if(deathParticle != null) deathParticle.Play();
        deathParticle.gameObject.transform.SetParent(null, true); //detacha do bixin e mantem a posição global
        this.gameObject.SetActive(false);
    }
}
