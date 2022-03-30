using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlappyBuraders.GlobalStuff;

public class CreditsController : MonoBehaviour
{
    public GameObject TTC1;
    public GameObject TTC2;
    public GameObject TTC3;
    public GameObject TTC4;
    public GameObject TTC5;

    public Sprite[] stuff;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(back());
        // StartCoroutine(changeImage());
        image = null;
        AudioManager.instance.PlayBGM(BGMS.PSALM6);
    }
    IEnumerator back() {
        yield return new WaitForSeconds(10);
        AudioManager.instance.PlayVOX(VOXS.THANKYOU);
        yield return new WaitForSeconds(15);
        TTC1.SetActive(true);
        yield return new WaitForSeconds(110);
        LocalData.BP += 2000;
        AudioManager.instance.PlaySFX(SFXS.HEALS);
        yield return  new WaitForSeconds(10);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    int asd = 0;
    IEnumerator changeImage() {
        yield return new WaitForSeconds(10);
        image.sprite = stuff[asd];
        image.SetNativeSize();
        asd++;
        
        image.gameObject.SetActive(false);
        image.gameObject.SetActive(true);
        StartCoroutine(changeImage());
    }

    // Update is called once per frame
    public void impatient(){
        if (TTC1.activeInHierarchy) {
            AudioManager.instance.PlaySFX(SFXS.INSTANT);
            TTC1.SetActive(false);
            TTC2.SetActive(true);
        }
        else if (TTC2.activeInHierarchy) {
            AudioManager.instance.PlaySFX(SFXS.INSTANT);
            TTC2.SetActive(false);
            TTC3.SetActive(true);
        }
        else if (TTC3.activeInHierarchy) {
            AudioManager.instance.PlaySFX(SFXS.EXPLOSION);
            TTC3.SetActive(false);
            StartCoroutine(OK());
        }
        else if (TTC4.activeInHierarchy) {
            AudioManager.instance.PlaySFX(SFXS.INSTANT);
            TTC4.SetActive(false);
            TTC5.SetActive(true);
        }
        else if (TTC5.activeInHierarchy) {

            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator OK() {
        yield return new WaitForSeconds (5);
        AudioManager.instance.PlaySFX(SFXS.INSTANT);
        TTC4.SetActive(true);
    }
}
