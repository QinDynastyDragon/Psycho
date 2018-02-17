using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour {

	protected float attackCooldown = 3f;
	private float enterAreaTime = 0f;

	public float damage = 30;

	public GameObject target;

	private Enemy enemy;

	//cone
	private Vector3 coneCenter;
	private float coneRadius = 1.5f;
	private float coneAngle = 120f; //in degrees

	//temp
	private Vector3 leftVector;
	private Vector3 rightVector;

	void Start () {
		enemy = GetComponent<Enemy> ();
	}

	void Update () {
		leftVector = Quaternion.AngleAxis (-coneAngle / 2, Vector3.up) * transform.forward;
		rightVector = Quaternion.AngleAxis (coneAngle / 2, Vector3.up) * transform.forward;

		Debug.DrawRay (transform.position, leftVector * 10, Color.red);
		Debug.DrawRay (transform.position, rightVector * 10, Color.red);

		if (isInsideCone(target)){
			if (!enemy.isAttacking) { //if just entered the cone
				enterAreaTime = Time.time;
				StartAttack ();
			} else { //if stayed in the cone
				if (Time.time - enterAreaTime >= attackCooldown) {
					print ("You are damaged");
					enemy.isAttacking = false;
				}
			}
		} else {
			enterAreaTime = 0;
			enemy.isAttacking = false;
		}
	}

	private bool isInsideCone(GameObject target){
		coneCenter = transform.position;
		Vector3 targetPos = target.transform.position;
		if ((targetPos - coneCenter).magnitude <= coneRadius) { //check if within radius
			Vector3 vecToTarget = targetPos - coneCenter;
			if(Vector3.Angle(transform.forward, vecToTarget) <= coneAngle / 2){
				return true;
			}
		}
		return false;
	}

	private void StartAttack(){
		enemy.isAttacking = true;
		//play attack animation
		transform.LookAt(target.transform.position);
	}
}
