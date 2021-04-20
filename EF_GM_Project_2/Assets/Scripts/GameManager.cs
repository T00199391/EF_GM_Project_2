using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0, levelNumber = 1;
    private AdManager ads = new AdManager();
    private BlockHandler[] blocks;
    private BallHandler ball;
    private PaddleHandler paddle;
    private Object currentLevel;
    private string levelName;
    public enum GameStates { NONE, RUNNING, PAUSED, OVER, WON }
    private GameStates currentState = GameStates.NONE;
    private float gameTimer = 0.0f;
    string[] gameUserData;
    private bool powerUpActive = false;

    private void Start()
    {
        if (PlayerPrefs.GetInt("LevelsComplete") == 0)
        {
            PlayerPrefs.SetInt("LevelsComplete", 1);
            levelNumber = PlayerPrefs.GetInt("LevelsComplete");
        }
        else
        {
            levelNumber = PlayerPrefs.GetInt("LevelsComplete");
        }

        if(PlayerPrefs.GetString("CurrentLevel").Equals("") || PlayerPrefs.GetString("CurrentLevel") == null)
        {
            PlayerPrefs.SetString("CurrentLevel", "Level1");
            levelName = PlayerPrefs.GetString("CurrentLevel");
        }
        else
        {
            levelName = PlayerPrefs.GetString("CurrentLevel");
        }

        blocks = FindObjectsOfType<BlockHandler>();
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        blocks = FindObjectsOfType<BlockHandler>();

        //if there are no blocks in the game scene during the game running then the end game method will be called
        if (blocks.Length == 0 && currentState == GameStates.RUNNING)
            SetGameWon();

        ball = FindObjectOfType<BallHandler>();
        paddle = FindObjectOfType<PaddleHandler>();

        if(currentState == GameStates.RUNNING)
            gameTimer += Time.deltaTime;
    }

    #region setters
    //Sets the score fo rthe user when they destroy a block
    //The score will be determined by the amount time taken by the user to complete the level and what destroyed the block
    public void SetScore(string type)
    {
        if (type.Equals("Ball"))
        {
            if (gameTimer <= 30)
                score += 100;
            else if (gameTimer > 30 && gameTimer < 60)
                score += 70;
            else
                score += 40;
        }
        else
        {
            if (gameTimer <= 30)
                score += 80;
            else if (gameTimer > 30 && gameTimer < 60)
                score += 50;
            else
                score += 20;
        }
    }

    public void SetLevelName(string name)
    {
        levelName = name;
    }

    //Sets the level design based on the level name saved
    public void SetLevelDesign(Transform parent)
    {
        if (GameObject.FindWithTag("Level"))
        {
            Destroy(GameObject.FindWithTag("Level"));
        }
        currentLevel = Resources.Load("Prefabs/Levels/" + levelName);
        Instantiate(currentLevel, parent);
    }

    //sets the next level name after the user complets a level
    public void SetLevelName()
    {
        if (levelName.Equals("Level1"))
            levelName = "Level2";
        else if (levelName.Equals("Level2"))
            levelName = "Level3";
        else if (levelName.Equals("Level3"))
            levelName = "Level4";
        else if (levelName.Equals("Level4"))
            levelName = "Level5";
        else
            levelName = "";
    }

    //Sets the level number to be the next level
    public void SetLevelNumber()
    {
        int nextLevel = (int)System.Char.GetNumericValue(levelName[5]) + 1;

        if (levelNumber < nextLevel)
        {
            levelNumber = nextLevel;
        }
    }

    public void StartGame()
    {
        currentState = GameStates.RUNNING;
    }

    public void SetGameOver()
    {
        currentState = GameStates.OVER;
        ads.GameOver();
    }

    public void SetPauseGame()
    {
        if (currentState != GameStates.PAUSED)
            currentState = GameStates.PAUSED;
        else
            currentState = GameStates.RUNNING;
    }

    public void SetGameWon()
    {
        currentState = GameStates.WON;
    }

    public void SetPowerUpActive(bool pua)
    {
        powerUpActive = pua;
    }
    #endregion

    #region getters
    public int GetScore()
    {
        return score;
    }

    public int GetLevelNumber()
    {
        return levelNumber;
    }

    public string GetLevelName()
    {
        return levelName;
    }

    public GameStates GetCurrentState()
    {
        return currentState;
    }

    public bool GetPowerUpActive()
    {
        return powerUpActive;
    }
    #endregion

    public void ResetVariables()
    {
        currentState = GameStates.NONE;
        score = 0;
        gameTimer = 0;
        powerUpActive = false;
    }

    public void SaveData()
    {
        SetLevelNumber();
        SetLevelName();
        if (GetLevelNumber() > PlayerPrefs.GetInt("LevelsComplete"))
        {
            PlayerPrefs.SetInt("LevelsComplete", GetLevelNumber());
            PlayerPrefs.SetString("CurrentLevel", GetLevelName());
        }
    }
}
