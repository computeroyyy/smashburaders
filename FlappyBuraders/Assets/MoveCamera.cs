using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlappyBuraders.GlobalStuff;

public class MoveCamera : MonoBehaviour
{
    public bool isMoving;
    void Start()
    {
        isMoving = true;
        // if (GameController.gameMode == Enums.GameMode.ONLINE)
        //     Photon.Pun.PhotonNetwork.AddCallbackTarget(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
            this.transform.position += Vector3.right * Time.deltaTime * 2;
    }
}
