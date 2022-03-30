using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeOfDoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ready());
        AudioManager.instance.PlayVOX(VOXS.THE_EYE);
    }

    IEnumerator ready() {
        yield return new WaitForSeconds(1.5f);
        switch (Random.Range(0, 3)) {
            case 0 :
                this.gameObject.GetComponent<Animator>().SetTrigger("lookUp");
                break;
            case 1:
                this.gameObject.GetComponent<Animator>().SetTrigger("lookDown");
                break;
            case 2:
                this.gameObject.GetComponent<Animator>().SetTrigger("lookMid");
                break;
        }
        AudioManager.instance.PlaySFX(SFXS.JUMP);
        yield return new WaitForSeconds(.3f);
        AudioManager.instance.PlaySFX(SFXS.JUMP);
        yield return new WaitForSeconds(.3f);
        AudioManager.instance.PlaySFX(SFXS.JUMP);
        yield return new WaitForSeconds(.4f);
        this.transform.Find("lazurs").gameObject.SetActive(true);
        AudioManager.instance.PlaySFX(SFXS.MECHA);
        
    }
}
