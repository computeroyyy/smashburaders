using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlappyBuraders.GlobalStuff;


public class EndingController : MonoBehaviour
{
    public GameObject FlappyEnd;
    private GameObject explosion1;
    private GameObject explosion2;
    private GameObject explosion3;
    private GameObject explosion4;
    private GameObject explosion5;
    public GameObject WhiteFade; 
    public GameObject BlackFade;
    public GameObject PausePanel;
    public GameObject[] Stars;
    public Image WinnerPic;
    public Text YouGained;
    // Start is called before the first frame update
    void Start()
    {
        LocalData.toShowRateMe = true;
        PlayerPrefs.SetInt(Prefs.STAGENUMBER, 1);
        AudioManager.instance.PlayBGM(BGMS.SILENT);
        FlappyEnd.GetComponent<Rigidbody2D>().drag = 9999;
        explosion1 = Resources.Load("Particles/hitheavy") as GameObject;
        explosion2 = Resources.Load("Particles/hitheavy3") as GameObject;
        explosion3 = Resources.Load("Particles/hit") as GameObject;
        explosion4 = Resources.Load("Particles/SMOKE") as GameObject;
        explosion5 = Resources.Load("Particles/Spark") as GameObject;
        StartCoroutine(RandomExplosions());
        StartCoroutine(ExplodeBig());
        int starLevel = 0;
        starLevel = PlayerPrefs.GetInt(Prefs.DIFFICULTY);
        Enums.BuraderName winner;
        if (PlayerPrefs.GetInt(Prefs.PLAYER1isHOOMAN) == 1) {
            winner = (Enums.BuraderName) PlayerPrefs.GetInt(Prefs.FLAPPYONEint);
        }
        else {
            winner = (Enums.BuraderName) PlayerPrefs.GetInt(Prefs.FLAPPYTWOint);
        }
        Debug.Log(winner.ToString());

        int bonus;
        switch (starLevel) {
            case 0: bonus = 1000; break;
            case 1: bonus = 2500; break;
            case 2: bonus = 5000; break;
            default: bonus = 10000; break;
        }

        bool isFirstTime = true;
        if (LocalData.BuraderStars == null) {
            LocalData.BuraderStars.Add(winner, starLevel);
        }
        else {
            foreach (KeyValuePair<Enums.BuraderName, int> star in LocalData.BuraderStars) {
                Debug.Log("content: " + star.Key);
                if (star.Key == winner) {
                    //tinapos mo na dati
                    if (starLevel > star.Value) {
                        //pero mas mataas difficulty ngaun
                        bonus -= (star.Value + 1) * 1000;
                        isFirstTime = true;
                        LocalData.BuraderStars.Remove(winner);
                        Debug.Log("Removed " + winner + " from dict");
                        break;
                    }
                    else {
                        isFirstTime = false;
                    }
                }
            }
        }
        

        if (isFirstTime) {
            LocalData.BuraderStars.Add(winner, starLevel);
            Debug.Log("Added " + winner + " from dict, with " + (starLevel+1) + " starLevel");
        }
        GameObject sprite = Resources.Load("Buraders/" + winner.ToString()) as GameObject; 
        WinnerPic.sprite = sprite.GetComponent<Image>().sprite;
        for (int x = 0; x <= 2; x++) {
            if (starLevel >= 0)
                Stars[x].SetActive(true);
            else {
                Stars[x].SetActive(false);
            }

            starLevel--;
        }
        YouGained.text = "You Gained " + bonus + " BP" + "\r\nfor winning!\r\n(Bonus BP and STAR is based on Difficulty)";
        LocalData.BP += bonus;
        SaveController.SaveGame();
    }
    IEnumerator RandomExplosions() {
        yield return new WaitForSecondsRealtime(0.05f);
        Vector2 rnd;
        if (FlappyEnd == null) yield break;

        rnd = new Vector2(FlappyEnd.transform.position.x + Random.Range(-1f, 1f), FlappyEnd.transform.position.y + Random.Range(-1f, 1f));
        switch (Random.Range(0, 5)) {
            case 0: Instantiate(explosion1, rnd, this.transform.rotation); AudioManager.instance.PlaySFX(SFXS.SMALLHIT); break;
            case 1: Instantiate(explosion2, rnd, this.transform.rotation); AudioManager.instance.PlaySFX(SFXS.MEDIUMHIT);break;
            case 2: Instantiate(explosion3, rnd, this.transform.rotation); AudioManager.instance.PlaySFX(SFXS.HIT3);break;
            case 3: Instantiate(explosion4, rnd, this.transform.rotation); AudioManager.instance.PlaySFX(SFXS.WINDEXPLODE);break;
            case 4: Instantiate(explosion5, rnd, this.transform.rotation); AudioManager.instance.PlaySFX(SFXS.HIT5);break;
        }
        StartCoroutine(RandomExplosions());
    }
    IEnumerator ExplodeBig() {
        yield return new WaitForSecondsRealtime(5f);
        FlappyEnd.transform.Find("SUPERSAIYAN").gameObject.SetActive(true);
        GameObject superSaiyan;
        superSaiyan = Resources.Load("Particles/ACTIVATE") as GameObject;
        superSaiyan = Instantiate(superSaiyan, this.transform.position, this.transform.rotation, this.transform);
        AudioManager.instance.PlaySFX(SFXS.ICE);
        AudioManager.instance.PlaySFX(SFXS.SPLASH);
        yield return new WaitForSecondsRealtime(0.5f);
        GameObject boom;
        boom = Resources.Load("Effects/SiponBoom") as GameObject;
        boom = Instantiate(boom, this.transform.position, this.transform.rotation, this.transform);
        AudioManager.instance.PlaySFX(SFXS.HEAVYHIT);
        AudioManager.instance.PlaySFX(SFXS.WINDEXPLODE);
        AudioManager.instance.PlaySFX(SFXS.EXPLOSION);
        AudioManager.instance.StopName(BGMS.SILENT);
        // StopCoroutine("RandomExplosions");
        Instantiate(explosion4, FlappyEnd.transform.position, this.transform.rotation); AudioManager.instance.PlaySFX(SFXS.WINDEXPLODE);
        WhiteFade.SetActive(true);
        Destroy(FlappyEnd.gameObject);

        yield return new WaitForSecondsRealtime(2);
        BlackFade.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        
        LocalData.toSave = true;
        PausePanel.SetActive(true);
    }

    public void GoToCredits() {
        AudioManager.instance.PlaySFX(SFXS.READY);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
