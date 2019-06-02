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
    public static bool ZeVit;
    public static bool DedeVit;
    public static bool ManeVit;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "ZeTag")
        {
            collider.gameObject.SetActive(false);
            ZeVit = true;
            Morte.ZeMorto = false;
            WinCount++;
            Debug.Log("Ganhei");
        }
        if (collider.gameObject.tag == "DedeTag")
        {
            collider.gameObject.SetActive(false);
            DedeVit = true;
            Morte.DedeMorto = false;
            WinCount++;
            Debug.Log("Ganhei");
        }
        if (collider.gameObject.tag == "ManeTag")
        {
            collider.gameObject.SetActive(false);
            ManeVit = true;
            Morte.ManeMorto = false;
            WinCount++;
            Debug.Log("Ganhei");
        }
    }
}
