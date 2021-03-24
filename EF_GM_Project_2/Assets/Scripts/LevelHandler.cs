using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        gm.SetLevel(transform);
    }

    //Resets the level to the beginning
    public void RestartLevel()
    {
        gm.SetLevel(transform);
        SceneManager.LoadScene(2);
        gm.ResetVariables();
    }
}
