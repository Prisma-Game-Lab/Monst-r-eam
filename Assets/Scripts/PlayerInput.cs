using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* a classe player input implementa, para várias possíveis plataformas, 
    as funcionalidades de interação do jogador com a gameplay. Dessa maneira, 
    todas as variações e potenciais ajustes cross-plataforma ficam aqui em vez de
    espalhados entre vários scripts. 
    Tudo é static, porque a ideia é ela funcionar como uma "referência global". Tipo uma intermediária
    entre os sistemas e a classe Input do próprio Unity

    autores: André Mazal Krauss, 
*/
public class PlayerInput : MonoBehaviour
{
    //ação chamada começa a interação
    public static Action OnPress = delegate { };
    
    //ação chamada ao fim da interação. Argumento float indica o tempo de duração da mesma
    public static Action<float> OnRelease = delegate { };
    
    // Start is called before the first frame update
    //o tempo em que começa o press
    private static float PressTime = 0.0f;

    //informa se está rolando (algum) press
    private static bool isPressed = false;

    void Start()
    {
        isPressed = false;
        PressTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //implementa chamadas para PC
        #if UNITY_STANDALONE || UNITY_EDITOR
        
        //considera key a ser pressionada como a key "jump"
        if(Input.GetButtonDown("Jump"))
        {
            isPressed = true;
            PressTime = Time.time;
            OnPress();
        }

        if(Input.GetButtonUp("Jump"))
        {
            isPressed = true;
            OnRelease(Time.time - PressTime);
        }
        #endif
        
        #if UNITY_IOS || UNITY_ANDROID

            //estou (arbitrariamente) considerando somente o primeiro touch na tela. Ou seja, 
            //para essa interação, é como se o multi-touch estivesse desabilitado

            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                isPressed = true;
                PressTime = Time.time;
                OnPress();
            }
            else if(Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled))
            {
                isPressed = true;
                OnRelease(Time.time - PressTime);
            }

        #endif
    }

    static bool IsPressed()
    {
        return isPressed;
    }
}
