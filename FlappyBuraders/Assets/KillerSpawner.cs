using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlappyBuraders.GlobalStuff;

public class KillerSpawner : MonoBehaviour
{
    public GameObject[] SpawnPoinstLeft;
    public GameObject[] SpawnPoinstRight;
    public bool dontSpawn;
    
    void Start()
    {
        if (TutorialManager.isTutorial && (TutorialManager.tutorialSeq == 3 || TutorialManager.tutorialSeq == 9))
            dontSpawn = true;
        else
            dontSpawn = false;

        StartCoroutine(SpawnChainsaw()); 
        StartCoroutine(SpawnMissile());
        StartCoroutine(SpawnLangawssRoutine());

        // if (GameController.gameMode == Enums.GameMode.ONLINE)
        //     Photon.Pun.PhotonNetwork.AddCallbackTarget(this);
        // StartCoroutine(SpawnTardigoodRoutine());
    }
    IEnumerator SpawnLangawssRoutine() {
        yield return new WaitForSeconds(2f);
        if (GameController.gameMode == Enums.GameMode.HOLD_HANDS) {
            StartCoroutine(SpawnLangaws(7f));
        }
        else {
            StartCoroutine(SpawnPowerUp());
            Enums.Stage theStage = (Enums.Stage) PlayerPrefs.GetInt(Prefs.STAGEtype);
            switch (theStage) {
                case Enums.Stage.INNER_SPACE: StartCoroutine(SpawnMeteor()); break;
                case Enums.Stage.LUSH_FOREST: StartCoroutine(SpawnTardigrade()); break;
                case Enums.Stage.STRATOSPHERE: StartCoroutine(SpawnAirplane()); break;
                case Enums.Stage.OCEAN_PARADISE: StartCoroutine(SpawnJellyPish()); break;
                case Enums.Stage.RADIANT_CASTLE: StartCoroutine(SpawnCrow()); break;
                case Enums.Stage.TOWN_OUTSKIRTS: StartCoroutine(SpawnLightning()); break;
                case Enums.Stage.MAGICAL_ACADEMY: StartCoroutine(SpawnLangaw()); break;
                case Enums.Stage.THE_ARCHITECT: 
                    // StartCoroutine(SpawnLightning());
                    // StartCoroutine(SpawnMeteor());
                    break;
            }
        }
    }
    IEnumerator SpawnTardigoodRoutine() {
        yield return new WaitForSeconds(30);
        if (powerSpawnCount >= 10)
        if (Random.Range(0, 13) == 0) {
            GameObject langaw = Resources.Load("Buraders/TARDIGOOD") as GameObject;
            langaw = Instantiate(langaw, new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y + 4.5f), langaw.transform.rotation, Camera.main.transform);
        }
        StartCoroutine(SpawnTardigoodRoutine());
    }
    IEnumerator SpawnLangaws(float low) {
        //hold hands mode only
        GameObject langaw = Resources.Load("Buraders/LANGAW") as GameObject;
        langaw = Instantiate(langaw, new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y + 4.5f), langaw.transform.rotation, Camera.main.transform);
        if (low < 0) low = 0;
        yield return new WaitForSeconds(Random.Range(low, 10f));
        if (Random.Range(0, 3) == 0){ 
            int rnd = Random.Range(0, 5);
            GameObject hotFudge = Resources.Load("PowerUp/HealthCard") as GameObject;
            hotFudge = Instantiate(hotFudge, SpawnPoinstRight[rnd].transform.position, hotFudge.transform.rotation);
            hotFudge.GetComponent<ChainsawMove>().direction = ChainsawMove.KillerMove.LEFT;
            hotFudge.GetComponent<ChainsawMove>().Speed = (rnd + 1) / 2;
        }
        StartCoroutine(SpawnLangaws(low - 0.1f));
    }
    int powerSpawnCount;
    IEnumerator SpawnPowerUp() {
        //for normal gamemode, spawns powerups and occasional langaws
        float wait;
        string thePowah = "";
        switch (Random.Range(0, 2)) {
            case 0: thePowah = "Whopper"; break;
            case 1: thePowah = "HotFudge"; break;
        }
        switch (PlayerPrefs.GetInt(Prefs.POWERFREQ, 1)) {
            case 0: wait = Random.Range(7f, 16f); break;
            case 1: wait = Random.Range(5.5f, 12f); break;
            case 2: wait = Random.Range(4f, 10f); break;
            default: wait = 0; break;
        }
        yield return new WaitForSeconds(wait);
        GameObject hotFudge = Resources.Load("PowerUp/" + thePowah) as GameObject;
        int rnd = Random.Range(0, 5);
        hotFudge = Instantiate(hotFudge, SpawnPoinstRight[rnd].transform.position, hotFudge.transform.rotation);
        hotFudge.GetComponent<ChainsawMove>().direction = ChainsawMove.KillerMove.LEFT;
        hotFudge.GetComponent<ChainsawMove>().Speed = (rnd + 1) / 3;
        hotFudge.GetComponent<Rigidbody2D>().gravityScale = Random.Range(-0.05f, 0.05f);
        if (hotFudge.gameObject != null)
            Destroy(hotFudge, 14f);

        StartCoroutine(SpawnPowerUp());
        powerSpawnCount++;
    }
    IEnumerator SpawnLangaw() {
        float wait;
        switch (PlayerPrefs.GetInt(Prefs.POWERFREQ, 1)) {
            case 0: wait = Random.Range(7f, 16f); break;
            case 1: wait = Random.Range(5.5f, 12f); break;
            case 2: wait = Random.Range(4f, 10f); break;
            default: wait = 0; break;
        }
        yield return new WaitForSeconds(wait);
        GameObject langaw = Resources.Load("Buraders/LANGAW") as GameObject;
        if (powerSpawnCount >= 1)
        if (Random.Range(0, 1) == 0) {
            langaw = Instantiate(langaw, new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y + 4.5f), langaw.transform.rotation, Camera.main.transform);
        }
        StartCoroutine(SpawnLangaw());
    }
    IEnumerator SpawnJellyPish() {
        float wait;
        switch (PlayerPrefs.GetInt(Prefs.POWERFREQ, 1)) {
            case 0: wait = Random.Range(7f, 16f); break;
            case 1: wait = Random.Range(5.5f, 12f); break;
            case 2: wait = Random.Range(4f, 10f); break;
            default: wait = 0; break;
        }
        yield return new WaitForSeconds(wait + 2);
        GameObject langaw = Resources.Load("Buraders/JELLYFISH") as GameObject;
        if (powerSpawnCount >= 1)
        if (Random.Range(0, 1) == 0) {
            langaw = Instantiate(langaw, new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y + 4.5f), langaw.transform.rotation, Camera.main.transform);
        }
        StartCoroutine(SpawnJellyPish());
    }
    IEnumerator SpawnTardigrade() {
        float wait = 30;
        
        yield return new WaitForSeconds(wait);
        GameObject langaw = Resources.Load("Buraders/TARDIGOOD") as GameObject;
        if (Random.Range(0, 2) == 0) {
            langaw = Instantiate(langaw, new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y + 4.5f), langaw.transform.rotation, Camera.main.transform);
        }
        StartCoroutine(SpawnTardigrade());
    }
    IEnumerator SpawnLightning() {
        float wait = Random.Range(2f, 5f);
        
        yield return new WaitForSeconds(wait);
        GameObject langaw = Resources.Load("Effects/kidlat") as GameObject;
        if (Random.Range(0, 2) == 0) {
            langaw = Instantiate(langaw, new Vector2(Camera.main.transform.position.x + Random.Range(-3f, 3f), Camera.main.transform.position.y), langaw.transform.rotation, Camera.main.transform);
        }
        StartCoroutine(SpawnLightning());
    }

    IEnumerator SpawnMeteor() {
        float wait = Random.Range(1f, 2f);
        
        yield return new WaitForSeconds(wait);
        GameObject langaw = Resources.Load("Effects/Meteor") as GameObject;
        if (Random.Range(0, 1) == 0) {
            langaw = Instantiate(langaw, new Vector2(Camera.main.transform.position.x + 3, Camera.main.transform.position.y + Random.Range(-4.5f, 4.5f)), langaw.transform.rotation, Camera.main.transform);
        }
        StartCoroutine(SpawnMeteor());
    }
    IEnumerator SpawnCrow() {
        float wait = Random.Range(3f, 5f);
        
        yield return new WaitForSeconds(wait);
        GameObject langaw = Resources.Load("Effects/Crow") as GameObject;
        if (Random.Range(0, 1) == 0) {
            langaw = Instantiate(langaw, new Vector2(Camera.main.transform.position.x + 3, Camera.main.transform.position.y + Random.Range(-4.5f, 4.5f)), langaw.transform.rotation, Camera.main.transform);
        }
        StartCoroutine(SpawnCrow());
    }
    IEnumerator SpawnAirplane() {
        float wait = Random.Range(15f, 60f);
        
        yield return new WaitForSeconds(wait);
        GameObject langaw = Resources.Load("Effects/Airplane") as GameObject;
        if (Random.Range(0, 1) == 0) {
            langaw = Instantiate(langaw, new Vector2(Camera.main.transform.position.x - 15, Camera.main.transform.position.y + Random.Range(-5.5f, 5.5f)), langaw.transform.rotation, Camera.main.transform);
        }
        StartCoroutine(SpawnAirplane());
    }
    
    IEnumerator SpawnMissile() {
        GameObject missile, warning;
        missile = Resources.Load("Killers/MissileFromLeft") as GameObject;
        warning = Resources.Load("Killers/Warning") as GameObject;
        GameObject warnPosition = GameObject.FindGameObjectWithTag("WarningPosition");

        float wait;
        switch (PlayerPrefs.GetInt(Prefs.KILLERFREQ, 1)) {
            case 0: wait = Random.Range(10f, 20f); break;
            case 1: wait = Random.Range(9f, 18f); break;
            case 2: wait = Random.Range(8f, 16f); break;
            default: wait = 0; break;
        }

        //if hold hands, always fast
        if (GameController.gameMode == Enums.GameMode.HOLD_HANDS) wait = Random.Range(6f, 8f);

        if (TutorialManager.ChainsawBarrage) wait = 3;

        yield return new WaitForSeconds(wait);
        int rnd;
        if (!dontSpawn)
        switch(Random.Range(0, 3)) {
            case 0:
                //single missile from left
                rnd = Random.Range(0, 5);
                missile = Instantiate(missile, SpawnPoinstLeft[rnd].transform.position, missile.transform.rotation);//, SpawnPoinstLeft[rnd].transform);
                warning = Instantiate(warning, new Vector2(warnPosition.transform.position.x, missile.transform.position.y), warning.transform.rotation);
                Destroy(missile, 16f);
                break;
            case 1:
                //two missile from left
                rnd = Random.Range(0, 2);
                missile = Instantiate(missile, SpawnPoinstLeft[rnd].transform.position, missile.transform.rotation);//, SpawnPoinstLeft[rnd].transform);
                warning = Instantiate(warning, new Vector2(warnPosition.transform.position.x, missile.transform.position.y), warning.transform.rotation);
                Destroy(missile, 16f);

                yield return new WaitForSeconds(0.5f);
                rnd = Random.Range(3, 5);
                missile = Instantiate(missile, SpawnPoinstLeft[rnd].transform.position, missile.transform.rotation);//, SpawnPoinstLeft[rnd].transform);
                warning = Instantiate(warning, new Vector2(warnPosition.transform.position.x, missile.transform.position.y), warning.transform.rotation);
                
                Destroy(missile, 16f);
                break;
            case 2:
                //3 missile from left
                rnd = Random.Range(0, 2);
                missile = Instantiate(missile, SpawnPoinstLeft[rnd].transform.position, missile.transform.rotation);//, SpawnPoinstLeft[rnd].transform);
                warning = Instantiate(warning, new Vector2(warnPosition.transform.position.x, missile.transform.position.y), warning.transform.rotation);
                Destroy(missile, 16f);

                yield return new WaitForSeconds(0.5f);
                rnd = Random.Range(3, 5);
                missile = Instantiate(missile, SpawnPoinstLeft[rnd].transform.position, missile.transform.rotation);//, SpawnPoinstLeft[rnd].transform);
                warning = Instantiate(warning, new Vector2(warnPosition.transform.position.x, missile.transform.position.y), warning.transform.rotation);
                Destroy(missile, 16f);

                yield return new WaitForSeconds(0.5f);
                rnd = Random.Range(0, 5);
                missile = Instantiate(missile, SpawnPoinstLeft[rnd].transform.position, missile.transform.rotation);//, SpawnPoinstLeft[rnd].transform);
                warning = Instantiate(warning, new Vector2(warnPosition.transform.position.x, missile.transform.position.y), warning.transform.rotation);
                Destroy(missile, 16f);
                break;
        }
        StartCoroutine(SpawnMissile());
        Enums.Stage theStage = (Enums.Stage) PlayerPrefs.GetInt(Prefs.STAGEtype);
        if (theStage == Enums.Stage.THE_ARCHITECT){
            if (Random.Range(0, 2) == 0)
                StartCoroutine(SpawnEYE());    
        }
        else {
            if (Random.Range(0, 20) == 0)
                StartCoroutine(SpawnEYE());
        }
        
    }
    IEnumerator SpawnEYE() {
        if (TutorialManager.isTutorial) yield break;
        
        yield return new WaitForSeconds(2.5f);
        GameObject missile = Resources.Load("Killers/EyeOfDoom") as GameObject;
        missile = Instantiate(missile, new Vector3(Camera.main.transform.position.x - 0.14f, Camera.main.transform.position.y + Random.Range(-4f, 2f)), missile.transform.rotation, Camera.main.transform);//, SpawnPoinstLeft[rnd].transform);
        missile.transform.position = new Vector3(missile.transform.position.x, missile.transform.position.y, 1);
    }

    IEnumerator SpawnChainsaw() {
        GameObject chainsaw = new GameObject();
        GameObject warning = Resources.Load("Killers/Warning") as GameObject;
        GameObject warnPosition = GameObject.FindGameObjectWithTag("WarningPosition");
        float wait;
        switch (PlayerPrefs.GetInt(Prefs.KILLERFREQ, 1)) {
            case 0: wait = Random.Range(10f, 15f); break;
            case 1: wait = Random.Range(8f, 12f); break;
            case 2: wait = Random.Range(6f, 9f); break;
            default: wait = 0; break;
        }
        
        //if hold hands, always fast
        if (GameController.gameMode == Enums.GameMode.HOLD_HANDS) wait = Random.Range(4f, 5f);
        
        if (TutorialManager.ChainsawBarrage) wait = 2;

        yield return new WaitForSeconds(wait);
        if (!dontSpawn)
        switch(Random.Range(0, 2)) {
            case 0:
                //single chainsaw from right
                if (Random.Range(0, 2) == 0)
                    chainsaw = Resources.Load("Killers/ChainsawDown") as GameObject;
                else
                    chainsaw = Resources.Load("Killers/ChainsawUp") as GameObject;
                chainsaw = Instantiate(chainsaw, SpawnPoinstRight[Random.Range(1, 4)].transform.position, chainsaw.transform.rotation);
                chainsaw.GetComponent<ChainsawMove>().direction = ChainsawMove.KillerMove.LEFT;
                //rotate or not
                if (Random.Range(0, 5) == 0) {
                    if (Random.Range(0, 2) == 0)
                        chainsaw.GetComponent<ChainsawMove>().rotation = ChainsawMove.KillerRotate.CLOCKWISE;
                    else
                        chainsaw.GetComponent<ChainsawMove>().rotation = ChainsawMove.KillerRotate.COUNTER_CLOCKWISE;
                }
                Destroy(chainsaw, 15f);
                warning = Instantiate(warning, new Vector2(warnPosition.transform.position.x + 3, chainsaw.transform.position.y), warning.transform.rotation);
                break;
            case 1:
                //double chainsaw from right
                chainsaw = Resources.Load("Killers/ChainsawDown") as GameObject;
                chainsaw = Instantiate(chainsaw, SpawnPoinstRight[0].transform.position, chainsaw.transform.rotation);
                chainsaw.GetComponent<ChainsawMove>().direction = ChainsawMove.KillerMove.LEFT;
                Destroy(chainsaw, 15f);
                warning = Instantiate(warning, new Vector2(warnPosition.transform.position.x + 3, chainsaw.transform.position.y), warning.transform.rotation);
                //rotate or not
                if (Random.Range(0, 5) == 0) {
                    if (Random.Range(0, 2) == 0)
                        chainsaw.GetComponent<ChainsawMove>().rotation = ChainsawMove.KillerRotate.CLOCKWISE;
                    else
                        chainsaw.GetComponent<ChainsawMove>().rotation = ChainsawMove.KillerRotate.COUNTER_CLOCKWISE;
                }

                chainsaw = Resources.Load("Killers/ChainsawUp") as GameObject;
                chainsaw = Instantiate(chainsaw, SpawnPoinstRight[4].transform.position, chainsaw.transform.rotation);
                chainsaw.GetComponent<ChainsawMove>().direction = ChainsawMove.KillerMove.LEFT;
                Destroy(chainsaw, 15f);
                warning = Instantiate(warning, new Vector2(warnPosition.transform.position.x + 3, chainsaw.transform.position.y), warning.transform.rotation);
                //rotate or not
                if (Random.Range(0, 5) == 0) {
                    if (Random.Range(0, 2) == 0)
                        chainsaw.GetComponent<ChainsawMove>().rotation = ChainsawMove.KillerRotate.CLOCKWISE;
                    else
                        chainsaw.GetComponent<ChainsawMove>().rotation = ChainsawMove.KillerRotate.COUNTER_CLOCKWISE;
                }
                break;
        }
        StartCoroutine(SpawnChainsaw()); 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
