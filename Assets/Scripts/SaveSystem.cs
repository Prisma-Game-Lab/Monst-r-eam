using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
    
    private static string saveFileName = "save";
    private static string SavePath
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, saveFileName + ".dat");
        }
    }

    //usado para, a todo momento, saber o último level que foi jogado, ou está sendo jogado
    public int currLevelPage;
    public int currLevelNumber;

    public string currLevelString;

    //usado quando o countdown de um reset, quando ele não deve descarregar a cena
    public bool CountdownDontReloadScene;

    private static SaveSystem instance;
    public static SaveSystem GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        if(instance != null)
        {
            //não pode haver 2 desse script em cena
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            //tenta loadar
            if(!LoadState())
            {
                //se falhou, instancia novo save e o salva
                pointSystem = GameObject.Instantiate(emptySave);
                SaveState();
                string path = Path.Combine(Application.persistentDataPath, saveFileName + ".dat");
                Debug.Log("new save on path:" + path);

                currLevelPage = 0;
                currLevelNumber = 0;
            }        
        }
        
        DontDestroyOnLoad(this.gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
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

    //função helper pra setar score do level corrente. Só pra evitar gets/sets bobos
    public void SetScoreForCurrentLevel(int stars, bool cleared)
    {
        pointSystem.UpdateLevel(currLevelPage, currLevelNumber, stars, cleared);
    }

    public void ClearState()
    {
        pointSystem = GameObject.Instantiate(emptySave); 
    }

    //[MenuItem("OurGame/CleanSave")] por algum motivo doido isso quebra a build?
    public static void DeleteSaveFile()
    {
       File.Delete(SavePath);
       if(instance != null)
       {
           instance.pointSystem = GameObject.Instantiate(instance.emptySave);
       }
    }

    //ao sair do jogo, salvar estado
    void OnApplicationQuit()
    {
        SaveState();
    }

}
