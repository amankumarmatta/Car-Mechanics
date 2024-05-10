using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
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

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }

    public void LoadBannerAd()
    {
        BannerLoadOptions loadOptions = new BannerLoadOptions
        {
            loadCallback = BannerLoaded,
            errorCallback = BannerLoadError
        };

        Advertisement.Banner.Load(adUnitID, loadOptions);
    }

   

    public void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions
        {
            showCallback = BannerShown,
            clickCallback = BannerClicked,
            hideCallback = BannerHidden
        };

        Advertisement.Banner.Show(adUnitID, options);
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }

    private void BannerHidden()
    {

    }

    private void BannerClicked()
    {

    }

    private void BannerShown()
    {

    }

    private void BannerLoaded()
    {

    }

    private void BannerLoadError(string message)
    {
       
    }
}
