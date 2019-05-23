using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//script bobão só pra poder chamar a função estática PlaySound do script SoundSystem
public class SoundHelper : MonoBehaviour
{
    public void PlaySound(string name)
    {
        SoundSystem.PlaySound(name);
    }
}
