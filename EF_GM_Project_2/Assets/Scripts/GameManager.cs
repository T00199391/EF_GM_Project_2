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
    FileHandler fileHandler = new FileHandler();

    private void Start()
    {
        string[] gameUserData = fileHandler.ReadTextFromFile();

        if(gameUserData.Length == 0 || gameUserData == null)
        {
            levelNumber = 1;
            levelName = "Level1";
        }
        else
        {
            levelNumber = gameUserData[0][0];
            levelName = gameUserData[1];
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
    //The score will be determined by the amount time taken by the user to complete the level
    public void SetScore()
    {
        if (gameTimer <= 30)
            score += 100;
        else if (gameTimer > 30 && gameTimer < 60)
            score += 70;
        else
            score += 40;
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
    #endregion

    public void ResetVariables()
    {
        currentState = GameStates.NONE;
        score = 0;
        gameTimer = 0;
    }

    public void SaveData()
    {
        Debug.Log(levelName + "  " + levelNumber);
        SetLevelNumber();
        SetLevelName();
        Debug.Log(levelName + "  " + levelNumber);
        fileHandler.AddTextToFile(GetLevelNumber().ToString(), GetLevelName().ToString());
    }
}
