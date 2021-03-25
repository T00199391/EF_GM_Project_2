using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0, levelNumber = 1;
    private UnityAds ad = new UnityAds();
    private AdMob adMob = new AdMob();
    private BlockHandler[] blocks;
    private BallHandler ball;
    private PaddleHandler paddle;
    private Object currentLevel;
    private string levelName;
    public enum GameStates { NONE, RUNNING, PAUSED, OVER, WON }
    private GameStates currentState = GameStates.NONE;

    private void Start()
    {
        if (PlayerPrefs.GetInt("LevelNumber") > 1)
            levelNumber = PlayerPrefs.GetInt("LevelNumber");

        if (PlayerPrefs.GetInt("LevelNumber") == 0)
            PlayerPrefs.SetInt("LevelNumber", 1);

        if (PlayerPrefs.GetString("LevelName").Equals(""))
            levelName = "level1";
        else
            levelName = PlayerPrefs.GetString("LevelName");

        blocks = FindObjectsOfType<BlockHandler>();
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        blocks = FindObjectsOfType<BlockHandler>();

        if (blocks.Length == 0 && currentState == GameStates.RUNNING)
            SetGameWon();

        ball = FindObjectOfType<BallHandler>();
        paddle = FindObjectOfType<PaddleHandler>();
    }

    #region setters
    public void SetScore()
    {
        score += 100;
    }

    public void SetLevelName(string name)
    {
        levelName = name;
    }

    public void SetLevel(Transform parent)
    {
        if (GameObject.FindWithTag("Level"))
        {
            Destroy(GameObject.FindWithTag("Level"));
        }
        currentLevel = Resources.Load("Prefabs/Levels/" + levelName);
        Instantiate(currentLevel, parent);
    }

    public void SetLevelNumber()
    {
        for(int i = 0; i < levelName.Length; i++)
        {
            if(levelName[i] >= '1' && levelName[i] <= '4')
            {
                if (levelNumber < levelName[i] + 1)
                    levelNumber++;
            }
        }
    }

    public void StartGame()
    {
        currentState = GameStates.RUNNING;
    }

    public void SetGameOver()
    {
        currentState = GameStates.OVER;
        //ad.ShowInterstial();
        adMob.GameOver();
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

    public GameStates GetCurrentState()
    {
        return currentState;
    }
    #endregion

    public void ResetVariables()
    {
        currentState = GameStates.NONE;
        score = 0;
    }

    public void SaveData()
    {
        SetLevelNumber();
        PlayerPrefs.SetInt("LevelNumber", GetLevelNumber());
        PlayerPrefs.Save();
    }
}
