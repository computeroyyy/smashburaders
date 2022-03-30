using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FlappyBuraders.GlobalStuff;

public class PauseController : MonoBehaviour
{
    public Button wait;
    public Button ok;
    public Button back;
    public GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Pause() {
        AudioManager.instance.PlaySFX(SFXS.READY);
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void OK() {
        AudioManager.instance.PlaySFX(SFXS.TOGGLE);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void Back() {
        Time.timeScale = 1;
        AudioManager.instance.PlaySFX(SFXS.READY);
        LocalData.isContinue = false;
        PlayerPrefs.SetInt(Prefs.SHOW_ADS, 1);
        Debug.Log("Before Tips: " + LocalData.isContinue);
        SceneManager.LoadScene("Tips");
    }
}
