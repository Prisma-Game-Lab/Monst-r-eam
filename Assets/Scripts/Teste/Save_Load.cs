using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//script bobão para testar funcionalidade de Load-Save usando PlayerPrefs, que acho que será suficiente para nosso projeto. Ass: Krauss
public class Save_Load : MonoBehaviour
{
    public InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //geralmente poderia botar tanto Save quanto Load como funções static, que recebem uma referência para a classe que contém as informações a serem salvas.
    // Mas esse exemplo é tão bobo que ia ficar só redundante, inútil e confuso dar essa volta
    
    public /*static */ void SaveState(/*informação a ser salva */)
    {
        if(inputField != null)
        {
            PlayerPrefs.SetString("characterName", inputField.text);
        }
        PlayerPrefs.Save();
    }

    public /*static */ void LoadState(/* referencia para lugar onde guardar info */)
    {
        if(inputField != null)
        {
            inputField.text = PlayerPrefs.GetString("characterName", "No player saved");
        }
    }
}
