using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameRunning = false, gameOver = false, paddleBonus = false, pauseGame = false, gameWon = false;
    private int score = 0, levelNumber = 1;
    private AdsManager ad = new AdsManager();
    private BlockHandler[] blocks;
    private BallHandler ball;
    private PaddleHandler paddle;
    private Object currentLevel;
    private string levelName;
    public enum GameStates { NONE, RUNNING, PAUSED, OVER, WON }

    private void Start()
    {
        if (PlayerPrefs.GetInt("LevelNumber") > 1)
            levelNumber = PlayerPrefs.GetInt("LevelNumber");

        if (PlayerPrefs.GetInt("LevelNumber") == 0)
            PlayerPrefs.SetInt("LevelNumber", 1);

        blocks = FindObjectsOfType<BlockHandler>();
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        blocks = FindObjectsOfType<BlockHandler>();

        if (blocks.Length == 0 && gameRunning)
            SetGameWon();

        ball = FindObjectOfType<BallHandler>();
        paddle = FindObjectOfType<PaddleHandler>();
    }

    #region setters
    public void StartGame()
    {
        gameRunning = true;
    }

    public void SetGameRunning()
    {
        gameRunning = !gameRunning;
    }

    public void SetScore()
    {
        score += 100;
    }

    public void SetGameOver()
    {
        gameOver = true;
        ad.ShowInterstial();
    }

    public void SetLevelName(string name)
    {
        levelName = name;
    }

    public void SetLevel(Transform parent)
    {
        if (GameObject.FindWithTag("Level"))
        {
            DestroyObject(GameObject.FindWithTag("Level"));
        }
        currentLevel = Resources.Load("Prefabs/Levels/" + levelName);
        Instantiate(currentLevel, parent);
    }

    public void SetPaddleBonus()
    {
        paddleBonus = true;
    }

    public void SetPauseGame()
    {
        pauseGame = !pauseGame;
    }

    public void SetGameWon()
    {
        gameWon = true;
    }

    public void SetLevelNumber()
    {
        levelNumber++;
    }
    #endregion

    #region getters
    public bool GetGameRunning()
    {
        return gameRunning;
    }

    public int GetScore()
    {
        return score;
    }

    public bool GetGameOver()
    {
        return gameOver;
    }

    public bool GetPaddleBonus()
    {
        return paddleBonus;
    }

    public bool GetPauseGame()
    {
        return pauseGame;
    }

    public bool GetGameWon()
    {
        return gameWon;
    }

    public int GetLevelNumber()
    {
        return levelNumber;
    }
    #endregion

    public void ResetVariables()
    {
        gameOver = false;
        gameRunning = false;
        gameWon = false;
        paddleBonus = false;
        score = 0;
    }

    public void SaveData()
    {
        SetLevelNumber();
        PlayerPrefs.SetInt("LevelNumber", GetLevelNumber());
        PlayerPrefs.Save();
    }
}
