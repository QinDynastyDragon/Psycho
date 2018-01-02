﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public int maxHealth = 100;
    public float moveSpeed = 5;

    private int health;

    public Text healthText;

	// Use this for initialization
	void Start () {
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public int Health { set { health = value; changeHealthText(value); } get { return health; }}

    private void changeHealthText(int newHealth) {
        healthText.text = newHealth.ToString() + "/" + maxHealth.ToString();
    }

	public void DecreaseHealth(int amount){
		Health -= amount;
		if (health <= 0f) {
			print(gameObject.name + " got killed");
			Destroy(gameObject);
		}
	}
}