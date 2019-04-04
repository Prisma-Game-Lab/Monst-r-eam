using UnityEngine;
using System.Collections;

public class GameObjScript : MonoBehaviour
{
    /* Classe para ser associada aos Lemmings. 
     *
     * Last update 03/04.
     * 
     * Permite identificar os Game Objects que o script "CheckIt" precisa.
     *
     * Autor: Menezes.
     */
    CheckIt parentScript;

    void Awake()
    {
        parentScript = transform.GetComponentInParent<CheckIt>();
    }

    void OnDisable()
    {
        parentScript.CheckObjStatus();
    }
}