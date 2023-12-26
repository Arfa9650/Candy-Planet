using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdInitializer : MonoBehaviour
{
    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
            Debug.Log("Ads Initialized");
        });
    }


}
