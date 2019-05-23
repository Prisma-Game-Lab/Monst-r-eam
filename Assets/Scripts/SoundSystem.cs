using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    
    [Header("Esse script é só pra facilitar nossa vida pra botar os sons ONE-SHOT!")]
    [Header("Ex. pulo, botão, morte")]
    [Header("Contra-exemplo: música do menu, gameplay, créditos. Qualquer coisa que loope")]
    [Header("Esse script funciona baseado no nome dos gameobjects filhos! NÃO MEXER NOS NOMES! Grato")]
    private static SoundSystem instance;
    Dictionary<string, AudioSource> sounds;
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            //não pode haver duas instâncias disso (singleton)
            //porém, eu deleto o outro! Fiz isso pra ser possível usar referencias pelo inspector dentro de uma mesma cena
            GameObject.Destroy(instance.gameObject);
        }
        instance = this;
        sounds = new Dictionary<string, AudioSource>();
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            AudioSource audio = child.GetComponent<AudioSource>();
            if(audio != null)
            {
                sounds[child.gameObject.name] = audio;
            }
        }

        DontDestroyOnLoad(this.gameObject);
        Debug.Log(sounds.Count + "sounds loaded");
    }

    //toca o som que estiver carregado com esse nome. O nome é baseado no nome do gameObject em que estiver o AudioSource, lá nos filhos do soundSystem
    //testar se dá pra isso 
    public static void PlaySound(string name)
    {
        if(instance != null && instance.sounds.ContainsKey(name))
        {
            instance.sounds[name].Play();
        }
        else
        {
            Debug.LogWarning("Sound" + name + "couldnt be player because it was not properly loaded");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
