using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Walk : MonoBehaviour
{


    public bool direction;
    public float velocidade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(direction == true) //Direita
        {
            transform.Translate(Vector3.right * Time.deltaTime * velocidade, Camera.main.transform);
        }
        if (direction == false) //Esquerda
        {
            transform.Translate(Vector3.left * Time.deltaTime * velocidade, Camera.main.transform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "parede")
        {
            if(direction == true)
            {
                direction = false;
            }
            else
            {
                direction = true;
            }
        }
        if(collision.gameObject.tag == "Finish")
        {
            Destroy(this.gameObject);
        }
    }

    
}
