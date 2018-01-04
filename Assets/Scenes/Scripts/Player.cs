using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {
	private float dashLength = 6f;   //the max length on each dash
	private float dashDuration = 0.5f; //time to spend on each dash
	private float dashDamage = 30;
	private float dashCoolDown = 0.7f;
	private float dashTimeSpent = 0f; //time it spent on current dash
	private float lastDashTime;
	private Vector3 dashStartPos;
	private Vector3 dashTargetPos;
	private bool isDashGliding = false;
	private HashSet<Enemy> dashDamagedEnemies = new HashSet<Enemy> ();

	private bool isControllable = true;

	private float moveSpeed = 4f; //for moving with mouseHold
    private Vector3 moveDir;
    
    public GameObject afterShadowCloneOfDash;
    
    private float gravity = .3f;

    private CharacterController charCtrl;

    // Use this for initialization
    void Start () {
        charCtrl = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isDashGliding)
        {
            DashGliding(); // glide till it stops 
        }
        if (!charCtrl.isGrounded) {
            charCtrl.Move(Vector3.down * gravity); //WRONG%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            InstantiateAfterShadow();
			StartDashing((MouseToPlayerHorPlanePos() + Vector3.up * charCtrl.height / 2 - transform.position).normalized);
        }
    }

	public void OnSwipe(Vector3 dir){ // is called once when user swipes
		print("Swiped");
		if (isControllable)
			StartDashing(SwipeDirToDashDir(dir));
	}
	public void OnMouseHold(){
		transform.LookAt (MouseToPlayerHorPlanePos ()+Vector3.up * charCtrl.height / 2);
		if(isControllable){
			charCtrl.Move((MouseToPlayerHorPlanePos() + Vector3.up * charCtrl.height / 2 - transform.position).normalized*moveSpeed*Time.deltaTime);
		}
		//MouseToPlayerHorPlanePos()
	}
		


    private Vector3 SwipeDirToDashDir(Vector2 swipeDir)
    {
        return new Vector3(swipeDir.x, 0, swipeDir.y);
    }

    private void InstantiateAfterShadow()
    {
        var instantiatedShadowToBeDeleted = Instantiate(afterShadowCloneOfDash, transform.position, transform.rotation);
        Destroy(instantiatedShadowToBeDeleted, 0.3f);
    }

    private void StartDashing(Vector3 direction) {
		//assumes that the direction is normalized
		if (lastDashTime + dashCoolDown > Time.time) { //if dash not ready
			//print("dash not ready");
			return;
		}
        isDashGliding = true;
        lastDashTime = Time.time;
        isControllable = false;

        dashStartPos = transform.position;
		dashTargetPos = dashStartPos + direction * dashLength;
		transform.LookAt(dashTargetPos);
    }

    private void DashGliding()
    {
        Vector3 pos = transform.position;
		Vector3 toTarget = dashTargetPos - dashStartPos;
		if (dashTimeSpent >= dashDuration){
            StopDashGliding();
			return;
        }
		dashTimeSpent += Time.deltaTime;
		if (dashDuration > dashTimeSpent) {
			charCtrl.Move (toTarget * Time.deltaTime / dashDuration);
		} else {
			charCtrl.Move (toTarget * (dashTimeSpent - dashDuration) / dashDuration);
		}
    }

    private void StopDashGliding()
    {
		isControllable = true;
        isDashGliding = false;
		dashTimeSpent = 0;
		dashDamagedEnemies.Clear ();
		//print ("done dashing");
    }

    void OnTriggerEnter(Collider other)
    {
		var enemy = other.gameObject.GetComponent<Enemy> ();
		if (enemy != null) {
			if (Vector3.Angle (dashTargetPos - dashStartPos, enemy.transform.position - transform.position) <= 90) {
				if (IsDashing ()) {
					if (!dashDamagedEnemies.Contains (enemy)) {
						dashDamagedEnemies.Add (enemy);
						InflictDamage (enemy);
						print ("Player dashed through " + other.name);
					}
				}
			}
		}
    }

    private void InflictDamage(Enemy target) {
		target.DecreaseHealth ((int)dashDamage);
    }

   
    private Vector3 MouseToPlayerHorPlanePos()
    {
        Vector3 hitPoint = Vector3.zero;
        Vector3 pos = transform.position;

        Plane horPlane = new Plane(Vector3.down, new Vector3(pos.x, pos.y - charCtrl.height / 2, pos.z)); //pos.y - charCtrl.height / 2
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDist;

        if (horPlane.Raycast(camRay, out rayDist))
        {
            hitPoint = camRay.GetPoint(rayDist);
        }
        return hitPoint;
    }

    public CharacterController GetCol()
    {
		return GetComponent<CharacterController> ();
    }

	public bool IsDashing(){
		return isDashGliding;
	}
}