using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump_B : MonoBehaviour
{

    private Rigidbody2D rb;
    private CapsuleCollider2D coll2d;

    public float jumpVelocity = 10f;
    [Tooltip("Um multiplicador da gravidade que vai fazer com que o bixin caia mais rápido, EM QUALQUER QUEDA")]
    public float fallMultiplier = 2.0f;
    [Tooltip("Um multiplicador da gravidade que vai fazer com que o bixin caia mais rápido, quando o player soltar o botão de pulo")]
    public float lowJumpMultiplier = 2.0f;
    

    public float FloorDetectionRayDistance = 0.05f;

    public AudioSource tempJumpSound;

    private bool shouldTryToJump = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //capsule collider?
        coll2d = GetComponent<CapsuleCollider2D>();

        //adds try to jump function to press callback
        //function TryToJump will be called every time "button is pressed", for all platforms
        //PlayerInput.OnPress += TryToJump;
        //Debug.Log("Jumps" + PlayerInput.OnPress.GetInvocationList().GetLength(0));
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerInput.IsPressed())
        {
            TryToJump();
        }
    }

    void FixedUpdate()
    {
        //https://www.youtube.com/watch?v=7KiK0Aqtmzc
         //aumenta velocidade de queda, como no script lá
         if(rb.velocity.y <= 0.0f)
         {
             rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime);
         }
         else if(rb.velocity.y > 0.0f && !PlayerInput.IsPressed())
         {
             rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime);
         }

    }

    private void TryToJump()
    {
        Debug.Log("start:" + gameObject.name);
        //faz raycasting pra checar se o lemming pode pular
        //TO-DO: implementar delayzinho

        int layerMask = LayerMask.GetMask("Default");
        float collsize = (coll2d.size.y / 2) * transform.lossyScale.y;
        Vector3 rayStartingPosition = new Vector3(transform.position.x, transform.position.y - collsize, transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(rayStartingPosition, Vector2.down, FloorDetectionRayDistance, layerMask);
        if((rb.velocity.y <= 0.1f ) && hit.collider != null)
        {
            Debug.Log("JUMP!" + gameObject.name);
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);    
            
            if(tempJumpSound) tempJumpSound.Play();
        }

        Debug.Log("end:" + gameObject.name);
    }
}
