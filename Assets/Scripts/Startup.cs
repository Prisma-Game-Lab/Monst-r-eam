using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class Startup {
    
    public static GameObject SaveSystemPrefab;

    static Startup()
    {
        SaveSystem ss = (SaveSystem) GameObject.FindObjectOfType(typeof(SaveSystem));
        if(ss == null)
        {
            //instancia save system na cena:
            GameObject.Instantiate(SaveSystemPrefab);
        }
    }
}