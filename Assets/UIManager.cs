using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public List<HealthBar> normalEnemyHealthbars;
	public Player player;
	public RectTransform playerHealthBar;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//playerHealthBar.localScale = new Vector3 (1f, player.Health/player.StartHealth, 1f);
		//loop ls tog healthbars and show them to screen as UI
	}

	public void AddNormalEnemyHealthBar(HealthBar obj){
		normalEnemyHealthbars.Add(obj);
	}
	public void RemoveNormalEnemyHealthBar(HealthBar obj){
		normalEnemyHealthbars.Remove(obj);
	}

	public void UpdatePlayerHealthBar(float percentage){
		playerHealthBar.localScale = new Vector3 (1f, percentage, 1f);
	}
}
