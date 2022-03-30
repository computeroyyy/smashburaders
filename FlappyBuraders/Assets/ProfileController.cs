using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlappyBuraders.GlobalStuff;
public class ProfileController : MonoBehaviour
{

    public Text FullName;
    public Text Type;
    public Text SpecialName;
    public Text SpecialDescription;
    public Text Stuff;
    public GameObject ProfilePanel;
    public GameObject StatsPanel;
    public Button BuraderButton;
    public Enums.BuraderName yourBurader;
    public GameObject F1Panel;
    public GameObject[] stars;
    public Text SkillDescription;

    // Start is called before the first frame update
    void Start()
    {
        if (ProfilePanel) {
            ProfilePanel.SetActive(false);
            StatsPanel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectBurader() {
        if (ProfilePanel) {
            ProfilePanel.SetActive(true);
            StatsPanel.SetActive(false);
        }
        ProfilesStuff theStuff = ProfilesStuff.GetProfiles(yourBurader);
        FullName.text = theStuff.FullName;
        Type.text = theStuff.Type;
        SpecialName.text = theStuff.SpecialName;
        SpecialDescription.text = theStuff.SpecialDescription;
        Stuff.text = theStuff.Stuff;

        GetStars();
    }
    public void GetStars() {
        int starLevel = -1;
        if (LocalData.BuraderStars != null) {
            Debug.Log("buraderStars count: " + LocalData.BuraderStars.Count + "\r\n" + yourBurader);
            foreach (KeyValuePair<Enums.BuraderName, int> star in LocalData.BuraderStars) {
                Debug.Log(star.Key + "|" + star.Value);
                if (star.Key == yourBurader) {
                    starLevel = star.Value;
                }
            }

            for (int x = 0; x <= 2; x++) {
                if (starLevel >= 0)
                    stars[x].SetActive(true);
                else {
                    stars[x].SetActive(false);
                }

                starLevel--;
            }
        }
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains("BuraderSelect")) {
            SkillDescription.gameObject.SetActive(true);
            ProfilesStuff theStuff = ProfilesStuff.GetProfiles(yourBurader);
            SkillDescription.text = theStuff.SpecialName;
            SkillDescription.GetComponentsInChildren<Text>()[1].text = theStuff.SpecialDescription;
        }
    }
    public void Back() {
        ProfilePanel.SetActive(false);
        StatsPanel.SetActive(true);
    }
    public void BackToMenu() {
        AudioManager.instance.PlaySFX(SFXS.TOGGLE);
        if (ProfilePanel.activeInHierarchy) {
            F1Panel.SetActive(false);
            ProfilePanel.SetActive(false);
            StatsPanel.SetActive(true);
            return;
        }
        LocalData.toSave = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
