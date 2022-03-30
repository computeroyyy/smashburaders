using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FlappyBuraders.GlobalStuff;

public class TutorialManager : MonoBehaviour
{
    public GameController gameController;
    public GameObject tutorialPanel;
    public Text tutorialText;
    public Text buttonText;
    public Image[] tutorialImages;
    public static bool ChainsawBarrage = false;
    public static bool isTutorial = false;
    public Text TutorialReminderText;


    public static int tutorialSeq = 0;
    void Start()
    {
        toSmash = false;
        TutorialReminderText.text = "";
        isTutorial = true;
        GameController.difficulty = 0;
        PlayerPrefs.SetInt(Prefs.GAMEMODEint, 1);
        PlayerPrefs.SetInt(Prefs.KILLGOALint, 2);
        PlayerPrefs.SetInt(Prefs.STAGEtype, 2);
        PlayerPrefs.SetInt(Prefs.FLAPPYONEint, 0);
        PlayerPrefs.SetInt(Prefs.FLAPPYTWOint, 3);
        PlayerPrefs.SetInt(Prefs.PLAYER1isHOOMAN, 1);
        PlayerPrefs.SetInt(Prefs.PLAYER2isHOOMAN, 0);
        PlayerPrefs.SetInt(Prefs.STAGENUMBER, 1);
        PlayerPrefs.SetInt(Prefs.YOUareGAMEOVER, 0);
        
        Debug.Log("tutSeq: " + tutorialSeq);
        switch (tutorialSeq) {
            case 0:
                tutorialPanel.SetActive(false);
                AudioManager.instance.PlayBGM(BGMS.SILENT);
                StartCoroutine(Tutorial00());
                break;
            case 1:
                tutorialPanel.SetActive(true);
                gameController.FlappyTwo.gameObject.SetActive(false);
                Time.timeScale = 0;
                tutorialText.text = "Press the buttons below to move your character left or right.";
                buttonText.text = "Fair enough.";
                ShowTutImage(0);
                tutorialSeq += 100;
                break;
            case 101:
                tutorialPanel.SetActive(true);
                gameController.FlappyTwo.gameObject.SetActive(false);
                Time.timeScale = 0;
                tutorialText.text = "Tapping it once will make your character jump to that direction";
                ShowTutImage(1);
                break;
            case 102:
                tutorialPanel.SetActive(true);
                gameController.FlappyTwo.gameObject.SetActive(false);
                Time.timeScale = 0;
                tutorialText.text = "Press-holding it will make your character dash.";
                ShowTutImage(2);
                tutorialSeq = 1;
                break;
            case 2:
                tutorialPanel.SetActive(true);
                gameController.FlappyTwo.gameObject.SetActive(false);
                Time.timeScale = 0;
                tutorialText.text = "Try it now! I'll give you like 10 seconds to practice flying!";
                buttonText.text = "Sure, let's go!";
                ShowTutImage(999);
                break;
            case 3:
                tutorialPanel.SetActive(false);
                gameController.FlappyTwo.gameObject.SetActive(false);
                Time.timeScale = 1;
                TutorialReminderText.text = "Try moving around! Press and HOLD the move button to dash!";
                StartCoroutine(TutorialFly());
                break;
            case 4:
                tutorialPanel.SetActive(true);
                gameController.FlappyTwo.gameObject.SetActive(false);
                Time.timeScale = 0;
                tutorialText.text = "Now try to dodge the flying chainsaws!";
                buttonText.text = "Wait, what?!";
                break;
            case 5:
                tutorialPanel.SetActive(false);
                gameController.FlappyTwo.gameObject.SetActive(false);
                Time.timeScale = 1;
                TutorialReminderText.text = "At least try to dodge the chainsaws!";
                StartCoroutine(TutorialDodgeChainsaws());
                break;
            case 6:
                tutorialPanel.SetActive(true);
                gameController.FlappyTwo.gameObject.SetActive(false);
                Time.timeScale = 0;
                tutorialText.text = "Good! Now let me teach you how to fight.";
                buttonText.text = "Yes, finally!";
                break;
            case 7:
                tutorialPanel.SetActive(true);
                gameController.FlappyTwo.gameObject.SetActive(false);
                Time.timeScale = 0;
                tutorialText.text = "Your goal is to Smash your opponent by hitting them hard, and to push them towards the flying chainsaws.";
                buttonText.text = "Okay, so?";
                ChainsawBarrage = false;
                GameController.difficulty = 0;
                ShowTutImage(4);
                break;
            case 8:
                tutorialPanel.SetActive(true);
                gameController.FlappyTwo.gameObject.SetActive(false);
                Time.timeScale = 0;
                tutorialText.text = "Now try to hit your enemy with all your might!";
                TutorialReminderText.text = "";
                remindermessage = "Smash your enemy 3 times!";
                buttonText.text = "Whohooo!";
                break;
            case 9:
                tutorialPanel.SetActive(false);
                gameController.FlappyTwo.gameObject.SetActive(true);
                Time.timeScale = 1;
                ChainsawBarrage = true;
                TutorialReminderText.text = "Smash your enemy 3 times!";
                remindermessage = "Smash your enemy 3 times!";
                toSmash = true;
                // StartCoroutine(TutorialSmashIt());
                break;
            case 109:
                ChainsawBarrage = false;
                GameController.difficulty = 0;
                tutorialPanel.SetActive(true);
                Time.timeScale = 0;
                tutorialText.text = "Smashing your enemies damages and stuns them a bit";
                buttonText.text = "Amazing!";
                ShowTutImage(3);
                tutorialSeq = 9;
                break;
            case 10:
                tutorialPanel.SetActive(true);
                gameController.FlappyTwo.gameObject.SetActive(false);
                Time.timeScale = 0;
                tutorialText.text = "Now time to teach you to use your special move!";
                buttonText.text = "Exhillirating!";
                break;
            case 11:
                tutorialPanel.SetActive(true);
                gameController.FlappyTwo.gameObject.SetActive(false);
                Time.timeScale = 0;
                tutorialText.text = "The Percentage(%) besides your face is called the Special Gauge.";
                buttonText.text = "Okay...?";
                ShowTutImage(0);
                break;
            case 12:
                tutorialPanel.SetActive(true);
                gameController.FlappyTwo.gameObject.SetActive(false);
                Time.timeScale = 0;
                tutorialText.text = "You fill it up by smashing your enemies. It also fills up faster automatically the lesser Health you have.";
                buttonText.text = "Okay...?";
                ShowTutImage(0);
                break;
            case 13:
                tutorialPanel.SetActive(true);
                gameController.FlappyTwo.gameObject.SetActive(false);
                Time.timeScale = 0;
                tutorialText.text = "You can also get some random powerUps flying around. \r\nGetting the Burger increases your Special Gauge, while the Sundae heals you a bit.";
                buttonText.text = "Good to know.";
                ShowTutImage(8);
                break;
            case 14:
                tutorialPanel.SetActive(true);
                gameController.FlappyTwo.gameObject.SetActive(false);
                Time.timeScale = 0;
                tutorialText.text = "Once it's full, you'll notice your character will have a yellowish Aura reminiscent of some Dragon fighters.";
                buttonText.text = "Wow.";
                ShowTutImage(5);
                break;
            case 15:
                tutorialPanel.SetActive(true);
                gameController.FlappyTwo.gameObject.SetActive(false);
                Time.timeScale = 0;
                tutorialText.text = "Then Tap on your FACE to use your special skill!\r\nDifferent Buraders have different kinds of Special Move!";
                buttonText.text = "Lemme try!";
                ShowTutImage(6);
                break;
            case 16:
                tutorialPanel.SetActive(true);
                gameController.FlappyTwo.gameObject.SetActive(false);
                Time.timeScale = 0;
                tutorialText.text = "Now go and try to defeat your enemy!\r\nRemember to tap your Face Icon whenever your Special Gauge is Ready to use your Special Move!";
                buttonText.text = "Let's go!";
                ShowTutImage(3);
                break;
            case 17:
                tutorialPanel.SetActive(false);
                gameController.FlappyTwo.gameObject.SetActive(true);
                Time.timeScale = 1;
                TutorialReminderText.text = "Tap your Face Icon once your Special Gauge says 'READY!' to activate your Special Skill!";
                StartCoroutine(TutorialUseSpecial());
                break;
            case 18:
                tutorialPanel.SetActive(true);
                gameController.FlappyTwo.gameObject.SetActive(false);
                Time.timeScale = 0;
                tutorialText.text = "And..that's it!\r\nThere's a lot going on in this game, but those other things you can easily figure out on your own. You're a pro gamer.";
                buttonText.text = "Yes! I am!";
                break;
            case 19:
                tutorialPanel.SetActive(true);
                gameController.FlappyTwo.gameObject.SetActive(false);
                Time.timeScale = 0;
                tutorialText.text = "Now it's time for you to actually play the game. Do your best!";
                buttonText.text = "Okay thanks!";
                break;
            case 20:
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }
    public void ShowTutImage(int toShow) {
        int ctr = 0;
        foreach(Image image in tutorialImages) {
            image.gameObject.SetActive(false);
            if (toShow == ctr) {
                image.gameObject.SetActive(true);
            }
            ctr++;
        }
    }
    bool toSmash = false;
    void Update() {
        if (toSmash) {
            Debug.Log(remindermessage);
            TutorialReminderText.text = remindermessage;
        }
    }
    IEnumerator Tutorial00() {
        yield return new WaitForSeconds(5);
        tutorialPanel.SetActive(true);
        gameController.FlappyTwo.gameObject.SetActive(false);
        Time.timeScale = 0;
        tutorialText.text = "Welcome to Smash Buraders! \r\nBut before anything else, let me teach you the basics.";
        
    }

    IEnumerator TutorialFly() {
        yield return new WaitForSeconds(10);
        TutorialReminderText.text = "Press and hold both move buttons to fly in a curved motion!";
        yield return new WaitForSeconds(10);
        TutorialReminderText.text = "";
        tutorialPanel.SetActive(true);
        gameController.FlappyTwo.gameObject.SetActive(false);
        tutorialText.text = "Nice one! \r\nGot the hang of flying?";
        buttonText.text = "That was fun!";
        Time.timeScale = 0;
    }

    IEnumerator TutorialDodgeChainsaws() {
        ChainsawBarrage = true;
        yield return new WaitForSeconds(10);
        TutorialReminderText.text = "If you get hit by them, you get a lot of damage!";
        yield return new WaitForSeconds(10);
        TutorialReminderText.text = "Chainsaws can kill you if your Health drops to zero!";
        yield return new WaitForSeconds(10);
        tutorialPanel.SetActive(true);
        gameController.FlappyTwo.gameObject.SetActive(false);
        TutorialReminderText.text = "";
        tutorialText.text = "Getting hit by these killers will damage you severly, \r\nand once your Health drops to 0, these chainsaws will kill you!";
        buttonText.text = "I almost died!";
        ShowTutImage(3);
        Time.timeScale = 0;
    }
    public static int smashCount = 0;
    private static string remindermessage;
    public static IEnumerator AfterHit() {
        smashCount++;
        if (smashCount == 0)
            remindermessage = "Smash your enemy 3 times!";
        if (smashCount == 1)
            remindermessage = "Smash your enemy 2 more times!";
        if (smashCount == 2)
            remindermessage = "Smash your enemy 1 last time!";
        if (smashCount == 3)
            remindermessage = "Very good!";
        if (smashCount < 3) yield break;
        
        yield return new WaitForSecondsRealtime(3);
        tutorialSeq += 100;
        AudioManager.instance.PlaySFX(SFXS.READY);
        SceneManager.LoadScene("Tutorial");
        
    }
    IEnumerator TutorialSmashIt() {
        ChainsawBarrage = false;
        yield return new WaitForSeconds(1);
        GameController.difficulty = 0;
        yield return new WaitForSeconds(10);
        tutorialPanel.SetActive(true);
        Time.timeScale = 0;
        tutorialText.text = "Smashing your enemies damages and stuns them a bit";
        buttonText.text = "Amazing!";
        ShowTutImage(3);
    }
    IEnumerator TutorialUseSpecial() {
        ChainsawBarrage = false;
        yield return new WaitForSeconds(5);
        StartCoroutine(TutorialCheat());
        yield return new WaitForSeconds(10);
        TutorialReminderText.text = "Smash your enemy to stun them and push them towards the chainsaw for more damage!";
        yield return new WaitForSeconds(15);
        TutorialReminderText.text = "If your enemy is in DANGER, smash them towards the chainsaw to finish them off!";
    }
    IEnumerator TutorialCheat() {
        gameController.FlappyOne.SpecialCurrent += 999;
        gameController.FlappyOne.HPCurrent += 50;
        yield return new WaitForSeconds(5);
        StartCoroutine(TutorialCheat());
    }

    public void NextTutorial(bool isNextTutorial = true) {
        if (isNextTutorial) tutorialSeq++;
        AudioManager.instance.PlaySFX(SFXS.READY);
        SceneManager.LoadScene("Tutorial");
    }
    
}
