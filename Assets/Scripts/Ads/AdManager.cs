using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    public InitializeAds initializeAds;
    public InterstitialAds interstitialAds;
    public RewardedAds rewardedAds;
    public BannerAds bannerAds;

    public static AdManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        bannerAds.LoadBannerAd();
        interstitialAds.LoadInterstitialAds();
        rewardedAds.LoadRewardedAds();
    }
}