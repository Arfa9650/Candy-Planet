using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField input;

    public UnityEvent<string, int> submitScoreEvent;

        // These ad units are configured to always serve test ads.
    #if UNITY_ANDROID
        private string _adUnitId = "ca-app-pub-1201587525740665/8793228234";
    #elif UNITY_IPHONE
      private string _adUnitId = "ca-app-pub-3940256099942544/1712485313";
    #else
      private string _adUnitId = "unused";
    #endif

    private RewardedAd _rewardedAd;

    public void SubmitScore()
    {
        if (!string.IsNullOrWhiteSpace(input.text))
        {
            AudioManager.Play(AudioClipNames.Button);
            //PlayerPrefs.SetString("Username", input.text);
            //submitScoreEvent.Invoke(input.text, PlayerPrefs.GetInt("High Score"));
            LoadRewardedAd();
            ShowRewardedAd();
        }
    }

    public void ShowRewardedAd()
    {
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                PlayerPrefs.SetString("Username", input.text);
                submitScoreEvent.Invoke(input.text, PlayerPrefs.GetInt("High Score"));
            });
        }
    }

    /// <summary>
    /// Loads the rewarded ad.
    /// </summary>
    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                _rewardedAd = ad;
            });
    }


}
