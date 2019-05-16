using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
monobehaviour HorizontalMovement, 
implementa o movemento lateral dos lemmings, inclusive o comportamento de voltar quando bater numa parede
cada lemming com esse comportamento se desloca independente dos demais


autores: André Mazal Krauss, 
*/

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class HorizontalMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D coll2d;

    private Animator anim;

    [Tooltip("Determina para qual lado começa o movimento")]
    public bool facingRight = true;
    [Tooltip("Determina a velocidade de movimento")]
    public float movementSpeed;
    [Tooltip("Determina se o bixin, enquanto estiver no ar, deve \"quicar\" elasticamente na parede, ou bater de cara nela e parar")]
    public bool ShouldBounceOnWallWhileAirborne;
    [Tooltip("A distância da parede em que o lemming vira para o outro lado")]
    public float turnOnWallDetectionDistance;

    public float FloorDetectionRayDistance = 0.05f;

    public float smoothTime = .05f;

    //pra que serve esse vector2? o brackeys usa ele na função smoothdamp, mas pra que?
    private Vector2 m_velocity = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //capsule collider?
        coll2d = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();

       
        if(facingRight)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3( -Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //será que eu deveria fazer isso no fixed update?
        
        //o brackeys faz algo parecido nesse script:
        //https://github.com/Brackeys/2D-Character-Controller/blob/master/CharacterController2D.cs


        int dir = facingRight ? 1 : -1;

        //só seta movimentação lateral se estiver tocando no chão:
        //cheque com raycast pra ver se está grounded. Não me parece pesado demais para estar no update, o que acham? (Krauss)
        //pensei agora: será que basta checar se minha velocidade vertical é != 0? Tão simples, tão elegante!
        int layerMask = LayerMask.GetMask("Default");
        float collsize = (coll2d.size.y / 2) * transform.lossyScale.y;
        Vector3 rayStartingPosition = new Vector3(transform.position.x, transform.position.y - collsize, transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(rayStartingPosition, Vector2.down, FloorDetectionRayDistance, layerMask);

        if(hit.collider != null)
        {
            // Move the character by finding the target velocity
            //para direito caso facing right, senão para esquerda (= direita * -1)
            Vector2 targetVelocity = new Vector2(movementSpeed * dir, rb.velocity.y);
            // And then smoothing it out and applying it to the character
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref m_velocity, smoothTime);

        }
        //faz um raycast na direção do movimento e vê se precisa virar de direção
        
        RaycastHit2D hit_h = Physics2D.Raycast(transform.position, dir * Vector2.right, turnOnWallDetectionDistance, layerMask);
        if(hit_h.collider != null)
        {
            if(ShouldBounceOnWallWhileAirborne)
            {
                rb.velocity = new Vector2( -rb.velocity.x, rb.velocity.y);
            }
            Flip();
        }
        else if(rb.velocity.x * (facingRight? 1 : -1) < 0)
        {
            Flip();
        }
            
    }

    private void Flip()
    {
        facingRight = !facingRight;

        //truquezinho pra fazer o sprite virar pro outro lado
        //como alinhar isso com a animação? Vai precisar ainda?
        anim.SetBool("Rotate", true);
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    //um potencial problema disso é que eu não sei vai dar pra taggear todas as paredes com "wall"...
    //decidi fazer de outro jeito por causa disso, por enquanto pelo menos

    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     if(col.gameObject.CompareTag("Wall"))
    //     {
    //         this.Flip();
    //     }
    // }
}
