using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ferramenta para ajudar os gds a testarem o jogo em velocidades menores (pra garantir que coisas sejam difíceis mas não impossíveis)
/// </summary>
public class ChangeTimeScale : MonoBehaviour
{
    [Range(0,1)] public float timeScale;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = timeScale;
    }
}
