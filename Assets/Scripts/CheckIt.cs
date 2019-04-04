using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Objetivo: Criar um sistema de condições de vitória ou derrota.
 * 
 * Last update 03/04.
 * 
 * Proposta implementada -> Criei uma variavel chamada LemmingAlive que conta quantos Lemmings chegaram ao fim da fase.
 * -> A partir dela eu dou as condições de vitoria e derrota.
 * -> Os Lemming são posto em um array de Game Object.
 * -> Uma vez que todos os Game Objects do Array são desativados, seja por que terminaram a fase seja por que morreram,
 * a função EndGame é chamada.
 * 
 * ATENÇÃO!!! Os Lemming ainda não são destruidos no jogo, seja pelo "final da fase" seja pelo "inimigos".
 * 
 * Futuras implementações: Vitoria diferenciada de acordo com a quantidade de Lemming vivos.
 * 
 * Autor: Menezes.
 */

public class CheckIt : MonoBehaviour
{
    public int LemmingAlive = 0;

    public GameObjScript[] gameObjArray;

    public void Start()
    {
        PopulateGameObjArray();
    }

    /* Preenche o array com todos os Lemming. */
    void PopulateGameObjArray()
    {
        gameObjArray = GetComponentsInChildren<GameObjScript>();
    }

    /* Bool que significa se todos os Geme Objects (Lemming) estão ou não na cena. */
    bool AreAllGameObjInactive()
    {
        foreach (GameObjScript gameObj in gameObjArray)
        {
            if (gameObj.gameObject.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }

    /* Função Teste para ver se os Lemming estão sendo reconhecidos e desativados adequadamente. */
    public void CheckObjStatus()
    {
        if (AreAllGameObjInactive())
        {
            Debug.Log("All are inactive.");
        }
        else
        {
            Debug.Log("One of the objects are active");
        }
    }

    /* Função que acaba o jogo, se algum lemming sobreviver isso é chegar ao final da fase´load a cena de vitória, caso contrário a de derrota. */
    public void EndGame()
    {
        if (AreAllGameObjInactive())
        {
            if (LemmingAlive > 0)
            {
                //ChangeScene.VictoryScene(); Não estou conseguindo chamar a função.
            }
            else
            {
                //ChangeScene.LoseScene(); Não estou conseguindo chamar a função.
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        EndGame();
    }
}
/* A variável LemmingAlive é publica para pode ser altera quando for implentada o "fim de fase" aumentando o numero de Lemming que cheagram ao fim de fase. */