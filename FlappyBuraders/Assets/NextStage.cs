using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlappyBuraders.GlobalStuff;

public class NextStage : MonoBehaviour
{
    public Text StageText;
    public Text subText;
    public Text P1Name;
    public Text P2Name;
    public Button yes;
    public Button no;

    void Start() {
        GameController.gameMode = (Enums.GameMode)PlayerPrefs.GetInt(Prefs.GAMEMODEint);
        if (GameController.gameMode == Enums.GameMode.ARCADE) {
            Debug.Log("Stage number: " + PlayerPrefs.GetInt(Prefs.STAGENUMBER));
            
            if (PlayerPrefs.GetInt(Prefs.YOUareGAMEOVER) == 0) {
                LocalData.DynamicDifficulty += 0.5f;
                if (LocalData.DynamicDifficulty > 3) {
                    LocalData.DynamicDifficulty = 3;
                }
                if (
                    (
                        PlayerPrefs.GetInt(Prefs.STAGENUMBER) == 9 
                    &&  (GameController.difficulty < 2 || PlayerPrefs.GetInt(Prefs.CONTINUE_COUNT, 0) > 0)
                    )
                || PlayerPrefs.GetInt(Prefs.STAGENUMBER) == 10) {
                    //end na
                    StageText.text = "";
                    subText.text = "";   
                    P1Name.text = "";
                    P2Name.text = "";
                    GameObject.Find("VSText").SetActive(false);
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Ending");
                
                }
                else {
                    AudioManager.instance.PlayBGM(BGMS.PRELUDE);
                    StageText.text = "Stage " + PlayerPrefs.GetInt(Prefs.STAGENUMBER);
                    subText.text = "Get ready for a bloody round...";
                    
                    if (PlayerPrefs.GetInt(Prefs.PLAYER1isHOOMAN) == 1) {
                        Enums.BuraderName you = (Enums.BuraderName) PlayerPrefs.GetInt(Prefs.FLAPPYONEint);
                        P1Name.text = you.ToString();
                        P2Name.text = LocalData.arcadeEnemies[PlayerPrefs.GetInt(Prefs.STAGENUMBER) - 1].ToString();
                    }
                    else {
                        Enums.BuraderName buraderType1 = (Enums.BuraderName) PlayerPrefs.GetInt(Prefs.FLAPPYTWOint);
                        P1Name.text = buraderType1.ToString();
                        P2Name.text = LocalData.arcadeEnemies[PlayerPrefs.GetInt(Prefs.STAGENUMBER) - 1].ToString();
                    }
                    StartCoroutine(GoFightNa());
                }
            }
            else {
                StageText.GetComponent<Animator>().enabled = false;
                StageText.text = "GAME OVER";
                subText.text = "So...Continue?";
                P1Name.text = "";
                P2Name.text = "";
                GameObject.Find("VSText").SetActive(false);
                yes.gameObject.SetActive(true);
                no.gameObject.SetActive(true); 
                AudioManager.instance.PlayVOX(VOXS.REMATCH);

                LocalData.DynamicDifficulty -= 1f;
                if (LocalData.DynamicDifficulty < 1) {
                    LocalData.DynamicDifficulty = 1;
                }
                if (PlayerPrefs.GetInt(Prefs.STAGENUMBER) > 4)
                    LocalData.toShowRateMe = true;
            }

        }
        else if (GameController.gameMode == Enums.GameMode.HEAD_TO_HEAD || GameController.gameMode == Enums.GameMode.TRAIN_YOUR_BURADER) {
            AudioManager.instance.PlayBGM(BGMS.PRELUDE);
            StageText.text = "HEAD TO HEAD!";
            subText.text = "Get ready for a bloody round...";
            
            Enums.BuraderName you = (Enums.BuraderName) PlayerPrefs.GetInt(Prefs.FLAPPYONEint);
            P1Name.text = you.ToString();
            Enums.BuraderName her = (Enums.BuraderName) PlayerPrefs.GetInt(Prefs.FLAPPYTWOint);
            P2Name.text = her.ToString();

            StartCoroutine(GoFightNa());
        }
        else if (GameController.gameMode == Enums.GameMode.SURVIVAL) {
            AudioManager.instance.PlayBGM(BGMS.PRELUDE);
            StageText.text = "Try to beat your best!";
            subText.text = "This is getting interesting...";
            Enums.BuraderName you;
            if (PlayerPrefs.GetInt(Prefs.PLAYER1isHOOMAN) == 1) {
                you = (Enums.BuraderName) PlayerPrefs.GetInt(Prefs.FLAPPYONEint);
            }
            else {
                you = (Enums.BuraderName) PlayerPrefs.GetInt(Prefs.FLAPPYTWOint);
            }
            P1Name.text = you.ToString();
            P2Name.text = "EVERYONE!";
            StartCoroutine(GoFightNa());
        }
        else if (GameController.gameMode == Enums.GameMode.HOLD_HANDS) {
            AudioManager.instance.PlayBGM(BGMS.PRELUDE);
            StageText.text = "Stay ALIVE as long as possible!";
            subText.text = "Find and get health cards if you can!";
            Enums.BuraderName you;
            Enums.BuraderName her;
            
            you = (Enums.BuraderName) PlayerPrefs.GetInt(Prefs.FLAPPYONEint);
            her = (Enums.BuraderName) PlayerPrefs.GetInt(Prefs.FLAPPYTWOint);
            P1Name.text = you.ToString() + "\r\n" + her.ToString();
            P2Name.text = "LANGAWS!";
            StartCoroutine(GoFightNa());
        }
        SaveController.SaveGame();
        Debug.Log("dynamic D: " + LocalData.DynamicDifficulty);
    }
    IEnumerator GoFightNa() {
        yield return new WaitForSeconds(2.8f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");
    }

    IEnumerator GoUwiNa() {
        yield return new WaitForSeconds(5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tips");
    }
    public void Continue() {
        AudioManager.instance.PlaySFX(SFXS.READY);
        PlayerPrefs.SetInt(Prefs.YOUareGAMEOVER, 0);
        // int nextstage = PlayerPrefs.GetInt(Prefs.STAGENUMBER);
        // nextstage--;
        // PlayerPrefs.SetInt(Prefs.STAGENUMBER, nextstage);
        LocalData.isContinue = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tips");
    }
    public void BackToMenu() {
        AudioManager.instance.PlaySFX(SFXS.TOGGLE);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tips");
    }
}
