using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> pagesContainers;
    public GameObject ButtonNext;
    public GameObject ButtonPrevious;
    private int currentPage;
    [HideInInspector]
    public List<int> TotalStars;
    public List<int> StarsToNextPage;
    [HideInInspector]
    public List<int> aux;
    //public GameObject[] ButtonsLevels;
    [HideInInspector]
    public List<bool> foimaltime;

    public Text TextNextLevel;
    public GameObject Cadeado;

    // Start is called before the first frame update
    void Start()
    {
        //To-do: fazer voltar para mesma página que o player estava jogando
        currentPage = SaveSystem.GetInstance().currLevelPage;
    }
    void Update()
    {
        //desliga todos, menos a página corrente, que deve ficar ligada
        //Pega o valor total de estrelas de cada pagina
        for(int i = 0; i < pagesContainers.Count;i++) 
        {
            if(aux.Count < 6) {
                aux.Add(0);
                TotalStars.Add(0);
                foimaltime.Add(false);
            }
            for(int j = 0; j < 6;j++) 
            {
                aux[i] += pagesContainers[i].transform.GetChild(0).gameObject.transform.GetChild(j).gameObject.GetComponent<LevelButton>().CountStars;
            }
            if(aux[i] > 0 && foimaltime[i] == false) {
                TotalStars[i] = aux[i];
                foimaltime[i] = true;
            }
        }
        if(currentPage < 4) {
            TextNextLevel.text = TotalStars[currentPage]+"/"+StarsToNextPage[currentPage];
        }
        else
        {
            TextNextLevel.text = "";
        }

        for(int i = 0; i < pagesContainers.Count; i++)
        {
            pagesContainers[i].SetActive(i == currentPage);
        }

        //ajusta os botões pra frente e pra trás, vendo se estamos na última página ou não
        if(currentPage == 0)
        {
            ButtonNext.SetActive(true);
            ButtonPrevious.SetActive(false);
        }
        else if(currentPage == pagesContainers.Count - 1)
        {
            ButtonNext.SetActive(false);
            ButtonPrevious.SetActive(true);
        }
        else
        {
            ButtonNext.SetActive(true);
            ButtonPrevious.SetActive(true);
        }
        if(TotalStars[currentPage] < StarsToNextPage[currentPage]) 
        {
            ButtonNext.SetActive(false);
        }
    }

    public void NextPage()
    {
        ButtonPrevious.SetActive(true);

        if(currentPage < pagesContainers.Count - 1)
        {
            pagesContainers[currentPage].SetActive(false);
            currentPage++;
            pagesContainers[currentPage].SetActive(true);   
        }

        if(currentPage == pagesContainers.Count - 1 || TotalStars[currentPage] < StarsToNextPage[currentPage])
        {
            ButtonNext.SetActive(false);
        }
        if(currentPage == 4) {
            for(int i = 0;i<6;i++) {
                pagesContainers[currentPage].transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.SetActive(false);
                Cadeado.SetActive(false);
            }
        }
    }

    public void PreviousPage()
    {
        ButtonNext.SetActive(true);

        if(currentPage > 0)
        {
            pagesContainers[currentPage].SetActive(false);
            currentPage--;
            pagesContainers[currentPage].SetActive(true);
        }

        if(currentPage == 0)
        {
            ButtonPrevious.SetActive(false);
        }
    }
}
