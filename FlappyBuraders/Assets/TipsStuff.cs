using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlappyBuraders.GlobalStuff;
using UnityEngine.Advertisements;

public class TipsStuff : MonoBehaviour, IUnityAdsListener
{
    private string gameId = "3298620";
    private string rewardedVideoAd = "rewardedVideo";
    private string regularVideoAd = "video";
    private bool testMode = false;

    public Text theTip;
    private Button continueButton;
    
    void Awake() {
        continueButton = GetComponent<Button>();
        continueButton.interactable = false;
        continueButton.onClick.RemoveAllListeners();

        Advertisement.AddListener (this);
        if (!Advertisement.isInitialized)
            Advertisement.Initialize (gameId, testMode);
    }
    void Start()
    {
        //AudioManager.instance.StopAll();
        AudioManager.instance.PlayBGM(BGMS.TITLE);
        AudioManager.instance.PlayVOX(VOXS.KONOBANGUMI);
        theTip.text = PromptMessage.GetRandomTipsMessages().MainMessage;
        SaveController.SaveGame();
        
        Debug.Log("During Tips: " + LocalData.isContinue + " asd: " + PlayerPrefs.GetInt(Prefs.SHOW_ADS));

        if (LocalData.isContinue) {
            LocalData.isContinue = false;
            // LocalData.continueCount++;
            PlayerPrefs.SetInt(Prefs.CONTINUE_COUNT, PlayerPrefs.GetInt(Prefs.CONTINUE_COUNT) + 1);
            Debug.Log("continue count: " + PlayerPrefs.GetInt(Prefs.CONTINUE_COUNT));
            //continue many times.
            if (!LocalData.hasGonePRO) {
                if (PlayerPrefs.GetInt(Prefs.CONTINUE_COUNT) == 5) {
                    if (PlayerPrefs.GetInt(Prefs.CONTINUE_COUNT) == 5)
                        theTip.text = "Don't give up..you can win this rematch! After the ads. Huehue";
                    StartCoroutine(ShowRegularAdRoutine());
                }
                else if (PlayerPrefs.GetInt(Prefs.CONTINUE_COUNT) >= 2) {
                    if (PlayerPrefs.GetInt(Prefs.CONTINUE_COUNT) == 2)
                        theTip.text = "Ok, cool your head first and watch some cool ads before you continue.";
                    StartCoroutine(ShowRegularAdRoutine());
                }
                else {
                    StartCoroutine(ContinueRoutine()); 
                }
            }
            else {
                StartCoroutine(ContinueRoutine()); 
            }
        }
        else {
           
            PlayerPrefs.SetInt(Prefs.CONTINUE_COUNT, 0);
            if (GameController.gameMode == Enums.GameMode.SURVIVAL || GameController.gameMode == Enums.GameMode.HEAD_TO_HEAD || GameController.gameMode == Enums.GameMode.HOLD_HANDS) {
                if (Random.Range(0, 2) == 0 && !LocalData.hasGonePRO)
                    StartCoroutine(ShowRegularAdRoutine());
                else {
                    StartCoroutine(BackToMainRoutine()); 
                }
            }
            else
                StartCoroutine(BackToMainRoutine()); 
        }

        if (PlayerPrefs.GetInt(Prefs.STAGENUMBER) > 4)
            LocalData.toShowRateMe = true;
    }

    IEnumerator ContinueRoutine() {
        yield return new WaitForSeconds(5f);        
        continueButton.interactable = true;
        continueButton.GetComponentInChildren<Text>().text = "Tap To Continue";
        continueButton.onClick.AddListener(Continue);
    }

    private void Continue() {
        Advertisement.RemoveListener (this);
        UnityEngine.SceneManagement.SceneManager.LoadScene("StageNumber");
    }

    IEnumerator BackToMainRoutine() {
        yield return new WaitForSeconds(5f);        
        continueButton.interactable = true;
        continueButton.GetComponentInChildren<Text>().text = "Tap To Continue";
        continueButton.onClick.AddListener(BackToMain);
    }

    private void BackToMain() {
        PlayerPrefs.SetInt(Prefs.SHOW_ADS, 0);
        Advertisement.RemoveListener (this);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");    
    }

    IEnumerator ShowRegularAdRoutine() {
        yield return new WaitForSeconds(5f);
        continueButton.interactable = true;
        continueButton.GetComponentInChildren<Text>().text = "Tap To Continue";
        continueButton.onClick.AddListener(ShowRegularAd);
    }
    private void ShowRegularAd() {
        Advertisement.Show(regularVideoAd);
    }
    IEnumerator ShowRewardedAdRoutine() {
        yield return new WaitForSeconds(5);
        continueButton.interactable = true;
        continueButton.GetComponentInChildren<Text>().text = "Tap To Continue";
        continueButton.onClick.AddListener(ShowRewardedAd);
    }
    private void ShowRewardedAd() {
        Advertisement.Show(rewardedVideoAd);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady (string placementId) {
        if (placementId == rewardedVideoAd) {        
            continueButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {
        if (placementId == rewardedVideoAd) {
            switch (showResult) {
                case ShowResult.Finished:
                    if (PlayerPrefs.GetInt(Prefs.SHOW_ADS) == 1)
                        BackToMain();
                    else
                        Continue(); 
                    break;
                case ShowResult.Skipped:
                    theTip.text = "You HAVE to watch it to proceed. C'mon, it's not that long.";
                    break;
                case ShowResult.Failed:
                    Debug.LogError("UnityAds failed O__O");
                    if (PlayerPrefs.GetInt(Prefs.SHOW_ADS) == 1)
                        BackToMain();
                    else
                        Continue(); 
                    break;
            }
        }
        else {
            if (GameController.gameMode == Enums.GameMode.SURVIVAL || GameController.gameMode == Enums.GameMode.HEAD_TO_HEAD || GameController.gameMode == Enums.GameMode.HOLD_HANDS) {
                BackToMain();
            }
            else {
                Continue();
            }
        }
    }

    public void OnUnityAdsDidError (string message) {
        // Log the error.
        Debug.LogError("Worked on my machine");
        theTip.text = message;
        continueButton.interactable = true;
    }

    public void OnUnityAdsDidStart (string placementId) {
        // Optional actions to take when the end-users triggers an ad.
    } 
}
