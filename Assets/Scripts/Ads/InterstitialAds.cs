using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidAdUnitID;
    [SerializeField] private string iOSUnitID;

    private string adUnitID;

    private void Awake()
    {
        #if UNITY_IOS
        adUnitID = iosadUnitID;
        #elif UNITY_ANDROID
        adUnitID = androidAdUnitID;
        #endif
    }

    public void LoadInterstitialAds()
    {
        Advertisement.Load(adUnitID, this);
    }

    public void ShowInterstitialAd()
    {
        Advertisement.Show(adUnitID, this);
        LoadInterstitialAds();
    }

    #region LoadCallBacks
    public void OnUnityAdsAdLoaded(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region ShowCallBacks
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message){ }

    public void OnUnityAdsShowStart(string placementId){ }

    public void OnUnityAdsShowClick(string placementId){}

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        
    }
    #endregion
}
