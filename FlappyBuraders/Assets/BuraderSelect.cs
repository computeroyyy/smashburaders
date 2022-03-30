using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FlappyBuraders.GlobalStuff;
using System.Runtime.Serialization.Formatters.Binary;

public class BuraderSelect : MonoBehaviour
{
    public Button GameModeBtn;
    public Button KillGoalBtn;
    public Button StageSelect;
    public Button FlappyOneBtn;
    public Button FlappyTwoBtn;
    public Text FlappyOneName;
    public Text FlappyTwoName;
    public Text GameModeText;
    public Text Subtext;

    public Button Ready1;
    public Button Ready2;
    public Button Player1;
    public Button Player2;
    public GameObject Shroud1;
    public GameObject Shroud2;
    public GameObject Player1Panel;
    public GameObject Player2Panel;
    public GameObject SkillDescriptionPanel;
    

    public Enums.GameMode gameMode = Enums.GameMode.HEAD_TO_HEAD;
    public Enums.KillGoal killGoal = Enums.KillGoal.KILL3;
    public Enums.Stage stage = Enums.Stage.RANDOM;
    private Camera cameraMain;

    

    void Awake() {
        Application.targetFrameRate = 60;
        gameMode = (Enums.GameMode) PlayerPrefs.GetInt(Prefs.GAMEMODEint);
        SetGameModeName();

        killGoal = (Enums.KillGoal) PlayerPrefs.GetInt(Prefs.KILLGOALint);
        SetKillGoalName();

        stage = Enums.Stage.RANDOM;//(Enums.Stage) PlayerPrefs.GetInt(Prefs.STAGEtype);
        SetStageName();

        

    }
    void Start() {
        UnityEngine.Advertisements.Advertisement.Banner.Hide();
        TutorialManager.isTutorial = false;
        if (!AudioManager.instance.IsBGMPlaying(BGMS.TITLE)) {
            AudioManager.instance.PlayBGM(BGMS.TITLE);
        }
        buraderType1 = (Enums.BuraderName) PlayerPrefs.GetInt(Prefs.FLAPPYONEint);
        buraderType2 = (Enums.BuraderName) PlayerPrefs.GetInt(Prefs.FLAPPYTWOint);
        killGoal = Enums.KillGoal.KILL3;
        SetKillGoalName();
        cameraMain = Camera.main;
        
        SetShroud();
        // SetAI();
        ToggleAIPlayer1();
        ToggleAIPlayer2();

        StartCoroutine(spawn());
        StartCoroutine(langaws());
        if (gameMode == Enums.GameMode.ARCADE || gameMode == Enums.GameMode.SURVIVAL) {
            SkillDescriptionPanel.SetActive(true);
            PlayerPrefs.SetInt(Prefs.CONTINUE_COUNT, 0);
            Ready2.GetComponentInChildren<Text>().text = "Switch";
            isReady2 = true;
            isReady1 = false;
            isHuman1 = true;
            isHuman2 = false;
        }
        else {
            SkillDescriptionPanel.SetActive(false);
        }
        if (gameMode == Enums.GameMode.HOLD_HANDS) {
            isHuman1 = true;
            isHuman2 = true;
            Player1.GetComponentInChildren<Text>().text = "Hooman";
            Player2.GetComponentInChildren<Text>().text = "Hooman";
        }
        Debug.Log(Application.persistentDataPath);
        if (LocalData.toSave)
            SaveController.SaveGame();
        else {
            
        }

    }

    private void LockStuffOnStart() {
        LocalData.lockedBuraders.Add(Enums.BuraderName.DINK);
        LocalData.lockedBuraders.Add(Enums.BuraderName.NICOLLO);
        LocalData.lockedBuraders.Add(Enums.BuraderName.ERIC);
        LocalData.lockedBuraders.Add(Enums.BuraderName.MARK);
        LocalData.lockedBuraders.Add(Enums.BuraderName.LOKIA);
        LocalData.lockedBuraders.Add(Enums.BuraderName.BRYAN);
        LocalData.lockedBuraders.Add(Enums.BuraderName.BRITTANY);
        LocalData.lockedBuraders.Add(Enums.BuraderName.CHRISTIAN);
        LocalData.lockedBuraders.Add(Enums.BuraderName.ZANTETSU);
        LocalData.lockedBuraders.Add(Enums.BuraderName.DOGE);
        LocalData.lockedBuraders.Add(Enums.BuraderName.POPPINS);
        LocalData.lockedBuraders.Add(Enums.BuraderName.KYLE);
        LocalData.lockedBuraders.Add(Enums.BuraderName.IVAN);
        LocalData.lockedBuraders.Add(Enums.BuraderName.RIZAL);
        LocalData.lockedBuraders.Add(Enums.BuraderName.MASSIMO);
        LocalData.lockedBuraders.Add(Enums.BuraderName.DARK_M);
        LocalData.lockedBuraders.Add(Enums.BuraderName.GIO);
        LocalData.lockedBuraders.Add(Enums.BuraderName.MIGS);
        
        LocalData.lockedStage.Add(Enums.Stage.INNER_SPACE);
        LocalData.BP = 10;
        LocalData.CoopHighScore = 0;
        LocalData.SurvivalHighScore = 0;
    }

    IEnumerator spawn() {
        yield return new WaitForSeconds(Random.Range(4f, 80f));
        GameObject missile = Resources.Load("Killers/EyeOfDoom") as GameObject;
        missile = Instantiate(missile, new Vector3(cameraMain.transform.position.x - 0.14f, cameraMain.transform.position.y + Random.Range(-4f, 2f)), missile.transform.rotation, cameraMain.transform);//, SpawnPoinstLeft[rnd].transform);
        StartCoroutine(spawn());
    }
    IEnumerator langaws() {
        yield return new WaitForSeconds(Random.Range(20f, 60f));
        GameObject langaw = Resources.Load("Buraders/LANGAW") as GameObject;
        langaw = Instantiate(langaw, new Vector2(cameraMain.transform.position.x, cameraMain.transform.position.y + 4.5f), langaw.transform.rotation, cameraMain.transform);
        StartCoroutine(langaws());
    }

    private void SetShroud() {
        ////Debug.Log("setshroud");
        if (gameMode == Enums.GameMode.SURVIVAL || gameMode == Enums.GameMode.HOLD_HANDS) {
            KillGoalBtn.GetComponentInChildren<Text>().text = "You Die, You Lose";
        }
        else {
            SetKillGoalName();
        }

        if (gameMode == Enums.GameMode.ARCADE || gameMode == Enums.GameMode.SURVIVAL || gameMode == Enums.GameMode.ONLINE) {
            Player2Panel.SetActive(true);
            Shroud2.SetActive(true);
            Player1Panel.SetActive(false);
            Shroud1.SetActive(false);
            
            FlappyTwoName.text = "SECRET";
            // isHuman2 = true;
            // ToggleAIPlayer2();
            isHuman2 = false;
            Player2.GetComponentInChildren<Text>().text = "[Computer]";
        }
        else {
            Player1Panel.SetActive(false);
            Shroud1.SetActive(false);
            FlappyOneName.text = "wala naman";
            Player2Panel.SetActive(false);
            Shroud2.SetActive(false);
            FlappyTwoName.text = "wala naman";
        }
    }

    public void ToggleGameMode(bool forward = true) {
        AudioManager.instance.PlaySFX(SFXS.TOGGLE);
        if (forward) {
            gameMode++;
            if (gameMode == Enums.GameMode.END) {
                gameMode = 0;
            }
        }
        else {
            gameMode--;
            if (gameMode < 0) {
                gameMode = Enums.GameMode.END - 1;
            }
        }
        SetShroud();
        SetGameModeName();
    }
    public void SetGameModeName() {
        ////Debug.Log("SetGameModeName");
        switch (gameMode) {
            case Enums.GameMode.HEAD_TO_HEAD:
                GameModeText.text = "HEAD to HEAD";
                Subtext.text = "Flex your Burader skills against your friend for lifetime of bragging rights!";
                break;
            case Enums.GameMode.HOLD_HANDS:
                GameModeText.text = "HOLD HANDS";
                Subtext.text = "Join forces together to rid this world of the annoying flies!";
                break;
            case Enums.GameMode.TRAIN_YOUR_BURADER:
                GameModeText.text = "Train Your Burader";
                break;
            case Enums.GameMode.ARCADE:
                GameModeText.text = "ARCADE";
                Subtext.text = "Beat the game by fighting through several Stages and beat the final Boss!";
                break;
            case Enums.GameMode.ONLINE:
                GameModeText.text = "ONLINE MODE";
                Subtext.text = "Destroy everyone and come up on top of the Buraders food chain.";
                break;
            case Enums.GameMode.SURVIVAL:
                GameModeText.text = "SURVIVAL";
                Subtext.text = "You VS everyone! No one can stop you unless you let them stop you!";
                break;
        }
    }

    public void ToggleKillGoal(bool forward = true) {
        Debug.Log("ToggleKillGoal");
        AudioManager.instance.PlaySFX(SFXS.TOGGLE);
        if (forward) {
            killGoal++;
            if (killGoal == Enums.KillGoal.END)
                killGoal = 0;
        }
        else {
            killGoal--;
            if (killGoal < 0)
                killGoal = Enums.KillGoal.END - 1;
        }
        SetKillGoalName();
        //SetShroud();
    }
    private void SetKillGoalName() {
        ////Debug.Log("SetKillGoalName");
        if (gameMode == Enums.GameMode.SURVIVAL || gameMode == Enums.GameMode.HOLD_HANDS) {
            KillGoalBtn.GetComponentInChildren<Text>().text = "You Die, You Lose";
        }
        else {
            switch(killGoal) {
            case Enums.KillGoal.KILL1:
                KillGoalBtn.GetComponentInChildren<Text>().text = "Kill One Buraders";
                break;
            case Enums.KillGoal.KILL2:
                KillGoalBtn.GetComponentInChildren<Text>().text = "Kill Two Buraders";
                break;
            case Enums.KillGoal.KILL3:
                KillGoalBtn.GetComponentInChildren<Text>().text = "Kill Three Buraders";
                break;
            case Enums.KillGoal.KILL5:
                KillGoalBtn.GetComponentInChildren<Text>().text = "Kill Five Buraders";
                break;
        }
        }
    }

    public void ToggleStageSelect(bool forward = true) {
        AudioManager.instance.PlaySFX(SFXS.TOGGLE);
        if (forward) {
            stage++;
            if (stage == Enums.Stage.THE_ARCHITECT)
                stage++;
            if (stage == Enums.Stage.END)
                stage = 0;
        }
        else {
            stage--;
            if (stage == Enums.Stage.THE_ARCHITECT)
                stage--;
            if (stage < 0)
                stage = Enums.Stage.END - 1;
        }
        SetStageName();
        //SetShroud();
    }
    private void SetStageName() {
        if (gameMode == Enums.GameMode.ARCADE || gameMode == Enums.GameMode.ONLINE) {
            StageSelect.GetComponentInChildren<Text>().text = "------";
        }
        else{
            switch(stage) {
                case Enums.Stage.TOWN_OUTSKIRTS:
                    StageSelect.GetComponentInChildren<Text>().text = "Town Outskirts";
                    break;
                case Enums.Stage.INNER_SPACE:
                    StageSelect.GetComponentInChildren<Text>().text = "Zeon Colony";
                    break;
                case Enums.Stage.LUSH_FOREST:
                    StageSelect.GetComponentInChildren<Text>().text = "Chunin Exam";
                    break;
                case Enums.Stage.MAGICAL_ACADEMY:
                    StageSelect.GetComponentInChildren<Text>().text = "Cavite City";
                    break;
                case Enums.Stage.RADIANT_CASTLE:
                    StageSelect.GetComponentInChildren<Text>().text = "Road to Damascus";
                    break;
                case Enums.Stage.STRATOSPHERE:
                    StageSelect.GetComponentInChildren<Text>().text = "Air Corridor";
                    break;
                case Enums.Stage.OCEAN_PARADISE:
                    StageSelect.GetComponentInChildren<Text>().text = "Hadalpelagic Zone";
                    break;
                case Enums.Stage.THE_ARCHITECT:
                    StageSelect.GetComponentInChildren<Text>().text = "The Architect";
                    break;
                case Enums.Stage.RANDOM:
                    StageSelect.GetComponentInChildren<Text>().text = "RANDOM";
                    break;
            }
        }
    }

    public Enums.BuraderName buraderType1;
    public Enums.BuraderName buraderType2;

    public bool isHuman1 = true;
    public bool isHuman2 = false;
    public void ToggleAIPlayer1() {
        AudioManager.instance.PlaySFX(SFXS.TING);
        if (gameMode == Enums.GameMode.SURVIVAL || gameMode == Enums.GameMode.ARCADE || gameMode == Enums.GameMode.HOLD_HANDS || gameMode == Enums.GameMode.ONLINE) {
            //hoomans only
        }
        else {
            isHuman1 = !isHuman1;
            if (isHuman1) {
                Player1.GetComponentInChildren<Text>().text = "Hooman";
            }
            else {
                Player1.GetComponentInChildren<Text>().text = "[Computer]";
            }
        }
    }
    public void ToggleAIPlayer2() {
        AudioManager.instance.PlaySFX(SFXS.TING);
        if (gameMode == Enums.GameMode.SURVIVAL || gameMode == Enums.GameMode.ARCADE || gameMode == Enums.GameMode.HOLD_HANDS || gameMode == Enums.GameMode.ONLINE) {
            //hoomans only
        }
        else {
            isHuman2 = !isHuman2;
            if (isHuman2) {
                Player2.GetComponentInChildren<Text>().text = "Hooman";
            }
            else {
                Player2.GetComponentInChildren<Text>().text = "[Computer]";
            }
        }
    }

    public bool isReady1 = false;
    public bool isReady2 = false;
    public void ToggleReady1() {
        AudioManager.instance.PlaySFX(SFXS.READY);
        if (Ready1.GetComponentInChildren<Text>().text == "Switch") {
            //switch to player 1
            Player1Panel.SetActive(false);
            Shroud1.SetActive(false);
            FlappyOneName.text = "ewan";
            Player2Panel.SetActive(true);
            Shroud2.SetActive(true);
            FlappyOneName.text = "SECRET";
            
            isHuman1 = true;
            isHuman2 = false;
            isReady1 = false;
            isReady2 = true;
            Ready2.GetComponentInChildren<Text>().text = "Switch";
            Player1.GetComponentInChildren<Text>().text = "Hooman";
            Player2.GetComponentInChildren<Text>().text = "[Computer]";
        }
        else {
            isReady1 = !isReady1;
        }
        if (isReady1) {
            Ready1.GetComponentInChildren<Text>().text = "READY!!!";
        }
        else {
            Ready1.GetComponentInChildren<Text>().text = "Ready?";
        }
        GameNaBa();
    }
    public void ToggleReady2() {
        AudioManager.instance.PlaySFX(SFXS.READY);
        if (Ready2.GetComponentInChildren<Text>().text == "Switch") {
            //switch to player2
            Player2Panel.SetActive(false);
            Shroud2.SetActive(false);
            FlappyTwoName.text = "ewan";
            Player1Panel.SetActive(true);
            Shroud1.SetActive(true);
            FlappyOneName.text = "SECRET";
            
            isHuman1 = false;
            isHuman2 = true;
            isReady1 = true;
            isReady2 = false;
            Ready1.GetComponentInChildren<Text>().text = "Switch";
            Player1.GetComponentInChildren<Text>().text = "[Computer]";
            Player2.GetComponentInChildren<Text>().text = "Hooman";
        }
        else {
            isReady2 = !isReady2;
        }

        if (isReady2) {
            Ready2.GetComponentInChildren<Text>().text = "READY!!!";
        }
        else {
            Ready2.GetComponentInChildren<Text>().text = "Ready?";
        }
        GameNaBa();
    }

    private void GameNaBa() {
        if (isReady1 && isReady2) {
            PlayerPrefs.SetInt(Prefs.GAMEMODEint, (int)gameMode);
            PlayerPrefs.SetInt(Prefs.KILLGOALint, (int)killGoal);
            PlayerPrefs.SetInt(Prefs.STAGEtype, (int)stage);
            PlayerPrefs.SetInt(Prefs.FLAPPYONEint, (int)buraderType1);
            PlayerPrefs.SetInt(Prefs.FLAPPYTWOint, (int)buraderType2);
            PlayerPrefs.SetInt(Prefs.PLAYER1isHOOMAN, isHuman1 ? 1 : 0);
            PlayerPrefs.SetInt(Prefs.PLAYER2isHOOMAN, isHuman2 ? 1 : 0);
            PlayerPrefs.SetInt(Prefs.STAGENUMBER, 1);
            PlayerPrefs.SetInt(Prefs.YOUareGAMEOVER, 0);
            if (gameMode == Enums.GameMode.ARCADE) {
                LocalData.arcadeLastPick = isHuman1 ? buraderType1 : buraderType2;
                GetArcadeEnemies();
                SceneManager.LoadScene("StageNumber");    
            }
            if (gameMode == Enums.GameMode.ONLINE) {
                PlayerPrefs.SetInt(Prefs.PLAYER1isHOOMAN, 1);
                PlayerPrefs.SetInt(Prefs.PLAYER2isHOOMAN, 1);
                PlayerPrefs.SetInt(Prefs.FLAPPYONEint, (int)Enums.BuraderName.OYYY);
                PlayerPrefs.SetInt(Prefs.FLAPPYTWOint, (int)Enums.BuraderName.PAU);
                SceneManager.LoadScene("BattleOnline");    
            }
            else {
                SceneManager.LoadScene("StageNumber");
            }
        }
    }
    private void GetArcadeEnemies(){
        int stage = 1;
        bool meronNa = false;
        if (LocalData.arcadeEnemies != null)
            LocalData.arcadeEnemies.Clear();
        else
            LocalData.arcadeEnemies = new List<Enums.BuraderName>();
        //get TIER 1
        while (stage <= 2) {
            Enums.BuraderName randomEnemy = (Enums.BuraderName) Random.Range(0, (int)Enums.BuraderName.T109);
            foreach(Enums.BuraderName enemy in LocalData.arcadeEnemies) {
                if (enemy == randomEnemy) {
                    meronNa = true;
                    break;
                }
            }
            //walang kapareho
            if (!meronNa) {
                Debug.Log("Stage " + stage + " - " + randomEnemy);
                LocalData.arcadeEnemies.Add(randomEnemy);
                stage++;
            }
            meronNa = false;
        }

        //get TIER 2
        while (stage <= 4) {
            Enums.BuraderName randomEnemy = (Enums.BuraderName) Random.Range((int)Enums.BuraderName.ENDTIER1 + 1, (int)Enums.BuraderName.T207);
            foreach(Enums.BuraderName enemy in LocalData.arcadeEnemies) {
                if (enemy == randomEnemy) {
                    meronNa = true;
                    break;
                }
            }
            //walang kapareho
            if (!meronNa) {
                Debug.Log("Stage " + stage + " - " + randomEnemy);
                LocalData.arcadeEnemies.Add(randomEnemy);
                stage++;
            }
            meronNa = false;
        }
        //get TIER 3
        while (stage <= 6) {
            Enums.BuraderName randomEnemy = (Enums.BuraderName) Random.Range((int)Enums.BuraderName.ENDTIER2 + 1, (int)Enums.BuraderName.T309);
            foreach(Enums.BuraderName enemy in LocalData.arcadeEnemies) {
                if (enemy == randomEnemy) {
                    meronNa = true;
                    break;
                }
            }
            //walang kapareho
            if (!meronNa) {
                Debug.Log("Stage " + stage + " - " + randomEnemy);
                LocalData.arcadeEnemies.Add(randomEnemy);
                stage++;
            }
            meronNa = false;
        }
        
        //TIER 4s
        LocalData.arcadeEnemies.Add(Enums.BuraderName.MASSIMO);
        LocalData.arcadeEnemies.Add(Enums.BuraderName.DARK_M);
        LocalData.arcadeEnemies.Add(Enums.BuraderName.GIO);
    }

    public void GoToSecret() {
        SceneManager.LoadScene("Secret");
    }

    public void BackTOMain() {
        LocalData.toSave = true;
        SceneManager.LoadScene("MainMenu");
    }

}
