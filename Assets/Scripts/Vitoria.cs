using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitoria : MonoBehaviour
{
    /* Testa colisao entre o objeto Player e a porta de saida,
       caso haja colisao o GameObject do Player se torna Inativo e o contador de vitorias sobe

       Autores: Vinny
    */

    public static int WinCount = 0;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            //queremos destruir ou só desativar?
            collider.gameObject.SetActive(false);
            WinCount++;
            Debug.Log("Ganhei");
        }
    }
}
