using System.IO;
using UnityEngine;
using FlappyBuraders.GlobalStuff;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveController : MonoBehaviour
{
    public static SaveController instance;

	void Awake () {
		if (instance == null)
			instance = this;
		else {
			Destroy(this.gameObject);
			return;
		}
        DontDestroyOnLoad(this.gameObject);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Start() {
        if (LocalData.toSave)
            SaveController.SaveGame();
        else {
            //at the start of game onley
            GetEmptySlots();
            SaveController.LoadGame();

            LocalData.forSale.Clear();
            LocalData.lockedBuraders.Shuffle();
            int SCount = 10;
            foreach (KeyValuePair<Enums.BuraderName, int> star in LocalData.BuraderStars) {
                SCount += star.Value + 1;
            }
            SCount += (LocalData.SurvivalHighScore);
            SCount += (LocalData.CoopHighScore / 3);
            SCount += LocalData.TotalMatches / 100;
            SCount += ((int)(Enums.BuraderName.END - LocalData.lockedBuraders.Count)) - 4;
            SCount += LocalData.emptySlotBuraders.Count;
            if (LocalData.hasGonePRO) SCount += 10;
        

            Debug.Log("locked buraders count: " + LocalData.lockedBuraders.Count);
            Debug.Log("SCount: " + SCount / 3);
            bool isDone = false;
            if (LocalData.lockedBuraders.Count > 6) {
                int adjuster = 0;
                for (int x = 0; x < 3; x++) {
                    isDone = false;
                    Debug.Log("===============>ForSale# " + (x+1));
                    Debug.Log("toCheck: " + LocalData.lockedBuraders[x + adjuster]);
                    Debug.Log("adjuster = " + adjuster);
                    
                    Enums.BuraderName toCheck = LocalData.lockedBuraders[x + adjuster];
                    while (
                            (
                                toCheck == Enums.BuraderName.PADDING1 
                            || toCheck == Enums.BuraderName.PADDING2 
                            || toCheck == Enums.BuraderName.PADDING3
                            || toCheck.ToString().Contains("T1")
                            || toCheck.ToString().Contains("T2")
                            || toCheck.ToString().Contains("T3")
                            || toCheck.ToString().Contains("T4")
                            || (SCount) < (int)toCheck
                            )
                        && !isDone
                        ) 
                    { //startwhile  
                        //check if you can still add adjuster and not break the array
                        if (LocalData.lockedBuraders.Count > (x + adjuster + 3)) { //+3 kasi may 3 paddings
                            adjuster++;
                            toCheck = LocalData.lockedBuraders[x + adjuster];
                            Debug.Log("adjusted, now: " + adjuster);
                            Debug.Log("toCheck: " + toCheck);
                        }
                        else{
                            LocalData.forSale.Add(Enums.BuraderName.PADDING1);
                            Debug.Log("EH forSale " + x +  ": "  + Enums.BuraderName.PADDING1);
                            isDone = true;
                        }
                    } //endwhile

                    if (!isDone) {
                        LocalData.forSale.Add(LocalData.lockedBuraders[x + adjuster]);
                        Debug.Log("forSale " + x +  ": " + LocalData.lockedBuraders[x + adjuster]);
                    }
                }
            }
            else {
                for (int x = 0; x < LocalData.lockedBuraders.Count; x++) {
                    LocalData.forSale.Add(LocalData.lockedBuraders[x]);
                    Debug.Log("forSale X: " + LocalData.lockedBuraders[x]);
                }
                LocalData.forSale.Add(Enums.BuraderName.PADDING1);
                LocalData.forSale.Add(Enums.BuraderName.PADDING2);
                LocalData.forSale.Add(Enums.BuraderName.PADDING3);
            }
        }
    }
    private static void LockStuffOnStart() {
        //sa StatsPanel kelangan i-specify kung ilan yung starting characters para sa initial rank.
        LocalData.lockedBuraders.Add(Enums.BuraderName.DINK);
        LocalData.lockedBuraders.Add(Enums.BuraderName.PLATIMOON);
        LocalData.lockedBuraders.Add(Enums.BuraderName.ERIC);
        LocalData.lockedBuraders.Add(Enums.BuraderName.LOKIA);
        LocalData.lockedBuraders.Add(Enums.BuraderName.BRYAN);
        LocalData.lockedBuraders.Add(Enums.BuraderName.BRITTANY);
        LocalData.lockedBuraders.Add(Enums.BuraderName.CHRISTIAN);
        LocalData.lockedBuraders.Add(Enums.BuraderName.ZANTETSU);
        LocalData.lockedBuraders.Add(Enums.BuraderName.DOGE);
        LocalData.lockedBuraders.Add(Enums.BuraderName.KYLE);
        LocalData.lockedBuraders.Add(Enums.BuraderName.IVAN);
        LocalData.lockedBuraders.Add(Enums.BuraderName.RIZAL);
        LocalData.lockedBuraders.Add(Enums.BuraderName.MASSIMO);
        LocalData.lockedBuraders.Add(Enums.BuraderName.DARK_M);
        LocalData.lockedBuraders.Add(Enums.BuraderName.GIO);
        LocalData.lockedBuraders.Add(Enums.BuraderName.MIGS);
        LocalData.lockedBuraders.Add(Enums.BuraderName.HORORO);
        foreach (Enums.BuraderName empty in LocalData.emptySlotBuraders) {
            LocalData.lockedBuraders.Add(empty);
        }
        
        LocalData.lockedStage.Add(Enums.Stage.INNER_SPACE);
        LocalData.BP = 9001;
        LocalData.CoopHighScore = 0;
        LocalData.SurvivalHighScore = 0;
    }
    private static Save CreateSaveGameObject()
    {
        Save save = new Save();
        save.locked = LocalData.lockedBuraders;
        save.lockedStage = LocalData.lockedStage;
        save.BP = LocalData.BP;
        save.CoopHighScore = LocalData.CoopHighScore;
        save.SurvivalHighScore = LocalData.SurvivalHighScore;
        save.Stars = LocalData.BuraderStars;
        save.TotalMatches = LocalData.TotalMatches;
        save.hasGonePRO = LocalData.hasGonePRO;
        save.arcadeEnemies = LocalData.arcadeEnemies;
        save.arcadeLastPick = LocalData.arcadeLastPick;
        save.continueCount = LocalData.continueCount;

        //?update the patch
        save.isUpdated0001 = true;

        return save;
    }
    public const string SAVEFILE = "/FurappyDisk.save";
    public static void SaveGame() {
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + SAVEFILE);
        bf.Serialize(file, save);
        file.Close();

        //lagi na sya magsesave dapat
        // LocalData.toSave = false;
        Debug.Log("Saved Game!");
    }
    public static void LoadGame()
    { 
        if (File.Exists(Application.persistentDataPath + SAVEFILE))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + SAVEFILE, FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();
            LocalData.lockedBuraders = save.locked;
            LocalData.lockedStage = save.lockedStage;
            LocalData.BP = save.BP;
            LocalData.CoopHighScore = save.CoopHighScore;
            LocalData.SurvivalHighScore = save.SurvivalHighScore;
            LocalData.TotalMatches = save.TotalMatches;
            LocalData.BuraderStars = save.Stars;   
            LocalData.hasGonePRO = save.hasGonePRO;
            LocalData.arcadeEnemies = save.arcadeEnemies;
            LocalData.arcadeLastPick = save.arcadeLastPick;
            LocalData.continueCount = save.continueCount;

            //?load if updated
            Debug.Log(save.isUpdated0001);
            LocalData.isUpdated0001 = save.isUpdated0001;

            Debug.Log("Loaded Game!");
        }
        else {
            LockStuffOnStart();
            Debug.Log("Create New Game!");
            SaveGame();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
        }

        // if (!LocalData.isUpdated0001) {
        //     Debug.Log("go update");

        //     //patch here
        //     LocalData.lockedBuraders.Add(Enums.BuraderName.PADDING1);
        //     LocalData.lockedBuraders.Add(Enums.BuraderName.PADDING2);
        //     LocalData.lockedBuraders.Add(Enums.BuraderName.PADDING3);

        //     //?end patch
        //     LocalData.isUpdated0001 = true;
        //     SaveGame();
        // }

        LocalData.toSave = true;
    }
    private void GetEmptySlots() {
        //empty slots for future updates
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T109);
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T110);
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T111);
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T112);
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T113);
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T114);
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T115);
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T207);
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T208);
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T209);
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T210);
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T211);
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T212);
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T213);
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T214);
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T215);
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T309);
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T310);
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T404);
        LocalData.emptySlotBuraders.Add(Enums.BuraderName.T405);
    }
}
