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
    //private Collider2D collider;

    [Tooltip("Determina para qual lado começa o movimento")]
    public bool facingRight = true;
    [Tooltip("Determina a velocidade de movimento")]
    public float movementSpeed;
    [Tooltip("A distância da parede em que o lemming vira para o outro lado")]
    public float turnOnWallDetectionDistance;

    public float smoothTime = .05f;

    //pra que serve esse vector2? o brackeys usa ele na função smoothdamp, mas pra que?
    private Vector2 m_velocity = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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

        // Move the character by finding the target velocity
        //para direito caso facing right, senão para esquerda (= direita * -1)
        int dir = facingRight ? 1 : -1;
        Vector2 targetVelocity = new Vector2(movementSpeed * dir, rb.velocity.y);
        // And then smoothing it out and applying it to the character
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref m_velocity, smoothTime);

        //faz um raycast na direção do movimento e vê se precisa virar de direção
        int layerMask = LayerMask.GetMask("Default");
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir * Vector2.right, turnOnWallDetectionDistance, layerMask);
        if(hit.collider != null)
        {
            Flip();
        }
            
    }

    private void Flip()
    {
        facingRight = !facingRight;

        //truquezinho pra fazer o sprite virar pro outro lado
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
