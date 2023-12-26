using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class HUD : IntEventInvoker
{
    #region Fields

    [SerializeField]
    GameObject MainCanvas;

    [SerializeField]
    GameObject PauseCanvas;

    [SerializeField]
    public Image music;
    
    [SerializeField]
    public Image vfx;

    [SerializeField]
    AudioClip defaultClip;

    [SerializeField]
    Sprite Img1;
    
    [SerializeField]
    Sprite Img2;

    int score = 0;

    // These ad units are configured to always serve test ads.
    #if UNITY_ANDROID
        private string _adUnitId = "ca-app-pub-1201587525740665/1049635433";
    #elif UNITY_IPHONE
      private string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
    #else
      private string _adUnitId = "unused";
    #endif

    private InterstitialAd _interstitialAd;

    #endregion


    #region Methods

    /// <summary>
    /// Loads the interstitial ad.
    /// </summary>
    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                _interstitialAd = ad;
            });
    }

    /// <summary>
    /// Shows the interstitial ad.
    /// </summary>
    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }

    public void Music()
    {
        AudioManager.Play(AudioClipNames.Button);
        if(AudioManager.audioSource.clip != null)
        {
            AudioManager.audioSource.clip = null;
            music.sprite = Img2;
        }
        else
        {
            AudioManager.audioSource.clip = defaultClip;
            AudioManager.audioSource.Play();
            music.sprite = Img1;
        }
    }
    
    public void Vfx()
    {
        AudioManager.Play(AudioClipNames.Button);
        if (AudioManager.audioSource.mute == false)
        {
            AudioManager.audioSource.mute = true;
            vfx.sprite = Img2;
        }
        else
        {
            AudioManager.audioSource.mute = false;
            vfx.sprite = Img1;
        }
    }


    public void Play()
    {
        AudioManager.Play(AudioClipNames.Button);
        Time.timeScale = 1;
        AudioManager.audioSource.volume = 1f;
        SceneManager.LoadScene(1);
    }
    
    public void Restart()
    {
        LoadInterstitialAd();
        ShowInterstitialAd();

        AudioManager.Play(AudioClipNames.Button);
        Time.timeScale = 1;
        AudioManager.audioSource.volume = 1f;
        SceneManager.LoadScene(1);
    }
    
    public void Menu()
    {
        LoadInterstitialAd();
        ShowInterstitialAd();

        AudioManager.Play(AudioClipNames.Button);
        Time.timeScale = 1;
        AudioManager.audioSource.volume = 1f;
        SceneManager.LoadScene(0);
    }
    
    public void Pause()
    {
        AudioManager.Play(AudioClipNames.Button);
        Time.timeScale = 0;
        MainCanvas.SetActive(false);
        PauseCanvas.SetActive(true);
    }
    
    public void Resume()
    {
        LoadInterstitialAd();
        ShowInterstitialAd();

        AudioManager.Play(AudioClipNames.Button);
        Time.timeScale = 1;
        MainCanvas.SetActive(true);
        PauseCanvas.SetActive(false);
    }

    public void Leaderboard()
    {
        AudioManager.Play(AudioClipNames.Button);
        SceneManager.LoadScene(2);
    }
    
    public void Shop()
    {
        AudioManager.Play(AudioClipNames.Button);
        SceneManager.LoadScene(3);
    }

    #endregion
}
