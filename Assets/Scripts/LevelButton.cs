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

    private bool ZeStar;
    private bool DedeStar;
    private bool ManeStar;
    public int CountStars;

    [Header("O numero da pagina e da do level associados a esse botão!")]
    [Tooltip("Indexado em 1! (GD friendly)")]
    public int levelPage;
    [Tooltip("Indexado em 1! (GD friendly)")]
    public int levelNumber;


    public Button myButton;


    private void Start()
    {
        
        SaveSystem ss = SaveSystem.GetInstance();
        PointsClass level = ss.pointSystem.GetLevelInfo(levelPage - 1, levelNumber - 1); //indexado em 1!
        Debug.Assert(level != null);

        //pega num de estrelas do save persistente
        CountStars = level.LevelPoints;
        Completed = level.cleared;

        //pega infos de quem passou vivo no save system
        ZeStar = level.charSaved[0];
        DedeStar = level.charSaved[1];
        ManeStar = level.charSaved[2];

        Star1.SetActive(false);
        Star2.SetActive(false);
        Star3.SetActive(false);
        
        if(ZeStar) {
            Star1.SetActive(true);
        }
        if(DedeStar) {
            Star2.SetActive(true);
        }
        if(ManeStar) {
            Star3.SetActive(true);
        }

        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(() => SoundSystem.PlaySound("click")); 
        //geralmente não gosto de trabalhar dessa maneira, e sim adicionando os eventos pelo inspector, o que considero mais legível e organizado. 
        //mas dessa vez esse atalho me salvou uns 20 minutos de clicar idiotamente. Ass: Krauss
    }

    //função helper para ser chamada a partir de um botão. Seta, no SaveSystem, a pagina e num desse level para o corrente
    public void SetLevelAsCurrent()
    {
        SaveSystem.GetInstance().currLevelPage = levelPage - 1;
        SaveSystem.GetInstance().currLevelNumber = levelNumber - 1;
    }
}