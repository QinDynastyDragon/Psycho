using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    private NavMeshAgent pathFinder;
    public Player player;
    private CapsuleCollider capsCol;
    private float provocRange = 10;
 
  
	// Use this for initialization
	void Start () {
        pathFinder = GetComponent<NavMeshAgent>();
        //targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        capsCol = GetComponent<CapsuleCollider>();    
	}
	
	// Update is called once per frame
	void Update () {
		if ((player.transform.position - transform.position).magnitude <= provocRange) {
			pathFinder.SetDestination (player.transform.position);
		} else {
			pathFinder.SetDestination (transform.position);
		}
        //CheckChase();
	}

    public void StopChase()
	{
		pathFinder.isStopped = true;
		pathFinder.SetDestination (transform.position);
	}

    public void StartChase(GameObject target){
		pathFinder.SetDestination(target.transform.position);
        pathFinder.isStopped = false;
    }

	void LateUpdate(){
		MoveOut ();
	}

	void FixedUpdate(){
		MoveOut ();
	}

	void MoveOut(){
		//move out of the collider of player.

		//if player is dashing, let player get through
		if (player.IsDashing())
			return;

		Vector3 a = transform.position;
		Vector3 b = player.transform.position;
		float m = (a - b).magnitude;
		float s = capsCol.radius + player.GetCol ().radius;// + 0.03f;

		if (m < s) {
			transform.position += (a-b).normalized * (s - m);
		}
	}
}


