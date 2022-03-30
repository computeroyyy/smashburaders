using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using FlappyBuraders.GlobalStuff;

public class BannerAdScript : MonoBehaviour {

    private string gameId = "3298620";
    private string placementId = "loadingBanner";
    private bool testMode = false;

    void Start () {
        // Advertisement.Initialize (gameId, testMode);
        DontDestroyOnLoad(this.gameObject);
        if (!LocalData.hasGonePRO){
            // StartCoroutine (ShowBannerWhenReady ());
        }
        if (!Advertisement.isInitialized)
            Advertisement.Initialize (gameId, testMode);
    }

    IEnumerator ShowBannerWhenReady () {
        while (!Advertisement.IsReady (placementId)) {
            yield return new WaitForSeconds (0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(placementId);
    }
}