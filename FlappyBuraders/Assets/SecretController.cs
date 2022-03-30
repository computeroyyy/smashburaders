using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FlappyBuraders.GlobalStuff;

public class SecretController : MonoBehaviour
{
    public static int AddBP;
    public Button BackToMenuBtn;
    public Text BPText;
    public Text SurvivalText;
    public Text TeamworkText;
    public Button Item1;
    public Button Item2;
    public Button Item3;
    public GameObject BuyPanel;
    public GameObject NoCashPanel;
    public Text SkillDescription;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayBGM(BGMS.SUBMERGED);
        GameObject sprite;
        //check if current sale is already unlocked
        bool item1isLocked = false;
        bool item2isLocked = false;
        bool item3isLocked = false;
        foreach(Enums.BuraderName a in LocalData.lockedBuraders) {
            if (a == LocalData.forSale[0]) {
                //already locked
                item1isLocked = true;
            }
        }
        foreach(Enums.BuraderName a in LocalData.lockedBuraders) {
            if (a == LocalData.forSale[1]) {
                //already locked
                item2isLocked = true;
            }
        }
        foreach(Enums.BuraderName a in LocalData.lockedBuraders) {
            if (a == LocalData.forSale[2]) {
                //already locked
                item3isLocked = true;
            }
        }
        for (int x = 0; x < 3; x++) {
            if (LocalData.forSale[x].ToString().Contains("PADDING")) {
                switch(x) {
                    case 0: item1isLocked = false; break;
                    case 1: item2isLocked = false; break;
                    case 2: item3isLocked = false; break;
                }
            }
        }
        if (item1isLocked) {
            Item1.transform.Find("NameText").gameObject.GetComponent<Text>().text = LocalData.forSale[0].ToString();
            Item1.transform.Find("PriceText").gameObject.GetComponent<Text>().text = GetPrice(LocalData.forSale[0]).ToString() + "BP";
            sprite = Resources.Load("Buraders/" + LocalData.forSale[0].ToString()) as GameObject;
            Item1.GetComponentInChildren<Image>().sprite = sprite.GetComponent<Image>().sprite;
        }
        else {
            Item1.transform.Find("NameText").gameObject.GetComponent<Text>().text = "Sold Out";
            Item1.transform.Find("PriceText").gameObject.GetComponent<Text>().text = "--";
            Item1.interactable = false;
            sprite = Resources.Load("Buraders/PADDING1") as GameObject;
            Item1.GetComponentInChildren<Image>().sprite = sprite.GetComponent<Image>().sprite;
            Item1.transform.Find("SoldOut").gameObject.SetActive(true); 
        }
        if (item2isLocked) {
            Item2.transform.Find("NameText").gameObject.GetComponent<Text>().text = LocalData.forSale[1].ToString();
            Item2.transform.Find("PriceText").gameObject.GetComponent<Text>().text = GetPrice(LocalData.forSale[1]).ToString() + "BP";
            sprite = Resources.Load("Buraders/" + LocalData.forSale[1].ToString()) as GameObject;
            Item2.GetComponentInChildren<Image>().sprite = sprite.GetComponent<Image>().sprite;
        }
        else {
            Item2.transform.Find("NameText").gameObject.GetComponent<Text>().text = "Sold Out";
            Item2.transform.Find("PriceText").gameObject.GetComponent<Text>().text = "--";
            Item2.interactable = false;
            sprite = Resources.Load("Buraders/PADDING1") as GameObject;
            Item2.GetComponentInChildren<Image>().sprite = sprite.GetComponent<Image>().sprite;
            Item2.transform.Find("SoldOut").gameObject.SetActive(true); 
        }
        if (item3isLocked) {
            Item3.transform.Find("NameText").gameObject.GetComponent<Text>().text = LocalData.forSale[2].ToString();
            Item3.transform.Find("PriceText").gameObject.GetComponent<Text>().text = GetPrice(LocalData.forSale[2]).ToString() + "BP";
            sprite = Resources.Load("Buraders/" + LocalData.forSale[2].ToString()) as GameObject;
            Item3.GetComponentInChildren<Image>().sprite = sprite.GetComponent<Image>().sprite;
        }
        else {
            Item3.transform.Find("NameText").gameObject.GetComponent<Text>().text = "Sold Out";
            Item3.transform.Find("PriceText").gameObject.GetComponent<Text>().text = "--";
            Item3.interactable = false;
            sprite = Resources.Load("Buraders/PADDING1") as GameObject;
            Item3.GetComponentInChildren<Image>().sprite = sprite.GetComponent<Image>().sprite;
            Item3.transform.Find("SoldOut").gameObject.SetActive(true); 
        }
        
        Initialize();
    }
    public void Initialize() {
        BPText.text = "Burader Points: " + LocalData.BP;
        SurvivalText.text = "Survival HighScore: " + LocalData.SurvivalHighScore;
        TeamworkText.text = "Teamwork HighScore: " + LocalData.CoopHighScore;
        NoCashPanel.SetActive(false);
        BuyPanel.SetActive(false);
        SaveController.SaveGame();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)) {
            CheatMagician();
        }
        if (Input.GetKey(KeyCode.JoystickButton1)) {
            CheatMagician2();  
        }
    }
    public void CheatMagician() {
        AudioManager.instance.PlaySFX(SFXS.HEALS);
        LocalData.BP += 2345;
        Initialize();
    }
    public void CheatMagician2() {
        AudioManager.instance.PlaySFX(SFXS.HEALS);
        LocalData.lockedBuraders.Clear();
        Initialize();
    }

    private int GetPrice(Enums.BuraderName item) {
        switch (item) {
            case Enums.BuraderName.BRITTANY:
                return 12000;
            case Enums.BuraderName.PAU:
                return 12000;
            case Enums.BuraderName.MIGS:
                return 12000;
            case Enums.BuraderName.OYYY:
                return 12000;
            case Enums.BuraderName.JF:
                return 12000;
            case Enums.BuraderName.ERIC:
                return 12000;
            case Enums.BuraderName.CHRISTIAN:
                return 12000;
            case Enums.BuraderName.SHINMEN:
                return 16969;
            case Enums.BuraderName.BRYAN:
                return 12000;
            case Enums.BuraderName.MARK:
                return 12000;
            case Enums.BuraderName.KYLE:
                return 12000;
            case Enums.BuraderName.DINK:
                return 15000;
            case Enums.BuraderName.IVAN:
                return 15000;
            case Enums.BuraderName.RIZAL:
                return 15000;
            case Enums.BuraderName.MASSIMO:
                return 50000;
            case Enums.BuraderName.MAYU:
                return 10000;
            case Enums.BuraderName.NICOLLO:
                return 12000;
            case Enums.BuraderName.PLATIMOON:
                return 18000;
            case Enums.BuraderName.HORORO:
                return 18000;
            case Enums.BuraderName.LOKIA:
                return 15000;
            case Enums.BuraderName.ZANTETSU:
                return 18000;
            case Enums.BuraderName.DOGE:
                return 20000;
            case Enums.BuraderName.POPPINS:
                return 12000;
            case Enums.BuraderName.DARK_M:
                return 99999;
            case Enums.BuraderName.GIO:
                return 120000;
            default:
                return 0;
        }
    }

    public void BuyThis(int itemNumber) {
        itemSelected = itemNumber;
        int price = GetPrice(LocalData.forSale[itemNumber]);
        if (price > 0) {
            if (LocalData.BP >= price) {
                AudioManager.instance.PlaySFX(SFXS.READY);
                BuyPanel.SetActive(true);
                GameObject sprite;
                sprite = Resources.Load("Buraders/" + LocalData.forSale[itemNumber].ToString()) as GameObject;
                GameObject toBuy = BuyPanel.transform.Find("ToBuy").gameObject;
                toBuy.GetComponentInChildren<Image>().sprite = sprite.GetComponent<Image>().sprite;
                toBuy.transform.Find("NameText").gameObject.GetComponent<Text>().text = LocalData.forSale[itemNumber].ToString();
                toBuy.transform.Find("PriceText").gameObject.GetComponent<Text>().text = GetPrice(LocalData.forSale[itemNumber]).ToString() + "BP";
                ProfilesStuff theStuff = ProfilesStuff.GetProfiles(LocalData.forSale[itemNumber]);
                SkillDescription.text = theStuff.SpecialName;
                SkillDescription.GetComponentsInChildren<Text>()[1].text = theStuff.SpecialDescription;
            }
            else {
                AudioManager.instance.PlaySFX(SFXS.TOGGLE);
                NoCashPanel.SetActive(true);
                StartCoroutine(NoCashRoutine());
            }
        }
    }
    private IEnumerator NoCashRoutine() {
        NoCashPanel.transform.Find("Message").gameObject.GetComponent<Text>().text = "Not enough\r\nBurader Points.";
        yield return null;
    }
    private IEnumerator NabiliNaRoutine() {
        NoCashPanel.transform.Find("Message").gameObject.GetComponent<Text>().text = "Ok. Enjoy!";
        yield return null;
        // AudioManager.instance.PlaySFX(SFXS.TOGGLE);
        // NoCashPanel.SetActive(false);
        Initialize();
    }
    public void WatchedVideo() {
        NoCashPanel.SetActive(true);
        NoCashPanel.transform.Find("Message").gameObject.GetComponent<Text>().text = "Here's your 500BP as promised!";
        
    }
    int itemSelected;
    public void OKBuy() {
        AudioManager.instance.PlaySFX(SFXS.HEALS);
        int price = GetPrice(LocalData.forSale[itemSelected]);
        LocalData.BP -= price;
        LocalData.lockedBuraders.Remove(LocalData.forSale[itemSelected]);
        switch(itemSelected) {
            case 0: 
                Item1.interactable = false;
                Item1.transform.Find("SoldOut").gameObject.SetActive(true); 
                Item1.transform.Find("NameText").gameObject.GetComponent<Text>().text = "Sold Out";
                Item1.transform.Find("PriceText").gameObject.GetComponent<Text>().text = "--";
                break;
            case 1: 
                Item2.interactable = false;
                Item2.transform.Find("SoldOut").gameObject.SetActive(true); 
                Item2.transform.Find("NameText").gameObject.GetComponent<Text>().text = "Sold Out";
                Item2.transform.Find("PriceText").gameObject.GetComponent<Text>().text = "--";
                break;
            case 2: 
                Item3.interactable = false;
                Item3.transform.Find("SoldOut").gameObject.SetActive(true); 
                Item3.transform.Find("NameText").gameObject.GetComponent<Text>().text = "Sold Out";
                Item3.transform.Find("PriceText").gameObject.GetComponent<Text>().text = "--";
                break;
        }
        BuyPanel.SetActive(false);
        NoCashPanel.SetActive(true);
        StartCoroutine(NabiliNaRoutine());
    }

    public void Nope() {
        AudioManager.instance.PlaySFX(SFXS.TOGGLE);
        BuyPanel.SetActive(false);
    }

    public void BackToMenu() {
        AudioManager.instance.PlaySFX(SFXS.TOGGLE);
        LocalData.toSave = true;
        SceneManager.LoadScene("MainMenu");
    }
    public void ToProfiles() {
        AudioManager.instance.PlaySFX(SFXS.READY);
        SceneManager.LoadScene("Profiles");
    }

    public void PlaySoundREADY() {
        AudioManager.instance.PlaySFX(SFXS.READY);
    }
    public void PlaySoundBACK() {
        AudioManager.instance.PlaySFX(SFXS.SLASH);
    }

}
