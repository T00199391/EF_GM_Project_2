using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour
{
    public Text score;
    private GameManager gm;
    public GameObject gameOverGO,bonusBtn,gameWonGo;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        score.text = "Score: " + gm.GetScore().ToString();

        //deactivates the reward ad button
        if (gm.GetPaddleBonus())
        {
            bonusBtn.SetActive(false);
        }

        //Will set the canvas buttons to display
        if (gm.GetPauseGame() || gm.GetGameOver())
            gameOverGO.SetActive(true);
        else
            gameOverGO.SetActive(false);

        gameWonGo.SetActive(gm.GetGameWon());
    }

    //Will load the menu scene
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Will load the level select scene
    public void LoadLevelSelect()
    {
        gm.SaveData();
        gm.ResetVariables();
        SceneManager.LoadScene(1);
    }
}
