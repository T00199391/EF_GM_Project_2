using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.Advertisements;
using ShowResult = UnityEngine.Advertisements.ShowResult;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    #region Unity Ad Variables
    private string gameId = "1234567";
    private bool testMode = true;
    string placement_video = "Video_Ad";
    string placementId = "Banner_Ad";
    string placement_Interstitial = "Interstitia_Ad";
    #endregion

    #region AdMob variables
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    #endregion

    private PaddleHandler paddle;
    private bool paddleBonus, ibanner = false, ubanner = false;
    private GameManager gm;

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
            gameId = "4048915";
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
            gameId = "4048914";
    }

    void Start()
    {
        paddle = FindObjectOfType<PaddleHandler>();
        gm = FindObjectOfType<GameManager>();
        paddleBonus = true;

        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        this.RequestReward();

        //Show banner
        if (RandomAd() == 0)
        {
            if (ibanner)
            {
                this.interstitial.Destroy();
                ibanner = false;
            }
            StartCoroutine(ShowBannerWhenReady());
            ubanner = true;
        }
        else
        {
            if (ubanner)
            {
                Advertisement.Banner.Hide(true);
                ubanner = false;
            }
            this.RequestBanner();
            ibanner = true;
        }
    }

    #region Unity Ads methods
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

    public void OnUnityAdsReady(string placementId)
    {
        
    }

    public void OnUnityAdsDidError(string message)
    {
        
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            paddle.Reward();
            paddleBonus = false;
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
    #endregion

    #region AdMob methods
    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    private void RequestReward()
    {
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        paddle.Reward();
        paddleBonus = false;
    }

    private void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);

        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }
    #endregion

    private int RandomAd()
    {
        return Random.Range(0, 2);
    }

    public void DisplayReward()
    {
        if(RandomAd() == 0)
        {
            DisplayReward();
        }
        else
        {
            if (this.rewardedAd.IsLoaded())
            {
                this.rewardedAd.Show();
            }
        }
    }

    public void GameOver()
    {
        if (!gm.GetNoAds())
        {
            if (RandomAd() == 0)
            {
                ShowInterstial();
            }
            else
            {
                this.RequestInterstitial();
            }
        }
    }

    public bool GetPaddleBonus()
    {
        return paddleBonus;
    }
}
