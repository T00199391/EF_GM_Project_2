using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    //Sets the menu or level select scene
    public void SetMenus()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
            
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(0);
        }
    }

    //Will load the game scene
    public void LoadGame()
    {
        SceneManager.LoadScene(2);
    }

    public void SetLevel(string name)
    {
        gm.SetLevelName(name);
    }
}
