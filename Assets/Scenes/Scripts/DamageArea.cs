using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour {

	protected float delay;
	public float damage = 30;

	public GameObject target;

	//cone
	private Vector3 coneDir = new Vector3(0, 0, 1);
	private float coneCenter = transform.position;
	private float coneRadius = 0.7f;
	private float coneAngle = 1f;

	void Start () {
		
	}

	void Update () {
		if (isInsideCone(target)){
			
		}
	}

	private bool isInsideCone(GameObject target){
		
	}
}
