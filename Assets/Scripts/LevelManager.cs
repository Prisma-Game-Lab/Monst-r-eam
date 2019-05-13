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
    //public GameObject[] ButtonsLevels;

    // Start is called before the first frame update
    void Start()
    {
        
        //To-do: fazer voltar para mesma página que o player estava jogando
        currentPage = SaveSystem.GetInstance().currLevelPage;
        //desliga todos, menos a página corrente, que deve ficar ligada
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

        if(currentPage == pagesContainers.Count - 1)
        {
            ButtonNext.SetActive(false);
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
