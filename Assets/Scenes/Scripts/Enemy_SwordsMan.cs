using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SwordsMan : Enemy {
	private float attackDamage = 100f;
	protected override void Start(){
		base.Start ();
		chaseProvocRadius = 15f;
		moveSpeed = 8f;
	}

	protected override void Update(){
		base.Update ();

	}

	protected override IEnumerator Attack(){
		isAttacking = true;
		yield return new WaitForSeconds (1f); //if this gameobject is destroyed within this time, it will not run further
		print ("Attacked");
		if (IsInAttackRange (chaseTarget)) {
			var damagable = chaseTarget.GetComponent<IDamagable> ();
			if (damagable.IsVulnerable ()) {
				damagable.ReceivedDamage (attackDamage);
				print ("Damaged Player");
			} else
				print ("Player is currently invurnerable");
		}
		isAttacking = false;
	}
	protected override bool IsInAttackRange(GameObject obj){
		if ((transform.position - obj.transform.position).magnitude <= 4f) {
			return true;
		}
		return false;
	}


}
