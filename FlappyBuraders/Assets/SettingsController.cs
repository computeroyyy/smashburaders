using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlappyBuraders.GlobalStuff;

public class SettingsController : MonoBehaviour
{
    public Text DifficultyText;
    public Text KillerFrequencyText;
    public Text PowerFrequencyText;
    public Text ControlSchemeText;
    public enum Difficulty {
        EASY,
        NORMAL,
        HARD,
        PRO,
        END
    }
    public Difficulty difficulty;

    public enum KillerFrequency {
        LOW,
        MID,
        HIGH,
        END
    }

    public KillerFrequency killerFrequency;

    public enum PowerFrequency {
        LOW,
        MID,
        HIGH,
        END
    }

    public PowerFrequency powerFrequency;

    public enum ControlScheme {
        CORNER,
        MIDDLE,
        END
    }

    public ControlScheme controlScheme;

    void Awake() {
        difficulty = (Difficulty) PlayerPrefs.GetInt(Prefs.DIFFICULTY, 0);
        killerFrequency = (KillerFrequency) PlayerPrefs.GetInt(Prefs.KILLERFREQ, 1);
        powerFrequency = (PowerFrequency) PlayerPrefs.GetInt(Prefs.POWERFREQ, 1);
        controlScheme = (ControlScheme) PlayerPrefs.GetInt(Prefs.CONTROLS, 1);
        SetPowerFName();
        SetDifficultyName();
        SetKillerFName();
        SetControlSchemeName();
    }
    void Start() {
        AudioManager.instance.PlayBGM(BGMS.SUBMERGED);
        SaveController.SaveGame();
    }

    public void SetDifficultyName() {
        switch (difficulty) {
            case Difficulty.EASY:
                DifficultyText.text = "1 - EZ PZ";
                break;
            case Difficulty.NORMAL:
                DifficultyText.text = "2 - Normal";
                break;
            case Difficulty.HARD:
                DifficultyText.text = "3 - HARD";
                break;
            case Difficulty.PRO:
                DifficultyText.text = "4 - PRO";
                break;
        }
    }

    public void SetKillerFName() {
        switch (killerFrequency) {
            case KillerFrequency.LOW:
                KillerFrequencyText.text = "Scarce";
                break;
            case KillerFrequency.MID:
                KillerFrequencyText.text = "Just Right";
                break;
            case KillerFrequency.HIGH:
                KillerFrequencyText.text = "Heavy Traffic";
                break;
            
        }
    }

    public void SetPowerFName () {
        switch (powerFrequency) {
            case PowerFrequency.LOW:
                PowerFrequencyText.text = "Scarce";
                break;
            case PowerFrequency.MID:
                PowerFrequencyText.text = "Just Right";
                break;
            case PowerFrequency.HIGH:
                PowerFrequencyText.text = "Heavy Traffic";
                break;
            
        }
    }

    public void SetControlSchemeName () {
        switch (controlScheme) {
            case ControlScheme.CORNER:
                ControlSchemeText.text = "1-Handed Mode"; break;
            case ControlScheme.MIDDLE:
                ControlSchemeText.text = "2-Handed Mode"; break;
        }
    }

    public void ToggleDifficulty(bool forward = true) {
        AudioManager.instance.PlaySFX(SFXS.TOGGLE);
        if (forward) {
            difficulty++;
            if (difficulty == Difficulty.END) {
                difficulty = 0;
            }
        }
        else {
            difficulty--;
            if (difficulty < 0) {
                difficulty = Difficulty.END - 1;
            }
        }
        SetDifficultyName();
    }
    public void ToggleKillF(bool forward = true) {
        AudioManager.instance.PlaySFX(SFXS.TOGGLE);
        if (forward) {
            killerFrequency++;
            if (killerFrequency == KillerFrequency.END) {
                killerFrequency = 0;
            }
        }
        else {
            killerFrequency--;
            if (killerFrequency < 0) {
                killerFrequency = KillerFrequency.END - 1;
            }
        }
        SetKillerFName();
    }
    public void TogglePowerF(bool forward = true) {
        AudioManager.instance.PlaySFX(SFXS.TOGGLE);
        if (forward) {
            powerFrequency++;
            if (powerFrequency == PowerFrequency.END) {
                powerFrequency = 0;
            }
        }
        else {
            powerFrequency--;
            if (powerFrequency < 0) {
                powerFrequency = PowerFrequency.END - 1;
            }
        }
        SetPowerFName();
    }

    public void ToggleControl(bool forward = true) {
        AudioManager.instance.PlaySFX(SFXS.TOGGLE);
        if (forward) {
            controlScheme++;
            if (controlScheme == ControlScheme.END) {
                controlScheme = 0;
            }
        }
        else {
            controlScheme--;
            if (controlScheme < 0) {
                controlScheme = ControlScheme.END - 1;
            }
        }
        SetControlSchemeName();
    }

    public void BackToMain() {
        AudioManager.instance.PlaySFX(SFXS.READY);
        PlayerPrefs.SetInt(Prefs.DIFFICULTY, (int)difficulty);
        PlayerPrefs.SetInt(Prefs.KILLERFREQ, (int)killerFrequency);
        PlayerPrefs.SetInt(Prefs.POWERFREQ, (int)powerFrequency);
        PlayerPrefs.SetInt(Prefs.CONTROLS, (int)controlScheme);
        LocalData.toSave = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
