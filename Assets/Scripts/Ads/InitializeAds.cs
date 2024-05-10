using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string androidID;
    [SerializeField] private string iosID;
    [SerializeField] private bool isTesting;

    private string gameID;

    public void OnInitializationComplete()
    {
        Debug.Log("Initialized Ads");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {

    }

    private void Awake()
    {
        #if UNITY_IOS
        gameID = iosgameID;
        #elif UNITY_ANDROID
        gameID = androidID;

        #elif UNITY_EDITOR
            gameID = androidID;
        #endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameID, isTesting, this);
        }
    }
}
