using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToTutorial : MonoBehaviour
{
    public void Go() {
        TutorialManager.tutorialSeq = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
    }
}
