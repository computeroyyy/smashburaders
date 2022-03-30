using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlappyBuraders.GlobalStuff;

[System.Serializable]
public class Save
{
    public List<Enums.BuraderName> locked;
    public List<Enums.Stage> lockedStage;
    public int BP;
    public int SurvivalHighScore;
    public int CoopHighScore;
    public int TotalMatches;
    public bool hasGonePRO;
    public bool isUpdated0001;
    public Dictionary<Enums.BuraderName, int> Stars;
    public List<Enums.BuraderName> arcadeEnemies = new List<Enums.BuraderName>();
    public Enums.BuraderName arcadeLastPick = new Enums.BuraderName();
    public int continueCount = 0;
}
