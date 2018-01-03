using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public int maxHealth = 100;
    public float moveSpeed = 5;
    private int health;
    private Vector3 knockBackForce = Vector3.zero;
    private float knockBackLightness = 0.1f;

	public Camera camera;

    public Text healthText;
	public Canvas canvas;
	private Quaternion canvasOriRotation;

	//chasing
	private EnemyMovement enemyMovement;
	public GameObject chaseTarget;
	private float provocRadius = 10;

	//states
	public bool isStunned = false;
	public bool isAttacking = false;

    void Start () {
		enemyMovement = GetComponent<EnemyMovement> ();

        health = maxHealth;
		canvas.transform.LookAt (-camera.transform.position);
		canvasOriRotation = canvas.transform.rotation;
    }

    // Update is called once per frame
    void Update () {
		KnockBackGlide ();

		if (!isStunned && !isAttacking) {
			CheckChase ();
		} else {
			enemyMovement.StopChase ();
		}
    }

	void CheckChase()
	{
		Vector3 pos = transform.position;
		Vector3 targetPos = chaseTarget.transform.position;

		if ((pos - targetPos).magnitude <= transform.localScale.x/2 + chaseTarget.transform.localScale.x/2){
			enemyMovement.StopChase();
		}else if ((pos - targetPos).magnitude <= provocRadius){
			enemyMovement.StartChase(chaseTarget);
		}else{
			enemyMovement.StopChase();
		}
	}

	void LateUpdate(){
		canvas.transform.rotation = canvasOriRotation;
	}

    public int Health { set { health = value; changeHealthText(value); } get { return health; }}

    private void changeHealthText(int newHealth) {
        healthText.text = newHealth.ToString() + "/" + maxHealth.ToString();
		print ("Enemy is damaged");
    }

    public void DecreaseHealth(int amount){
        Health -= amount;
        if (health <= 0f) {
            Destroy(gameObject);
        }
    }

    public void KnockBack(Vector3 dir){
        knockBackForce = dir * knockBackLightness;
    }
    private void KnockBackGlide(){
		if (knockBackForce.magnitude > 0.01f) {
			isStunned = true;
			transform.position += knockBackForce;
			knockBackForce *= 0.95f;
		} else {
			knockBackForce = Vector3.zero;
			isStunned = false;
		}
    }
}
