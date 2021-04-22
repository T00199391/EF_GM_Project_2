using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;

public class PlayGameServices : MonoBehaviour
{
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Debug.Log("Play games initilised");
        SignInPlayer();
    }

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
    private void SignInPlayer()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (success) =>
         {
             switch(success)
             {
                 case SignInStatus.Success:
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

    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }
    #endregion

    #region Achievements
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

    public void ShowAchievements()
    {
        Social.ShowAchievementsUI();
    }
    #endregion
}
