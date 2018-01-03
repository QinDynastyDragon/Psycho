using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {


    private NavMeshAgent pathFinder;
    public Player targetPlayer;
    private CapsuleCollider capsCol;
    private float provocRange = 10;

    private Enemy self;

	// Use this for initialization
	void Start () {
        pathFinder = GetComponent<NavMeshAgent>();
        //targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //capsCol = GetComponent<CapsuleCollider>();

        self = GetComponent<Enemy>();
	}

	// Update is called once per frame
	void Update () {
		if (!self.isStunned) {
			CheckChase ();
		}
	}

    void CheckChase()
    {
        Vector3 pos = transform.position;
        Vector3 targetPos = targetPlayer.transform.position;

		if ((pos - targetPos).magnitude <= transform.localScale.x/2 + targetPlayer.transform.localScale.x/2)
        {
            StopChase();
        }
        else if ((pos - targetPos).magnitude <= provocRange)
        {
            Chase(targetPos);
        }
        else
        {
            StopChase();
        }
    }

    void StopChase()
    {
        pathFinder.isStopped = true;
        pathFinder.SetDestination(transform.position);
    }

    void Chase(Vector3 target)
    {
        pathFinder.SetDestination(target);
        pathFinder.isStopped = false;
    }
}
