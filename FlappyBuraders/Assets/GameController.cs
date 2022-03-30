using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FlappyBuraders.GlobalStuff;
using UnityEngine.Advertisements;

public class GameController : MonoBehaviour
{
    float force = 5.9f;
    public int maxKill;
    public static Enums.GameMode gameMode;
    public static int LangawKills;
    public YourBurader FlappyOne;
    public YourBurader FlappyTwo;
    public Text LeftKillsText;
    public Text RightKillsText;

    bool FlappyOneWins = false;
    bool FlappyTwoWins = false;
    bool FlappyCoopLose = false;
    public Text SpecialCounter1;
    public Text SpecialCounter2;


    public Text MainText;
    public Text SubText;
    public Text FinisherIDText;
    public Text SmashText1;
    public Text SmashText2;
    public GameObject SpeedLine;
    public Image ProfilePic1;
    public Image ProfilePic2;
    GameObject particle_jump;
    public GameObject GameEnd;
    public Camera cameraMain;
    public GameObject dangerPanel;
    public GameObject Buttons1Panel1;
    public GameObject Buttons1Panel2;
    public GameObject Buttons2Panel1;
    public GameObject Buttons2Panel2;
    public Text stunnedDurationText1;
    public Text stunnedDurationText2;
    float finisherIDDuration;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Advertisements.Advertisement.Banner.Hide();
        if (gameMode == Enums.GameMode.ARCADE) {
            if (PlayerPrefs.GetInt(Prefs.STAGENUMBER) - 1 > 7){
                PlayerPrefs.SetInt(Prefs.STAGEtype, 6);
            }
            else {
                PlayerPrefs.SetInt(Prefs.STAGEtype, PlayerPrefs.GetInt(Prefs.STAGENUMBER) - 1);
            }
        }
        else if (gameMode == Enums.GameMode.ONLINE) {
            PlayerPrefs.SetInt(Prefs.STAGEtype, 6);
        }
        if (TutorialManager.isTutorial) {
            gameMode = Enums.GameMode.ARCADE;
            difficulty = 0;
            PlayerPrefs.SetInt(Prefs.STAGEtype, 2);
            PlayerPrefs.SetInt(Prefs.FLAPPYONEint, 0);
            PlayerPrefs.SetInt(Prefs.FLAPPYTWOint, 3);
        }
        
        Enums.Stage theStage = (Enums.Stage)PlayerPrefs.GetInt(Prefs.STAGEtype);
        if (theStage == Enums.Stage.RANDOM) {
            theStage = (Enums.Stage)Random.Range(0, (int)Enums.Stage.RANDOM);
            PlayerPrefs.SetInt(Prefs.STAGEtype, (int)theStage);
        }
        GameObject makeStage = Resources.Load("Stages/" + theStage.ToString()) as GameObject;
        makeStage = Instantiate(makeStage, new Vector3 (-1, -1.55f, 61), this.transform.rotation);
        // cameraMain = Camera.main;
        if (theStage == Enums.Stage.TOWN_OUTSKIRTS) {
            GameObject stageEffect = Resources.Load("Particles/ULAN") as GameObject;
            Instantiate(stageEffect, stageEffect.transform.position, stageEffect.transform.rotation, cameraMain.transform);
        }
        else if (theStage == Enums.Stage.OCEAN_PARADISE) {
            GameObject stageEffect = Resources.Load("Particles/BUBBLES") as GameObject;
            Instantiate(stageEffect, stageEffect.transform.position, stageEffect.transform.rotation, cameraMain.transform);
        }
        InitializeStuff();
        switch (gameMode) {
            case Enums.GameMode.HEAD_TO_HEAD:    
                StartCoroutine(SetRandomReadyMessage(PromptMessage.GetRandomReadyMessages()));
                break;
            case Enums.GameMode.HOLD_HANDS:
                FlappyTwo.life++;
                StartCoroutine(SetRandomCoopMessage(PromptMessage.GetRandomCoopMessages()));
                break;
            default:
                StartCoroutine(SetRandomReadyMessage(PromptMessage.GetRandomReadyMessages()));
                break;
        }
        
        //AI stuff
        StartCoroutine(ChangeBot());
        StartCoroutine(ChangeCharge());
        StartCoroutine(ChageMood1());
        StartCoroutine(ChageMood2());
        StartCoroutine(ChangeAIDash());
        GetDifficulty();

        // if (GameController.gameMode == Enums.GameMode.ONLINE)
        //     Photon.Pun.PhotonNetwork.AddCallbackTarget(this);
        SaveController.SaveGame();
    }
    public static int difficulty;
    private void GetDifficulty() {

        difficulty = PlayerPrefs.GetInt(Prefs.DIFFICULTY, 0);
        if (PlayerPrefs.GetInt(Prefs.STAGEtype) == 7) {
            if (difficulty < 2) {
                difficulty++;
            }
        }
        //adjusting difficulty
        if (gameMode == Enums.GameMode.SURVIVAL) {
            difficulty = 0;
        }
    }
    public void PlayBGMs() {
        if (TutorialManager.isTutorial) return;
        switch (PlayerPrefs.GetInt(Prefs.STAGEtype)) {
            case 0: AudioManager.instance.PlayBGM(BGMS.SILENT); break;
            case 1: AudioManager.instance.PlayBGM(BGMS.DISTORTED); break;
            case 2: AudioManager.instance.PlayBGM(BGMS.REALTIME); break;
            case 3: AudioManager.instance.PlayBGM(BGMS.LOST); break;
            case 4: AudioManager.instance.PlayBGM(BGMS.ECCLESIASTES); break;
            case 5: AudioManager.instance.PlayBGM(BGMS.PILGRIM); break;
            case 6: AudioManager.instance.PlayBGM(BGMS.EXTRACTUM); break;
            case 7: AudioManager.instance.PlayBGM(BGMS.TERMINUS); break;
        }
    }

    public void UseSpecial1() {
        if (FlappyOne.isHooman)
            FlappyOne.SpecialUse();
    }
    public void UseSpecial2() {
        if (FlappyTwo.isHooman)
            FlappyTwo.SpecialUse();
    }

    public IEnumerator SetRandomReadyMessage(PromptMessage theMessage) {
        if (TutorialManager.isTutorial) 
            yield break;
        AudioManager.instance.PlayVOX(VOXS.WHOWILLSURVIVE);
        MainText.gameObject.SetActive(false);
        MainText.gameObject.SetActive(true);
        MainText.text = "WHO WILL SURVIVE?!";
        SubText.text = theMessage.MainMessage;

        yield return new WaitForSeconds(3.5f);
        AudioManager.instance.PlayVOX(VOXS.ENGAGE);
        MainText.gameObject.SetActive(false);

        MainText.gameObject.SetActive(true);
        MainText.text = "FIGHT FOR YOUR LIFE!"; //theMessage.fMainMessage;
        SubText.text = theMessage.fMainMessage;
        
        yield return new WaitForSeconds(3);
        MainText.gameObject.SetActive(false);
    }
    public IEnumerator SetRandomCoopMessage(PromptMessage theMessage) {
        AudioManager.instance.PlayVOX(VOXS.SURVIVAL);
        MainText.gameObject.SetActive(false);
        MainText.gameObject.SetActive(true);
        MainText.text = theMessage.MainMessage;
        SubText.text = theMessage.SubMessage;

        yield return new WaitForSeconds(3.5f);
        AudioManager.instance.PlayVOX(VOXS.DONTDIE);
        MainText.gameObject.SetActive(false);

        MainText.gameObject.SetActive(true);
        MainText.text = theMessage.fMainMessage;
        SubText.text = theMessage.fSubMessage;
        
        yield return new WaitForSeconds(3);
        MainText.gameObject.SetActive(false);
    }
    public IEnumerator SetRandomDeadMessage(PromptMessage theMessage, string theDead) {
        MainText.gameObject.SetActive(false);
        MainText.gameObject.SetActive(true);
        MainText.text = theDead + theMessage.MainMessage;
        SubText.text = theMessage.SubMessage;

        yield return new WaitForSeconds(3.5f);
        MainText.gameObject.SetActive(false);
    }
    public IEnumerator SetMessage(string main, string sub) {
        MainText.gameObject.SetActive(false);
        MainText.gameObject.SetActive(true);
        MainText.text = main;
        SubText.text = sub;

        yield return new WaitForSeconds(3f);
        MainText.gameObject.SetActive(false);
    }
    public IEnumerator SetFinishMessageRoutine(string _message, int _BP) {
        FinisherIDText.text = _message + "\r\n+" + _BP.ToString() + "BP";
        FinisherIDText.gameObject.SetActive(false);
        FinisherIDText.gameObject.SetActive(true);
        finisherIDDuration = 2;
        yield return null;
        
    }
    public IEnumerator SetPowerupMessageRoutine(string _message) {
        FinisherIDText.text = _message;
        FinisherIDText.gameObject.SetActive(false);
        FinisherIDText.gameObject.SetActive(true);
        finisherIDDuration = 1;
        yield return null;
        
    }
    public IEnumerator SetSMASHMessageRoutine(int who, string message) {
        if (who == 1) {
            SmashText1.text = message;
            SmashText1.gameObject.SetActive(false);
            SmashText1.gameObject.SetActive(true);
            // yield return new WaitForSeconds(0.95f);
            // SmashText1.gameObject.SetActive(false);
        }
        else if (who == 2) {
            SmashText2.text = message;
            SmashText2.gameObject.SetActive(false);
            SmashText2.gameObject.SetActive(true);
            // yield return new WaitForSeconds(0.95f);
            // SmashText2.gameObject.SetActive(false);
        }
        yield return null;
    }


    private void InitializeStuff() {
        PlayBGMs();
        GetGameMode();
        FlappyOne.GetComponent<Rigidbody2D>().gravityScale = 2;
        FlappyTwo.GetComponent<Rigidbody2D>().gravityScale = 2;
        FlappyOne.playerNumber = 1;
        FlappyTwo.playerNumber = 2;

        if ((gameMode == Enums.GameMode.HEAD_TO_HEAD || gameMode == Enums.GameMode.HOLD_HANDS) && FlappyOne.isHooman && FlappyTwo.isHooman) {
            FlappyOne.Controls = Buttons1Panel1;
            FlappyTwo.Controls = Buttons2Panel1;
            Buttons1Panel1.SetActive(true);
            Buttons2Panel1.SetActive(true);
            Buttons1Panel2.SetActive(false);
            Buttons2Panel2.SetActive(false);
        }
        else if (gameMode == Enums.GameMode.ONLINE) {
            FlappyOne.Controls = Buttons1Panel1;
            FlappyTwo.Controls = Buttons2Panel1;
            // Buttons1Panel1.SetActive(false);
            // Buttons2Panel1.SetActive(false);
            // Buttons1Panel2.SetActive(false);
            // Buttons2Panel2.SetActive(false);
        }
        else {
            //0 - corner; 1 - middle
            if (PlayerPrefs.GetInt(Prefs.CONTROLS, 1) == 0) {
                FlappyOne.Controls = Buttons1Panel1;
                FlappyTwo.Controls = Buttons2Panel1;
                Buttons1Panel1.SetActive(true);
                Buttons2Panel1.SetActive(true);
                Buttons1Panel2.SetActive(false);
                Buttons2Panel2.SetActive(false);
            }
            else {
                FlappyOne.Controls = Buttons1Panel2;
                FlappyTwo.Controls = Buttons2Panel2;
                Buttons1Panel1.SetActive(false);
                Buttons2Panel1.SetActive(false);
                Buttons1Panel2.SetActive(true);
                Buttons2Panel2.SetActive(true);
            }
        }
        if (!FlappyOne.isHooman) {
            FlappyOne.Controls.transform.position = new Vector3(9999,9999,9999);
        }
        if (!FlappyTwo.isHooman) {
            FlappyTwo.Controls.transform.position = new Vector3(9999,9999,9999);
        }

        FlappyOne.GetControlButtons();
        FlappyTwo.GetControlButtons();

        LangawKills = 0;
        ChangeFace();
        GetMaxKillGoal();
        FlappyOne.GetFightingStyle();
        FlappyTwo.GetFightingStyle();
        RigHP(FlappyOne);
        RigHP(FlappyTwo);
    }
    private void RigHP(YourBurader Flappy) {
        // if (gameMode == Enums.GameMode.ARCADE) {
        //     if (Flappy.isHooman) {
        //         Flappy.HPMax = 152 - ((PlayerPrefs.GetInt(Prefs.STAGENUMBER) * (difficulty + 1)));
        //     }
        // }
    }
    private void GetMaxKillGoal() {
        switch (PlayerPrefs.GetInt(Prefs.KILLGOALint)) {
            case 0:
                maxKill = 1;
                break;
            case 1:
                maxKill = 2;
                break;
            case 2:
                maxKill = 3;
                break;
            case 3:
                maxKill = 5;
                break;
        }
    }
    private void GetGameMode() {
        gameMode = (Enums.GameMode) PlayerPrefs.GetInt(Prefs.GAMEMODEint);
        
        Debug.Log(PlayerPrefs.GetInt("P1 ishooman?: " + Prefs.PLAYER1isHOOMAN));
        Debug.Log(PlayerPrefs.GetInt("P2 ishooman?: " + Prefs.PLAYER2isHOOMAN));
        isFlappyOneAI = PlayerPrefs.GetInt(Prefs.PLAYER1isHOOMAN) == 1 ? false : true;
        isFlappyTwoAI = PlayerPrefs.GetInt(Prefs.PLAYER2isHOOMAN) == 1 ? false : true;
        FlappyOne.isHooman = !isFlappyOneAI;
        FlappyTwo.isHooman = !isFlappyTwoAI;

        if (isFlappyOneAI) {
            FlappyOne.ControlLeft.transform.localScale = new Vector2(0, 0);
            FlappyOne.ControlRight.transform.localScale = new Vector2(0, 0);
        }
        if (isFlappyTwoAI) {
            FlappyTwo.ControlLeft.transform.localScale = new Vector2(0, 0);
            FlappyTwo.ControlRight.transform.localScale = new Vector2(0, 0);
        }

        //!!!
        //isFlappyTwoAI = true;
    }
    public void ChangeFace() {
        GameObject sprite;
        GameObject animator;
        GameObject animator2;
        if ((gameMode == Enums.GameMode.SURVIVAL && !FlappyOne.isHooman)) {
            RandomBuraders(FlappyOne);
        }
        else if (gameMode == Enums.GameMode.ARCADE  && !FlappyOne.isHooman) {
            FlappyOne.buraderType = LocalData.arcadeEnemies[PlayerPrefs.GetInt(Prefs.STAGENUMBER) - 1];
            sprite = Resources.Load("Buraders/" + FlappyOne.buraderType.ToString()) as GameObject;
            FlappyOne.GetComponentInChildren<SpriteRenderer>().sprite = sprite.GetComponent<Image>().sprite;   
            animator = Resources.Load("Buraders/" + FlappyOne.buraderType.ToString() + "X") as GameObject;
            FlappyOne.GetComponent<Animator>().runtimeAnimatorController = animator.GetComponent<Animator>().runtimeAnimatorController;   
        }
        else {
            FlappyOne.buraderType = (Enums.BuraderName)PlayerPrefs.GetInt(Prefs.FLAPPYONEint);
            sprite = Resources.Load("Buraders/" + FlappyOne.buraderType.ToString()) as GameObject;
            FlappyOne.GetComponentInChildren<SpriteRenderer>().sprite = sprite.GetComponent<Image>().sprite;  
            animator = Resources.Load("Buraders/" + FlappyOne.buraderType.ToString() + "X") as GameObject;
            FlappyOne.GetComponent<Animator>().runtimeAnimatorController = animator.GetComponent<Animator>().runtimeAnimatorController;     
        }

        if ((gameMode == Enums.GameMode.SURVIVAL && !FlappyTwo.isHooman)) {
            RandomBuraders(FlappyTwo);
        }
        else if (gameMode == Enums.GameMode.ARCADE  && !FlappyTwo.isHooman) {
            FlappyTwo.buraderType = LocalData.arcadeEnemies[PlayerPrefs.GetInt(Prefs.STAGENUMBER) - 1];
            sprite = Resources.Load("Buraders/" + FlappyTwo.buraderType.ToString()) as GameObject;
            FlappyTwo.GetComponentInChildren<SpriteRenderer>().sprite = sprite.GetComponent<Image>().sprite;     
            animator2 = Resources.Load("Buraders/" + FlappyTwo.buraderType.ToString() + "X") as GameObject;
            FlappyTwo.GetComponent<Animator>().runtimeAnimatorController = animator2.GetComponent<Animator>().runtimeAnimatorController;  
        }
        else {
            FlappyTwo.buraderType = (Enums.BuraderName)PlayerPrefs.GetInt(Prefs.FLAPPYTWOint);
            sprite = Resources.Load("Buraders/" + FlappyTwo.buraderType.ToString()) as GameObject;
            FlappyTwo.GetComponentInChildren<SpriteRenderer>().sprite = sprite.GetComponent<Image>().sprite;   
            animator2 = Resources.Load("Buraders/" + FlappyTwo.buraderType.ToString() + "X") as GameObject;
            //// Debug.Log("Buraders/" + FlappyTwo.buraderType.ToString() + "X");
            //// Debug.Log(animator2.GetComponent<Animator>().runtimeAnimatorController);
            FlappyTwo.GetComponent<Animator>().runtimeAnimatorController = animator2.GetComponent<Animator>().runtimeAnimatorController;
        }

        sprite = Resources.Load("Buraders/" + FlappyOne.buraderType.ToString()) as GameObject;
        ProfilePic1.sprite = sprite.GetComponent<Image>().sprite;
        sprite = Resources.Load("Buraders/" + FlappyTwo.buraderType.ToString()) as GameObject;
        ProfilePic2.sprite = sprite.GetComponent<Image>().sprite;
    }

    public void RandomBuraders(YourBurader Flappy) {
        GameObject animator;
        Flappy.buraderType = Enums.BuraderName.END;
        while(Flappy.buraderType.ToString().Contains("END")
            ||Flappy.buraderType.ToString().Contains("T1")
            ||Flappy.buraderType.ToString().Contains("T2")
            ||Flappy.buraderType.ToString().Contains("T3")
            ||Flappy.buraderType.ToString().Contains("T4")
            ) {
            Flappy.buraderType = (Enums.BuraderName) Random.Range(0, (int)Enums.BuraderName.END);
        }
        GameObject sprite;
        sprite = Resources.Load("Buraders/" + Flappy.buraderType.ToString()) as GameObject;
        Flappy.GetComponentInChildren<SpriteRenderer>().sprite = sprite.GetComponent<Image>().sprite;   
        Flappy.SpecialCurrent = 0;
        Flappy.GetFightingStyle();
        animator = Resources.Load("Buraders/" + Flappy.buraderType.ToString() + "X") as GameObject;
        Flappy.GetComponent<Animator>().runtimeAnimatorController = animator.GetComponent<Animator>().runtimeAnimatorController;   
        difficulty++;
        if (difficulty > 3) difficulty = 3;
    }
    
    float distance;
    Vector2 between;
    float zoomX;
    float zoomY;
    // Update is called once per frame
    void Update()
    {
        //? camera zoom?
        between = (FlappyOne.transform.position + FlappyTwo.transform.position) / 2;
        distance = Vector2.Distance(FlappyOne.transform.position, FlappyTwo.transform.position);
        if (distance < 3.5f 
        && (FlappyOne.isActiveAndEnabled && FlappyTwo.isActiveAndEnabled) 
        && (FlappyOne.invincibilityDuration <= 0 || FlappyTwo.invincibilityDuration <=0)) 
        {

            if (between.x > this.transform.position.x) {
                if (zoomX < 0.5)
                    zoomX += Time.deltaTime * 1.5f;
            }
            else {
                if (zoomX > -0.5)
                    zoomX -= Time.deltaTime * 1.5f;
            }   
            if (between.y > this.transform.position.y) {
                if (zoomY < 0.9) {
                    zoomY += Time.deltaTime * 1.5f;
                }
            }
            else {
                if (zoomY > -0.9) {
                    zoomY -= Time.deltaTime * 1.5f;
                }
            }

            if (cameraMain.orthographicSize > 5)
                cameraMain.orthographicSize -= Time.deltaTime * 3;
        }
        else {
            if (zoomX > 0) {
                zoomX -= Time.deltaTime;
            }
            else if (zoomX < 0) {
                zoomX += Time.deltaTime ;
            }

            if (zoomY > -0.025) {
                zoomY -= Time.deltaTime;
            }
            else if (zoomY < -0.025) {
                zoomY += Time.deltaTime;
            }
            if (cameraMain.orthographicSize < 6)
                cameraMain.orthographicSize += Time.deltaTime;
        }

        

        cameraMain.transform.position = new Vector3(this.transform.position.x + zoomX, this.transform.position.y + zoomY, -10);

        //stunned duration text
        if (FlappyOne.stunnedDuration > 0)
            stunnedDurationText1.text = "Stunned: " + FlappyOne.stunnedDuration.ToString("0.0");
        else
            stunnedDurationText1.text = "";
        
        if (FlappyTwo.stunnedDuration > 0)
            stunnedDurationText2.text = "Stunned: " + FlappyTwo.stunnedDuration.ToString("0.0");
        else
            stunnedDurationText2.text = "";

        //finisher id duraiton
        if (finisherIDDuration > 0) {
            finisherIDDuration -= Time.deltaTime;
        }
        if (finisherIDDuration <= 0) {
            FinisherIDText.gameObject.SetActive(false);
        }
        //combo duration
        if (FlappyOne.comboDuration <= 0)
            SmashText1.gameObject.SetActive(false);
        if (FlappyTwo.comboDuration <= 0)
            SmashText2.gameObject.SetActive(false);

        if (FlappyOne.SpecialCurrent >= FlappyOne.SpecialMax && FlappyOne.tag != "Dead") {
            ProfilePic1.gameObject.GetComponent<Button>().interactable = true;
            FlappyOne.transform.Find("SUPERSAIYAN").gameObject.SetActive(true);
        }
        else {
            ProfilePic1.gameObject.GetComponent<Button>().interactable = false;
            FlappyOne.transform.Find("SUPERSAIYAN").gameObject.SetActive(false);
        }

        if (FlappyTwo.SpecialCurrent >= FlappyTwo.SpecialMax && FlappyOne.tag != "Dead") {
            ProfilePic2.gameObject.GetComponent<Button>().interactable = true;
            FlappyTwo.transform.Find("SUPERSAIYAN").gameObject.SetActive(true);
        }
        else {
            ProfilePic2.gameObject.GetComponent<Button>().interactable = false;
            FlappyTwo.transform.Find("SUPERSAIYAN").gameObject.SetActive(false);
        }

        if (FlappyOne.speedLineDuration > 0 || FlappyTwo.speedLineDuration > 0) {
            SpeedLine.SetActive(true);
        }
        else {
            SpeedLine.SetActive(false);
        }

        if (FlappyOne.SpecialCurrent < FlappyOne.SpecialMax)
            SpecialCounter1.text = ((FlappyOne.SpecialCurrent / FlappyOne.SpecialMax) * 100).ToString("00.0") + "%";
        else
            SpecialCounter1.text = "Ready!";
        
        if (FlappyTwo.SpecialCurrent < FlappyTwo.SpecialMax)            
            SpecialCounter2.text = ((FlappyTwo.SpecialCurrent / FlappyTwo.SpecialMax) * 100).ToString("00.0") + "%";
        else 
            SpecialCounter2.text = "Ready!";
        
        SpecialGradualIncrease(FlappyOne);
        SpecialGradualIncrease(FlappyTwo);
        dangerPanel.GetComponent<Image>().color = new Color(1, 0, 0, (FlappyOne.dangerAlphaConstant + FlappyTwo.dangerAlphaConstant));


        if (gameMode == Enums.GameMode.HOLD_HANDS) {
            LeftKillsText.text = "Total Kills: " + LangawKills.ToString();
            RightKillsText.text = "Health Card: " + (FlappyOne.life + FlappyTwo.life < 0 ? 0: (FlappyOne.life + FlappyTwo.life));
        }
        else if (gameMode == Enums.GameMode.SURVIVAL) {
            if (FlappyOne.isHooman) {
                LeftKillsText.text = "Total Kills: " + FlappyOne.kills.ToString();
                RightKillsText.text = "";
            }
            else {
                LeftKillsText.text = "";
                RightKillsText.text = "Total Kills: " + FlappyTwo.kills.ToString();;
            }
        }
        else {
            LeftKillsText.text = "P.One Kills: " + FlappyOne.kills.ToString();
            RightKillsText.text = "P.Two Kills: " + FlappyTwo.kills.ToString();
        }
        

        //check if win. Yes, this is lame code
        if (gameMode == Enums.GameMode.HOLD_HANDS) {
            int totalLife = FlappyOne.life + FlappyTwo.life;
            if ((totalLife < 0) && !FlappyCoopLose) {
                FlappyCoopLose = true;
                StartCoroutine(SetMessage("GAME OVER!", "Ay, patay si " + ((FlappyOne.kills > 0) ? FlappyOne.buraderType : FlappyTwo.buraderType)));
                StartCoroutine(GoToResultCoop(((FlappyOne.kills > 0) ? FlappyOne.buraderType : FlappyTwo.buraderType), LangawKills));
                GameEnd.SetActive(true);
            }
        }
        else if (gameMode == Enums.GameMode.SURVIVAL) {
            if (FlappyOne.isHooman)
            if ((FlappyTwo.kills > 0) && !FlappyCoopLose) {
                FlappyCoopLose = true;
                StartCoroutine(SetMessage("GAME OVER!", "Nadali ka ni " + FlappyTwo.buraderType));
                StartCoroutine(GoToResultSurvival(FlappyOne.buraderType, FlappyOne.kills));
                GameEnd.SetActive(true);
            }
            if (FlappyTwo.isHooman)
            if ((FlappyOne.kills > 0) && !FlappyCoopLose) {
                FlappyCoopLose = true;
                StartCoroutine(SetMessage("GAME OVER!", "Nadali ka ni " + FlappyOne.buraderType));
                StartCoroutine(GoToResultSurvival(FlappyTwo.buraderType, FlappyTwo.kills));
                GameEnd.SetActive(true);
            }
        }
        else {
            if (FlappyOne.kills == maxKill && !FlappyOneWins && !FlappyTwoWins) {
                FlappyOneWins = true;
                RandomWinMessage (FlappyOne.buraderType);
                StartCoroutine(GoToResult(FlappyOne, FlappyOne.isHooman ? 1 : 0));
                GameEnd.SetActive(true);
            }
            if (FlappyTwo.kills == maxKill && !FlappyOneWins && !FlappyTwoWins) {
                FlappyTwoWins = true;
                RandomWinMessage (FlappyTwo.buraderType);
                StartCoroutine(GoToResult(FlappyTwo, FlappyTwo.isHooman ? 1 : 0));
                GameEnd.SetActive(true);
            }
        }


        //keyboard input controls

        // if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
        //     OneBreak();
        // }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) {
            OneBreak();
        }
        if (Input.GetKey(KeyCode.A ) || Input.GetAxis("DPAD_H") < 0 || Input.GetAxis("DPAD_V") < 0) {
            OneLeft();
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
            OneRelease();
        }

        if (Input.GetKey(KeyCode.D) || Input.GetAxis("DPAD_H") > 0 || Input.GetAxis("DPAD_V") > 0) {
                OneRight();
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton4)) {
            if (FlappyOne.SpecialCurrent >= FlappyOne.SpecialMax && FlappyOne.tag != "Dead")
                FlappyOne.SpecialUse();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton5)) {
            if (FlappyTwo.SpecialCurrent >= FlappyTwo.SpecialMax && FlappyTwo.tag != "Dead")
                FlappyTwo.SpecialUse();
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket)) {
            FlappyOne.SpecialCurrent += 300;
        }

        if (Input.GetKeyDown(KeyCode.RightBracket)) {
            FlappyTwo.SpecialCurrent += 300;
        }

        if (Input.GetKeyDown(KeyCode.I)) {
            FlappyTwo.transform.rotation = Quaternion.Euler(0,0,0);
            FlappyTwo.GetComponent<Rigidbody2D>().velocity = FlappyTwo.transform.up * force;
        }
        if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.JoystickButton2)) {
                TwoLeft();
        }
        if (Input.GetKey(KeyCode.L) || Input.GetKey(KeyCode.JoystickButton1)) {
                TwoRight();
        }
        if (Input.GetKeyUp(KeyCode.J) || Input.GetKeyUp(KeyCode.L)) {
            TwoRelease();
        }
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.L)) {
            TwoBreak();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            FlappyOne.kills++;
            if (GameController.gameMode == Enums.GameMode.SURVIVAL) { 
                SecretController.AddBP += 200;
                if (FlappyOne.kills > LocalData.SurvivalHighScore) {
                    SecretController.AddBP += 300 * FlappyOne.kills;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            FlappyTwo.kills++;
            if (GameController.gameMode == Enums.GameMode.SURVIVAL) { 
                SecretController.AddBP += 200;
                if (FlappyTwo.kills > LocalData.SurvivalHighScore) {
                    SecretController.AddBP += 300 * FlappyTwo.kills;
                }
            }
        }

        if (isFlappyOneAI && canAIMove1) {
            if (difficulty == 0) {
                // AIver1(FlappyOne);
                StartCoroutine(AIver2(FlappyOne));
            }
            else {
                StartCoroutine(AIver2(FlappyOne));
            }
        }
        if (isFlappyTwoAI && canAIMove2) {
            if (TutorialManager.isTutorial) {
                // AIver1(FlappyTwo);
                AIver1(FlappyTwo);
            }
            else {
                StartCoroutine(AIver2(FlappyTwo));
            }
        }
    }
    float specialChargeSpeed;
    private void SpecialGradualIncrease(YourBurader Flappy) {
        if (Flappy.gameObject.activeInHierarchy && !Flappy.LokiaSkill) {
            specialChargeSpeed = ((3 - (((float)Flappy.HPCurrent / (float)Flappy.HPMax) * 3)));
            
            if (!Flappy.isHooman && Flappy.buraderType == Enums.BuraderName.GIO) {
                Flappy.SpecialCurrent += (Time.deltaTime * specialChargeSpeed * 4);
            }
            else 
            {
                if(!Flappy.isHooman) { // if AI
                    if (difficulty == 3)
                        Flappy.SpecialCurrent += (Time.deltaTime * specialChargeSpeed * 1.6f * (LocalData.DynamicDifficulty / (4 - difficulty)));
                    else
                        Flappy.SpecialCurrent += (Time.deltaTime * specialChargeSpeed * (LocalData.DynamicDifficulty / (4 - difficulty)));
                }
                else {
                    Flappy.SpecialCurrent += (Time.deltaTime * specialChargeSpeed);
                }
            }
        }
        else {
            if (Flappy.LokiaSkill) {
                Flappy.SpecialCurrent -= Time.deltaTime * 10;
            }
            
            SmashText1.gameObject.SetActive(false);
            SmashText2.gameObject.SetActive(false);
        }
    }
    private void RandomWinMessage(Enums.BuraderName winner) {
        switch (Random.Range(0, 5)) {
            case 0:
                StartCoroutine(SetMessage("FIGHT END!", "Durog ka kay " + winner));
                break;
            case 1:
                StartCoroutine(SetMessage("CONFLICT RESOLVED!", "Bati na kayo ni " + winner));
                break;
            case 2:
                StartCoroutine(SetMessage("GAME SET!", "Ang nagwagi - " + winner));
                break;
            case 3:
                StartCoroutine(SetMessage("VENGEANCE IS SERVED!", "Justice is with " + winner));
                break;
            case 4:
                StartCoroutine(SetMessage("PARTY IS OVER!", "GGWP " + winner));
                break;
        }
    }

    IEnumerator GoToResult(YourBurader winnerBurader, int winnerPlayer) {
        yield return new WaitForSeconds(3f);
        PlayerPrefs.SetInt(Prefs.WINNERisHOOMAN, winnerPlayer);
        PlayerPrefs.SetInt(Prefs.WINNERburaderType, (int)winnerBurader.buraderType);
        PlayerPrefs.SetInt(Prefs.FLAPPYONE_KILLSint, FlappyOne.kills);
        PlayerPrefs.SetInt(Prefs.FLAPPYTWO_KILLSint, FlappyTwo.kills);
        SceneManager.LoadScene("BattleResult");
    }
    IEnumerator GoToResultCoop(Enums.BuraderName naiwan, int score) {
        yield return new WaitForSeconds(3f);
        PlayerPrefs.SetInt(Prefs.WINNERburaderType, (int)naiwan);
        PlayerPrefs.SetInt(Prefs.FLAPPYONE_KILLSint, score);
        SceneManager.LoadScene("BattleResult");
    }
    IEnumerator GoToResultSurvival(Enums.BuraderName naiwan, int score) {
        yield return new WaitForSeconds(3f);
        PlayerPrefs.SetInt(Prefs.WINNERburaderType, (int)naiwan);
        PlayerPrefs.SetInt(Prefs.FLAPPYONE_KILLSint, score);
        SceneManager.LoadScene("BattleResult");
    }

    public void OneLeftBtn() {
        OneLeft();
    }
    public void OneRightBtn() {
        OneRight();
    }
    public void TwoLeftBtn() {
        TwoLeft();
    }
    public void TwoRightBtn() {
        
        TwoRight();
    }
    public void OneLeft() {
        FlappyOne.isMovingLeft += Time.deltaTime;
        JumpTowards(FlappyOne, 1);
    }
    public void OneRight() {
        FlappyOne.isMovingRight += Time.deltaTime;
        JumpTowards(FlappyOne, -1);
    }
    public void TwoLeft() {
        FlappyTwo.isMovingLeft += Time.deltaTime;
        JumpTowards(FlappyTwo, 1);
    }
    public void TwoRight() {
        FlappyTwo.isMovingRight += Time.deltaTime;
        JumpTowards(FlappyTwo, -1);
    }
    public void OneBreak() {
        Break(FlappyOne);
    }
    public void TwoBreak() {
        Break(FlappyTwo);
    }
    public void OneRelease() {
        MoveRelease(FlappyOne);
    }
    public void TwoRelease() {
        MoveRelease(FlappyTwo);
    }
    
    /// <summary>
    /// jump there
    /// </summary>
    /// <param name="theBurader">FlappyOne or FlappyTwo</param>
    /// <param name="dir">-1 is right</param>
    private void JumpTowards(YourBurader theBurader, int dir) {
        
        if (theBurader.isMovingLeft > 0) {
            if (theBurader.isMovingLeft < theBurader.isMovingRight) {
                dir = 1;
            }
        } 
        if (theBurader.isMovingRight > 0) {
            if (theBurader.isMovingRight < theBurader.isMovingLeft) {
                dir = -1;
            }
        }
        if (theBurader.stunnedDuration <= 0) {
            if (theBurader.gameObject != null) {
                float angle = -40;
                int ericBug = 1;

                if (theBurader.EricSkill) {
                    ericBug = Random.Range(-7, 7);
                }

                theBurader.transform.rotation = Quaternion.Euler(0,0,(angle + (theBurader.acceleration * 50)) * dir * ericBug);
                theBurader.GetComponent<Rigidbody2D>().velocity = theBurader.transform.up * force * theBurader.acceleration;

                if (theBurader.BryanSkill) {
                    theBurader.otherBurader.transform.rotation = Quaternion.Euler(0,0,angle * dir);
                    theBurader.otherBurader.GetComponent<Rigidbody2D>().velocity = theBurader.transform.up * force * theBurader.acceleration;
                }
                
                theBurader.acceleration += Time.deltaTime * (3 + (theBurader.acceleration / 2));
            }
        }
    }
    public void Break(YourBurader theBurader) {
        if (theBurader.stunnedDuration <= 0) {
            theBurader.acceleration = 1;
            if (theBurader.gameObject != null) {
                if (theBurader.GetComponent<Rigidbody2D>().velocity.magnitude > 12) {  
                    GameObject flash = Resources.Load("Particles/FLASH") as GameObject;
                    flash = Instantiate(flash, theBurader.transform.position, theBurader.transform.rotation, theBurader.transform);
                    AudioManager.instance.PlaySFX(SFXS.SPLASH);
                    theBurader.GetComponent<Rigidbody2D>().drag = 35;
                    theBurader.transform.rotation = Quaternion.Euler(0,0,0);
                    theBurader.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                }
            }
        }
    }
    public void MoveRelease(YourBurader theBurader) {
        theBurader.isMovingLeft = 0;
        theBurader.isMovingRight = 0;
    }


    bool isFlappyOneAI = false;
    bool isFlappyTwoAI = false;
    float botSize = 2f;
    public void GoGoAI(YourBurader theAI) {
        
    }
    bool canAIMove1 = true;
    bool canAIMove2 = true;
    void FixedUpdate() {
        
    }
    GameObject[] killers;
    bool toCharge;
    bool AIMood1;
    bool AIMood2;
    bool AIDash;
    bool AIDashLocked;
    bool DashRight;
    bool AIWarning;
    private IEnumerator AIver2(YourBurader theAI) {
        if (!theAI.gameObject.activeInHierarchy)
            yield break;

        int layerKiller = (int)LayerMask.NameToLayer("Killer"); //look only for base
        Vector3 bot = new Vector2(theAI.transform.position.x - 1, theAI.transform.position.y - 0.75f);
        RaycastHit2D hitBot = Physics2D.Raycast(bot, Vector2.right, 2, 1 << layerKiller);
        Vector3 top = new Vector2(theAI.transform.position.x - 0.75f, theAI.transform.position.y + 0.75f);
        RaycastHit2D hitTop = Physics2D.Raycast(top, Vector2.right, 1.5f, 1 << layerKiller);
        Vector3 head = new Vector2(theAI.transform.position.x, theAI.transform.position.y);
        RaycastHit2D hitHead = Physics2D.Raycast(head, theAI.transform.right, 1f, 1 << layerKiller);

        //mga autocast
        if (theAI.buraderType == Enums.BuraderName.MASSIMO
        ||  theAI.buraderType == Enums.BuraderName.DOGE
        ||  theAI.buraderType == Enums.BuraderName.MARK
        ||  theAI.buraderType == Enums.BuraderName.DARK_M
        ||  difficulty == 0)
        if (theAI.SpecialCurrent >= theAI.SpecialMax && theAI.otherBurader.tag == "Player") {
            theAI.SpecialUse();
        }
        killers = GameObject.FindGameObjectsWithTag("Killers");
        if (killers.Length > 0 && !theAI.otherBurader.GIOSkill) {
            //hitbot safety
            if (difficulty > 0) {
                if (difficulty == 1 && AIMood2) {
                    //wala sa mood
                }
                else {
                    if (hitBot.collider != null) {
                        if (hitBot.collider.transform.position.x > theAI.transform.position.x) {
                            if (theAI.transform.position.y > cameraMain.transform.position.y + 1.2f
                            && theAI.transform.position.x < cameraMain.transform.position.x - 2.2f) {
                                Debug.Log("Cornered hitBot");
                                // AIRight(theAI);
                                AILeft(theAI, 2);
                                yield break;
                            } 
                            else {
                                Debug.Log("hitBot");
                                if (theAI.transform.position.y < cameraMain.transform.position.y - 1.3)
                                    Break(theAI);
                                AILeft(theAI);
                                yield break;
                            }
                        }
                        else {
                            if (theAI.transform.position.y < cameraMain.transform.position.y - 1.3)
                                Break(theAI);
                            AIRight(theAI);
                            yield break;
                        }
                    }
                }
            }
            if (difficulty > 0) {
                if (difficulty == 1 && AIMood2) {
                    //wala sa mood
                }
                else {
                    if (hitTop.collider != null) {
                        if (hitTop.collider.transform.position.x > theAI.transform.position.x) {
                            if (theAI.transform.position.y > cameraMain.transform.position.y// + 1
                            && theAI.transform.position.x < cameraMain.transform.position.x - 2.2f) {
                                Debug.Log("Cornered hitTop");
                                AIRight(theAI);
                                AILeft(theAI);
                                yield break;
                            } 
                            else {
                                // Break(theAI);
                                AILeft(theAI, 2);
                                yield break;
                            }
                        }
                        if (theAI.stunnedDuration <= 0) {
                            Break(theAI);MoveRelease(theAI);
                            theAI.acceleration = 1;
                            theAI.GetComponent<Rigidbody2D>().velocity = Vector3.down;
                        }
                        yield break;
                    }
                }
            }
            Debug.DrawRay(top, Vector2.right * 1.5f, Color.blue);
            Debug.DrawRay(bot, Vector2.right * 2, Color.red);
            foreach(GameObject killer in killers) {
                //? top and bottom
                if (theAI.transform.position.x - 1f > killer.transform.position.x - 2f && theAI.transform.position.x + 1f < killer.transform.position.x + 2 
                &&  theAI.transform.position.y - 2 > killer.transform.position.y - 4 && theAI.transform.position.y + 1 < killer.transform.position.y + 2)
                {
                    //mga walang shield sa special
                    if (theAI.buraderType != Enums.BuraderName.PAU 
                    ||  theAI.buraderType != Enums.BuraderName.IVAN
                    ||  theAI.buraderType != Enums.BuraderName.MASSIMO
                    ||  theAI.buraderType != Enums.BuraderName.DINK
                    ||  theAI.buraderType != Enums.BuraderName.DOGE
                    ||  theAI.buraderType != Enums.BuraderName.POPPINS
                    ||  theAI.buraderType != Enums.BuraderName.SHINMEN) 
                    if (theAI.SpecialCurrent >= theAI.SpecialMax && theAI.otherBurader.tag == "Player") {
                        theAI.SpecialUse();
                    }

                    if (theAI.transform.position.y < killer.transform.position.y) {
                        Debug.Log("above");
                        Break(theAI);MoveRelease(theAI);
                        if (difficulty == 2 && theAI.stunnedDuration <= 0) {
                            if (theAI.transform.position.y < cameraMain.transform.position.y + 2)
                                theAI.GetComponent<Rigidbody2D>().velocity = Vector3.down * 2;
                        }
                        yield break;
                    }
                    else if (theAI.transform.position.y > killer.transform.position.y) {
                        if (theAI.transform.position.x > killer.transform.position.x) {
                            if (theAI.transform.position.y > cameraMain.transform.position.y
                            && theAI.transform.position.x > cameraMain.transform.position.x + 2.2f) {
                                Debug.Log("Cornered Right 1");
                                // theAI.GetComponent<Rigidbody2D>().velocity = Vector3.down * 15;
                                Break(theAI);MoveRelease(theAI);
                                yield break;
                            }
                            Debug.Log("B-moveRight");
                            //Break(theAI);
                            AIRight(theAI);
                            yield break;
                        }
                        else if (theAI.transform.position.x < killer.transform.position.x) {
                            if (theAI.transform.position.y > cameraMain.transform.position.y// + 1
                            && theAI.transform.position.x < cameraMain.transform.position.x - 2.2f) {
                                Debug.Log("Cornered 1");
                                Break(theAI);MoveRelease(theAI);
                                // theAI.GetComponent<Rigidbody2D>().velocity = Vector3.down * 15;
                                yield break;
                            } 
                            else {
                                Debug.Log("B-moveLeft");
                                //Break(theAI);
                                // AILeft(theAI, difficulty + 1);
                                AILeft(theAI);
                                yield break;
                            }
                        }
                    }
                }
                //? left and right
                else if (theAI.transform.position.x - 3 > killer.transform.position.x - 6 && theAI.transform.position.x + 3f < killer.transform.position.x + 6 
                &&  theAI.transform.position.y - 0.5f > killer.transform.position.y - 1 && theAI.transform.position.y + 0.5f < killer.transform.position.y + 1)
                {
                    if (theAI.transform.position.x > killer.transform.position.x) {
                        if (theAI.transform.position.y > cameraMain.transform.position.y
                        && theAI.transform.position.x > cameraMain.transform.position.x + 2.2f) {
                            Debug.Log("Cornered Right 1");
                            // theAI.GetComponent<Rigidbody2D>().velocity = Vector3.down * 15;
                            Break(theAI);MoveRelease(theAI);
                            yield break;
                        }
                        else {
                            Debug.Log("mRight");
                            AIRight(theAI);
                            yield break;
                        }
                    }
                    else if (theAI.transform.position.x < killer.transform.position.x) {
                        if (theAI.transform.position.y > cameraMain.transform.position.y
                        && theAI.transform.position.x < cameraMain.transform.position.x - 2.2f) {
                            Debug.Log("Cornered 2");
                            // theAI.GetComponent<Rigidbody2D>().velocity = Vector3.down * 15;
                            Break(theAI);MoveRelease(theAI);
                            yield break;
                        }
                        else {
                            Debug.Log("moveLeft");
                            // AILeft(theAI, difficulty + 1);
                            AILeft(theAI);
                            yield break;
                        }
                    }
                }
                //safety
                if (theAI.transform.position.x - 3f > killer.transform.position.x - 6 && theAI.transform.position.x + 3f < killer.transform.position.x + 6f 
                &&  theAI.transform.position.y - 3 > killer.transform.position.y - 6 && theAI.transform.position.y + 3 < killer.transform.position.y + 6)
                {
                    if (difficulty > 0) {
                        AIWarning = true;
                        if (hitHead.collider != null) {
                            Break(theAI);
                            MoveRelease(theAI);
                            yield break;
                        }
                        if (theAI.transform.position.x > killer.transform.position.x) {
                            AILeft(theAI);
                            AIRight(theAI);
                        }
                        else {
                            AIRight(theAI);
                            AILeft(theAI);
                        }
                        //no dash. pls
                        if (AIDashLocked) {
                            Debug.Log("DashSTOP~");
                            Break(theAI);
                            if (difficulty >= 2){
                                theAI.GetComponent<Rigidbody2D>().velocity = Vector3.down;
                                if (DashRight) {
                                    AILeft(theAI);
                                }
                                else {
                                    AIRight(theAI);
                                }
                            }
                            AIDashLocked = false;
                            AIDash = false;
                        }
                    }
                }
                else {
                    AIWarning = false;
                    
                }

                //!additional Special Use case

                //enemy has chainsaw below
                if (theAI.otherBurader.transform.position.x - 1f > killer.transform.position.x - 2f && theAI.otherBurader.transform.position.x + 1f < killer.transform.position.x + 2 
                &&  theAI.otherBurader.transform.position.y > killer.transform.position.y && theAI.otherBurader.transform.position.y + 3 < killer.transform.position.y + 6)
                {
                    if (theAI.buraderType == Enums.BuraderName.MAYU
                    ||  theAI.buraderType == Enums.BuraderName.IVAN
                    ||  theAI.buraderType == Enums.BuraderName.SHINMEN
                    ||  theAI.buraderType == Enums.BuraderName.HORORO
                    ||  theAI.buraderType == Enums.BuraderName.RIZAL
                    ||  theAI.buraderType == Enums.BuraderName.GIO
                    ||  theAI.buraderType == Enums.BuraderName.POPPINS) 
                    if (theAI.SpecialCurrent >= theAI.SpecialMax && theAI.otherBurader.tag == "Player") {
                        theAI.SpecialUse();
                    }
                }

                //enemy is above you
                if (theAI.transform.position.x - 1f > theAI.otherBurader.transform.position.x - 2f && theAI.transform.position.x + 1f < theAI.otherBurader.transform.position.x + 2 
                &&  theAI.transform.position.y - 3 > theAI.otherBurader.transform.position.y - 6 && theAI.transform.position.y < theAI.otherBurader.transform.position.y)
                {
                    if (theAI.buraderType == Enums.BuraderName.DINK) 
                    if (theAI.SpecialCurrent >= theAI.SpecialMax && theAI.otherBurader.tag == "Player") {
                        theAI.SpecialUse();
                    }
                }
            }
        }
        //!end of killers check

        //check powerUps
        if (difficulty > 0) {
            if (difficulty == 1 && AIMood1) {
                //walang gagawin
                //pag medium difficulty depende sa Mood kung kukunin P.Up
            } 
            else {
                GameObject burger = GameObject.FindGameObjectWithTag("PowerUp");
                if (burger != null) { 
                    //burger is seen
                    if (burger.transform.position.x > cameraMain.transform.position.x - 2 && burger.transform.position.x < cameraMain.transform.position.x + 2) {
                        //burger is above 
                        if (theAI.transform.position.y < burger.transform.position.y) {
                            //and in range
                            if (burger.transform.position.y - 5 < theAI.transform.position.y) {
                                if (theAI.transform.position.x > burger.transform.position.x) {
                                    Debug.Log("i see burger left");
                                    Break(theAI);
                                    AILeft(theAI);
                                    yield break;
                                }
                                else {
                                    Debug.Log("i see burger right");
                                    Break(theAI);
                                    AIRight(theAI);
                                    yield break;
                                }
                            }
                        }
                    }
                }
            }
        }
        
        //! Special use check if melee
        if ((theAI.transform.position.x - 1.5f > theAI.otherBurader.transform.position.x - 3 && theAI.transform.position.x + 1.5f < theAI.otherBurader.transform.position.x + 3 &&
            theAI.transform.position.y - 1.5f > theAI.otherBurader.transform.position.y - 3 && theAI.transform.position.y + 1.5f < theAI.otherBurader.transform.position.y + 3))
        if (theAI.Style == Enums.Style.MELEE
        &&  theAI.buraderType != Enums.BuraderName.HORORO
        &&  theAI.buraderType != Enums.BuraderName.RIZAL) {
            if (theAI.SpecialCurrent >= theAI.SpecialMax && theAI.otherBurader.tag == "Player") {
                theAI.SpecialUse();                    
            }
        }

        //!restrict AI magsiksikan on top
        if (theAI.transform.position.y > cameraMain.transform.position.y + 3.6f
         && AIMood1) {
            if (toCharge) {
                MoveRelease(theAI);
                yield break;
            }
            else {
                AILeft(theAI);
                if (theAI.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 13) {
                    toCharge = true;
                }
                yield break;
            }   
        }

        //! free movement
        //enemy is close
        if ((theAI.transform.position.x - .75f > theAI.otherBurader.transform.position.x - 1.5f && theAI.transform.position.x + .75f < theAI.otherBurader.transform.position.x + 1.5f &&
            theAI.transform.position.y - 15 > theAI.otherBurader.transform.position.y - 30 && theAI.transform.position.y + 15 < theAI.otherBurader.transform.position.y + 30))
        {
            if (toCharge) {
                MoveRelease(theAI);
                yield break;
            }
            else {
                if (theAI.transform.position.x > theAI.otherBurader.transform.position.x)
                {
                    if (theAI.acceleration >= 2.2f)
                        Break(theAI);
                    AILeft(theAI);
                    yield break;
                }
                else if (theAI.transform.position.x < theAI.otherBurader.transform.position.x) {
                    if (theAI.acceleration >= 2.2f)
                        Break(theAI);
                    if (difficulty < 2)
                        Break(theAI);
                    AIRight(theAI);
                    yield break;
                }
            }
        }
        //if enemy not in range (reverse lang ng sa killer)
        else {
            //AI is lower
            if (theAI.otherBurader.transform.position.y > theAI.transform.position.y) {
                if (AIMood2 && difficulty > 0) {
                    if (toCharge) {
                        MoveRelease(theAI);
                        yield break;
                    }
                    else {
                        if (theAI.transform.position.x > theAI.otherBurader.transform.position.x) {
                            if (theAI.acceleration >= 2.2f)
                                Break(theAI);
                            AIRight(theAI);
                        }
                        else if (theAI.transform.position.x < theAI.otherBurader.transform.position.x) {
                            if (theAI.acceleration >= 2.2f)
                                Break(theAI);
                            AILeft(theAI);
                        }
                        if (theAI.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 13) {
                            toCharge = true;
                        }
                        yield break;
                    }
                }
                else {
                    if (difficulty == 0) {
                        if (Random.Range(0, 2) == 0) {
                            // Break(theAI);
                            AILeft(theAI);
                            yield return new WaitForSeconds(0.3f);
                            yield break;
                        }
                        else {
                            // Break(theAI);
                            AIRight(theAI);
                            yield return new WaitForSeconds(0.3f);
                            yield break;
                        }
                    }
                    else {
                        if (theAI.transform.position.x > theAI.otherBurader.transform.position.x) {
                            if (theAI.acceleration >= 2.2f)
                                Break(theAI);
                            AILeft(theAI);
                            yield break;
                        }
                        else if (theAI.transform.position.x < theAI.otherBurader.transform.position.x) {
                            if (theAI.acceleration >= 2.2f)
                                Break(theAI);
                            AIRight(theAI);
                            yield break;
                        }
                    }
                }
            }
            //AI is higher
            else {
                // if (AIMood1 && difficulty < 2) {
                if (AIMood1 && difficulty > 0) {
                    if (toCharge) {
                        MoveRelease(theAI);
                        yield break;
                    }
                    else {
                        if (theAI.transform.position.x > theAI.otherBurader.transform.position.x)
                        {
                            // AILeft(theAI);
                            AIRight(theAI);
                        }
                        else if (theAI.transform.position.x < theAI.otherBurader.transform.position.x) {
                            // AIRight(theAI);
                            AILeft(theAI);
                            
                        }
                        if (theAI.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 13) {
                            toCharge = true;
                        }
                        yield break;
                    }
                }
                else if (AIMood2 && difficulty == 2 && !AIWarning) {
                    if (theAI.transform.position.x > theAI.otherBurader.transform.position.x)
                    {
                        AIRight(theAI);
                        AILeft(theAI);
                    }
                    else if (theAI.transform.position.x < theAI.otherBurader.transform.position.x) {
                        AILeft(theAI);
                        AIRight(theAI);
                        
                    }
                    if (theAI.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 13) {
                        toCharge = true;
                    }
                    yield break;
                }
                else if (difficulty == 3 && !AIWarning) {
                    if (theAI.transform.position.x > theAI.otherBurader.transform.position.x)
                    // if (AIMood2)
                    {
                        AIRight(theAI);
                        AILeft(theAI);
                    }
                    else if (theAI.transform.position.x < theAI.otherBurader.transform.position.x) {
                    // else {
                        AILeft(theAI);
                        AIRight(theAI);
                        
                    }
                    if (theAI.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 13) {
                        toCharge = true;
                    }
                    yield break;
                }

                if (toCharge && AIDash)
                if (theAI.transform.position.x > theAI.otherBurader.transform.position.x) {
                    AILeft(theAI);
                    yield break;
                }
                else if (theAI.transform.position.x < theAI.otherBurader.transform.position.x) {
                    AIRight(theAI);
                    yield break;
                }
                
                if (AIDash) {
                    if (!AIDashLocked) {
                        if (theAI.transform.position.x < theAI.otherBurader.transform.position.x) {
                            DashRight = true;
                            AIDashLocked = true;
                        }
                        else {
                            DashRight = false;
                            AIDashLocked = true;
                        }
                    }
                    if (AIDashLocked) {
                        Debug.Log("AI DASHHHH");
                        if (DashRight) {
                            AIRight(theAI);
                        }
                        else {
                            AILeft(theAI);
                        }
                        yield break;
                    }
                }

                if (AIMood2) {
                    Break(theAI);
                    if (theAI.transform.position.y < cameraMain.transform.position.y) {
                        if (theAI.transform.position.x < theAI.otherBurader.transform.position.x) {
                            AILeft(theAI);
                            yield break;
                        }
                        else {
                            AIRight(theAI);
                            yield break;
                        }
                    }
                }
                yield break;
            }
        }
    }
    private void AIver1(YourBurader FlappyAI) {
        //ai
        if (FlappyAI.gameObject.activeInHierarchy) {

            int layerKiller = (int)LayerMask.NameToLayer("Killer"); //look only for base
            int layerWall = (int)LayerMask.NameToLayer("Wall"); //look only for base
            int layerPower = (int)LayerMask.NameToLayer("PowerUp"); //look only for base

            Vector3 top = new Vector2(FlappyAI.transform.position.x, FlappyAI.transform.position.y + 0.5f);
            Vector3 topX = new Vector2(FlappyAI.transform.position.x - 4, FlappyAI.transform.position.y + 1f);
            Vector3 midX = new Vector2(FlappyAI.transform.position.x - 4, FlappyAI.transform.position.y - 0.65f);
            Vector3 mid = new Vector2(FlappyAI.transform.position.x, FlappyAI.transform.position.y);
            Vector3 bot = new Vector2(FlappyAI.transform.position.x, FlappyAI.transform.position.y - 0.75f);
            Vector3 botm = new Vector2(FlappyAI.transform.position.x, FlappyAI.transform.position.y - 1f);
            Vector3 botx = new Vector2(FlappyAI.transform.position.x, FlappyAI.transform.position.y - 1.5f);

            RaycastHit2D hitTopX = Physics2D.Raycast(topX, Vector2.right, 8, 1 << layerKiller);
            RaycastHit2D hitMidX = Physics2D.Raycast(midX, Vector2.right, 8, 1 << layerKiller);

            RaycastHit2D hitBurgerFinderTop = Physics2D.Raycast(topX, Vector2.right, 8, 1 << layerPower);
            RaycastHit2D hitBurgerFinderBot = Physics2D.Raycast(midX, Vector2.right, 8, 1 << layerPower);

            //wall left sensor
            RaycastHit2D hitWallLeftMid = Physics2D.Raycast(mid, -Vector2.right, 3, 1 << layerWall);

            RaycastHit2D hitRightTop = Physics2D.Raycast(top, Vector2.right, 2, 1 << layerKiller);
            Debug.DrawRay(top, Vector2.right * 2);
            RaycastHit2D hitRightBotx = Physics2D.Raycast(botx, Vector2.right, 2, 1 << layerKiller);
            Debug.DrawRay(botx, Vector2.right * 2);
            RaycastHit2D hitRightMid = Physics2D.Raycast(mid, Vector2.right, 2, 1 << layerKiller);
            Debug.DrawRay(mid, Vector2.right * 2);
            RaycastHit2D hitRightBot = Physics2D.Raycast(bot, Vector2.right, 2, 1 << layerKiller);
            Debug.DrawRay(bot, Vector2.right * 2, Color.blue);
            RaycastHit2D hitRightBotM = Physics2D.Raycast(botm, Vector2.right, 2, 1 << layerKiller);
            Debug.DrawRay(botm, Vector2.right * 2);

            RaycastHit2D hitLeftTop = Physics2D.Raycast(top, -Vector2.right, 2, 1 << layerKiller);
            Debug.DrawRay(top, -Vector2.right * 2);
            RaycastHit2D hitLeftMid = Physics2D.Raycast(mid, -Vector2.right, 2, 1 << layerKiller);
            Debug.DrawRay(mid, -Vector2.right * 2);
            RaycastHit2D hitLeftBotX = Physics2D.Raycast(botx, -Vector2.right, 2, 1 << layerKiller);
            Debug.DrawRay(botx, -Vector2.right * 2);
            RaycastHit2D hitLeftBot = Physics2D.Raycast(bot, -Vector2.right, 2, 1 << layerKiller);
            Debug.DrawRay(bot, -Vector2.right * 2);
            RaycastHit2D hitLeftBotM = Physics2D.Raycast(botm, -Vector2.right, 2, 1 << layerKiller);
            Debug.DrawRay(botm, -Vector2.right * 2);
            
            RaycastHit2D hitTop = Physics2D.Raycast(FlappyAI.transform.position, Vector2.up, 1.5f, 1 << layerWall);
            Debug.DrawRay(FlappyAI.transform.position, Vector2.up * 1.5f);

            RaycastHit2D hitTBot = Physics2D.Raycast(FlappyAI.transform.position, -Vector2.up, botSize, 1 << layerWall);
            Debug.DrawRay(FlappyAI.transform.position, -Vector2.up * botSize);


            Debug.DrawRay(topX, Vector2.right * 8, Color.red);
            Debug.DrawRay(midX, Vector2.right * 8, Color.red);

        
            //dodge rotating chainsaws
            if ((hitTopX.collider != null)) {
                //if (hitTopX.collider.gameObject.GetComponent<ChainsawMove>().rotation != ChainsawMove.KillerRotate.NOT_ROTATING) {
                    if (hitTopX.collider.transform.position.x > FlappyAI.transform.position.x) {
                        if (hitTopX.collider != null && hitWallLeftMid.collider != null ) {
                            if (hitTopX.collider.transform.position.y > cameraMain.transform.position.y) {
                                MoveRelease(FlappyAI);
                                return;
                            }
                            else {
                                Break(FlappyAI);
                                AILeft(FlappyAI);
                                StartCoroutine(AICanMoveRoutine(FlappyAI));
                            }
                        }
                    }
                    else {
                        Break(FlappyAI);
                        AIRight(FlappyAI);
                        StartCoroutine(AICanMoveRoutine(FlappyAI));
                    }
                //}
            }
            if ((hitMidX.collider != null)){
                //if (hitMidX.collider.gameObject.GetComponent<ChainsawMove>().rotation != ChainsawMove.KillerRotate.NOT_ROTATING) {
                    if (hitMidX.collider.transform.position.x > FlappyAI.transform.position.x) {
                        if (hitMidX.collider != null && hitWallLeftMid.collider != null ) {
                            if (hitMidX.collider.transform.position.y > cameraMain.transform.position.y) {
                                Break(FlappyAI);MoveRelease(FlappyAI);
                                return;
                            }
                            else {
                                Break(FlappyAI);
                                AILeft(FlappyAI);
                                StartCoroutine(AICanMoveRoutine(FlappyAI));
                            }
                        }
                    }
                    else {
                        Break(FlappyAI);
                        AIRight(FlappyAI);
                        StartCoroutine(AICanMoveRoutine(FlappyAI));
                    }
                //}
            }

            // If it hits top wall...
            if (hitTop.collider != null) {
                Break(FlappyAI);
                MoveRelease(FlappyAI);
                return;
            }

            //found 
        
            //dodge killers
            if (hitRightTop.collider != null || hitRightBot.collider != null || hitRightBotx.collider != null || hitRightBotM.collider != null || hitRightMid.collider != null && hitTopX.collider != null) {
                //priority hitbot dapat tumalon
                if (hitRightBot.collider != null) {
                    Break(FlappyAI);
                    AILeft(FlappyAI);
                    StartCoroutine(AICanMoveRoutine(FlappyAI));
                }
                if (hitTopX.collider != null && hitWallLeftMid.collider != null ) {
                    if (hitTopX.collider.transform.position.y > cameraMain.transform.position.y + 2) {
                        Break(FlappyAI);
                        MoveRelease(FlappyAI);
                        return;
                    }
                }
                else if (hitRightTop.collider != null && hitWallLeftMid.collider != null ) {
                    if (hitRightTop.collider.transform.position.y > cameraMain.transform.position.y) {
                        Break(FlappyAI);
                        MoveRelease(FlappyAI);
                        return;
                    }
                }
                else if (hitRightMid.collider != null && hitWallLeftMid.collider != null ) {
                    if (hitRightMid.collider.transform.position.y > cameraMain.transform.position.y) {
                        Break(FlappyAI);
                        MoveRelease(FlappyAI);
                        return;
                    }
                }
                else {
                    Break(FlappyAI);
                    AILeft(FlappyAI);
                    StartCoroutine(AICanMoveRoutine(FlappyAI));
                }
            }
            if (hitLeftTop.collider != null || hitLeftBot.collider != null || hitLeftBotM.collider != null || hitLeftBotX.collider != null || hitLeftMid.collider != null) {
                Break(FlappyAI);
                AIRight(FlappyAI);
                StartCoroutine(AICanMoveRoutine(FlappyAI));
                //return;
            }
            
            if (difficulty == 0) {
                if (hitTBot.collider != null) {
                    if (FlappyAI.Style == Enums.Style.MELEE) {
                        if (FlappyAI.transform.position.x > FlappyAI.otherBurader.transform.localPosition.x) {
                            Break(FlappyAI);
                            AILeft(FlappyAI);
                            StartCoroutine(AICanMoveRoutine(FlappyAI));
                        }
                        else {
                            Break(FlappyAI);
                            AIRight(FlappyAI);
                            StartCoroutine(AICanMoveRoutine(FlappyAI));
                        }
                    }
                    else {
                        if (FlappyAI.transform.position.x > cameraMain.transform.position.x - 1) {
                            AILeft(FlappyAI);
                            Break(FlappyAI);
                            StartCoroutine(AICanMoveRoutine(FlappyAI));
                        }
                        else {
                            AIRight(FlappyAI);
                            Break(FlappyAI);
                            StartCoroutine(AICanMoveRoutine(FlappyAI));
                        }
                    }
                }
            }

            //normal and hard only cares about getting burgers
            else if (difficulty >= 1) {
                GameObject burger = GameObject.FindGameObjectWithTag("PowerUp");
                if (burger != null) { 
                    //burger is seen
                    if (burger.transform.position.x > cameraMain.transform.position.x - 2 && burger.transform.position.x < cameraMain.transform.position.x + 2) {
                        //burger is above 
                        if (FlappyAI.transform.position.y < burger.transform.position.y) {
                            //and in range
                            if (burger.transform.position.y - 5 < FlappyAI.transform.position.y) {
                                if (FlappyAI.transform.position.x > burger.transform.position.x) {
                                    ////Debug.Log("i see burger left");
                                    AILeft(FlappyAI);
                                    StartCoroutine(AICanMoveRoutine(FlappyAI));
                                }
                                else {
                                    ////Debug.Log("i see burger right");
                                    AIRight(FlappyAI);
                                    StartCoroutine(AICanMoveRoutine(FlappyAI));
                                }
                            }
                        }
                        //burger is below
                        else {//if (FlappyAI.transform.position.y > burger.transform.position.y) {// && burger.transform.position.y - 3 > FlappyAI.transform.position.y) {
                            // if (FlappyAI.transform.position.y > cameraMain.transform.position.y - 4f) {
                            //     return;
                            // }
                            // else {
                                //!MOVE FREE
                                if (hitTBot.collider != null) {
                                    if (FlappyAI.Style == Enums.Style.MELEE) {
                                        if (FlappyAI.transform.position.x > FlappyAI.otherBurader.transform.position.x) {
                                            AILeft(FlappyAI, 3);
                                            Break(FlappyAI);
                                            StartCoroutine(AICanMoveRoutine(FlappyAI));
                                        }
                                        else {
                                            AIRight(FlappyAI, 3);
                                            Break(FlappyAI);
                                            StartCoroutine(AICanMoveRoutine(FlappyAI));
                                        }
                                    }
                                    else {
                                        if (FlappyAI.transform.position.x > cameraMain.transform.position.x - 1) {
                                            AILeft(FlappyAI, 3);
                                            Break(FlappyAI);
                                            StartCoroutine(AICanMoveRoutine(FlappyAI));
                                        }
                                        else {
                                            AIRight(FlappyAI, 3);
                                            Break(FlappyAI);
                                            StartCoroutine(AICanMoveRoutine(FlappyAI));
                                        }
                                    }
                                }
                            // }
                        }
                    }
                    else {
                        //!MOVE FREE
                        if (hitTBot.collider != null) {
                            if (FlappyAI.Style == Enums.Style.MELEE) {
                                if (FlappyAI.transform.position.x > FlappyAI.otherBurader.transform.position.x) {
                                    AILeft(FlappyAI, 3);
                                    Break(FlappyAI);
                                    StartCoroutine(AICanMoveRoutine(FlappyAI));
                                }
                                else {
                                    AIRight(FlappyAI, 3);
                                    Break(FlappyAI);
                                    StartCoroutine(AICanMoveRoutine(FlappyAI));
                                }
                            }
                            else {
                                if (FlappyAI.transform.position.x > cameraMain.transform.position.x - 1) {
                                    AILeft(FlappyAI, 3);
                                    Break(FlappyAI);
                                    StartCoroutine(AICanMoveRoutine(FlappyAI));
                                }
                                else {
                                    AIRight(FlappyAI, 3);
                                    Break(FlappyAI);
                                    StartCoroutine(AICanMoveRoutine(FlappyAI));
                                }
                            }
                        }
                    }
                    
                    
                }
                else {
                    //no burger
                    //!MOVE FREE
                    if (hitTBot.collider != null) {
                        if (FlappyAI.Style == Enums.Style.MELEE) {
                            if (FlappyAI.transform.position.x > FlappyAI.otherBurader.transform.localPosition.x) {
                                AILeft(FlappyAI, 3);
                                Break(FlappyAI);
                                StartCoroutine(AICanMoveRoutine(FlappyAI));
                            }
                            else {
                                AIRight(FlappyAI, 3);
                                Break(FlappyAI);
                                StartCoroutine(AICanMoveRoutine(FlappyAI));
                            }
                        }
                        else {
                            if (FlappyAI.transform.position.x > cameraMain.transform.position.x - 1) {
                                AILeft(FlappyAI, 3);
                                Break(FlappyAI);
                                StartCoroutine(AICanMoveRoutine(FlappyAI));
                            }
                            else {
                                AIRight(FlappyAI, 3);
                                Break(FlappyAI);
                                StartCoroutine(AICanMoveRoutine(FlappyAI));
                            }
                        }
                    }
                }
            }

            

            //when to use SPECIAL
            if (difficulty == 0) {
                if (FlappyAI.SpecialCurrent >= FlappyAI.SpecialMax && FlappyAI.otherBurader.tag == "Player") {
                    FlappyAI.SpecialUse();                    
                }
            }
            else if (difficulty == 1) {
                if (FlappyAI.SpecialCurrent >= FlappyAI.SpecialMax && FlappyAI.otherBurader.tag == "Player") {
                    if (FlappyAI.Style == Enums.Style.MELEE) {
                            //in range
                        if ((FlappyAI.transform.position.x - 1f > FlappyAI.otherBurader.transform.position.x - 2f && FlappyAI.transform.position.x + 1f < FlappyAI.otherBurader.transform.position.x + 2f &&
                            FlappyAI.transform.position.y - 1f > FlappyAI.otherBurader.transform.position.y - 2f && FlappyAI.transform.position.y + 1f < FlappyAI.otherBurader.transform.position.y + 2f))
                            {
                                FlappyAI.SpecialUse();
                        }
                    }
                    else {
                        if (hitTopX.collider != null || hitMidX.collider != null) {
                            FlappyAI.SpecialUse();
                        }
                    }
                }
            }
            else if (difficulty == 2) {
                if (FlappyAI.SpecialCurrent >= FlappyAI.SpecialMax && FlappyAI.otherBurader.tag == "Player") {
                    if (FlappyAI.buraderType == Enums.BuraderName.DOGE || FlappyAI.buraderType == Enums.BuraderName.MASSIMO || FlappyAI.buraderType == Enums.BuraderName.DINK || FlappyAI.buraderType == Enums.BuraderName.CHRISTIAN) {
                        FlappyAI.SpecialUse();
                    }
                    else if (FlappyAI.Style == Enums.Style.MELEE) {
                        //in range
                        if ((FlappyAI.transform.position.x - 1f > FlappyAI.otherBurader.transform.position.x - 2f && FlappyAI.transform.position.x + 1f < FlappyAI.otherBurader.transform.position.x + 2f &&
                            FlappyAI.transform.position.y - 1f > FlappyAI.otherBurader.transform.position.y - 2f && FlappyAI.transform.position.y + 1f < FlappyAI.otherBurader.transform.position.y + 2f)
                            || (hitTopX.collider != null || hitMidX.collider)) {
                                FlappyAI.SpecialUse();
                        }
                    }
                    else {
                        if (hitTopX.collider != null || hitMidX.collider != null) {
                            FlappyAI.SpecialUse();
                        }
                    }
                }
            }
            
        }       
         
    }
    private void AILeft(YourBurader a, int count = 1) {
        for (int x = 0; x < count; x++) {
                if (a == FlappyOne) {
                // OneBreak();
                OneLeft();
            }
            if (a == FlappyTwo) {
                // TwoBreak();
                TwoLeft();
            }
        }
        
    }
    private void AIRight(YourBurader a, int count = 1) {
        for (int x = 0; x < count; x++) {
            if (a == FlappyOne) {
                // OneBreak();
                OneRight();
            }
            if (a == FlappyTwo) {
                // TwoBreak();
                TwoRight();
            }
        }
    }
    private void AIRelease(YourBurader a) {
        MoveRelease(a);
    }
    
    IEnumerator AICanMoveRoutine(YourBurader a) {
        float reflex;
        switch (difficulty) {
            case 0: reflex = 0.2f; break;
            case 1: reflex = 0.15f; break;
            default: reflex = 0.1f; break;
        }
        if (a == FlappyOne)
            canAIMove1 = false;
        if (a == FlappyTwo)
            canAIMove2 = false;
        yield return new WaitForSeconds(reflex);
        if (a == FlappyOne)
            canAIMove1 = true;
        if (a == FlappyTwo)
            canAIMove2 = true;
        //yield return null;
    }
    bool isDelayed = true;
    IEnumerator KyleDelay() {
        isDelayed = false;
        yield return new WaitForSeconds(0.18f);
        isDelayed = true;
    }
    IEnumerator ChangeBot() {
        yield return new WaitForSeconds(0.5f);
        if (difficulty == 0) {
            botSize = Random.Range(0f, 8f);
        }
        else if (difficulty == 1) {
            botSize = Random.Range(1f, 6f);
        }
        else {
            botSize = Random.Range(1f, 4.5f);
        }
        StartCoroutine(ChangeBot());
    }
    IEnumerator ChangeCharge() {
        yield return new WaitForSeconds(1f);
        toCharge = false;
        yield return new WaitForSeconds(0.5f);
        toCharge = true;
        StartCoroutine(ChangeCharge());
    }
    IEnumerator ChageMood1() {
        yield return new WaitForSeconds(3);
        AIMood1 = !AIMood1;
        StartCoroutine(ChageMood1());
    }
    IEnumerator ChageMood2() {
        yield return new WaitForSeconds(4);
        AIMood2 = !AIMood2;
        StartCoroutine(ChageMood2());
    }
    IEnumerator ChangeAIDash() {
        yield return new WaitForSeconds(5);
        AIDash = true;
        yield return new WaitForSeconds(0.35f);
        AIDash = false;
        AIDashLocked = false;
        StartCoroutine(ChangeAIDash());
    }
}
