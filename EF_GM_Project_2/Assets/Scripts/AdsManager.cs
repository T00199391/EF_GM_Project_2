using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.Advertisements;
using ShowResult = UnityEngine.Advertisements.ShowResult;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private string gameId = "1234567";
    private bool testMode = true;
    string placement_video = "Video_Ad";
    string placementId = "Banner_Ad";
    string placement_Interstitial = "Interstitia_Ad";

    public static AdsManager adsManager;
    private GameManager gm;
    private PaddleHandler paddle;
    private bool paddleBonus;

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
            gameId = "4048915";
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
            gameId = "4048914";
    }

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
        StartCoroutine(ShowBannerWhenReady());
        gm = FindObjectOfType<GameManager>();
        paddle = FindObjectOfType<PaddleHandler>();
        paddleBonus = true;
    }

    public bool GetPaddleBonus()
    {
        return paddleBonus;
    }

    //Display the reward ad
    public void DisplayvideoAd()
    {
        if (Advertisement.IsReady(placement_video))
        {
            Advertisement.Show(placement_video);
        }
        else
        {
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }

    //Shows the skipable ad
    public void ShowInterstial()
    {
        if (Advertisement.IsReady(placement_video))
        {
            Advertisement.Show(placement_Interstitial);
        }
        else
        {
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }

    //Shows the banner ad at the botton the screen
    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(placementId);
    }

    //What happens when the ads end
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            paddle.Reward();
            paddleBonus = false;
        }
    }

    #region Unity ads methods
    public void OnUnityAdsReady(string placementId)
    {
        
    }

    public void OnUnityAdsDidError(string message)
    {
        
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }
    #endregion
}