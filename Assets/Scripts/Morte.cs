using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morte : MonoBehaviour
{
    /* Testa colisao entre o objeto Player e o Obstaculo,
       caso haja colisao o GameObject do Player se torna Inativo e o contador de mortes sobe

       Autores: Vinny
    */
    public static int DeathCount = 0;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var pd = collision.gameObject.GetComponent<PlayerDeath>();
            if(pd != null) pd.KillPlayer();

            collision.gameObject.SetActive(false);
            DeathCount++;
            Debug.Log("Perdi");
        }
    }
}
