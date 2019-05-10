using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Script responsible for the read/write to the persistent info, saved in a json file
 * Author: André Mazal Krauss
 * 
 */
public class SaveSystem : MonoBehaviour
{
    [HideInInspector] //deve ficar escondido, só tiro enquanto programo, se eu esquecer podem por de volta pfv
    public PointSystem pointSystem;
    public PointSystem emptySave;
    
    public string saveFileName = "save";

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
        //tenta loadar
        if(!LoadState())
        {
            //se falhou, instancia novo save e o salva
            pointSystem = GameObject.Instantiate(emptySave);
            SaveState();
            string path = Path.Combine(Application.persistentDataPath, saveFileName + ".dat");
            Debug.Log("new save on path:" + path);
        }
        
        
    }

    public void SaveState()
    {
        string path = Path.Combine(Application.persistentDataPath, saveFileName + ".dat");
        string json_ps = JsonUtility.ToJson(pointSystem);
        using (StreamWriter streamWriter = File.CreateText (path))
        {
            streamWriter.Write (json_ps);
        }
    }

    //retorna true se conseguiu ler, senão false
    public bool LoadState()
    {
        string path = Path.Combine(Application.persistentDataPath, saveFileName + ".dat");
        //checar se existe save
        if(!File.Exists(path))
		{
			return false;
		}
        using (StreamReader streamReader = File.OpenText (path))
        {
            pointSystem = ScriptableObject.CreateInstance<PointSystem>();
            string jsonString = streamReader.ReadToEnd ();
            JsonUtility.FromJsonOverwrite(jsonString, pointSystem);
        }
        return true;
    }

    public void ClearState()
    {
        pointSystem = GameObject.Instantiate(emptySave); 
    }

    //ao sair do jogo, salvar estado
    void OnApplicationQuit()
    {
        SaveState();
    }

}
