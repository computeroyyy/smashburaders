using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlappyBuraders.GlobalStuff;
// using Photon.Pun;

public class YourBurader : MonoBehaviour
{
    const int gravity = 2;
    public YourBurader otherBurader;
    public Enums.BuraderName buraderType;
    public Button ControlLeft;
    public Button ControlRight;
    public GameObject Controls;
    public GameObject YouIndicator;
    public float SpecialKillTimer = 0;
    public float TechnicalKillTimer = 0;
    public int life = 0;
    public int consecutiveKills = 0;

    public int kills;
    public Enums.Style Style;
    public float SpecialMax;
    public float SpecialCurrent;
    public bool isInvincible;
    public bool isHooman;
    public float acceleration = 1;
    public int playerNumber;
    public Camera cameraMain;
    public int HPCurrent;
    public int HPMax = 200;
    public Slider HPBar;
    GameObject particle_hit;
    public GameObject Aura;
    public float dangerAlphaConstant = 0;
    public int combo;
    public float comboDuration;
    public float isMovingLeft = 0;
    public float isMovingRight = 0;
    void Start() {
        kills = 0;
        if (this.tag != "PlayerClone")
            BeInvincible(90, 4.5f);
        
        cameraMain = Camera.main;
        HPCurrent = HPMax;
        
        HPRed = 0;
        combo = 0;
        comboDuration = 0;
        // if (GameController.gameMode == Enums.GameMode.ONLINE)
        //     PhotonNetwork.AddCallbackTarget(this);
    }
    
    public void GetControlButtons() {
        Button[] controlButtons = Controls.GetComponentsInChildren<Button>();
        ControlLeft = controlButtons[0];
        ControlRight = controlButtons[1];
    }
    public float invincibilityDuration;
    public void BeInvincible(int count, float _invincibilityDuration, bool withIndicator = true) {
        invincibilityDuration += _invincibilityDuration;
        // if (this.gameObject.tag == "Invincible") {
        //     StopCoroutine("Invincibility");
        // }
        if (this.gameObject.tag != "PlayeClone"){
            this.gameObject.tag = "Invincible";
            if (withIndicator)
                YouIndicator.SetActive(true);
            // StartCoroutine(Invincibility(_invincibilityDuration));
        }
    }
    public void GetFightingStyle() {
        switch (this.buraderType) {
            case Enums.BuraderName.BRITTANY:
                this.Style = Enums.Style.MELEE;
                this.SpecialMax = 105;
                break;
            case Enums.BuraderName.BRYAN:
                this.Style = Enums.Style.RANGED;
                this.SpecialMax = 140;
                break;
            case Enums.BuraderName.CHRISTIAN:
                this.Style = Enums.Style.RANGED;
                this.SpecialMax = 150;
                break;
            case Enums.BuraderName.DINK:
                this.Style = Enums.Style.RANGED;
                this.SpecialMax = 100;
                break;
            case Enums.BuraderName.ERIC:
                this.Style = Enums.Style.RANGED;
                this.SpecialMax = 90;
                break;
            case Enums.BuraderName.HORORO:
                this.Style = Enums.Style.MELEE;
                this.SpecialMax = 170;
                break;
            case Enums.BuraderName.IVAN:
                this.Style = Enums.Style.RANGED;
                this.SpecialMax = 160;
                break;
            case Enums.BuraderName.KYLE:
                this.Style = Enums.Style.RANGED;
                this.SpecialMax = 120;
                break;
            case Enums.BuraderName.MARK:
                this.Style = Enums.Style.MELEE;
                this.SpecialMax = 160;
                break;
            case Enums.BuraderName.MASSIMO:
                this.Style = Enums.Style.RANGED;
                this.SpecialMax = 120;
                break;
            case Enums.BuraderName.MIGS:
                this.Style = Enums.Style.MELEE;
                this.SpecialMax = 150;
                break;
            case Enums.BuraderName.NICOLLO:
                this.Style = Enums.Style.MELEE;
                this.SpecialMax = 120;
                break;
            case Enums.BuraderName.OYYY:
                this.Style = Enums.Style.MELEE;
                this.SpecialMax = 120;
                break;
            case Enums.BuraderName.PAU:
                this.Style = Enums.Style.MELEE;
                this.SpecialMax = 110;
                break;
            case Enums.BuraderName.PLATIMOON:
                this.Style = Enums.Style.MELEE;
                this.SpecialMax = 140;
                break;
            case Enums.BuraderName.RIZAL:
                this.Style = Enums.Style.MELEE;
                this.SpecialMax = 125;
                break;
            case Enums.BuraderName.MAYU:
                this.Style = Enums.Style.RANGED;
                this.SpecialMax = 130;
                break;
            case Enums.BuraderName.LOKIA:
                this.Style = Enums.Style.RANGED;
                this.SpecialMax = 300;
                break;
            case Enums.BuraderName.ZANTETSU:
                this.Style = Enums.Style.MELEE;
                this.SpecialMax = 165;
                break;
            case Enums.BuraderName.POPPINS:
                this.Style = Enums.Style.RANGED;
                this.SpecialMax = 150;
                break;
            case Enums.BuraderName.JF:
                this.Style = Enums.Style.MELEE;
                this.SpecialMax = 150;
                break;
            case Enums.BuraderName.DOGE:
                this.Style = Enums.Style.RANGED;
                this.SpecialMax = 50;
                break;
            case Enums.BuraderName.SHINMEN:
                this.Style = Enums.Style.RANGED;
                this.SpecialMax = 120;
                break;
            case Enums.BuraderName.DARK_M:
                this.Style = Enums.Style.RANGED;
                this.SpecialMax = 250;
                break;
            case Enums.BuraderName.GIO:
                this.Style = Enums.Style.RANGED;
                this.SpecialMax = 160;
                break;
        }
    }
    

    IEnumerator Invincibility(float duration) {
        yield return new WaitForSeconds(duration);

        YouIndicator.SetActive(false);
        this.gameObject.tag = "Player";
    }

    /// <summary>
    /// not actually respawn, just teleports back to camera view.false.i think
    /// </summary>
    public void Respawn() {
        if (this.LokiaSkill) {
            this.LokiaSkill = false;
            this.HPCurrent = 50;
            this.SpecialCurrent = 0;
        }
        else {
            this.HPCurrent = HPMax;
        }
        this.gameObject.SetActive(true);
        this.transform.position = new Vector2(cameraMain.transform.position.x, cameraMain.transform.position.y);
        this.gameObject.GetComponent<Rigidbody2D>().velocity = this.transform.up * 15;
        this.gameObject.tag = "Player";
        this.gameObject.GetComponent<Rigidbody2D>().drag = 20;
        this.otherBurader.GetComponent<Rigidbody2D>().gravityScale = gravity;
        this.GetComponent<Rigidbody2D>().gravityScale = gravity;
        // ControlLeft.interactable = true;
        // ControlRight.interactable = true;
        Controls.SetActive(true);
        this.BryanSkill = false;
        this.OyyySkill = false;
        this.BritSkill = false;
        this.MigsSkill = false;
        this.EricSkill = false;
        this.KyleSkill = false;
        otherBurader.KyleSkill = false;
        this.stunnedDuration = 0;
        this.steadyWhenStunned = false;
        EricSkillDuration = -1;
        this.GetComponent<SpriteRenderer>().flipY = false;
        this.otherBurader.GetComponent<SpriteRenderer>().flipY = false;

        if (this.gameObject.GetComponent<SpriteGlow.SpriteGlowEffect>() != null)
            Destroy(this.gameObject.GetComponent<SpriteGlow.SpriteGlowEffect>());
        if (this.gameObject.GetComponent<LaserStickMove>() != null)
            Destroy(this.gameObject.GetComponent<LaserStickMove>());
        if (otherBurader.gameObject.GetComponent<LaserStickMove>() != null)
            Destroy(otherBurader.gameObject.GetComponent<LaserStickMove>());

        hacked = GameObject.Find("hacked(Clone)");
        if (hacked != null) {
            Destroy(hacked.gameObject);
        }
        hacked = GameObject.Find("Sipon(Clone)");
        if (hacked != null) {
            Destroy(hacked.gameObject);
        }
        hacked = GameObject.Find("NANI(Clone)");
        if (hacked != null) {
            Destroy(hacked.gameObject);
        }
        if (this.transform.Find("KAT(Clone)") != null) {
            Destroy(this.transform.Find("KAT(Clone)").gameObject);
        }

        if (GameController.gameMode == Enums.GameMode.SURVIVAL && !isHooman) {
            References.GameController.RandomBuraders(this);
            if (!References.GameController.FlappyOne.isHooman) {
                GameObject sprite = Resources.Load("Buraders/" + References.GameController.FlappyOne.buraderType.ToString()) as GameObject;
                References.GameController.ProfilePic1.sprite = sprite.GetComponent<Image>().sprite;
            }
            else {
                GameObject sprite = Resources.Load("Buraders/" + References.GameController.FlappyTwo.buraderType.ToString()) as GameObject;
                References.GameController.ProfilePic2.sprite = sprite.GetComponent<Image>().sprite;
            }
        }
        BeInvincible(50, 3f);
    }
    

    public void PowerPAU() {
        GameObject atomic = Resources.Load("Effects/SiponBoom") as GameObject;
        if (this.transform.position.x >= otherBurader.transform.position.x) {
            atomic = Instantiate(atomic, new Vector2(this.transform.position.x -1, this.transform.position.y - 1), this.transform.rotation);
        }
        else {
            atomic = Instantiate(atomic, new Vector2(this.transform.position.x + 1, this.transform.position.y - 1), this.transform.rotation);
        }
        this.GetComponent<Rigidbody2D>().velocity = this.transform.up * 5;
        Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), atomic.GetComponent<CircleCollider2D>());
        this.GetComponent<Rigidbody2D>().drag = 15;
        
    }

    public void PowerCHRISTIAN() {
        GameObject sila = Resources.Load("Buraders/FlappyClone") as GameObject;
        GameObject sprite = Resources.Load("Buraders/" + otherBurader.buraderType.ToString() + "X") as GameObject;
        sila.GetComponent<Animator>().runtimeAnimatorController = sprite.GetComponent<Animator>().runtimeAnimatorController;
        int clonecount = 0;
        while (clonecount <= 4) {
            sila = Instantiate(sila.gameObject, otherBurader.transform.position, this.transform.rotation);
            Destroy(sila.gameObject, 5);
            sila.tag = "PlayerClone";
            Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), sila.GetComponent<CircleCollider2D>());
            if (sila.GetComponent<SpriteRenderer>().enabled == true) {
                sila.GetComponent<Rigidbody2D>().velocity = sila.transform.up * 10;
                // sila.GetComponent<YourBurader>().YouIndicator.SetActive(false);
                clonecount++;
            }
            else {
                Destroy(sila);
                break;
            }
            StartCoroutine(SilaMoves(sila.GetComponent<YourBurader>()));
        }   
        this.GetComponent<Rigidbody2D>().drag = 1500;
        this.GetComponent<CircleCollider2D>().enabled = false;
        alphaColor = this.GetComponent<SpriteRenderer>().material.color;
        alphaColor.a = 0;
        BritSkill = true;
        YouIndicator.SetActive(false);
        Aura.gameObject.SetActive(false);
        this.transform.Find("MAGIC_LOOP").gameObject.SetActive(false);
        steadyWhenStunned = true;
        StartCoroutine(StunnedRoutine(5f));
        //this.GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(ChristianShow());    
    }
    IEnumerator ChristianShow() {
        yield return new WaitForSeconds(5);
        acceleration = 1;
        this.transform.rotation = Quaternion.identity;
        this.GetComponent<Rigidbody2D>().drag = 12;
        this.GetComponent<CircleCollider2D>().enabled = true;
        SpecialUseTimer = 0.4f;
        BritSkill = false;
        steadyWhenStunned = false;
        AudioManager.instance.PlaySFX(SFXS.TING);
        Aura.gameObject.SetActive(true);
        
        this.tag = "Player";
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<Rigidbody2D>().mass = 1;
        BeInvincible(0, 0.5f, true);
    }

    IEnumerator SilaMoves(YourBurader sila) {
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        if (sila != null) {
            AudioManager.instance.PlaySFX(SFXS.POP);
            if (this.otherBurader.transform.position.x < sila.transform.position.x)
                sila.transform.rotation = Quaternion.Euler(0,0, 20);
            else
                sila.transform.rotation = Quaternion.Euler(0,0, -20);
            sila.GetComponent<Rigidbody2D>().velocity = sila.transform.up * 10;
            StartCoroutine(SilaMoves(sila.GetComponent<YourBurader>()));    
        }
    }

    public bool BritSkill = false;
    Color alphaColor;
    public void PowerBRIT() {
        if (!BritSkill) {
            this.GetComponent<Rigidbody2D>().drag = 1500;
            this.GetComponent<CircleCollider2D>().enabled = false;
            alphaColor = this.GetComponent<SpriteRenderer>().material.color;
            alphaColor.a = 0;
            BritSkill = true;
            YouIndicator.SetActive(false);
            Aura.gameObject.SetActive(false);
            this.transform.Find("MAGIC_LOOP").gameObject.SetActive(false);
            //this.GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine(BritShow());    
        }
        
    }
    IEnumerator BritShow() {
        yield return new WaitForSeconds(1);
        acceleration = 2;
        this.transform.rotation = Quaternion.identity;
        this.GetComponent<Rigidbody2D>().drag = 12;
        this.GetComponent<Rigidbody2D>().mass = 999;
        this.GetComponent<CircleCollider2D>().enabled = true;
        SpecialKillTimer = 1.5f;
        SpecialUseTimer = 1;
        AudioManager.instance.PlaySFX(SFXS.TING);
        Aura.gameObject.SetActive(true);
        if (otherBurader.tag == "Dead")
            this.transform.position = new Vector2(cameraMain.transform.position.x, cameraMain.transform.position.y);
        else {
            Time.timeScale = 0.2f;
            AudioManager.instance.PlaySFX(SFXS.MEDIUMHIT);
            particle_hit = Resources.Load("Particles/hitheavy") as GameObject;
            particle_hit = Instantiate(particle_hit, this.transform.position, this.transform.rotation);
            this.transform.position = otherBurader.transform.position;
            otherBurader.GetComponent<Rigidbody2D>().velocity = this.transform.up * 33;
            StartCoroutine(otherBurader.StunnedRoutine(1.5f));
            if (!otherBurader.isInvincible)
                otherBurader.HPCurrent -= 15;
        }
        this.tag = "Player";
        this.GetComponent<Rigidbody2D>().velocity = this.transform.up * 5;
        BritSkill = false;
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<Rigidbody2D>().mass = 1;
        BeInvincible(0, 0.5f, false);
    }

    public bool MigsSkill = false;
    public void PowerMIGS() {
        // this.gameObject.tag = "Rage";
        MigsSkill = true;
        this.GetComponent<Rigidbody2D>().gravityScale = 4;    
        this.gameObject.GetComponent<Rigidbody2D>().mass = 9990;
        SpecialUseTimer = 2f;
        BeInvincible(0, 2, false);
        StartCoroutine(MigsRageDuration(2f));
    }
    IEnumerator MigsRageDuration(float duration) {
        yield return new WaitForSeconds(duration);
        this.gameObject.tag = "Player";
        this.gameObject.GetComponent<Rigidbody2D>().mass = 1;
        this.GetComponent<Rigidbody2D>().gravityScale = gravity;
        MigsSkill = false;
    }
    public bool BryanSkill = false;
    GameObject hacked;
    public void PowerBRYAN() {
        if (!this.BryanSkill) {
            this.GetComponent<Rigidbody2D>().drag = 1000;
            this.GetComponent<Rigidbody2D>().gravityScale = 0;
            // GameObject pink = Resources.Load("Effects/PinkBoom") as GameObject;
            GameObject pink = Resources.Load("Particles/CircleWaveWhite") as GameObject;
            pink = Instantiate(pink, this.transform.position, this.transform.rotation);
            Destroy(pink.gameObject, 1f);
            BeInvincible(50, 2f);
            hacked = Resources.Load("Effects/hacked") as GameObject;
            hacked = Instantiate(hacked, otherBurader.transform.position, otherBurader.transform.rotation, otherBurader.transform);
            this.BryanSkill = true;
            StartCoroutine(otherBurader.StunnedRoutine(2));
            otherBurader.steadyWhenStunned = true;
            StartCoroutine(BryanResetRoutine());
            Time.timeScale = 0.3f;
        }
    }
    IEnumerator BryanResetRoutine() {
        yield return new WaitForSeconds(2f);
        BryanReset();
    }
    public void BryanReset() {
        this.GetComponent<Rigidbody2D>().drag = 0;
        this.GetComponent<Rigidbody2D>().gravityScale = gravity;
        if (hacked.gameObject!= null) {
            Destroy(hacked.gameObject);
        }
        this.BryanSkill = false;
        otherBurader.steadyWhenStunned = false;
        otherBurader.stunnedDuration = 0;
    }
    public bool OyyySkill = false;
    public void PowerOYYY() {
        this.OyyySkill = true;
        // GameObject pink = Resources.Load("Effects/PinkBoom") as GameObject;
        GameObject pink = Resources.Load("Particles/CircleWavePink") as GameObject;
        pink = Instantiate(pink, this.transform.position, this.transform.rotation);
        Destroy(pink.gameObject, 1f);
        GameObject shogyo = Resources.Load("Effects/shogyo") as GameObject;
        shogyo = Instantiate(shogyo, this.transform.position, this.transform.rotation, this.transform);
        Destroy(shogyo.gameObject, 3f);
        Time.timeScale = 0.25f;
        this.GetComponent<CircleCollider2D>().radius = 0.5f;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        this.gameObject.GetComponent<Rigidbody2D>().mass = 9990;
        steadyWhenStunned = true;
        BeInvincible(0, 0.5f);
        StartCoroutine(this.StunnedRoutine(0.5f));
        StartCoroutine(OyyyReset());
    }
    public IEnumerator OyyyReset() {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.tag = "Player";
        OyyySkill = false;
        steadyWhenStunned = false;
        this.GetComponent<CircleCollider2D>().radius = 0.08f;
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = gravity;
        this.gameObject.GetComponent<Rigidbody2D>().mass = 1;
    }

    public void PowerRIZAL() {
        Time.timeScale = 0.25f;
        particle_hit = Resources.Load("Particles/SMOKE") as GameObject;
        Instantiate(particle_hit, this.transform.position, this.transform.rotation);

        this.transform.rotation = Quaternion.Euler(0,0, 20);
        this.transform.position = otherBurader.transform.position;

        AudioManager.instance.PlaySFX(SFXS.INSTANT);
        AudioManager.instance.PlaySFX(SFXS.HEAVYHIT);
        particle_hit = Resources.Load("Particles/ACTIVATE") as GameObject;
        Instantiate(particle_hit, otherBurader.transform.position, this.transform.rotation);
        //this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.up * 2;
        this.gameObject.GetComponent<Rigidbody2D>().drag = 50;
        otherBurader.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.right * 15;
        otherBurader.gameObject.GetComponent<Rigidbody2D>().drag = 20;
        this.steadyWhenStunned = true;
        otherBurader.steadyWhenStunned = true;
        StartCoroutine(RizalSkillEnd());
        BeInvincible(0, 1.0f);
        float callDuration = 2.5f;

        StartCoroutine(this.otherBurader.StunnedRoutine(callDuration));
        if (!otherBurader.isInvincible)
            otherBurader.HPCurrent -= 15;
        otherBurader.steadyWhenStunned = true;
        GameObject afk = Resources.Load("Effects/AFK") as GameObject;
        afk = Instantiate(afk, otherBurader.transform.position, otherBurader.transform.rotation, otherBurader.transform);
        Destroy(afk.gameObject, 1);
        afk = Resources.Load("Effects/CALLME") as GameObject;
        afk = Instantiate(afk, this.transform.position, this.transform.rotation, this.transform);
        Destroy(afk.gameObject, 1);
    }

    IEnumerator RizalSkillEnd() {
        yield return new WaitForSeconds(0.7f);
        this.steadyWhenStunned = false;
        otherBurader.steadyWhenStunned = false;

    }
    public float EricSkillDuration;
    public bool EricSkill = false;
    public void PowerERIC() {
        otherBurader.EricSkillDuration = 4f;
        if (otherBurader.EricSkill == false) {
            otherBurader.EricSkill = true;
            GameObject boom = Resources.Load("Particles/CircleWaveRed") as GameObject;
            boom = Instantiate(boom, this.transform.position, this.transform.rotation);
            Destroy(boom.gameObject, 1f);
            GameObject nani = Resources.Load("Effects/NANI") as GameObject;
            nani = Instantiate(nani, otherBurader.transform.position, otherBurader.transform.rotation, otherBurader.transform);
            otherBurader.transform.parent = nani.transform;
            otherBurader.StartCoroutine(EricReset(nani));
        }
        
    }
    IEnumerator EricReset(GameObject bug) {
        yield return new WaitForSeconds(EricSkillDuration);
        // Destroy(bug.gameObject);
        // otherBurader.EricSkill = false;
    }
    
    public void PowerMASSIMO() {
        GameObject nani = Resources.Load("Effects/Building") as GameObject;
        nani = Instantiate(nani, new Vector2(cameraMain.transform.position.x - 2.3f, cameraMain.transform.position.y - 7f), cameraMain.transform.rotation, this.transform.parent); //, this.transform);
        Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), nani.GetComponentInChildren<BoxCollider2D>());
        StartCoroutine(MassimoSFX());
        Destroy(nani, 4f);
    }
    IEnumerator MassimoSFX() {
        yield return new WaitForSeconds(1f);
        AudioManager.instance.PlaySFX(SFXS.EXPLOSION);
        yield return new WaitForSeconds(2f);
        AudioManager.instance.PlaySFX(SFXS.SPECIAL + "MASSIMO");
    }
    public bool KyleSkill = false;
    public Transform target;
    public void PowerKYLE() {
        if (otherBurader.tag != "Dead") {
            AudioManager.instance.PlaySFX(SFXS.WINDEXPLODE);
            Vector2 pos = new Vector2(this.transform.position.x, this.transform.position.y + 1.5f);
            GameObject nani = Resources.Load("Effects/SEXYPIXEL") as GameObject;
            nani = Instantiate(nani, pos, Quaternion.identity, cameraMain.transform);
            GameObject nano = Resources.Load("Particles/Smoke") as GameObject;
            nano = Instantiate(nano, pos, Quaternion.identity, nani.transform);
            this.GetComponent<Rigidbody2D>().drag = 1500;
            steadyWhenStunned = true;
            StartCoroutine(StunnedRoutine(4));
            StartCoroutine(ResetDragRoutine(4f));
            BeInvincible(0, 4f);
            otherBurader.KyleSkill = true;
            otherBurader.target = nani.transform;
            StartCoroutine(otherBurader.StunnedRoutine(4));
            // GameObject harts = Resources.Load("Effects/HARTS") as GameObject;
            // harts = Instantiate(harts, otherBurader.transform.position, otherBurader.transform.rotation, otherBurader.transform);
            StartCoroutine(otherBurader.KyleReset(nani));
        }
    }
    IEnumerator ResetDragRoutine(float duration) {
        yield return new WaitForSeconds(duration);
        this.GetComponent<Rigidbody2D>().drag = 2;
        this.steadyWhenStunned = false;
    }
    IEnumerator KyleReset(GameObject nani) {
        yield return new WaitForSeconds(4f);
        //eto yung code ng nanghahabul
        if (this.gameObject.GetComponent<LaserStickMove>() != null)
            Destroy(this.gameObject.GetComponent<LaserStickMove>());
        Destroy(nani.gameObject);
        // Destroy(harts.gameObject);
        KyleSkill = false;
        steadyWhenStunned = false;
    }
    public void PowerMARK() {
        GameObject nani = Resources.Load("Buraders/KAT") as GameObject;
        nani = Instantiate(nani, this.transform.position, Quaternion.identity, this.transform);
        GameObject nano = Resources.Load("Particles/ACTIVATE") as GameObject;
        nano = Instantiate(nano, this.transform.position, Quaternion.identity, this.transform);
        Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), nani.GetComponent<CircleCollider2D>());
    }

    public void PowerIVAN() {
        StartCoroutine(IvanShoot(5));
    }
    IEnumerator IvanShoot(int bala) {
        if (bala > 0) {
            AudioManager.instance.PlaySFX(SFXS.POP);
            GameObject nano = Resources.Load("Particles/Smoke") as GameObject;
            nano = Instantiate(nano, this.transform.position, cameraMain.transform.rotation, cameraMain.transform);
            
            GameObject nani = Resources.Load("Effects/CRANE") as GameObject;
            nani = Instantiate(nani, this.transform.position, cameraMain.transform.rotation, cameraMain.transform);
            
            Vector2 dir = new Vector2 (
                otherBurader.transform.position.x - nani.transform.position.x,
                otherBurader.transform.position.y - nani.transform.position.y
            );
            Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), nani.GetComponentInChildren<CircleCollider2D>());
            nani.GetComponent<Rigidbody2D>().velocity = dir.normalized * 15;
            bala--;
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(IvanShoot(bala));
        }
        
    }
    bool steadyWhenStunned = false;
    public void PowerMAYU() {
        // GameObject pink = Resources.Load("Effects/PinkBoom") as GameObject;
        GameObject pink = Resources.Load("Particles/CircleWavePink") as GameObject;
        pink = Instantiate(pink, this.transform.position, this.transform.rotation);
        Destroy(pink.gameObject, 1f);
        GameObject[] toFloat = GameObject.FindGameObjectsWithTag("Killers");
        GameObject.FindGameObjectsWithTag("PlayerClone").CopyTo(toFloat, 0);
        GameObject.FindGameObjectsWithTag("Langaw").CopyTo(toFloat, 0);
        GameObject.FindGameObjectsWithTag("DogePoop").CopyTo(toFloat, 0);
        foreach (GameObject that in toFloat) {
            if (that.gameObject != null && that.GetComponent<Rigidbody2D>()) {
                that.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                that.GetComponent<Rigidbody2D>().gravityScale = -0.11f;
                if (that.tag == "Killers") {
                    that.GetComponent<ChainsawMove>().Speed = 1;
                    that.GetComponent<ChainsawMove>().direction = ChainsawMove.KillerMove.UP;
                }
            }
            
        }
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        this.otherBurader.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        if (this.transform.position.x < otherBurader.transform.position.x)
            this.otherBurader.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.right;
        else 
            this.otherBurader.gameObject.GetComponent<Rigidbody2D>().velocity = -Vector3.right;
        BeInvincible(30, 3f);
        this.GetComponent<Rigidbody2D>().gravityScale = -0.12f;
        StartCoroutine(StunnedRoutine(1f));
        StartCoroutine(otherBurader.StunnedRoutine(4f));
        this.otherBurader.GetComponent<Rigidbody2D>().gravityScale = -0.4f;
        
        steadyWhenStunned = true;
        StartCoroutine(VrarakaRelease(toFloat));
    }
    IEnumerator VrarakaRelease(GameObject[] toFloat) {
        yield return new WaitForSeconds(3f);
        foreach (GameObject that in toFloat) {
            if (that.gameObject != null) {
                //that.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                that.GetComponent<Rigidbody2D>().gravityScale = gravity;
                if (that.tag == "Killers") {
                    that.GetComponent<ChainsawMove>().direction = ChainsawMove.KillerMove.DOWN;
                }
            }
        }
        this.otherBurader.GetComponent<Rigidbody2D>().gravityScale = gravity;
        this.GetComponent<Rigidbody2D>().gravityScale = gravity;
        steadyWhenStunned = false;
    }
    public bool GIOSkill = false;
    public void PowerGIO() {
        GameObject effect = Resources.Load("Effects/HaroWarudoe") as GameObject;
        
        AudioManager.instance.MuteAll();
        AudioManager.instance.PlayVOX(VOXS.HAROWARUDO);
        cameraMain.GetComponent<MoveCamera>().isMoving = false;
        this.transform.parent.gameObject.GetComponent<MoveCamera>().isMoving = false;
        effect = Instantiate(effect, this.transform.position, this.transform.rotation);
        GameObject[] toFloat = GameObject.FindGameObjectsWithTag("Killers");
        GameObject.FindGameObjectsWithTag("PlayerClone").CopyTo(toFloat, 0);
        //GameObject.FindGameObjectsWithTag("Langaw").CopyTo(toFloat, 0);
        //GameObject.FindGameObjectsWithTag("DogePoop").CopyTo(toFloat, 0);
        GameObject spawner = GameObject.FindGameObjectWithTag("Respawn");
        spawner.GetComponent<KillerSpawner>().dontSpawn = true;
        //STAP THEM!
        foreach (GameObject that in toFloat) {
            if (that.gameObject != null && that.GetComponent<Rigidbody2D>()) {
                that.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                that.GetComponent<Rigidbody2D>().gravityScale = 0f;
                that.GetComponent<Rigidbody2D>().mass = 0f;
                that.GetComponent<Rigidbody2D>().angularDrag = 100f;
                that.GetComponent<Rigidbody2D>().drag = 5f;
                if (that.tag == "Killers") {
                    that.GetComponent<ChainsawMove>().Speed = 0;
                    that.GetComponent<ChainsawMove>().rotation = ChainsawMove.KillerRotate.NOT_ROTATING;
                    that.GetComponent<ChainsawMove>().direction = ChainsawMove.KillerMove.UP;
                }
            }
        }
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        this.otherBurader.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        BeInvincible(30, 5f, false);
        this.SpecialUseTimer = 1;
        // this.GetComponent<Rigidbody2D>().gravityScale = 0f;
        StartCoroutine(StunnedRoutine(1f));
        StartCoroutine(otherBurader.StunnedRoutine(5f));
        this.otherBurader.GetComponent<Rigidbody2D>().gravityScale = 0;
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
        steadyWhenStunned = true;
        this.gameObject.GetComponent<Rigidbody2D>().drag = 5;
        this.otherBurader.GetComponent<Rigidbody2D>().drag = 1;
        this.otherBurader.steadyWhenStunned = true;
        otherBurader.GIOSkill = true;
        StartCoroutine(GIORelease(toFloat));
    }
    IEnumerator GIORelease(GameObject[] toFloat) {
        yield return new WaitForSeconds(1f);
        GetComponent<Rigidbody2D>().gravityScale = gravity;
        StartCoroutine(GIOKunaifu(16));
        yield return new WaitForSeconds(4f);
        GameObject[] knaifus = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject knaifu in knaifus) {
            if (knaifu.GetComponent<Rigidbody2D>()){
                knaifu.GetComponent<Rigidbody2D>().drag = 0f;
                Vector2 dir = new Vector2 (
                    otherBurader.transform.position.x - knaifu.transform.position.x,
                    otherBurader.transform.position.y - knaifu.transform.position.y
                );
                knaifu.transform.up = dir.normalized;
                knaifu.GetComponent<Rigidbody2D>().velocity = knaifu.transform.up * 15;
            }
        }
        cameraMain.GetComponent<MoveCamera>().isMoving = true;
        this.transform.parent.gameObject.GetComponent<MoveCamera>().isMoving = true;
        foreach (GameObject that in toFloat) {
            if (that.gameObject != null) {
                //that.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                that.GetComponent<Rigidbody2D>().gravityScale = gravity;
                that.GetComponent<Rigidbody2D>().mass = 1;
                that.GetComponent<Rigidbody2D>().angularDrag = 0.05f;
                that.GetComponent<Rigidbody2D>().drag = 0f;
                if (that.tag == "Killers") {
                    that.GetComponent<ChainsawMove>().direction = ChainsawMove.KillerMove.DOWN;
                    that.GetComponent<Rigidbody2D>().gravityScale = gravity;
                }
            }
        }
        
        this.otherBurader.GetComponent<Rigidbody2D>().gravityScale = gravity;
        this.GetComponent<Rigidbody2D>().gravityScale = gravity;
        this.otherBurader.steadyWhenStunned = false;
        steadyWhenStunned = false;
        this.otherBurader.GetComponent<Rigidbody2D>().drag = 0;
        this.GetComponent<Rigidbody2D>().drag = 0;

        GameObject spawner = GameObject.FindGameObjectWithTag("Respawn");
        spawner.GetComponent<KillerSpawner>().dontSpawn = false;
        AudioManager.instance.UnMuteAll();
        otherBurader.GIOSkill = false;
        StartCoroutine(otherBurader.StunnedRoutine(0.5f));
    }

    IEnumerator GIOKunaifu(int bala) {
        if (bala > 0) {
            AudioManager.instance.PlaySFX(SFXS.POP);
        
            GameObject nani = Resources.Load("Effects/knaifu") as GameObject;
            nani = Instantiate(nani, this.transform.position, cameraMain.transform.rotation, cameraMain.transform);
            Vector2 dir = new Vector2 (
                otherBurader.transform.position.x - nani.transform.position.x,
                otherBurader.transform.position.y - nani.transform.position.y
            );
            nani.transform.up = dir.normalized;
            Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), nani.GetComponentInChildren<CircleCollider2D>());
            nani.GetComponentInChildren<Rigidbody2D>().velocity = dir.normalized;
            nani.GetComponentInChildren<Rigidbody2D>().drag = 55;
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(GIOKunaifu(bala - 1));
        }
    }
    public void PowerDINK() {
        GameObject bomb = Resources.Load("Effects/Bomb") as GameObject;
        bomb = Instantiate(bomb, this.transform.position, this.transform.rotation);
        bomb.GetComponent<Rigidbody2D>().velocity = Vector3.up * 10;
        bomb.GetComponent<bombTimer>().timex = 4;
        Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), bomb.GetComponentInChildren<CircleCollider2D>());
        StartCoroutine(DinkBoom());
    }
    IEnumerator DinkBoom() {
        yield return new WaitForSeconds(4f);
    }

    public void PowerNICOLLO() {
        
        AudioManager.instance.PlaySFX(SFXS.POWERUP);
        GameObject pink = Resources.Load("Effects/Aura") as GameObject;
        pink = Instantiate(pink, this.transform.position, this.transform.rotation, this.transform);
        Destroy(pink.gameObject, 2f);
        this.GetComponent<Rigidbody2D>().drag = 70;
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
        this.stunnedDuration = 2;
        steadyWhenStunned = true;
        BeInvincible(40, 2f);
        StartCoroutine(NicolloBeam());
    
    }
    IEnumerator NicolloBeam() {
        yield return new WaitForSeconds(1);
        this.GetComponent<Rigidbody2D>().drag = 70;
        // BeInvincible(40, 0.78f);
        AudioManager.instance.PlaySFX(SFXS.SHOT);
        StartCoroutine(ResetDragRoutine(1));
        Vector2 dir = new Vector2 (
            otherBurader.transform.position.x - this.transform.position.x,
            otherBurader.transform.position.y - this.transform.position.y
        );
        dir = dir.normalized;
        GameObject nani = Resources.Load("Effects/PewPew") as GameObject;
        nani = Instantiate(nani, this.transform.position, this.transform.rotation, cameraMain.transform);
        
        nani.GetComponent<bombTimer>().timex = 1;
        //nani.transform.rotation = Quaternion.LookRotation(dir);
        Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), nani.GetComponentInChildren<CircleCollider2D>());
        nani.GetComponent<Rigidbody2D>().velocity = dir.normalized * 40;
        nani.GetComponent<Rigidbody2D>().mass = 9999;
        
        
        this.GetComponent<Rigidbody2D>().velocity = -dir.normalized * 5;
        this.GetComponent<Rigidbody2D>().gravityScale = gravity;
    }
    

    public void PowerPLATIMOON() {
        this.GetComponent<Rigidbody2D>().drag = 70;
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
        this.stunnedDuration = 2;
        steadyWhenStunned = true;
        AudioManager.instance.PlaySFX(SFXS.MECHA);
        GameObject pink = Resources.Load("Effects/AuraViolet") as GameObject;
        pink = Instantiate(pink, this.transform.position, this.transform.rotation, this.transform);
        Destroy(pink.gameObject, 2f);

        BeInvincible(40, 2f);

        StartCoroutine(OraOra(40));
        StartCoroutine(OraEnd());
    }
    IEnumerator OraOra(int bala) {
        if (bala > 0) {
            AudioManager.instance.PlaySFX(SFXS.POP);
            AudioManager.instance.PlaySFX(SFXS.HIT5);
            if (bala % 2 == 1) {
                //AudioManager.instance.PlaySFX(SFXS.OLA);
            }
            GameObject nani = Resources.Load("Effects/ORA") as GameObject;
            nani = Instantiate(nani, new Vector2(this.transform.position.x + (Random.Range(-1f, 1f)), this.transform.position.y + (Random.Range(-1f, 1f)))
            , cameraMain.transform.rotation, cameraMain.transform);
            Vector2 dir = new Vector2 (
                otherBurader.transform.position.x - nani.transform.position.x,
                otherBurader.transform.position.y - nani.transform.position.y
            );
            nani.transform.right = -dir;
            Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), nani.GetComponentInChildren<CircleCollider2D>());
            nani.GetComponent<Rigidbody2D>().velocity = dir.normalized * 12;
            bala--;
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(OraOra(bala));
        }
    }
    IEnumerator OraEnd() {
        yield return new WaitForSeconds(2);
        this.GetComponent<Rigidbody2D>().gravityScale = gravity;steadyWhenStunned = false;
    }

    public bool LokiaSkill = false;
    public void PowerLOKIA() {
        AudioManager.instance.PlaySFX(SFXS.SPECIAL + this.buraderType.ToString());
        AudioManager.instance.PlaySFX(SFXS.HIDE);
        invincibilityDuration = 0;
        LokiaSkill = true;
        GameObject boom = Resources.Load("Particles/CircleWaveGreen") as GameObject;
        boom = Instantiate(boom, this.transform.position, this.transform.rotation);
        Destroy(boom.gameObject, 1f);
        GameObject clone = Resources.Load("Effects/LokiaClone") as GameObject;
        clone = Instantiate(clone, this.transform.position, this.transform.rotation);
        clone.tag = "Invincible";
        Destroy(clone.gameObject, 2f);

        //die
        this.GetComponent<Rigidbody2D>().drag = 9999;
        
        StartCoroutine(StunnedRoutine(5));
        
        GameObject chainsaw = Resources.Load("Killers/MissileFromLeft") as GameObject;
        chainsaw = Instantiate(chainsaw, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        chainsaw.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        chainsaw.GetComponent<Rigidbody2D>().velocity = Vector2.up * 10;
        chainsaw.GetComponent<Rigidbody2D>().gravityScale = 5f;
    
    }

    public void PowerDARK_M() {
        //charge
        AudioManager.instance.PlaySFX(SFXS.POWERUP);
        GameObject pink = Resources.Load("Effects/AuraViolet") as GameObject;
        pink = Instantiate(pink, this.transform.position, this.transform.rotation, this.transform);
        Destroy(pink.gameObject, 1.2f);
        this.GetComponent<Rigidbody2D>().drag = 100;
        this.GetComponent<Rigidbody2D>().angularDrag = 100;
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
        this.stunnedDuration = 2;
        steadyWhenStunned = true;
        BeInvincible(40, 2f);
        StartCoroutine(ToDarkCombo());

    }
    IEnumerator ToDarkCombo() {
        yield return new WaitForSeconds(2f);
        this.stunnedDuration = 2;
        GameObject boom = Resources.Load("Effects/AuraViolet") as GameObject;
        boom = Instantiate(boom, this.transform.position, this.transform.rotation, this.transform);
        Destroy(boom, 3);
        BeInvincible(75, 5);
        YouIndicator.SetActive(false);
        
        StartCoroutine(DarkCombo(7));
        SpecialUseTimer = 3f;
        SpecialCurrent = 0;
    }
    IEnumerator DarkCombo(int hits) {
        if (otherBurader.gameObject.activeInHierarchy && hits > 0) {
            AudioManager.instance.PlaySFX(SFXS.INSTANT);
            acceleration = 2;
            particle_hit = Resources.Load("Particles/hitheavy") as GameObject;
            particle_hit = Instantiate(particle_hit, otherBurader.transform.position, this.transform.rotation);
            Destroy(particle_hit.gameObject, 2f);

            StartCoroutine(otherBurader.StunnedRoutine(1f));
            ComboAddShow();
            this.transform.position = otherBurader.transform.position;
            this.transform.rotation = Quaternion.identity;
            this.GetComponent<Rigidbody2D>().mass = 9999;
            if (hits % 2 == 0) {
                if (this.transform.position.y > cameraMain.transform.position.y) {
                    otherBurader.GetComponent<Rigidbody2D>().velocity = Vector3.down * 15;    
                    this.GetComponent<Rigidbody2D>().velocity = Vector3.up * 2;    
                }
                else {
                    otherBurader.GetComponent<Rigidbody2D>().velocity = Vector3.up * 22;    
                    this.GetComponent<Rigidbody2D>().velocity = Vector3.up * 5;    
                }
            }
            else {
                if (this.transform.position.x > cameraMain.transform.position.x) {
                    this.GetComponent<Rigidbody2D>().velocity = Vector3.up * 5;  
                    otherBurader.GetComponent<Rigidbody2D>().velocity = -Vector3.right * 22;      
                }
                else {
                    otherBurader.GetComponent<Rigidbody2D>().velocity = Vector3.right * 12;    
                    this.GetComponent<Rigidbody2D>().velocity = Vector3.up * 5;    
                }
            }

            yield return new WaitForSeconds(0.5f);
            this.GetComponent<Rigidbody2D>().mass = 1;
            this.GetComponent<Rigidbody2D>().gravityScale = gravity;
            StartCoroutine(DarkCombo(hits - 1));
        }
        else {
            steadyWhenStunned = false;
            this.GetComponent<Rigidbody2D>().angularDrag = 0.05f;
        }
        this.SpecialCurrent = 0;
        yield return null;
    }

    bool ZantetsuSkill = false;
    public void PowerZANTETSU() {
        ZantetsuSkill = true;
        this.GetComponent<Rigidbody2D>().drag = 9999;
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
        // if (this.gameObject.GetComponent<SpriteGlow.SpriteGlowEffect>() == null)
        //     this.gameObject.AddComponent<SpriteGlow.SpriteGlowEffect>().GlowColor = Color.red;
        this.GetComponent<CircleCollider2D>().radius = 0.26f;

        GameObject shroud = Resources.Load("Effects/BAHU") as GameObject;
        shroud = Instantiate(shroud, this.transform.position, this.transform.rotation, this.transform);
        Destroy(shroud.gameObject, 3);
        steadyWhenStunned = true;
        StartCoroutine(StunnedRoutine(3f));
        BeInvincible(0,3);
        StartCoroutine(ZantetsuStop());
        //this.tag = "Rage";
    }
    IEnumerator ZantetsuStop() {
        yield return new WaitForSeconds(3);
        ZantetsuSkill = false;
        this.GetComponent<CircleCollider2D>().radius = 0.08f;
        this.tag = "Player";
        this.GetComponent<Rigidbody2D>().drag = 0;
        this.GetComponent<Rigidbody2D>().gravityScale = gravity;
        steadyWhenStunned = false;
    }

    public void PowerDOGE() {
        AudioManager.instance.PlaySFX(SFXS.HIDE);
        GameObject bomb = Resources.Load("Effects/POOP") as GameObject;
        bomb = Instantiate(bomb, this.transform.position, this.transform.rotation);
        bomb.GetComponent<Rigidbody2D>().velocity = Vector3.up * 10;
        bomb.GetComponent<MyPoop>().myDoge = this.gameObject;
        Destroy(bomb.gameObject, 300f);
        GameObject nano = Resources.Load("Particles/Smoke") as GameObject;
        nano = Instantiate(nano, this.transform.position, cameraMain.transform.rotation, cameraMain.transform);
            
    }

    public void PowerPOPPINS() {
        AudioManager.instance.PlaySFX(SFXS.SHOT);
        GameObject stick = Resources.Load("Effects/laserStick") as GameObject;
        stick = Instantiate(stick, this.transform.position, this.transform.rotation);
        stick.GetComponentInChildren<LaserStickMove>().Target = otherBurader.transform;
        this.GetComponent<Rigidbody2D>().drag = 22;
        // BeInvincible(0, 1f);
        Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), stick.GetComponentInChildren<CircleCollider2D>());
    }
    public bool JFSkill = false;
    public void PowerJF() {
        // this.gameObject.tag = "Rage";
        JFSkill = true;
        AudioManager.instance.PlaySFX(SFXS.EXPLOSION);
        GameObject shroud = Resources.Load("Effects/BAHU") as GameObject;
        shroud = Instantiate(shroud, this.transform.position, this.transform.rotation, this.transform);
        Destroy(shroud.gameObject, 1.3f);
        GameObject boom = Resources.Load("Particles/ACTIVATEBLUE") as GameObject;
        boom = Instantiate(boom, this.transform.position, this.transform.rotation);
        Destroy(boom.gameObject, 1f);
        Vector2 dir = new Vector2 (
            otherBurader.transform.position.x - this.transform.position.x,
            otherBurader.transform.position.y - this.transform.position.y
        );
        this.transform.right = -dir;
        this.GetComponent<Rigidbody2D>().drag = 0;
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody2D>().velocity = dir.normalized * 30;
        this.GetComponent<Rigidbody2D>().mass = 9999;
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
        this.GetComponent<Rigidbody2D>().angularDrag = 999;

        this.transform.rotation = Quaternion.identity;
        // this.transform.localScale = new Vector3(8,8,8);
        this.steadyWhenStunned = true;
        StartCoroutine(StunnedRoutine(1f));
        BeInvincible(0, 1.5f, false);
        StartCoroutine(JFRageDuration(1f));
    }
    IEnumerator JFRageDuration(float duration) {
        yield return new WaitForSeconds(duration);
        JFCancel();
    }
    private void JFCancel() {
        // if (this.gameObject.tag == "Rage") { 
        this.gameObject.GetComponent<Rigidbody2D>().mass = 1;
        this.GetComponent<Rigidbody2D>().gravityScale = gravity;
        JFSkill = false;
        // this.transform.localScale = new Vector3(5,5,5);
        this.GetComponent<Rigidbody2D>().angularDrag = 0.05f;
        this.steadyWhenStunned = false;
        stunnedDuration = 0;
        this.GetComponent<Rigidbody2D>().drag = 23;
        invincibilityDuration = 0;
        BeInvincible(0,0.5f);
        // }
        this.gameObject.tag = "Player";
    }

    public void PowerSHINMEN() {
        GameObject nano = Resources.Load("Effects/ShinBear") as GameObject;
        nano = Instantiate(nano, new Vector2(this.transform.position.x - 0.5f, this.transform.position.y + 0.5f), this.transform.rotation, this.transform);
            
        StartCoroutine(ShinmenShoot(20, nano));
    }
    IEnumerator ShinmenShoot(int bala, GameObject bear) {
        if (bala > 0) {
            AudioManager.instance.PlaySFX(SFXS.GUNFIRE);
            GameObject nano = Resources.Load("Particles/hit") as GameObject;
            nano = Instantiate(nano, this.transform.position, cameraMain.transform.rotation, cameraMain.transform);
            
            GameObject nani = Resources.Load("Effects/Pew") as GameObject;
            nani = Instantiate(nani, this.transform.position, cameraMain.transform.rotation, cameraMain.transform);
            Vector2 dir = new Vector2 (
                otherBurader.transform.position.x - nani.transform.position.x,
                otherBurader.transform.position.y - nani.transform.position.y
            );
            Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), nani.GetComponentInChildren<CircleCollider2D>());
            nani.GetComponent<Rigidbody2D>().velocity = dir.normalized * 25;
            yield return new WaitForSeconds(0.05f);
            
            
            // if (nani) {
            GameObject nani2 = Resources.Load("Effects/Pew") as GameObject;
            nani2 = Instantiate(nani2, bear.transform.position, cameraMain.transform.rotation, cameraMain.transform);
            dir = new Vector2 (
                otherBurader.transform.position.x - nani2.transform.position.x,
                otherBurader.transform.position.y - nani2.transform.position.y
            );
            Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), nani2.GetComponentInChildren<CircleCollider2D>());
            nani2.GetComponent<Rigidbody2D>().velocity = dir.normalized * 30;
            // }
            yield return new WaitForSeconds(0.13f);
            StartCoroutine(ShinmenShoot(bala - 1, bear));
        }
        
    }

    private void PowerHORORO() {
        AudioManager.instance.PlaySFX(SFXS.HORORO);
        this.GetComponent<Animator>().SetTrigger("FALL");
        this.GetComponent<Rigidbody2D>().drag = 90;
        BeInvincible(0, 1f);
        StartCoroutine(HororoSmash());
    }
    IEnumerator HororoSmash() {
        Time.timeScale = 0f;
        GameObject stat = Resources.Load("Effects/static") as GameObject;
        AudioManager.instance.PlaySFX(SFXS.STATIC);
        stat = Instantiate(stat, new Vector3(cameraMain.transform.position.x, cameraMain.transform.position.y, 0), Quaternion.identity);
        yield return new WaitForSecondsRealtime(0.2f);
        Destroy(stat.gameObject);
        yield return new WaitForSecondsRealtime(1.8f);
        AudioManager.instance.PlaySFX(SFXS.STATIC);
        GameObject stat2 = Resources.Load("Effects/static") as GameObject;
        stat2 = Instantiate(stat2, new Vector3(cameraMain.transform.position.x, cameraMain.transform.position.y, 0), Quaternion.identity);
        if (this.transform.position.x >= otherBurader.transform.position.x) {
            this.transform.position = new Vector2 (otherBurader.transform.position.x - 1f, otherBurader.transform.position.y);
            this.GetComponent<SpriteRenderer>().flipX = !this.GetComponent<SpriteRenderer>().flipX;
        }
        else {
            this.transform.position = new Vector2 (otherBurader.transform.position.x + 1f, otherBurader.transform.position.y);
            this.GetComponent<SpriteRenderer>().flipX = !this.GetComponent<SpriteRenderer>().flipX;
        }
        yield return new WaitForSecondsRealtime(0.25f);
        Destroy(stat2.gameObject);
        this.transform.rotation = Quaternion.Euler(0,0, 0);
        yield return new WaitForSecondsRealtime(1.35f);
        SpecialUseTimer = 1;
        AudioManager.instance.PlaySFX(SFXS.WINDEXPLODE);
        GameObject atomic = Resources.Load("Effects/HororoSmash") as GameObject;
        GameObject crack = Resources.Load("Effects/crack") as GameObject;
        Instantiate(crack, this.transform.position, Quaternion.identity);
        Time.timeScale = 0.1f;
        if (this.transform.position.x >= otherBurader.transform.position.x) {
            atomic = Instantiate(atomic, new Vector2(this.transform.position.x -0.5f, this.transform.position.y -0.3f), this.transform.rotation, this.transform);
            atomic.GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        else {
            atomic = Instantiate(atomic, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y -0.3f), this.transform.rotation, this.transform);
            atomic.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        this.GetComponent<Rigidbody2D>().velocity = this.transform.up * 5;
        Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), atomic.GetComponentInChildren<CircleCollider2D>());
        this.GetComponent<Rigidbody2D>().drag = 15;

        GameObject effect = Resources.Load("Particles/ACTIVATEBLUE") as GameObject;
        Instantiate(effect, atomic.transform.position, atomic.transform.rotation, atomic.transform);
        Destroy(atomic.gameObject, 0.8f);
    }


    //!other stuff==================================================================================================================================

    public float stunnedDuration;
    public IEnumerator StunnedRoutine(float duration) {
        if (duration > stunnedDuration) {
            if (duration >= 0.5f){
                isMovingRight = 0;
                isMovingLeft = 0;
                acceleration = 1;
            } 
            ////Debug.Log("stunned");
            
            stunnedDuration = duration + ((float)otherBurader.combo / 20);
            
            yield return null;
        }
    }
    GameObject superSaiyan;
    bool isInDanger;
    float HPRed = 100;
    bool toRed;
    float dangerAlphaMax;
    void Update() {
        if (buraderType == Enums.BuraderName.LOKIA) {
            if (isInDanger && SpecialCurrent >= SpecialMax) {
                //if (!LokiaSkill) 
                PowerLOKIA();
                SpecialKillTimer = 2f;
                SpecialUseTimer = 1.5f;
            }
        }
        //combo
        if (comboDuration > 0) {
            comboDuration -= Time.deltaTime;
        }
        if (comboDuration <= 0) {
            combo = 0;
        }
        //HPBar
        if (HPBar)
            HPBar.value = (float) HPCurrent / (float)HPMax;

        if (HPCurrent > HPMax) {
            HPCurrent = HPMax;
        }
        if (HPCurrent <= 0) {
            if (Aura){
                HPCurrent = 0;
                Aura.SetActive(false);
                dangerAlphaMax = 0.2f;
                HPBar.transform.Find("Fill Area").gameObject.SetActive(false);
                if (!isInDanger) {
                    Time.timeScale = 0.2f;
                    StartCoroutine(References.GameController.SetRandomDeadMessage(PromptMessage.GetDangerMessages(), this.buraderType.ToString()));
                    AudioManager.instance.PlayVOX(VOXS.DANGER);
                    isInDanger = true;
                    SpecialCurrent += 50;
                    this.gameObject.AddComponent<SpriteGlow.SpriteGlowEffect>().GlowColor = Color.red;
                }
            }
        }
        else {
            if (Aura){
                Aura.SetActive(true);
                dangerAlphaMax = 0;
                dangerAlphaConstant = 0;
                HPBar.transform.Find("Fill Area").gameObject.SetActive(true);
                isInDanger = false;
                if (this.GetComponent<SpriteGlow.SpriteGlowEffect>()) {
                    Destroy(this.GetComponent<SpriteGlow.SpriteGlowEffect>());
                }
            }
        }

        //HP red bar
        if (HPCurrent <= 0) {
            HPRed = HPMax;
        }
        else if (HPCurrent < HPRed) {
            HPRed -= 0.20f;
        }
        else {
            HPRed = HPCurrent;
        }

        //danger indicator
        if (dangerAlphaMax > 0) {
            if (toRed) {
                dangerAlphaConstant += Time.deltaTime / 2;
                if (dangerAlphaConstant >= dangerAlphaMax) {
                    toRed = false;
                }
            }
            else {
                dangerAlphaConstant -= Time.deltaTime / 2;
                if (dangerAlphaConstant <= 0) {
                    toRed = true;
                }
            }
        }
        if (HPBar) {
            HPBar.transform.Find("Red").GetComponent<RectTransform>().sizeDelta = new Vector2((HPRed / HPMax) * 400, 10);
            HPBar.transform.Find("Red").GetComponent<RectTransform>().anchoredPosition = new Vector2((((HPRed / HPMax) * 400) / 2) - 400, HPBar.transform.Find("Red").GetComponent<RectTransform>().anchoredPosition.y);
        }

        if (acceleration >= 1) {
            acceleration -= Time.deltaTime / (2);
        }
        if (acceleration >= 6) {
            References.GameController.Break(this);
            StartCoroutine(StunnedRoutine(0.3f));
        }
        if (LokiaSkill) {
            // SpecialCurrent -= Time.deltaTime * 60;
            // if (SpecialCurrent <= 0) {
            //     LokiaSkill = false;
            // }
        }
        else {
            if (SpecialCurrent > SpecialMax) {
                SpecialCurrent = SpecialMax;
                if (tag == "Player" || tag == "Invincible") {
                    if (superSaiyan == null) {
                        superSaiyan = Resources.Load("Particles/ACTIVATE") as GameObject;
                        superSaiyan = Instantiate(superSaiyan, this.transform.position, this.transform.rotation, this.transform);
                        AudioManager.instance.PlaySFX(SFXS.ICE);
                        AudioManager.instance.PlaySFX(SFXS.SPLASH);
                    }
                    // SpecialUse();
                    // SpecialCurrent = 0;
                }
            }
            else {
                if (superSaiyan != null) {
                    Destroy(superSaiyan.gameObject);
                }
            }
            if (SpecialCurrent <= 0) {
                SpecialCurrent = 0;
            }
        }

        if (speedLineDuration > 0) {
            speedLineDuration -= Time.deltaTime;
        }

        if (this.GetComponent<Rigidbody2D>().drag > 0 && !GIOSkill)
            this.GetComponent<Rigidbody2D>().drag--;
        
        if (SpecialKillTimer > 0){
            SpecialKillTimer -= Time.deltaTime;
            ////Debug.Log(SpecialKillTimer);
        }
        if (SpecialUseTimer > 0f) {
            SpecialUseTimer -= Time.deltaTime;
        }
        if (this.tag != "PlayerClone") {
            if (invincibilityDuration > 0) {
                isInvincible = true;
                // YouIndicator.SetActive(true);
                tag = "Invincible";
                invincibilityDuration -= Time.deltaTime;
            }
            else {
                isInvincible = false;
                YouIndicator.SetActive(false);
                tag = "Player";
            }
        }
            
        if (TechnicalKillTimer > 0) {
            TechnicalKillTimer -= Time.deltaTime;
            ////Debug.Log(TechnicalKillTimer);
        }
        if (EricSkillDuration > 0) {
            EricSkill = true;
            EricSkillDuration -= Time.deltaTime;
            if (Random.Range(0, 10) == 0) {
                HPCurrent -= 1;
            }
        }
        if (EricSkillDuration <= 0) {
            if (this.transform.Find("NANI(Clone)") != null) {
                Destroy(this.transform.Find("NANI(Clone)").gameObject);
            }
            EricSkillDuration = -1;
            EricSkill = false;
        }
        if (stunnedDuration > 0) {
            if (steadyWhenStunned) {
                //wala di rorotate
                transform.rotation = Quaternion.Euler(0,0,0);
            }
            else if (this.GetComponent<Rigidbody2D>().gravityScale < 0) {
                //mayu's skill
                transform.Rotate(Vector3.forward * 300 * Time.deltaTime);
            }
            else if (this.transform.position.x < this.otherBurader.transform.position.x) {
                transform.Rotate(Vector3.forward * 300 * (stunnedDuration + 1) * Time.deltaTime);
            }
            else {
                transform.Rotate(-Vector3.forward * 300 * (stunnedDuration + 1) * Time.deltaTime);
            }
            stunnedDuration -= Time.deltaTime;
        }
        else {
            if (this.transform.rotation.z < 0) {
                transform.Rotate(Vector3.forward * 300 * Time.deltaTime);
            }
            else if (this.transform.rotation.z > 0) {
                transform.Rotate(-Vector3.forward * 300 * Time.deltaTime);
            }
        }
        if (stunnedDuration <= 0) {
            if (Controls != null) {
                // Controls.SetActive(true);
                ControlLeft.interactable = true;
                ControlRight.interactable = true;
                steadyWhenStunned = false;
            }
        }
        else {
            if (Controls != null) {
                // Controls.SetActive(false);
                ControlLeft.interactable = false;
                ControlRight.interactable = false;
            }
        }
        
        if (this.transform.rotation.z > -0.015 && this.transform.rotation.z < 0.015)
            this.transform.rotation = Quaternion.identity;

        if (this.transform.position.y < -6 || this.transform.position.y > 6) {
            StartCoroutine(GoBack());
        }

    }
    
    void FixedUpdate() {
        if (Time.timeScale < 1 && Time.timeScale != 0) {
            Time.timeScale += Time.deltaTime;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }

        if (SpecialUseTimer > 0f) {
            this.GetComponent<Animator>().SetTrigger("SPECIAL");
        }
        else if(stunnedDuration > 0 || this.GetComponent<Rigidbody2D>().drag > 15) {
            this.GetComponent<Animator>().SetTrigger("OUCH");
        }
        else {
            if (this.GetComponent<Rigidbody2D>().velocity.y > 0) {
                this.GetComponent<Animator>().SetTrigger("JUMP");
            }
            else {
                this.GetComponent<Animator>().SetTrigger("FALL");
            }
        }

        //flip sprite
        if (tag != "PlayerClone") {
            if (this.transform.position.x > otherBurader.transform.position.x) {
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
            else {
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        if (BritSkill) {
            this.GetComponent<SpriteRenderer>().material.color = Color.Lerp(this.GetComponent<SpriteRenderer>().material.color, alphaColor, 5 * Time.deltaTime);
            this.GetComponentInChildren<SpriteRenderer>().material.color = Color.Lerp(this.GetComponentInChildren<SpriteRenderer>().material.color, alphaColor, 5 * Time.deltaTime);
            //Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), otherBurader.GetComponent<CircleCollider2D>());
            this.tag = "Invincible";
        }
        else {
            this.GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1);
            this.GetComponentInChildren<SpriteRenderer>().material.color = new Color(1, 1, 1);
            //Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), otherBurader.GetComponent<CircleCollider2D>(), false);
        }

        if (KyleSkill && target != null) {
            this.GetComponent<Rigidbody2D>().velocity = transform.up * 300 * Time.deltaTime;
            Vector3 targetVector = target.position - transform.position;
            float rotatingIndex = Vector3.Cross(targetVector.normalized, transform.up).z;
            this.GetComponent<Rigidbody2D>().angularVelocity = -1 * rotatingIndex * 66666 * Time.deltaTime; //rotatespeed

        }
        else {
            this.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
        
        if (this.GetComponent<Rigidbody2D>().velocity.magnitude > 10) {
            this.GetComponent<Rigidbody2D>().velocity = Vector3.ClampMagnitude(this.GetComponent<Rigidbody2D>().velocity, 18);
            this.GetComponentInChildren<TrailRenderer>().enabled = true;

            // Aura.transform.rotation = Quaternion.LookRotation(-this.GetComponent<Rigidbody2D>().velocity);

            if (this.GetComponent<Rigidbody2D>().velocity.magnitude > 12) {
                // if (!AudioManager.instance.IsSFXPlaying(SFXS.MECHA)) {
                //     AudioManager.instance.PlaySFX(SFXS.MECHA);
                // }
            }
        }
        else {
            this.GetComponentInChildren<TrailRenderer>().enabled = false;
            // Aura.transform.rotation = Quaternion.Euler(-90, 0,0);
            AudioManager.instance.StopName(SFXS.MECHA);
        }
        ////Debug.Log(EricSkillDuration + " " + EricSkill);
            
    }
    IEnumerator GoBack() {
        AudioManager.instance.PlaySFX(SFXS.INSTANT);
        this.transform.position = new Vector2(cameraMain.transform.position.x, cameraMain.transform.position.y);
        yield return null;
    }
    public float speedLineDuration;

    public void ComboAddShow() {
        combo++;
        if (combo > 1)
            StartCoroutine(References.GameController.SetSMASHMessageRoutine(playerNumber, combo + "    ORA!"));
        comboDuration = 2;
    }
    
    public void OnCollisionEnter2D(Collision2D bumangga) {
        if (this.tag == "PlayerClone")
            return;
        if (bumangga.gameObject.tag == "Player")  {
            switch (Random.Range(0, 6)) {
                case 0: AudioManager.instance.PlaySFX(SFXS.HIT0); break;
                case 1: AudioManager.instance.PlaySFX(SFXS.HIT1); break;
                case 2: AudioManager.instance.PlaySFX(SFXS.HIT2); break;
                case 3: AudioManager.instance.PlaySFX(SFXS.HIT3); break;
                case 4: AudioManager.instance.PlaySFX(SFXS.HIT4); break;
                case 5: AudioManager.instance.PlaySFX(SFXS.HIT5); break;
            }
        }
        else if (bumangga.gameObject.tag != "Wall"){
            AudioManager.instance.PlaySFX(SFXS.HIT4);
        }
        // else if (bumangga.gameObject.tag == "Wall" && JFSkill) {
        //     JFCancel();
        // }
        particle_hit = Resources.Load("Particles/hit") as GameObject;
        particle_hit = Instantiate(particle_hit, bumangga.collider.transform.position, this.transform.rotation);
        Destroy(particle_hit.gameObject, 1f);
        //hit other buraders
        // if ((bumangga.gameObject.tag == "Player" || bumangga.gameObject.tag == "Invincible") && (this.tag != "PlayerClone")) {
        if ((bumangga.gameObject.tag == "Player") && (this.tag != "PlayerClone")) {
            if (otherBurader != null) {
                StartCoroutine(otherBurader.StunnedRoutine(0.05f));
                TechnicalKillTimer = 0.3f;
            }

            //heavy hit
            //diko sure bakit mas accurate pag magnitude ng other ung kinuha, e ikaw yung bumangga. :/
            //ah, ang result kasi yung after ng bangga, mas malakas magnitude ng binangga, kaya sa other ang nicocompute
            float otherMagni = bumangga.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
            float yourMagni = this.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
            //so ngayon, kung sino lang mas malakas ang makakakuha ng points
            
            bool toSmash = false;
            if (this.acceleration > otherBurader.acceleration) {
                toSmash = true;
            }
            else if (this.acceleration == otherBurader.acceleration && otherMagni > yourMagni) {
                toSmash = true;
            }

            //!SMASHED
            if (toSmash) {
                if (otherMagni > 12f) {
                    particle_hit = Resources.Load("Particles/hitheavy3") as GameObject;
                    AudioManager.instance.PlaySFX(SFXS.HEAVYHIT);
                    particle_hit = Instantiate(particle_hit, bumangga.collider.transform.position, this.transform.rotation);
                    if (bumangga.gameObject.tag == "Player") {
                        ////Debug.Log(this.buraderType + " plus points");
                        // if (this.SpecialUseTimer <= 0)
                        //     SpecialCurrent += 20;
                        TechnicalKillTimer = 1.5f;
                        StartCoroutine(References.GameController.SetPowerupMessageRoutine(this.buraderType + " SMASH!"));
                        ComboAddShow();
                        if (TutorialManager.isTutorial && TutorialManager.tutorialSeq == 9) {
                            StartCoroutine(TutorialManager.AfterHit());
                        }
                        GameObject soul = Resources.Load("Particles/Souls") as GameObject;
                        for (int x = 0; x <= 10; x++) {
                            soul = Instantiate(soul, otherBurader.transform.position, Quaternion.identity, cameraMain.transform);
                            if (playerNumber == 1)
                                soul.GetComponent<SoulScript>().moveRight = false;
                            else
                                soul.GetComponent<SoulScript>().moveRight = true;
                        }
                    }
                    if (otherBurader != null && (bumangga.gameObject.tag == "Player")) {
                        if (otherBurader.isInDanger)
                            StartCoroutine(otherBurader.StunnedRoutine(2f));
                        else
                            StartCoroutine(otherBurader.StunnedRoutine(1f));
                        otherBurader.HPCurrent -= 8;
                        otherBurader.acceleration = 1;
                        this.GetComponent<Rigidbody2D>().drag = 30;
                        this.transform.rotation = Quaternion.Euler(0,0,0);
                        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

                        
                    }
                    speedLineDuration = 0.8f;

                    // Debug.Log(this.buraderType + ": " + otherMagni.ToString() + "|" + otherBurader.buraderType + ": " + yourMagni);
                    // Debug.Break();

                    Time.timeScale = 0.25f;
                }
                else if (otherMagni > 8f) {
                    particle_hit = Resources.Load("Particles/hitheavy") as GameObject;
                    particle_hit = Instantiate(particle_hit, bumangga.collider.transform.position, this.transform.rotation);
                    AudioManager.instance.PlaySFX(SFXS.MEDIUMHIT);
                    if (bumangga.gameObject.tag == "Player") {
                        StartCoroutine(References.GameController.SetPowerupMessageRoutine(this.buraderType + " HIT!"));
                        ComboAddShow();
                        // if (this.SpecialUseTimer <= 0) {
                        //     SpecialCurrent += 12;
                        // }
                        TechnicalKillTimer = 1f;
                        GameObject soul = Resources.Load("Particles/Souls") as GameObject;
                        for (int x = 0; x <= 5; x++) {
                            soul = Instantiate(soul, otherBurader.transform.position, Quaternion.identity, cameraMain.transform);
                            soul.GetComponent<SoulScript>().moveRight = false;
                            if (playerNumber == 1)
                                soul.GetComponent<SoulScript>().moveRight = false;
                            else
                                soul.GetComponent<SoulScript>().moveRight = true;
                        }
                    }
                    if (otherBurader != null && (bumangga.gameObject.tag == "Player")){
                        if (otherBurader.isInDanger)
                            StartCoroutine(otherBurader.StunnedRoutine(1.2f));
                        else
                            StartCoroutine(otherBurader.StunnedRoutine(0.6f));
                        otherBurader.HPCurrent -= 4;
                        otherBurader.acceleration = 1;
                        this.GetComponent<Rigidbody2D>().drag = 15;
                        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    }
                    speedLineDuration = 0.30f;

                    Time.timeScale = 0.55f;
                    
                }
            }
            
        }
        if (bumangga.gameObject.tag == "Rage") {
            particle_hit = Resources.Load("Particles/hitheavy") as GameObject;
            particle_hit = Instantiate(particle_hit, bumangga.collider.transform.position, this.transform.rotation);
            //Destroy(particle_hit.gameObject, 2f);
        }
        if (bumangga.gameObject.tag == "Projectile" || bumangga.gameObject.tag == "DogePoop" || bumangga.gameObject.tag == "PlayerClone") {
            particle_hit = Resources.Load("Particles/hit") as GameObject;
            particle_hit = Instantiate(particle_hit, bumangga.collider.transform.position, this.transform.rotation);
            if (bumangga.gameObject.name.Contains("CRANE")) {
                StartCoroutine(StunnedRoutine(1f));
                otherBurader.SpecialKillTimer = 1f;
                HPCurrent -= 3;
                AudioManager.instance.PlaySFX(SFXS.MEDIUMHIT);
                Time.timeScale = 0.4f;
                otherBurader.ComboAddShow();
            }
            if (bumangga.gameObject.tag == "DogePoop") {
                HPCurrent -= 2;
                otherBurader.SpecialKillTimer = 0.55f;
            }
            if (bumangga.gameObject.name.Contains("ORA")) {
                StartCoroutine(StunnedRoutine(0.3f));
                HPCurrent -= 5;
                otherBurader.ComboAddShow();
                otherBurader.SpecialKillTimer = 0.55f;
            }
            if (bumangga.gameObject.name.Contains("FlappyClone")) {
                StartCoroutine(StunnedRoutine(0.1f));
                HPCurrent -= 2;
            }
            if (bumangga.gameObject.name.Contains("knaifu")) {
                HPCurrent -= 5;
                StartCoroutine(StunnedRoutine(0.3f));
                otherBurader.ComboAddShow();
                AudioManager.instance.PlaySFX(SFXS.SLASH);
                Destroy(bumangga.gameObject);
            }
            //yung kay shinmen nasa "Boom"
        }

        if (bumangga.gameObject.tag != "Wall")  {
            if (bumangga.gameObject.tag == "Player" || bumangga.gameObject.tag == "Invincible")
                    SpecialCurrent += 2f;
        }
        //migs rage
        if (MigsSkill || JFSkill) {
            float force = 15;
            if (bumangga.gameObject.tag == "Player" || bumangga.gameObject.tag == "Langaw") {
                if (MigsSkill) {
                    StartCoroutine(otherBurader.StunnedRoutine(0.5f));
                    SpecialKillTimer = 1f;
                    otherBurader.HPCurrent -= 1;
                }
                if (JFSkill) {
                    StartCoroutine(otherBurader.StunnedRoutine(2.5f));
                    SpecialKillTimer = 2f;
                    otherBurader.HPCurrent -= 20;
                    particle_hit = Resources.Load("Particles/CircleWaveRed") as GameObject;
                    AudioManager.instance.PlaySFX(SFXS.HEAVYHIT);
                    particle_hit = Instantiate(particle_hit, bumangga.collider.transform.position, this.transform.rotation);
                    force = 33;
                    ComboAddShow();
                    Time.timeScale = 0.15f;
                    Vector2 dir2 = new Vector2 (
                        otherBurader.transform.position.x - this.transform.position.x,
                        otherBurader.transform.position.y - this.transform.position.y
                    );
                    this.transform.right = dir2;
                    this.GetComponent<Rigidbody2D>().velocity = -dir2.normalized * 20;
                }
            }
            // force is how forcefully we will push the player away from the enemy.
            
            // Calculate Angle Between the collision point and the player
            Vector3 dir = bumangga.contacts[0].point - new Vector2(this.transform.position.x, this.transform.position.y);
            // We then get the opposite (-Vector3) and normalize it
            dir = dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            if (bumangga.gameObject.tag != "Wall")
                bumangga.gameObject.GetComponent<Rigidbody2D>().velocity = (dir * force);
        }
        else if (ZantetsuSkill) {
            AudioManager.instance.PlaySFX(SFXS.SLASH);
            AudioManager.instance.PlaySFX(SFXS.READY);
            particle_hit = Resources.Load("Particles/hitheavy") as GameObject;
            particle_hit = Instantiate(particle_hit, bumangga.collider.transform.position, this.transform.rotation);
            Destroy(particle_hit.gameObject, 2f);
            GameObject slash = Resources.Load("Effects/SLASHU") as GameObject;
            slash = Instantiate(slash, this.transform.position, this.transform.rotation);
            Destroy(slash.gameObject, 1f);
            if (bumangga.gameObject.tag == "Player" || bumangga.gameObject.tag == "Langaw") {
                StartCoroutine(otherBurader.StunnedRoutine(2.5f));
                SpecialKillTimer = 2f;
                if (bumangga.gameObject.tag == "Player") {
                    otherBurader.HPCurrent -= 10;
                    ComboAddShow();
                }
            }
            float force = 15;
            Vector3 dir = bumangga.contacts[0].point - new Vector2(this.transform.position.x, this.transform.position.y);
            dir = dir.normalized;
            if (bumangga.gameObject.tag != "Wall") {
                if (bumangga.gameObject.GetComponent<Rigidbody2D>() != null) {
                    bumangga.gameObject.GetComponent<Rigidbody2D>().velocity = (dir * force);
                }
            }
        }
        else if (OyyySkill) {
            float force = 15;
            AudioManager.instance.PlaySFX(SFXS.HEAVYHIT);
            if (bumangga.gameObject.tag == "Player" || bumangga.gameObject.tag == "Langaw") {
                StartCoroutine(otherBurader.StunnedRoutine(2f));
                SpecialKillTimer = 1f;
                otherBurader.HPCurrent -= 5;
                ComboAddShow();
            }
            Vector3 dir = bumangga.contacts[0].point - new Vector2(this.transform.position.x, this.transform.position.y);
            dir = dir.normalized;
            if (bumangga.gameObject.tag != "Wall" && bumangga.gameObject.GetComponent<Rigidbody2D>())
                bumangga.gameObject.GetComponent<Rigidbody2D>().velocity = (dir * force);
        }
        else if (bumangga.gameObject.tag == "Boom") {
            if (isInvincible) return;

            if (buraderType == Enums.BuraderName.DINK) {
                StartCoroutine(StunnedRoutine(0.5f));
                HPCurrent -= 13;
            }
            if (otherBurader.buraderType == Enums.BuraderName.DINK) {
                StartCoroutine(StunnedRoutine(3.5f));
                otherBurader.SpecialKillTimer = 4f;
                HPCurrent -= 30;
                Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), bumangga.gameObject.GetComponent<CircleCollider2D>());
            }
            if (otherBurader.buraderType == Enums.BuraderName.SHINMEN) {
                StartCoroutine(StunnedRoutine(0.1f));
                AudioManager.instance.PlaySFX(SFXS.GUNHIT);
                otherBurader.SpecialKillTimer = 0.1f;
                HPCurrent -= 1;
                if (otherBurader) otherBurader.ComboAddShow();
            }
            else if (otherBurader.buraderType == Enums.BuraderName.NICOLLO) {
                StartCoroutine(StunnedRoutine(2.5f));
                if (otherBurader) otherBurader.ComboAddShow();
                otherBurader.SpecialKillTimer = 3f;
                Time.timeScale = 0.25f;
                HPCurrent -= 25;
            }
            else if (otherBurader.buraderType == Enums.BuraderName.PAU) {
                float Xforce = 15;
                StartCoroutine(this.StunnedRoutine(1));
                HPCurrent -= 20;
                Vector3 Xdir = bumangga.contacts[0].point - new Vector2(this.transform.position.x, this.transform.position.y);
                Xdir = -Xdir.normalized;
                this.gameObject.GetComponent<Rigidbody2D>().velocity = (Xdir * Xforce);
                GameObject sipon = Resources.Load("Effects/Sipon") as GameObject;
                sipon = Instantiate(sipon, this.transform.position, this.transform.rotation, this.transform);
                StartCoroutine(Sticky());
                Physics2D.IgnoreCollision(bumangga.gameObject.GetComponent<CircleCollider2D>(), this.GetComponent<CircleCollider2D>());
                otherBurader.SpecialKillTimer = 2f;
                return;
            }
            else if (otherBurader.buraderType == Enums.BuraderName.HORORO) {
                float Xforce = 33;
                StartCoroutine(this.StunnedRoutine(1f));
                HPCurrent -= 15;
                Vector3 Xdir = bumangga.contacts[0].point - new Vector2(this.transform.position.x, this.transform.position.y);
                Xdir = -Xdir.normalized;
                this.gameObject.GetComponent<Rigidbody2D>().velocity = (Xdir * Xforce);
                GameObject sipon = Resources.Load("Particles/HITPOW") as GameObject;
                sipon = Instantiate(sipon, this.transform.position, this.transform.rotation, this.transform);
                Physics2D.IgnoreCollision(bumangga.gameObject.GetComponentInChildren<CircleCollider2D>(), this.GetComponent<CircleCollider2D>());
                otherBurader.SpecialKillTimer = 2f;
                AudioManager.instance.PlaySFX(SFXS.HEAVYHIT);
                return;
            }
            float force = 9;
            // Calculate Angle Between the collision point and the player
            Vector3 dir = bumangga.contacts[0].point - new Vector2(this.transform.position.x, this.transform.position.y);
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            this.gameObject.GetComponent<Rigidbody2D>().velocity = (dir * force);
        }
        
    }

    IEnumerator Sticky() {
        yield return new WaitForSeconds(0.07f);
        Time.timeScale = 0.25f;
        yield return new WaitForSeconds(0.15f);
        this.gameObject.GetComponent<Rigidbody2D>().drag = 200;
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        StartCoroutine(StunnedRoutine(2));
        steadyWhenStunned = true;
        yield return new WaitForSeconds(2f);
        steadyWhenStunned = false;
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = gravity;
    }
    private float SpecialUseTimer = 0;
    public void SpecialUse() {
        if (this.GIOSkill) return;
        if (this.buraderType == Enums.BuraderName.LOKIA) return;

        AudioManager.instance.PlaySFX(SFXS.SPECIAL + this.buraderType.ToString());
        
        switch(this.buraderType) {
            case Enums.BuraderName.PAU:
                SpecialUseTimer = 1;
                PowerPAU();
                break;
            case Enums.BuraderName.CHRISTIAN:
                SpecialKillTimer = 5;
                SpecialUseTimer = 5.5f;
                PowerCHRISTIAN();
                break;
            case Enums.BuraderName.HORORO:
                SpecialKillTimer = 3;
                PowerHORORO();
                break;
            case Enums.BuraderName.BRITTANY:
                PowerBRIT();
                //nasa BritShow
                break;
            case Enums.BuraderName.MIGS:
                PowerMIGS();
                break;
            case Enums.BuraderName.BRYAN:
                PowerBRYAN();
                SpecialKillTimer = 2.25f;
                SpecialUseTimer = 2;
                break;
            case Enums.BuraderName.OYYY:
                SpecialUseTimer = 0.5f;
                PowerOYYY();
                break;
            case Enums.BuraderName.RIZAL:
                PowerRIZAL();
                SpecialKillTimer = 2.5f;
                SpecialUseTimer = 1;
                break;
            case Enums.BuraderName.ERIC:
                PowerERIC();
                SpecialKillTimer = 3;
                SpecialUseTimer = 3;
                break;
            case Enums.BuraderName.MASSIMO:
                PowerMASSIMO();
                SpecialKillTimer = 4;
                SpecialUseTimer = 0.5f;
                break;
            case Enums.BuraderName.KYLE:
                PowerKYLE();
                SpecialKillTimer = 4f;
                SpecialUseTimer = 4f;
                break;
            case Enums.BuraderName.MARK:
                PowerMARK();
                SpecialUseTimer = 1.5f;
                break;
            case Enums.BuraderName.IVAN:
                PowerIVAN();
                SpecialUseTimer = 3f;
                break;
            case Enums.BuraderName.MAYU:
                PowerMAYU();
                SpecialKillTimer = 5;
                SpecialUseTimer = 1f;
                break;
            case Enums.BuraderName.DINK:
                PowerDINK();
                SpecialUseTimer = 0.5f;
                break;
            case Enums.BuraderName.NICOLLO:
                PowerNICOLLO();
                SpecialUseTimer = 2f;
                break;
            case Enums.BuraderName.PLATIMOON:
                PowerPLATIMOON();
                SpecialUseTimer = 2f;
                break;
            case Enums.BuraderName.LOKIA:
                PowerLOKIA();
                SpecialKillTimer = 2f;
                SpecialUseTimer = 1.5f;
                break;
            case Enums.BuraderName.ZANTETSU:
                PowerZANTETSU();
                SpecialUseTimer = 3f;
                break;
            case Enums.BuraderName.DOGE:
                PowerDOGE();
                SpecialUseTimer = 0.5f;
                break;
            case Enums.BuraderName.POPPINS:
                PowerPOPPINS();
                SpecialUseTimer = 0.5f;
                break;
            case Enums.BuraderName.JF:
                PowerJF();
                SpecialUseTimer = 1f;
                break;
            case Enums.BuraderName.SHINMEN:
                PowerSHINMEN();
                SpecialUseTimer = 3f;
                SpecialKillTimer = 3f; 
                break;
            case Enums.BuraderName.DARK_M:
                PowerDARK_M();
                SpecialKillTimer = 4f;
                break;
            case Enums.BuraderName.GIO:
                PowerGIO();
                SpecialKillTimer = 5f;
                break;
        }
        if (buraderType != Enums.BuraderName.LOKIA)
            SpecialCurrent = 0;
        // this.GetComponent<Animator>().SetTrigger("SPECIAL");
        //Debug.Break();
    }
}
