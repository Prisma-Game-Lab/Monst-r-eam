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

    public float jumpforce = 0.625f;

    //[Range(0.0f,90.0f)]
    //[Tooltip("O angulo que determina o quanto o bixin pula pra frente num pulo. Medido a partir do eixo y, ou seja: 0 é puramente pra cima, 90 é puramente pra frente")]
    //public float ForwardAngulation = 0.0f;

    public float FloorDetectionRayDistance = 0.05f;

    private static bool AudioIsPlaying; //usada na corotina pra bloquear vários lemming emitindo som ao mesmo tempo

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

    void FixedUpdate()
    {
        if(PlayerInput.IsPressed())
        {
            TryToJump();
        }
    }

    private void TryToJump()
    {
        if(!this.gameObject.active) return;
        //faz raycasting pra checar se o lemming pode pular
        //TO-DO: implementar delayzinho

        int layerMask = LayerMask.GetMask("Default");
        float collsize = (coll2d.size.y / 2) * transform.lossyScale.y;
        Vector3 rayStartingPosition = new Vector3(transform.position.x, transform.position.y - collsize, transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(rayStartingPosition, Vector2.down, FloorDetectionRayDistance, layerMask);
        if((rd.velocity.y <= 0.1f ) && hit.collider != null)
        {
            //o ângulo em radianos
            //float angle = ForwardAngulation * Mathf.Deg2Rad;
            
            //angulação não está mais sendo usado

            //estamos usando add force, por hora. Por enquanto tem ido bem
            //usa angulação para direcionar o pulo mais pra frente
            //precisa saber pra qual lado estamos andando!
            // float fx = hm.facingRight ? jumpforce * Mathf.Sin(angle) : jumpforce * -Mathf.Sin(angle);
            // float fy = jumpforce * Mathf.Cos(angle);
            // rd.AddForce(new Vector2(fx, fy), ForceMode2D.Force);

            // if(bgsfgsdf)
            // {
            //     float jvalue = 380f;
            //     float fx = hm.facingRight ? jvalue * Mathf.Sin(angle) : jvalue * -Mathf.Sin(angle);
            //     float fy = jvalue * Mathf.Cos(angle);
            //     rd.AddForce(new Vector2(fx, fy), ForceMode2D.Force);
    
            // }
           
            rd.velocity = new Vector2(rd.velocity.x, jumpforce);
            
            
            

            //seta anim do pul0
            anim.SetBool("Fall", false);
            anim.SetBool("Jump", true);

            StartCoroutine(MakeJumpSound());
            
        }
    }

    private IEnumerator MakeJumpSound()
    {
        //se já estiver tocando, esquece
        if(AudioIsPlaying) yield return null;
        
        //senão, aviso que eu estou tocando o áudio agora, espero ele acabar, e desbloqueio
        //famosa "flags" / signal / farol de código crítico
        AudioIsPlaying = true;

        Debug.Log("som:" + gameObject.name + transform.GetSiblingIndex());

        //determino qual audio aleatório eu vou tocar
        int r = (int) Random.Range(1.0f, 4.999f); //até 4.99 pq são só 4 sons
        string str = "Jump" + r.ToString();
        Debug.Log(SoundSystem.GetInstance());
        Debug.Assert(SoundSystem.GetInstance().sounds.ContainsKey(str));
        SoundSystem.PlaySound(str);
        //espera acabar de tocar:
        yield return new WaitUntil(() => !SoundSystem.GetInstance().sounds[str].isPlaying);

        AudioIsPlaying = false;
        yield return null;
                
        
    }

    void OnDestroy()
    {
        //desinscreve as funções que eu inscrevi, evitando chamadas inválidas no futuro
        PlayerInput.OnPress -= TryToJump;
    }
}
