using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(DisplayBannerAd());
    }

    public void StartBtn()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator DisplayBannerAd()
    {
        yield return new WaitForSeconds(1f);
        AdManager.instance.bannerAds.ShowBannerAd();
    }
}
