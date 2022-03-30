using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FlappyBuraders.GlobalStuff;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenuController : MonoBehaviour
{
    public Button ArcadeButton;
    public Button HeadButton;
    public Button TrainButton;
    public Button SurvivalButton;
    public Button CoopButton;
    public Button SettingsButton;
    public GameObject ContinuePanel;

    void Start() {
        Application.targetFrameRate = 60;
        Time.timeScale = 1;
        StartCoroutine(sound());
        ContinuePanel.SetActive(false);
        Debug.Log(Application.persistentDataPath);
        
    }
    
    IEnumerator sound() {
        yield return new WaitForSeconds(0.3f);
        AudioManager.instance.PlaySFX(SFXS.HEAVYHIT); 
        AudioManager.instance.PlayBGM(BGMS.TITLE); 
    }
    public void Arcade() {
        if (PlayerPrefs.GetInt(Prefs.STAGENUMBER, 1) != 1) {
            AudioManager.instance.PlaySFX(SFXS.SPLASH); 
            ContinuePanel.SetActive(true);
        }
        else {
            PlayerPrefs.SetInt(Prefs.GAMEMODEint, (int) Enums.GameMode.ARCADE);
            AudioManager.instance.PlaySFX(SFXS.SPLASH); 
            SceneManager.LoadScene("BuraderSelect");   
        }
    }
    public void ArcadeContinue() {
        PlayerPrefs.SetInt(Prefs.GAMEMODEint, (int) Enums.GameMode.ARCADE);
        PlayerPrefs.SetInt(Prefs.FLAPPYONEint, (int) LocalData.arcadeLastPick);
        AudioManager.instance.PlaySFX(SFXS.SPLASH); 
        SceneManager.LoadScene("StageNumber");   
    }
    public void ArcadeStartOver() {
        PlayerPrefs.SetInt(Prefs.GAMEMODEint, (int) Enums.GameMode.ARCADE);
        AudioManager.instance.PlaySFX(SFXS.SPLASH); 
        SceneManager.LoadScene("BuraderSelect");   
    }
    public void Online() {
        PlayerPrefs.SetInt(Prefs.GAMEMODEint, (int) Enums.GameMode.ONLINE);
        AudioManager.instance.PlaySFX(SFXS.SPLASH); 
        SceneManager.LoadScene("BuraderSelect");   
    }
    public void HeadToHead() {
        PlayerPrefs.SetInt(Prefs.GAMEMODEint, (int) Enums.GameMode.HEAD_TO_HEAD);
        AudioManager.instance.PlaySFX(SFXS.SPLASH); 
        SceneManager.LoadScene("BuraderSelect");   
    }
    public void Train() {
        // PlayerPrefs.SetInt(Prefs.GAMEMODEint, (int) Enums.GameMode.TRAIN_YOUR_BURADER);
        AudioManager.instance.PlaySFX(SFXS.SPLASH); 
        SceneManager.LoadScene("Profiles");   
    }
    public void Survival() {
        PlayerPrefs.SetInt(Prefs.GAMEMODEint, (int) Enums.GameMode.SURVIVAL);
        AudioManager.instance.PlaySFX(SFXS.SPLASH); 
        SceneManager.LoadScene("BuraderSelect");   
    }
    public void Coop() {
        PlayerPrefs.SetInt(Prefs.GAMEMODEint, (int) Enums.GameMode.HOLD_HANDS);
        AudioManager.instance.PlaySFX(SFXS.SPLASH); 
        SceneManager.LoadScene("BuraderSelect");   
    }
    public void Settings() {
        AudioManager.instance.PlaySFX(SFXS.SPLASH); 
        // PlayerPrefs.SetInt(Prefs.GAMEMODEint, (int) Enums.GameMode);
        SceneManager.LoadScene("Settings");   
    }

    public void GoToSecret() {
        SceneManager.LoadScene("Secret");
    }

}
