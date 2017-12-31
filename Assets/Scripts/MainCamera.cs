using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    private Vector3 distFromTarget = new Vector3(0, 16, -6);
    private Vector3 moveTarget; //for the camera
    private float moveSpeed = 5f;
    public GameObject player;
    
	void Start () {
        transform.position = player.transform.position + distFromTarget;
        transform.LookAt(player.transform);
	}
	
	void Update () {
        FollowPlayer();

	}

    void FollowPlayer() {
        //targetPlayerDist = player.transform.position - (transform.position - distFromTarget);
        transform.position = Vector3.Lerp(transform.position, player.transform.position + distFromTarget, moveSpeed * Time.deltaTime);
    } //should follow player smoothly, should catch up faster when far away from player
}
