using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
// #if UNITY_ADS
using UnityEngine.Advertisements;
// #endif

public class UnityAdsManager : MonoBehaviour
{
    
    [Header("Config")]
    [SerializeField] private string gameID = "3298620";
    [SerializeField] private bool testMode = false;
    [SerializeField] private string rewardedVideoPlacementId;
    [SerializeField] private string regularPlacementId;
    [SerializeField] private string bannerPlacementId;

    public static UnityAdsManager instance;

    private void Awake() {
        if (instance == null)
			instance = this;
		else {
			Destroy(this.gameObject);
			return;
		}
		
		DontDestroyOnLoad(gameObject);
        if (!Advertisement.isInitialized)
            Advertisement.Initialize (gameID, testMode);
    }

    public string GetGameID() {
        return gameID;
    }
    public bool GetTestMode() {
        return testMode;
    }
    public string GetRewardedPlacementID() {
        return rewardedVideoPlacementId;
    }
    public string GetRegularPlacementID(){
        return regularPlacementId;
    }
    public string GetBannerPlacementID() {
        return bannerPlacementId;
    }
    
    
    // public void ShowRegularAd (Action<ShowResult>  callback) {
    //     if (Advertisement.IsReady(regularPlacementId)) {
    //         ShowOptions so = new ShowOptions();
    //         so.resultCallback = callback;
    //         Advertisement.Show(regularPlacementId, so);
    //     }
    //     else {
    //         Debug.Log("Not ready..wait");
    //     }
    // }
    // public void ShowRewardedAd (Action<ShowResult>  callback) {
    //     if (Advertisement.IsReady(rewardedVideoPlacementId)) {
    //         ShowOptions so = new ShowOptions();
    //         so.resultCallback = callback;
    //         Advertisement.Show(rewardedVideoPlacementId, so);
    //     }
    //     else {
    //         Debug.Log("Not ready..wait");
    //     }
    // }

    // public void ShowBannerAd() {
    //     StartCoroutine(ShowBannerAdRoutine());
    // }
    // public IEnumerator ShowBannerAdRoutine () {
    //     Debug.Log("Loading Banner");
    //     while (!Advertisement.IsReady(loadingBanner)) {
    //         yield return null;
    //     }
    //     Debug.Log("Showing Banner");
    //     Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
    //     Advertisement.Banner.Show(loadingBanner);
    //     yield return null;
    // }
}
