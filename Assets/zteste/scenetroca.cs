using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenetroca : MonoBehaviour
{
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            count++;
            if(count == 3)
            {
                SceneManager.LoadScene("vit", LoadSceneMode.Additive);
            }
            
        }
    }
}
