using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBehaviour : MonoBehaviour
{
    private GameObject player;
    private Player playerManager;
    private Player.PlayerStates state;
    public CinemachineVirtualCamera cam;

    /// <summary>
    /// When the player and hand are separated, the camera will
    /// find the area between them and target that.
    /// </summary>
    public Transform meanTarget;
    private GameObject rocketHand;
    

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rocketHand = GameObject.FindWithTag("Hand");
        playerManager = player.GetComponent<Player>();
    }

    void Update()
    {
        CheckPositions();
    }
    private void CheckPositions()
    {
        if (playerManager.currentState == Player.PlayerStates.Combined)
        {
            cam.Follow = player.transform;
            return;
        };
        var a = player.transform.position;
        var b = rocketHand.transform.position;
        var dist = (a + b) * 0.5f;
        
        // if (player.transform.position.y > rocketHand.transform.position.y)
        //     meanTarget.position = new Vector3(player.transform.position.x - dist,
        //         player.transform.position.y - dist,
        //         0);
        //
        // else
        //     meanTarget.position = new Vector3(player.transform.position.x + dist,
        //         player.transform.position.y + dist,
        //         0);
        meanTarget.position = dist;
        print(dist.ToString());
        cam.Follow = meanTarget;
    }
}

