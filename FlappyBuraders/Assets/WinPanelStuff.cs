using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FlappyBuraders.GlobalStuff;

public class WinPanelStuff : MonoBehaviour
{
    public Text WinnerMessageText;
    public Text WinnerNameText;
    public Text KillsText;
    public Button FlappyWinner;
    public Button TapToContinue;
    public Text GainedBP;
    bool isWin = false;
    void Start()
    {
        AudioManager.instance.UnMuteAll();
        AudioManager.instance.PlayBGM(BGMS.SUBMERGED);
        Time.timeScale = 1;
        LocalData.TotalMatches++;
        int deaths = 0;
        int score = 0;
        bool isHighscore = false;
        isWin = false;
        //IF TEAMWORK
        if (GameController.gameMode == Enums.GameMode.HOLD_HANDS) {
            WinnerMessageText.text = "Anyway, you killed...";
            score = PlayerPrefs.GetInt(Prefs.FLAPPYONE_KILLSint);
            WinnerNameText.text = score + " Flappy Flies.";
            if (score > LocalData.CoopHighScore) {
                LocalData.CoopHighScore = score;

                AudioManager.instance.PlayVOX(VOXS.HIGHSCORE);
                KillsText.text = "WOW! You get the high score! \r\nGo buy yourself a burger!";
                isHighscore = true;
                LocalData.toShowRateMe = true;
            }
            else {
                AudioManager.instance.PlayVOX(VOXS.BETTERLUCK);
                KillsText.text = "Last time you killed " + LocalData.CoopHighScore + " tho..";
            }
            Enums.BuraderName winner = (Enums.BuraderName) PlayerPrefs.GetInt(Prefs.WINNERburaderType);
            GameObject sprite;
            sprite = Resources.Load("Buraders/" + winner.ToString()) as GameObject;
            FlappyWinner.GetComponentInChildren<Image>().sprite = sprite.GetComponent<Image>().sprite;
        }
        //IF SURVIVAL
        else if (GameController.gameMode == Enums.GameMode.SURVIVAL) {
            WinnerMessageText.text = "Anyway, you killed...";
            score = PlayerPrefs.GetInt(Prefs.FLAPPYONE_KILLSint);
            WinnerNameText.text = score + " Buraders.";
            if (score > LocalData.SurvivalHighScore) {
                AudioManager.instance.PlayVOX(VOXS.HIGHSCORE);
                LocalData.SurvivalHighScore = score;
                KillsText.text = "WOW! You get the high score! \r\nGo buy yourself a burger!";
                isHighscore = true;
                LocalData.toShowRateMe = true;
            }
            else {
                AudioManager.instance.PlayVOX(VOXS.BETTERLUCK);
                KillsText.text = "Last time you killed " + LocalData.SurvivalHighScore + " tho..";
            }
            Enums.BuraderName winner = (Enums.BuraderName) PlayerPrefs.GetInt(Prefs.WINNERburaderType);
            GameObject sprite;
            sprite = Resources.Load("Buraders/" + winner.ToString()) as GameObject;
            FlappyWinner.GetComponentInChildren<Image>().sprite = sprite.GetComponent<Image>().sprite;
        }
        //IF HEAD TO HEAD and ARCADE
        else {
            WinnerMessageText.text = "And the winner is...";
            Enums.BuraderName winner = (Enums.BuraderName) PlayerPrefs.GetInt(Prefs.WINNERburaderType);
            WinnerNameText.text = ProfilesStuff.GetProfiles(winner).FullName + "!";
            
            if (PlayerPrefs.GetInt(Prefs.FLAPPYONE_KILLSint) < PlayerPrefs.GetInt(Prefs.FLAPPYTWO_KILLSint)) {
                //P2 WIN
                Debug.Log("P2 WIN");
                deaths = PlayerPrefs.GetInt(Prefs.FLAPPYONE_KILLSint);
                if (PlayerPrefs.GetInt(Prefs.WINNERisHOOMAN) == 1) {
                    AudioManager.instance.PlayVOX(VOXS.VICTORY);
                    PlayerPrefs.SetInt(Prefs.YOUareGAMEOVER, 0);
                    SecretController.AddBP += (GetMaxKill() - deaths) * 100;
                    isWin = true;
                }
                else {
                    AudioManager.instance.PlayVOX(VOXS.DEFEAT);
                    PlayerPrefs.SetInt(Prefs.YOUareGAMEOVER, 1);
                }
            }
            else {
                Debug.Log("P1 WIN");
                deaths = PlayerPrefs.GetInt(Prefs.FLAPPYTWO_KILLSint);
                if (PlayerPrefs.GetInt(Prefs.WINNERisHOOMAN) == 1) {
                    AudioManager.instance.PlayVOX(VOXS.VICTORY);
                    PlayerPrefs.SetInt(Prefs.YOUareGAMEOVER, 0);
                    SecretController.AddBP += (GetMaxKill() - deaths) * 100;
                    isWin = true;
                }
                else {
                    AudioManager.instance.PlayVOX(VOXS.DEFEAT);
                    PlayerPrefs.SetInt(Prefs.YOUareGAMEOVER, 1);
                }
            }
            KillsText.text = "Killed " + GetMaxKill() + " out of " + deaths + " deaths!";

            GameObject sprite;
            sprite = Resources.Load("Buraders/" + winner.ToString()) as GameObject;
            FlappyWinner.GetComponentInChildren<Image>().sprite = sprite.GetComponent<Image>().sprite;
        }
        
        //!gained BP
        if (GameController.gameMode == Enums.GameMode.ARCADE || GameController.gameMode == Enums.GameMode.HEAD_TO_HEAD) {
            GainedBP.text = "";
            GainedBP.text += "Special Kill(s): " + LocalData.SpecialKillCount.ToString() + " (+" + (LocalData.SpecialKillCount * 120) + "BP)" + "\r\n";
            GainedBP.text += "Technical Kill(s): " + LocalData.TechnicalKillCount.ToString() + " (+" + (LocalData.TechnicalKillCount * 100) + "BP)"+ "\r\n";
            if (isWin)
                GainedBP.text += "Deaths Difference: " + (GetMaxKill() - deaths).ToString() + " (+" + ((GetMaxKill() - deaths) * 50) + "BP)"+ "\r\n";
            SecretController.AddBP += (LocalData.SpecialKillCount * 120);
            SecretController.AddBP += (LocalData.TechnicalKillCount * 100);
            GainedBP.text += "You gained " + SecretController.AddBP + " BP.\r\n";
            if (PlayerPrefs.GetInt(Prefs.DIFFICULTY, 0) == 1) {
                SecretController.AddBP = (int)((float)SecretController.AddBP * 1.5f);
                GainedBP.text += "Difficulty Bonus +150%  = " + SecretController.AddBP + " BP";
            }
            else if (PlayerPrefs.GetInt(Prefs.DIFFICULTY, 0) == 2) {
                SecretController.AddBP *= 2;
                GainedBP.text += "Difficulty Bonus +200%  = " + SecretController.AddBP + " BP";
            }
            else if (PlayerPrefs.GetInt(Prefs.DIFFICULTY, 0) == 3) {
                SecretController.AddBP = (int)((float)SecretController.AddBP * 2.5f);
                GainedBP.text += "Difficulty Bonus +250%  = " + SecretController.AddBP + " BP";
            }
        }
        else if (GameController.gameMode == Enums.GameMode.HOLD_HANDS) {
            GainedBP.text = "";
            GainedBP.text += "Langaw Kill(s): " + score + " (+" + (score * 100) + "BP)" + "\r\n";
            if (isHighscore) {
                GainedBP.text += "HighScore Bonus: " + LocalData.CoopHighScore + " (+" + (SecretController.AddBP - (score * 100)) + "BP)" + "\r\n";
            }
            GainedBP.text += "You gained " + SecretController.AddBP + " BP.";
        }
        else if (GameController.gameMode == Enums.GameMode.SURVIVAL) {
            GainedBP.text = "";
            GainedBP.text += "Burader Kill(s): " + score + " (+" + ((score * 200)) + "BP)" + "\r\n";
            if (isHighscore) {
                GainedBP.text += "HighScore Bonus: " + LocalData.SurvivalHighScore + " (+" + (SecretController.AddBP - (score * 200)) + "BP)" + "\r\n";
            }
            GainedBP.text += "You gained " + SecretController.AddBP + " BP.";
        }
        
        //add BP to player
        PlayerPrefs.SetInt(Prefs.BP, PlayerPrefs.GetInt(Prefs.BP) + SecretController.AddBP); //pangJoke
        LocalData.BP += SecretController.AddBP;
        
        SecretController.AddBP = 0;
        LocalData.SpecialKillCount = 0;
        LocalData.TechnicalKillCount = 0;
        LocalData.toSave = true;

        GainedBP.text += "\r\nTotal BP: " + LocalData.BP;

        StartCoroutine(TapToContinueRoutine());
        SaveController.SaveGame();
    }
    IEnumerator TapToContinueRoutine() {
        yield return new WaitForSeconds(3f);
        TapToContinue.gameObject.SetActive(true);
    }

    public void Continue() {
        AudioManager.instance.PlaySFX(SFXS.READY);
        TapToContinue.gameObject.SetActive(false);
        if (GameController.gameMode == Enums.GameMode.ARCADE) {
            int nextstage = PlayerPrefs.GetInt(Prefs.STAGENUMBER);
            if (isWin){
                nextstage++;
                PlayerPrefs.SetInt(Prefs.STAGENUMBER, nextstage);
            }
            SceneManager.LoadScene("StageNumber");
        }
        else {
            SceneManager.LoadScene("Tips");
        }
    }
    private int maxKill;
    private int GetMaxKill() {
        switch (PlayerPrefs.GetInt(Prefs.KILLGOALint)) {
            case 0:
                return 1;
            case 1:
                return 2;
            case 2:
                return 3;
            default:
                return 5;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
