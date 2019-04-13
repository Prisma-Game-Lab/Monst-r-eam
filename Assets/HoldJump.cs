using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldJump : MonoBehaviour
{
   
    [Header("Valores de ajuste para um pulo que depende do tempo que o player segura o botão")]
    [Tooltip("A altura maxima do pulo")]
    public float MaxJumpHeight = 10.0f;
    [Tooltip("A altura base mínima do pulo. Mesmo o menor pulo vai atingir essa altura")]
    public float MinJumpHeight = 4.0f;
    [Tooltip("O tempo mínimo que o jogador deve segurar o botão para registrar um pulo")]
    public float MinHoldTime = 0.0f;
    [Tooltip("O tempo que resulta no pulo mais alto. Qualquer duração maior que essa vai ter a mesma altura")]
    public float MaxHoldTime = 2.0f;

    [Tooltip("Determina se o bixin deve cair mais rápido que o normal, PRA QUALQUER QUEDA")]
    public bool acceleratedFall;
    [Tooltip("Um multiplicador da gravidade que vai fazer com que o bixin caia mais rápido, EM QUALQUER QUEDA, se acceleratedFall == true")]
    public float fallMultiplier = 2.0f;


    public float FloorDetectionRayDistance = 0.4f;

    private Rigidbody2D rb;
    private CapsuleCollider2D coll2d;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //capsule collider?
        coll2d = GetComponent<CapsuleCollider2D>();

        //adds try to jump function to press callback
        //function TryToJump will be called every time "button is pressed", for all platforms
        PlayerInput.OnRelease += TryToJump;
        //Debug.Log("Jumps" + PlayerInput.OnPress.GetInvocationList().GetLength(0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //https://www.youtube.com/watch?v=7KiK0Aqtmzc
         //aumenta velocidade de queda, como no script lá
        
        if(acceleratedFall)
        {
            if(rb.velocity.y <= 0.0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime);
            }
        }
    }

    public void TryToJump(float holdDuration)
    {
        if(holdDuration < MinHoldTime) return;

        
        //faz raycasting pra checar se o lemming pode pular
        
        int layerMask = LayerMask.GetMask("Default");
        float collsize = (coll2d.size.y / 2) * transform.lossyScale.y;
        Vector3 rayStartingPosition = new Vector3(transform.position.x, transform.position.y - collsize, transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(rayStartingPosition, Vector2.down, FloorDetectionRayDistance, layerMask);
        //não deixa se ainda houver uma corotina ativa controlando o pulo
        if((rb.velocity.y <= 0.1f ) && hit.collider != null)
        {
            //interpola linearmente entre 0 e 1 o valor clamped da hold duration
            float jumpMultiplier = Mathf.Lerp(0.0f, 1.0f, Mathf.Clamp(holdDuration, MinHoldTime, MaxHoldTime));

            //pula de acordo com esse valor
            rb.velocity = new Vector2(rb.velocity.x, MinJumpHeight + jumpMultiplier * MaxJumpHeight);

        }
    }
}
