using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HorizontalMovement))]
public class Jump : MonoBehaviour
{

    private Rigidbody2D rd;
    private CapsuleCollider2D coll2d;
    private HorizontalMovement hm;
    private Animator anim;

    public float jumpforce = 600f;

    [Range(0.0f,90.0f)]
    [Tooltip("O angulo que determina o quanto o bixin pula pra frente num pulo. Medido a partir do eixo y, ou seja: 0 é puramente pra cima, 90 é puramente pra frente")]
    public float ForwardAngulation = 0.0f;

    public float FloorDetectionRayDistance = 0.05f;

    public AudioSource tempJumpSound;

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        //capsule collider?
        coll2d = GetComponent<CapsuleCollider2D>();
        hm = GetComponent<HorizontalMovement>();
        anim = GetComponent<Animator>();
        

        //adds try to jump function to press callback
        //function TryToJump will be called every time "button is pressed", for all platforms
        PlayerInput.OnPress += TryToJump;
        //Debug.Log("Jumps" + PlayerInput.OnPress.GetInvocationList().GetLength(0));
    }

    // Update is called once per frame
    void Update()
    {
        
        //seta pra true se bixin está caindo, false cc
        anim.SetBool("Fall", rd.velocity.y <= -0.1f);
    }

    private void TryToJump()
    {
        //faz raycasting pra checar se o lemming pode pular
        //TO-DO: implementar delayzinho

        int layerMask = LayerMask.GetMask("Default");
        float collsize = (coll2d.size.y / 2) * transform.lossyScale.y;
        Vector3 rayStartingPosition = new Vector3(transform.position.x, transform.position.y - collsize, transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(rayStartingPosition, Vector2.down, FloorDetectionRayDistance, layerMask);
        if((rd.velocity.y <= 0.1f ) && hit.collider != null)
        {
            //o ângulo em radianos
            float angle = ForwardAngulation * Mathf.Deg2Rad;
            
            //estamos usando add force, por hora. Por enquanto tem ido bem
            //usa angulação para direcionar o pulo mais pra frente
            //precisa saber pra qual lado estamos andando!
            float fx = hm.facingRight ? jumpforce * Mathf.Sin(angle) : jumpforce * -Mathf.Sin(angle);
            float fy = jumpforce * Mathf.Cos(angle);
            rd.AddForce(new Vector2(fx, fy), ForceMode2D.Force);

            //seta anim do pul0
            anim.SetBool("Fall", false);
            anim.SetBool("Jump", true);

            if(tempJumpSound) tempJumpSound.Play();
        }
    }

    void OnDestroy()
    {
        //desinscreve as funções que eu inscrevi, evitando chamadas inválidas no futuro
        PlayerInput.OnPress -= TryToJump;        
    }
}
