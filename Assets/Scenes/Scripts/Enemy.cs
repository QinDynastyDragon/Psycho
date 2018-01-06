using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour, IDamagable {
    public float moveSpeed = 5f;
	private Vector3 knockBackForce = Vector3.zero;
    private float knockBackLightness = 0.3f;


    private Text healthText;
	private Canvas canvas;
	private Quaternion canvasOriRotation;

	//chasing
	private EnemyMovement enemyMovement;
	public GameObject chaseTarget;
	public float chaseProvocRadius = 10f;

	//states
	public bool isStunned = false;
	public bool isAttacking = false;

	protected virtual void Start () {
		canvas = transform.Find ("Canvas").GetComponent<Canvas> ();
		healthText = canvas.transform.Find ("HealthText").GetComponent<Text> ();
		enemyMovement = GetComponent<EnemyMovement> ();
		StartHealth = 100f;
        Health = StartHealth;
		canvas.transform.LookAt (-Camera.main.transform.position);
		canvasOriRotation = canvas.transform.rotation;
    }

    // Update is called once per frame
    protected virtual void Update () {
		KnockBackGlide ();

		if (!isStunned && !isAttacking) {
			enemyMovement.StartChase (chaseTarget);
		} else {
			enemyMovement.StopChase ();
		}
		if (!isAttacking && IsInAttackRange (chaseTarget)) {
			StartCoroutine ("Attack");
		}

    }

	void LateUpdate(){
		canvas.transform.rotation = canvasOriRotation;
	}

    private void changeHealthText(int newHealth) {
        healthText.text = newHealth.ToString() + "/" + StartHealth.ToString();
    }
    public void KnockBack(Vector3 dir){
        knockBackForce = dir * knockBackLightness;
    }
    private void KnockBackGlide(){
		if (knockBackForce.magnitude > 0.01f) {
			isStunned = true;
			transform.position += knockBackForce;
			knockBackForce *= 0.85f;
		} else {
			knockBackForce = Vector3.zero;
			isStunned = false;
		}
    }

	#region IDamagable implementation
	public float StartHealth { get; private set; }
	public float Health { get; set; }
	public void ReceivedDamage (float amount) {
		print ("Enemy is damaged");
		Health -= amount;
		changeHealthText((int)Health);
		if (Health <= 0f) {
			Destroy(gameObject);
		}
	}
	public bool IsVulnerable (){
		return true;
	}
	#endregion



	//must be overridden
	protected abstract IEnumerator Attack();
	protected abstract bool IsInAttackRange (GameObject obj);

}
