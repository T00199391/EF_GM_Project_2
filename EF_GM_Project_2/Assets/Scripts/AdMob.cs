using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMob : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    private PaddleHandler paddle;
    private bool paddleBonus;

    public void Start()
    {
        paddle = FindObjectOfType<PaddleHandler>();
        paddleBonus = true;

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
        this.RequestReward();
    }

    private void RequestBanner()
    {
    #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
    #elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
    #else
        string adUnitId = "unexpected_platform";
    #endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
            string adUnitId = "unexpected_platform";
#endif

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

    private void RequestReward()
    {
        string adUnitId;
    #if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";
    #elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
    #else
            adUnitId = "unexpected_platform";
    #endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void GameOver()
    {
        this.RequestInterstitial();
    }

    public bool GetPaddleBonus()
    {
        return paddleBonus;
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        paddle.Reward();
        paddleBonus = false;
    }

    public void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }
}
