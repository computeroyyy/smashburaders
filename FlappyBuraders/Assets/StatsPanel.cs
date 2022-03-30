using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlappyBuraders.GlobalStuff;

public class StatsPanel : MonoBehaviour
{
    public Text starsCount;
    public Text rank;
    public Text SurvivalHS;
    public Text HoldHHS;
    public Text TotalMatches;
    public Text TotalBP;
    public Text UnlockedBuraders;
    // Start is called before the first frame update
    void Start()
    {
        int SCount = 0;
        foreach (KeyValuePair<Enums.BuraderName, int> star in LocalData.BuraderStars) {
            SCount += star.Value + 1;
        }
        SCount += (LocalData.SurvivalHighScore);
        SCount += (LocalData.CoopHighScore / 3);
        SCount += LocalData.TotalMatches / 100;
        SCount += ((int)(Enums.BuraderName.END - LocalData.lockedBuraders.Count) - 11);
        if (LocalData.hasGonePRO) SCount += 10;

        starsCount.text = "= " + SCount.ToString();
        int RankNo = SCount / 3;
        rank.text = RankNo.ToString() + " - " + ((Enums.Rank) (SCount / 3)).ToString();

        SurvivalHS.text = LocalData.SurvivalHighScore.ToString();
        HoldHHS.text = LocalData.CoopHighScore.ToString();
        TotalMatches.text = LocalData.TotalMatches.ToString();
        TotalBP.text = LocalData.BP.ToString();

        UnlockedBuraders.text = ((int)(Enums.BuraderName.END - LocalData.lockedBuraders.Count - 3)).ToString();
    }

}
