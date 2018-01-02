using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {


    private NavMeshAgent pathFinder;
    public Player targetPlayer;
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
        CheckChase();
	}

    void CheckChase()
    {
        Vector3 pos = transform.position;
        Vector3 targetPos = targetPlayer.transform.position;

        if ((pos - targetPos).magnitude <= capsCol.radius + targetPlayer.GetCapsCol().radius)
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
