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
        ButtonPrevious.SetActive(false);
        //ButtonsLevels = GameObject.FindGameObjectsWithTag("ButtonLevel");
        foreach(GameObject page in pagesContainers)
        {
            page.SetActive(false);
        }
        if(pagesContainers.Count > 0)
        {
            pagesContainers[0].SetActive(true);
            currentPage = 0;
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
