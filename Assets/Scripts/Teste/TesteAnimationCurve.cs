using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteAnimationCurve : MonoBehaviour
{
    [Header("Define o formato da curva no tempo.")]
    [Header("IMPORTANTE! Deixar ela sempre com valor no intervalo [0 (chão) e 1 (ápice), do tempo 0(inicio) ao 1 (final)]")]
    public AnimationCurve jumpCurve;

    [Header("As alturas e tempo de duração reais do pulo são definidas pelos valores abaixo")]
    public float MaxJumpHeight = 10.0f;
    public float JumpAirDuration = 1.0f;

    public float FloorDetectionRayDistance = 0.05f;

    public float JumpHeight;

    private bool IsJumping;
    
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
        PlayerInput.OnPress += TryToJump;
        //Debug.Log("Jumps" + PlayerInput.OnPress.GetInvocationList().GetLength(0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryToJump()
    {
        //faz raycasting pra checar se o lemming pode pular
        //TO-DO: implementar delayzinho

        int layerMask = LayerMask.GetMask("Default");
        float collsize = (coll2d.size.y / 2) * transform.lossyScale.y;
        Vector3 rayStartingPosition = new Vector3(transform.position.x, transform.position.y - collsize, transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(rayStartingPosition, Vector2.down, FloorDetectionRayDistance, layerMask);
        //não deixa se ainda houver uma corotina ativa controlando o pulo
        if(!IsJumping && (rb.velocity.y <= 0.1f ) && hit.collider != null)
        {
            StartCoroutine(ControlledJump());
        }
    }

    private IEnumerator ControlledJump()
    {
        //seta, para cada iteração, a velocidade desejada, conforme a curva
        IsJumping = true;
        float time  = 0.0f;
        while(time < JumpAirDuration)
        {
            Debug.Log(time);
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }
        IsJumping = false;
    }
}
