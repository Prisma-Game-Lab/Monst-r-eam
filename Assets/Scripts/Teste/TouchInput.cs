using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    
    public GameObject firework;
    public GameObject firework2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touch began");
                Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
                pos.z = 0.0f;
                GameObject go = GameObject.Instantiate(firework2, pos, Quaternion.identity);
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                Debug.Log("touch moved");
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Touch ended");
                Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
                pos.z = 0.0f;
                GameObject go = GameObject.Instantiate(firework, pos, Quaternion.identity);
            }

        }
    }
}