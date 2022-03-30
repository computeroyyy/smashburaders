using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References : MonoBehaviour
{
    static References instance;
    void Awake() { instance = this; }

    public GameController m_GameController;
    
    public static GameController GameController { get { return instance.m_GameController; } }

    
}
