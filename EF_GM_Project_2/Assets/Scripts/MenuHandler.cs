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

    //Sets the menu scene
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Sets the level select scene
    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }

    //Will load the game scene
    public void LoadGame()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadStore()
    {
        SceneManager.LoadScene(3);
    }

    public void SetLevel(string name)
    {
        gm.SetLevelName(name);
    }
}
