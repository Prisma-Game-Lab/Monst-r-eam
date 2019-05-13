using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Morte_Vit : MonoBehaviour
{

    /* Verifica se o jogo acabou ao comparar o contador de vitorias e mortes com o numero de players
       se houver pelo menos uma vitoria, a cena de vitoria sera carregada, caso contrario
       a cena de derrota sera carregada.

        Autores: Vinny
    */

    public GameObject[] Lemmings;
    public ChangeStars cs;
    private int Count;

    // Start is called before the first frame update
    void Start()
    {
        Count = Lemmings.Length; // Pega o numero de Players
    }

    // Update is called once per frame
    void Update()
    {
        if(Count == (Vitoria.WinCount + Morte.DeathCount)) // Compara com o numero de vitorias e derrotas
        {
            End(); // funcao que troca de cena. // Só que não? Essa função não troca de cena... quem troca de cena é o botão Ass: Krauss
            Vitoria.WinCount = 0; // pra nao bugar
            Morte.DeathCount = 0; // pra nao bugar
            PlayerInput.Unregister();
        }
    }
    private void End()
    {
        if(Vitoria.WinCount > 0)
        {
			GameObject.Find("UIprefab").GetComponent<ChangeScene>().VictoryScreen();
            //seta no save que a fase foi ganha, e com tantas estrelas
            SaveSystem.GetInstance().SetScoreForCurrentLevel(Vitoria.WinCount, true);
            
        }
        else
        {
			GameObject.Find("UIprefab").GetComponent<ChangeScene>().LoseScreen();
		}
    }
}
