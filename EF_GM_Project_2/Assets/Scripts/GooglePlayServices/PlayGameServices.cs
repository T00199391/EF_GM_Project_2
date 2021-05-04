using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;

public class PlayGameServices : MonoBehaviour
{
    private GameManager gm;
    private int currentLevels;

    void Start()
    {
        Initialize();
        DontDestroyOnLoad(gameObject);
        gm = FindObjectOfType<GameManager>();
        currentLevels = 0;
    }

    public int GetCurrentLevels()
    {
        return currentLevels;
    }

    //Set up all config for the google play services
    //Call sign in method to authenticate the player
    private void Initialize()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().RequestServerAuthCode(false).EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Debug.Log("Play games initilised");
        SignInPlayer();
    }

    //Will determine the id for either the leadboard or the achievments
    private string DetermineId(string ln, string type)
    {
        if (type.Equals("Leaderboard"))
        {
            if (ln.Equals("Level1"))
                return "CgkI8erA-qUcEAIQAg";
            else if (ln.Equals("Level2"))
                return "CgkI8erA-qUcEAIQCQ";
            else if (ln.Equals("Level3"))
                return "CgkI8erA-qUcEAIQCg";
            else if (ln.Equals("Level4"))
                return "CgkI8erA-qUcEAIQCw";
            else if (ln.Equals("Level5"))
                return "CgkI8erA-qUcEAIQDA";
            else
                return "";
        }
        else
        {
            if (ln.Equals("Level1"))
                return "CgkI8erA-qUcEAIQAw";
            else if (ln.Equals("Level2"))
                return "CgkI8erA-qUcEAIQBA";
            else if (ln.Equals("Level3"))
                return "CgkI8erA-qUcEAIQBQ";
            else if (ln.Equals("Level4"))
                return "CgkI8erA-qUcEAIQBg";
            else if (ln.Equals("Level5"))
                return "CgkI8erA-qUcEAIQBw";
            else
                return "";
        }
    }

    #region SignIn
    //Authenticates the player
    //if sign in successful the the game data for the player will be loaded
    public void SignInPlayer()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (success) =>
        {
            switch (success)
            {
                case SignInStatus.Success:
                    gm.SetLoadData();
                    Debug.Log("Sign in player");
                    break;
                default:
                    Debug.Log("Didn't sign in player");
                    break;
            }
        });
    }
    #endregion

    #region Leaderboard
    //Once the game ends this method will be called to see if the leadboard will be updated
    public void UploadScore(int score, string levelName)
    {
        string id = DetermineId(levelName, "Leaderboard");

        if (!id.Equals(""))
        {
            Social.ReportScore(score, id, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Added score to leaderboard");
                }
                else
                {
                    Debug.Log("Didn't add score to leaderboard");
                }
            });
        }
        else
        {
            Debug.Log("ID not found");
        }
    }

    //Shows the leadboard UI
    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }
    #endregion

    #region Achievements
    //Will unlock an achievement is the player successfully complets an objective
    public void AchievementComplete(string levelName)
    {
        string id = DetermineId(levelName, "Achievement");
        if (!id.Equals(""))
        {
            Social.ReportProgress(id, 100.0f, (bool success) =>
             {
                 if (success)
                 {
                     Debug.Log("Achievement unlocked");
                 }
                 else
                 {
                     Debug.Log("Didn't unlock achievement");
                 }
             });
        }
        else
        {
            Debug.Log("ID not found");
        }

        if (levelName.Equals("Level5"))
        {
            Social.ReportProgress("CgkI8erA-qUcEAIQCA", 100.0f, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Achievement unlocked");
                }
                else
                {
                    Debug.Log("Didn't unlock achievement");
                }
            });
        }
    }

    //Will show the achievement UI
    public void ShowAchievements()
    {
        Social.ShowAchievementsUI();
    }
    #endregion

    #region Save Games
    private bool issaving = false;
    private string SAVE_NAME = "savegames";

    //Determines if the game should load or save player data to the cloud
    public void OpenSaveToCloud(bool saving)
    {
        if (Social.localUser.authenticated)
        {
            issaving = saving;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution
                (SAVE_NAME, GooglePlayGames.BasicApi.DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime, SavedGameOpen);
        }
    }

    private void SavedGameOpen(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (issaving)//if is saving is true we are saving our data to cloud
            {
                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(GetDataToStoreinCloud());
                SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(meta, update, data, SaveUpdate);
            }
            else//if is saving is false we are opening our saved data from cloud
            {
                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(meta, ReadDataFromCloud);
            }
        }
    }

    private void ReadDataFromCloud(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            string savedata = System.Text.ASCIIEncoding.ASCII.GetString(data);
            LoadDataFromCloudToOurGame(savedata);
        }
    }

    private void LoadDataFromCloudToOurGame(string savedata)
    {
        string[] data = savedata.Split('|');
        gm.LoadLevelNumber(Convert.ToInt32(data[0]));
        gm.SetNoAds(Convert.ToBoolean(data[1]));
    }

    private void SaveUpdate(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        //use this to debug whether the game is uploaded to cloud
        Debug.Log("successfully add data to cloud");
    }

    private string GetDataToStoreinCloud()//  we seting the value that we are going to store the data in cloud
    {
        string data = "";
        //data [0]
        data += gm.GetLevelNumber().ToString();
        data += "|";
        //data [1]
        data += gm.GetNoAds().ToString();
        data += "|"; 

        return data;
    }
    #endregion
}
