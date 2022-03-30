using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlappyBuraders.GlobalStuff;

public class SelectBurader : MonoBehaviour
{
    bool selected = false;
    Vector3 origPos;
    public GameObject F1Panel;
    public GameObject F1Image;
    public GameObject F1Body;
    public BuraderSelect menuController;
    public ProfileController profileController;
    public Text SkillDescriptionText;
    public void SelectMe(int player) {
        AudioManager.instance.PlaySFX(SFXS.TOGGLE);
        this.transform.Find("Text").gameObject.SetActive(true);
        F1Panel.SetActive(true);
        F1Image.GetComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;
        F1Image.GetComponentInChildren<Text>().text = this.GetComponentInChildren<Text>().text;

        ////Debug.Log(this.gameObject.name);
        if (player == 1) {
            Enum.TryParse(this.gameObject.name, out menuController.buraderType1);
            AudioManager.instance.PlayVOX("N_" + menuController.buraderType1.ToString());

            //for the stars
            Enum.TryParse(this.gameObject.name, out profileController.yourBurader);
        }
        else {
            Enum.TryParse(this.gameObject.name, out menuController.buraderType2);
            AudioManager.instance.PlayVOX("N_" + menuController.buraderType2.ToString());

            Enum.TryParse(this.gameObject.name, out profileController.yourBurader);
        }

        ////Debug.Log(menuController.buraderType1);
        this.transform.Find("Text").gameObject.SetActive(false);
    }

    public void SelectMeProfile() {
        AudioManager.instance.PlaySFX(SFXS.TOGGLE);
        this.transform.Find("Text").gameObject.SetActive(true);
        F1Panel.SetActive(true);
        F1Image.GetComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;
        F1Image.GetComponentInChildren<Text>().text = this.GetComponentInChildren<Text>().text;

        Enum.TryParse(this.gameObject.name, out profileController.yourBurader);
        AudioManager.instance.PlayVOX("N_" + profileController.yourBurader.ToString());

        GameObject change = Resources.Load("Buraders/" + profileController.yourBurader.ToString() + "X") as GameObject;
        F1Body.GetComponent<Image>().sprite = change.GetComponent<SpriteRenderer>().sprite;
        

        ////Debug.Log(menuController.buraderType1);
        this.transform.Find("Text").gameObject.SetActive(false);
    }
    public void UnselectMe() {
        AudioManager.instance.PlaySFX(SFXS.TOGGLE);
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains("BuraderSelect"))
            SkillDescriptionText.gameObject.SetActive(false);
        F1Panel.SetActive(false);
    }
}
