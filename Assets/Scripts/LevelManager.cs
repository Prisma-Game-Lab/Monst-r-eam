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
        /* COMENTADO PARA BUILDAR
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
        */
        
        currentPage++;

        UpdateButtons(); //atualiza textos
    }

    public void PreviousPage()
    {
        /* COMENTADO PARA BUILDAR
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
        */


        currentPage--;
        UpdateButtons(); //atualiza textos
    }

    #region RushParaBuildar
    public void LoadLevel(int button){
        //gambiarra pro level select funcionar na build
        GameObject.Find("-----General-----").transform.Find("Game Manager").GetComponent<ChangeScene>().ToLoadScene("Level" + (currentPage+1) + "-" + button);
    }

    public void UpdateButtons(){
        if(currentPage >= 4){
            ButtonNext.SetActive(false);
            ButtonPrevious.SetActive(true);
        }

        else if(currentPage <= 0){
            ButtonNext.SetActive(true);
            ButtonPrevious.SetActive(false);
        }

        else{
            ButtonNext.SetActive(true);
            ButtonPrevious.SetActive(true);
        }

        List<string> scenesInBuild = new List<string>(new string[]{"Level0", "Level1-1","Level1-2",
        "Level1-3","Level1-4","Level1-5","Level1-6","Level2-1","Level2-2","Level2-3",
        "Level3-1","Level4-1"});

        foreach(Transform child in GameObject.Find("-----UI-----").transform.Find("Buttons")){
            string scene = "Level" + (currentPage+1) + "-" + (child.GetSiblingIndex()+1);
            child.Find("Text").GetComponent<Text>().text = scene;
            child.GetComponent<Button>().interactable = scenesInBuild.Contains(scene);
        }
    }
    #endregion
}
