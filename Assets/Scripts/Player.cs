using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private float moveSpeed = 0.1f;
    private float dashDist = 6f;
    private float dashDamage = 30;

    private Vector3 dashDir;
    private Vector3 moveDir;
    private float dashCoolDown = 0.3f;
    private bool isDashable = true;
    private float lastDashTime;
    private float dashSpeed = 15;
    private bool isDashing = false;
    private float swipableTime = 0.3f;
    private float startPressTime;
    private bool isMovable = true;

    private Vector3 dashStartPos;

    

    private bool isPressedMouseLastFrame;
    private float swipeDistMin = 8f;
    private Vector2 swipeDir;
    private Vector2 lastMousePos;

    public GameObject afterShadowCloneOfDash;
    private GameObject instantiatedShadowToBeDeleted;
    
    private float shadowStartTime;

    private float gravity = .3f;


    private CapsuleCollider capsCol;

    private CharacterController charCtrl;

    // Use this for initialization
    void Start () {
        charCtrl = GetComponent<CharacterController>();
        capsCol = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isDashing)
        {
            Dash();
        }
        if (Input.GetMouseButtonDown(0))
        {
            startPressTime = Time.time;
        }
        if (Input.GetMouseButton(0))
        {
            if (Time.time - startPressTime > swipableTime)
            {
                isDashable = false;
            }
            if (isMovable)
            {
                Move();
            }
            
            
            if (GetIsSwiped() && Time.time - (lastDashTime + dashDist/dashSpeed) > dashCoolDown && isDashable)
            {
                    
                StartDashing(SwipeDirToDashDir(swipeDir));
                
            }
        }
        else
        {
            isPressedMouseLastFrame = false;
            isDashable = true;
        }
        if (!charCtrl.isGrounded) {
            charCtrl.Move(Vector3.down * gravity);
        }
        

        if (Input.GetKeyDown(KeyCode.Space)) {
            InstantiateAfterShadow();
            StartDashing(MouseToPlayerHorPlanePos() + Vector3.up * charCtrl.height / 2 - transform.position);
        }
    }

    private Vector3 SwipeDirToDashDir(Vector2 swipeDir)
    {
        Vector3 newDashDir = new Vector3(swipeDir.x, 0, swipeDir.y);
        return newDashDir;

    }

    private void InstantiateAfterShadow()
    {
        instantiatedShadowToBeDeleted = (GameObject)Instantiate(afterShadowCloneOfDash, transform.position, transform.rotation);
        Destroy(instantiatedShadowToBeDeleted, 0.3f);
    }

    private void StartDashing(Vector3 direction) {
        
        isDashable = false;
        isDashing = true;
        lastDashTime = Time.time;
        isMovable = false;

        dashDir = direction.normalized;
        dashStartPos = transform.position;
        transform.LookAt(transform.position + direction);

    }

    private void Dash()
    {
        Vector3 pos = transform.position;
        if ((pos - dashStartPos).magnitude >= dashDist)
        {
            StopDashing();

        }
        else
        {
            charCtrl.Move(Time.deltaTime * dashSpeed * dashDir);
        }
    }

    private void StopDashing()
    {
        isMovable = true;
        isDashable = true;
        isDashing = false;
    }

    void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if (other.gameObject.name == "EnemyAI")
        {
            if (isDashing)
            {
                Damage(other.gameObject.GetComponent<Enemy>());
            }
        }
    }

    private void Damage(Enemy target) {
        int targetCurHealth = target.Health;
        target.Health = targetCurHealth - (int) dashDamage;
    }

    private bool GetIsSwiped() {
        if (!isPressedMouseLastFrame)
        {
            lastMousePos = Input.mousePosition;
            isPressedMouseLastFrame = true;
            return false;
        }
        Vector2 mousePos = Input.mousePosition;
        Vector2 mouseMovedDist = (mousePos - lastMousePos) * Time.deltaTime;
        lastMousePos = mousePos;
        if(mouseMovedDist.magnitude > swipeDistMin)
        {
            swipeDir = mouseMovedDist.normalized;

            return true;
        }
        return false;
    }
   
    private Vector3 GetMoveDir()
    {
        Vector3 pos = transform.position;
        Vector3 hitPoint = MouseToPlayerHorPlanePos();
        Vector3 moveTarget = new Vector3(hitPoint.x, pos.y, hitPoint.z);
        return (moveTarget - pos).normalized;
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
    private void Move()
    {
        moveDir = GetMoveDir();
        charCtrl.Move(moveDir * moveSpeed);
        transform.LookAt(transform.position + moveDir);
    }

    public CapsuleCollider GetCapsCol()
    {
        return capsCol;
    }

}
