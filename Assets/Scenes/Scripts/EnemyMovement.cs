using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

    private NavMeshAgent pathFinder;
	private Transform chaseTarget;
    private CapsuleCollider capsCol;
	private Enemy thisEnemy;
	private bool isTargetPlayer = false;
  
	// Use this for initialization
	void Start () {
		thisEnemy = GetComponent<Enemy> ();
		chaseTarget = thisEnemy.chaseTarget.transform;
        pathFinder = GetComponent<NavMeshAgent>();
		capsCol = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		if ((chaseTarget.position - transform.position).magnitude <= thisEnemy.chaseProvocRadius) {
			pathFinder.SetDestination (chaseTarget.position);
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
		isTargetPlayer = (bool)target.GetComponent<Player> ();
    }

	void LateUpdate(){
		MoveOut ();
	}

	void FixedUpdate(){
		MoveOut ();
	}

	void MoveOut(){
		if (!isTargetPlayer)
			return;
		//move out of the collider of player.
		var player = chaseTarget.GetComponent<Player>();
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


