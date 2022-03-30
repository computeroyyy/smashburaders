using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FlappyBuraders.GlobalStuff;

public class ChainsawMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public bool PowerUp = false;
    public float Speed;
    public enum KillerMove {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
    public enum KillerRotate {
        NOT_ROTATING,
        CLOCKWISE,
        COUNTER_CLOCKWISE
    }
    public KillerMove direction;
    public KillerRotate rotation;
    void FixedUpdate()
    {
        if (Speed > 0) {
            switch(direction) {
                case KillerMove.LEFT:
                    this.transform.localPosition = new Vector2 (this.transform.localPosition.x - Time.deltaTime * Speed, this.transform.localPosition.y);
                    break;
                case KillerMove.RIGHT:
                    this.transform.localPosition = new Vector2 (this.transform.localPosition.x + Time.deltaTime * Speed, this.transform.localPosition.y);
                    break;
                case KillerMove.UP:
                    this.transform.localPosition = new Vector2 (this.transform.localPosition.x + Time.deltaTime * 2, this.transform.localPosition.y  + Time.deltaTime * Speed);
                    break;
                case KillerMove.DOWN:
                    this.transform.localPosition = new Vector2 (this.transform.localPosition.x + Time.deltaTime * 2, this.transform.localPosition.y  - Time.deltaTime * Speed);
                    break;
            }
        }

        if (rotation == KillerRotate.CLOCKWISE) {
            transform.Rotate(Vector3.forward * 160 * Time.deltaTime);
        }
        else if (rotation == KillerRotate.COUNTER_CLOCKWISE) {
            transform.Rotate(-Vector3.forward * 160 * Time.deltaTime);
        }
    }
    IEnumerator DeadAnnounceVox (YourBurader theDead) {
        yield return new WaitForSeconds(0.25f);
        if (theDead.kills + theDead.otherBurader.kills == 1) {
            AudioManager.instance.PlayVOX(VOXS.FIRSTBLOOD);
        }
        else if (theDead.consecutiveKills >= 3) {
            AudioManager.instance.PlayVOX(VOXS.SHUTDOWN);
        }
        else {
            switch (theDead.otherBurader.consecutiveKills) {
                case 0:
                case 1: AudioManager.instance.PlayVOX(VOXS.V1X); break;
                case 2: AudioManager.instance.PlayVOX(VOXS.V2X); break;
                case 3: AudioManager.instance.PlayVOX(VOXS.V3X); break;
                case 4: AudioManager.instance.PlayVOX(VOXS.V4X); break;
                case 5: AudioManager.instance.PlayVOX(VOXS.CHAVAGE); break;
                default: AudioManager.instance.PlayVOX(VOXS.IMPOSSIBLE); break;
            }
        }
        theDead.consecutiveKills = 0;
    }
    IEnumerator FollowupVOX(YourBurader nakapatay) {
        string vox;
        if (GameController.gameMode == Enums.GameMode.HOLD_HANDS || (nakapatay.life + nakapatay.otherBurader.life > 0)) {
            StartCoroutine(References.GameController.SetRandomDeadMessage(PromptMessage.GetRandomCoopKillMessages(), nakapatay.otherBurader.buraderType.ToString()));
            vox = VOXS.FOCUS;
        }
        else {
            if (nakapatay.otherBurader.LokiaSkill) {
                vox = VOXS.IMPOSSIBLE;
                StartCoroutine(References.GameController.SetRandomDeadMessage(PromptMessage.GetLokiaKillMessage(), nakapatay.otherBurader.buraderType.ToString()));
            }
            else if (nakapatay.otherBurader.SpecialKillTimer > 0)
            {
                StartCoroutine(References.GameController.SetRandomDeadMessage(PromptMessage.GetRandomSpecialKillMessages(), nakapatay.otherBurader.buraderType.ToString()));
                vox = VOXS.COUNTER;
                if (nakapatay.isHooman) {StartCoroutine(References.GameController.SetFinishMessageRoutine("COUNTER FINISH!", 120));
                LocalData.TechnicalKillCount++;}
            }
            else if (nakapatay.SpecialKillTimer > 0) {
                StartCoroutine(References.GameController.SetRandomDeadMessage(PromptMessage.GetRandomSpecialKillMessages(), nakapatay.otherBurader.buraderType.ToString()));
                vox = VOXS.SPECIALKILL;
                if (nakapatay.isHooman) {StartCoroutine(References.GameController.SetFinishMessageRoutine("SPECIAL FINISH!", 120));
                LocalData.SpecialKillCount++;}
            }
            else if (nakapatay.TechnicalKillTimer > 0) {
                StartCoroutine(References.GameController.SetRandomDeadMessage(PromptMessage.GetRandomTechnicalKillMessages(), nakapatay.otherBurader.buraderType.ToString()));
                vox = VOXS.TECHNICALKILL;
                if (nakapatay.isHooman) {StartCoroutine(References.GameController.SetFinishMessageRoutine("TECHNICAL FINISH!", 100));
                LocalData.TechnicalKillCount++; }
            }
            else {
                StartCoroutine(References.GameController.SetRandomDeadMessage(PromptMessage.GetRandomAccidentDeadMessages(), nakapatay.otherBurader.buraderType.ToString()));
                vox = "";
                // SecretController.AddBP += 5;
            }
            
        }
        yield return new WaitForSeconds(1.7f);
        AudioManager.instance.PlayVOX(vox);
    }
    public void OnCollisionEnter2D(Collision2D hit) {
        if (!PowerUp)
        if (hit.gameObject.tag == "Player" && GameController.gameMode != Enums.GameMode.TRAIN_YOUR_BURADER) {
            //dapat buhay din kalaban bago may tamaan ulet
            YourBurader theDead = hit.gameObject.GetComponent<YourBurader>();

            //? new: BAWAS HP
            if (theDead.HPCurrent > 0
            && !theDead.isInvincible
            && GameController.gameMode != Enums.GameMode.HOLD_HANDS) {
                GameObject blad = Resources.Load("Effects/BloodSplatSmol") as GameObject;
                blad = Instantiate(blad, hit.gameObject.transform.position, hit.transform.rotation);
                Destroy(blad, 7.5f);
                theDead.HPCurrent -= 40 + (theDead.otherBurader.combo * 2);
                theDead.BeInvincible(0, 0.75f);
                AudioManager.instance.PlaySFX(SFXS.SLASH);
                Time.timeScale = 0.25f;
                StartCoroutine(theDead.StunnedRoutine(0.5f));
                StartCoroutine(References.GameController.SetPowerupMessageRoutine(PromptMessage.GetDeadlyHitMessages(theDead.otherBurader.combo).MainMessage));

                if (TutorialManager.isTutorial && TutorialManager.tutorialSeq == 5) {
                    theDead.HPCurrent += 30;
                }
                return;
            }

            
            if (theDead.otherBurader.gameObject.activeInHierarchy
             && !theDead.isInvincible) {
                theDead.HPBar.value = 0;
                theDead.HPBar.transform.Find("Fill Area").gameObject.SetActive(false);
                //add 1 score of the other burader
                if (hit.gameObject.tag == "Player") {
                    if (GameController.gameMode == Enums.GameMode.HOLD_HANDS) {
                        if (theDead.LokiaSkill) {
                            theDead.SpecialCurrent = 0;
                        }
                        else {
                            theDead.life--;
                        }
                        theDead.gameObject.tag = "Dead";
                    }
                    else {
                        //VS match

                        //if lokia
                        if (theDead.LokiaSkill) {
                            Destroy(theDead.GetComponent<SpriteGlow.SpriteGlowEffect>());
                            //theDead.SpecialCurrent = 0;
                        }
                        else {
                            theDead.otherBurader.kills++;
                            
                            theDead.otherBurader.consecutiveKills++;
                            theDead.otherBurader.HPCurrent += 25;
                            if (GameController.gameMode == Enums.GameMode.SURVIVAL) { 
                                SecretController.AddBP += 200;
                                theDead.otherBurader.HPCurrent += 25;
                                if (theDead.otherBurader.kills > LocalData.SurvivalHighScore) {
                                    SecretController.AddBP += 300 * theDead.otherBurader.kills;
                                }
                            }
                        }
                        hit.gameObject.tag = "Dead";
                    }
                }
                if (theDead.buraderType == Enums.BuraderName.DOGE) {
                    GameObject[] linisin = GameObject.FindGameObjectsWithTag("DogePoop");
                    foreach(GameObject poop in linisin) {
                        Destroy(poop);
                    }
                }
                //kill the one that's hit
                GameObject blad = Resources.Load("Effects/BloodSplat") as GameObject;
                blad = Instantiate(blad, hit.gameObject.transform.position, hit.transform.rotation);
                Destroy(blad, 7.5f);
                hit.gameObject.SetActive(false);

                //hit.transform.position = new Vector2(99,99);
                StartCoroutine(Respawn(hit.gameObject.GetComponent<YourBurader>()));
            

                AudioManager.instance.PlaySFX(SFXS.SLASH);
                StartCoroutine(DeadAnnounceVox(theDead));
                ////Debug.Log("consecutive: " + hit.gameObject.GetComponent<YourBurader>().otherBurader.consecutiveKills);
                StartCoroutine(FollowupVOX(hit.gameObject.GetComponent<YourBurader>().otherBurader));
            }
        }
        //if non player object
        else if (hit.gameObject.tag == "PlayerClone" || hit.gameObject.tag == "Langaw") {
            if (hit.gameObject.tag == "Langaw" && GameController.gameMode != Enums.GameMode.HOLD_HANDS){
                AudioManager.instance.PlaySFX(SFXS.SLASH);
                Destroy(hit.gameObject);
                GameObject blad = Resources.Load("Effects/BloodSplatSmol") as GameObject;
                blad = Instantiate(blad, hit.gameObject.transform.position, hit.transform.rotation);
                Destroy(blad, 7.5f);
            }
        }
        //if coop
        if (hit.gameObject.tag == "Langaw") {
            if (GameController.gameMode == Enums.GameMode.HOLD_HANDS) {
                GameController.LangawKills++;
                if (GameController.LangawKills > LocalData.CoopHighScore) {
                    SecretController.AddBP += 100 * GameController.LangawKills;
                }
                SecretController.AddBP += 100;
            }
        }

        if (PowerUp) {
            if (hit.gameObject.tag == "Player" || hit.gameObject.tag == "Invincible") {
                YourBurader thisBurader = hit.gameObject.GetComponent<YourBurader>();
                
                ////Debug.Log(thisBurader + ": " + this.gameObject.name);
                
                if (this.gameObject.name == "HealthCard(Clone)") {
                    AudioManager.instance.PlaySFX(SFXS.HEALS);
                    StartCoroutine(References.GameController.SetRandomDeadMessage(PromptMessage.GetRandomHealthCardMessages(), hit.gameObject.GetComponent<YourBurader>().buraderType.ToString()));
                    thisBurader.life++;
                }
                else if (this.gameObject.name == "Whopper(Clone)") {
                    AudioManager.instance.PlaySFX(SFXS.MUNCH);
                    AudioManager.instance.PlaySFX(SFXS.HEALS);
                    StartCoroutine(References.GameController.SetPowerupMessageRoutine(thisBurader.buraderType + " GOT SPECIAL UP!"));
                    if (!thisBurader.LokiaSkill)
                        thisBurader.SpecialCurrent += 50;
                    else 
                        thisBurader.SpecialCurrent += 25;
                }
                else if (this.gameObject.name == "HotFudge(Clone)") {
                    AudioManager.instance.PlaySFX(SFXS.MUNCH);
                    if (thisBurader.HPCurrent > 0) {
                        AudioManager.instance.PlaySFX(SFXS.HEALS);
                        StartCoroutine(References.GameController.SetPowerupMessageRoutine(thisBurader.buraderType + " RECOVERED HEALTH!"));
                        thisBurader.HPCurrent += 25;
                    }
                }
                
                //this.transform.localScale = Vector3.zero;
                this.transform.position = new Vector2 (99,99);
                Destroy(this.gameObject, 3f);//, 1f);
            }
        }
    }

    // IEnumerator ZoomOutRoutine(YourBurader thisBurader) {
    //     Debug.Log("in");
    //     yield return new WaitForSecondsRealtime(1f);
    //     Debug.Log("out");
    //     Camera.main.transform.parent = null;
    //     Camera.main.orthographicSize = 5;
    //     Time.timeScale = 1f;
    // }

    IEnumerator Respawn(YourBurader burader) {
        burader.otherBurader.BeInvincible(100, 6f);
        yield return new WaitForSeconds(3f);
        burader.Respawn();
        if (TutorialManager.isTutorial) {
            TutorialManager.tutorialSeq++;
            SceneManager.LoadScene("Tutorial");
        }
        
    }
}
