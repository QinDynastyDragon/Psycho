using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMouseControls : MonoBehaviour {
	private Player player;

	private float startMouseButtonDownTime = 0f;
	private Vector2 mousePosAtMouseDown;
	private float swipeDistMin;
	private Vector2 swipeDir;
	private float swipeMaxDuration = 0.2f;
	// Use this for initialization
	void Start () {
		player = GetComponent<Player> ();
		swipeDistMin = Mathf.Min (Screen.width, Screen.height) * 0.3f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0))
		{
			//print (Input.mousePosition);
			Vector2 mousePos = Input.mousePosition;
			Vector2 mouseTotalMovedDist = (mousePos - mousePosAtMouseDown);
			//print (mouseTotalMovedDist.magnitude);
			if (mouseTotalMovedDist.magnitude >= swipeDistMin && Time.time - startMouseButtonDownTime <= swipeMaxDuration)
			{
				print ("Swiped Screen");
				player.IsSwiped (mouseTotalMovedDist.normalized);
			}
		}
		if (Input.GetMouseButtonDown (0))
		{
			startMouseButtonDownTime = Time.time;
			mousePosAtMouseDown = Input.mousePosition;
		}

	}
}
