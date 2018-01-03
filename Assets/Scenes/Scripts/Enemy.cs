using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public int maxHealth = 100;
    public float moveSpeed = 5;
    public bool isStunned = false;
    private int health;
    private Vector3 knockBackForce = Vector3.zero;
    private float knockBackLightness = 0.1f;

	public Camera camera;

    public Text healthText;
	public Canvas canvas;
	private Quaternion canvasOriRotation;

    // Use this for initialization
    void Start () {
        health = maxHealth;
		canvas.transform.LookAt (-camera.transform.position);
		canvasOriRotation = canvas.transform.rotation;
    }

    // Update is called once per frame
    void Update () {
		KnockBackGlide ();

    }

	void LateUpdate(){
		canvas.transform.rotation = canvasOriRotation;
	}

    public int Health { set { health = value; changeHealthText(value); } get { return health; }}

    private void changeHealthText(int newHealth) {
        healthText.text = newHealth.ToString() + "/" + maxHealth.ToString();
		print ("Damaged");
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
