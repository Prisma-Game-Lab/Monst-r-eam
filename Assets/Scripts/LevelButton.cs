using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Text LevelText;
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;
    public GameObject CompletedMarker;
    public bool Completed;
    public bool CurrentLevel; //acho que isso caiu em desuso com as mudanças que eu fiz. Ass. Krauss
    public int CountStars;

    [Header("O numero da pagina e da do level associados a esse botão!")]
    [Tooltip("Indexado em 1! (GD friendly)")]
    public int levelPage;
    [Tooltip("Indexado em 1! (GD friendly)")]
    public int levelNumber;


    private void Start()
    {
        
        SaveSystem ss = SaveSystem.GetInstance();
        PointsClass level = ss.pointSystem.GetLevelInfo(levelPage - 1, levelNumber - 1); //indexado em 1!
        Debug.Assert(level != null);

        //pega num de estrelas do save persistente
        CountStars = level.LevelPoints;
        Completed = level.cleared;

        //pega alguma outra coisa do save? Nome da cena? Nome da cena?

        CompletedMarker.SetActive(Completed);


        //vinny, é bom que todos eles sejam setados pra false! Senão se já estivessem todos true, não muda nada! Ass: Krauss
        //krauss, eu to ligado, eh que eu estava testando com os gameobjects desativados mesmo mas eh um bom ponto! Ass: Vinny
        Star1.SetActive(false);
        Star2.SetActive(false);
        Star3.SetActive(false);
        if(CountStars == 1)
        {
            Star1.SetActive(true);
        }
        if (CountStars == 2)
        {
            Star1.SetActive(true);
            Star2.SetActive(true);
        }
        if (CountStars == 3)
        {
            Star1.SetActive(true);
            Star2.SetActive(true);
            Star3.SetActive(true);
        }
    }

    //o que faz essa função? Ass: Dede
    public void CurrentLevelOn(string name)
    {
       
    }

    //função helper para ser chamada a partir de um botão. Seta, no SaveSystem, a pagina e num desse level para o corrente
    public void SetLevelAsCurrent()
    {
        SaveSystem.GetInstance().currLevelPage = levelPage - 1;
        SaveSystem.GetInstance().currLevelNumber = levelNumber - 1;
    }
}