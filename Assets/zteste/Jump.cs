using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    private Rigidbody2D rd;
    private CapsuleCollider2D coll2d;

    public float jumpforce = 600f;

    private float FloorDetectionRayDistance = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        //capsule collider?
        coll2d = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //esses dois ifs vão ser unificados assim que o Krauss fizer a classe PlayerInput


        //faz raycasting pra checar se o lemming pode pular
        //TO-DO: implementar delayzinho

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            rd.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Force);
        }

        if(Input.GetButtonDown("Jump"))
        {
            
            
            int layerMask = LayerMask.GetMask("Default");
            Debug.Log(coll2d.size.y);
            float collsize = (coll2d.size.y / 2) * transform.lossyScale.y;
            Debug.Log(collsize);
            Vector3 rayStartingPosition = new Vector3(transform.position.x, transform.position.y - collsize, transform.position.z);
            RaycastHit2D hit = Physics2D.Raycast(rayStartingPosition, Vector2.down, FloorDetectionRayDistance, layerMask);
            
            Debug.DrawRay(rayStartingPosition, Vector2.down * FloorDetectionRayDistance, Color.red, 0.5f, false);
            if(hit.collider != null)
            {
                rd.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Force);
            }

        }
    }
}
