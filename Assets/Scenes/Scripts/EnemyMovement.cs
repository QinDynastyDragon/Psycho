using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    private NavMeshAgent pathFinder;
    
	private bool isChasing = false;

	void Start () {
        pathFinder = GetComponent<NavMeshAgent>();
	}

    public void StopChase(){
        pathFinder.isStopped = true;
        pathFinder.SetDestination(transform.position);
    }

    public void StartChase(GameObject target){
		pathFinder.SetDestination(target.transform.position);
        pathFinder.isStopped = false;
    }
}
