
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBuraders.GlobalStuff {
    public class LocalData : MonoBehaviour
    {
        public static List<Enums.BuraderName> lockedBuraders = new List<Enums.BuraderName>();
        public static List<Enums.BuraderName> emptySlotBuraders = new List<Enums.BuraderName>();
        public static List<Enums.Stage> lockedStage = new List<Enums.Stage>();
        public static int BP;
        public static int SurvivalHighScore;
        public static int CoopHighScore;
        public static bool toSave = false;
        public static bool isContinue;
        public static int continueCount = 0;
        public static List<Enums.BuraderName> forSale = new List<Enums.BuraderName>();
        public static bool isUpdated0001 = false;
        public static List<Enums.BuraderName> arcadeEnemies = new List<Enums.BuraderName>();
        public static Enums.BuraderName arcadeLastPick = new Enums.BuraderName();
        public static int SpecialKillCount;
        public static int TechnicalKillCount;
        public static int TotalMatches = 0;
        public static Dictionary<Enums.BuraderName, int> BuraderStars = new Dictionary<Enums.BuraderName, int>();
        public static bool hasGonePRO = false;
        public static float DynamicDifficulty = 1;

        public static bool toShowRateMe = false;
        public static bool alreadyShownRateMe = false;
        
    }
}