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
    private AdsManager ads;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        ads = FindObjectOfType<AdsManager>();
    }

    private void Update()
    {
        score.text = "Score: " + gm.GetScore().ToString();

        //deactivates the reward ad button
        bonusBtn.SetActive(ads.GetPaddleBonus());

        //Will set the canvas buttons to display
        if (gm.GetCurrentState() == GameManager.GameStates.PAUSED || gm.GetCurrentState() == GameManager.GameStates.OVER)
            gameOverGO.SetActive(true);
        else
            gameOverGO.SetActive(false);

        if(gm.GetCurrentState() == GameManager.GameStates.WON)
            gameWonGo.SetActive(true);
        else
            gameWonGo.SetActive(false);
    }

    //Will load the menu scene
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        gm.ResetVariables();
    }

    //Will load the level select scene
    public void LoadLevelSelect()
    {
        gm.SaveData();
        gm.ResetVariables();
        SceneManager.LoadScene(1);
    }
}
