using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionsInProgress {
    public bool stopGrab = false;
    public bool onClimbJump = false;
    public bool onJump = false;

    public void Init() {
        stopGrab = false;
        onClimbJump = false;
        onJump = false;
    }
}

public class Player : MonoBehaviour {

    public static Player instance;
    private Rigidbody2D rg2d;
    private Animator animator;
    private PlayerCollision collisions;

    private float speed = 18f;
    private float m_MovementSmoothing = .05f;
    private Vector2 m_Velocity = Vector2.zero;
    const int defaultGravityScale = 3;

    private Vector2 moveDirection;
    private Vector3 localScale;
    // toDo remettre tout en private apres juste pour debug!
    public bool vMove;
    public bool hMove;
    private float currentSpeed;
    public int facingDirection;
    private bool resetTransformAfterClimb;
    private bool resetTransformAfterJumpClimb;
    /* Peu effectuer une action */
    public bool canDoubleJump;
    public bool canGrab; // peu se suspendre à un mur
    public bool canClimbJump; // peu enjember un mur
    /* actions en cours */
    public bool onWallClimb;
    public bool onGrab;
    public bool onCrouch;
    public bool onFall;
    public float onFallTime;
    // public bool onClimbJump;
    // public bool onJump;

    public ActionsInProgress actionsInProgress;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        actionsInProgress = new ActionsInProgress();
    }

    void Start() {
        collisions = GetComponent<PlayerCollision>();
        //toolbar = GameObject.FindGameObjectWithTag("InventoryToolbar").GetComponent<InventoryToolbar>();
        // rg2d = gameObject.GetComponent<Rigidbody2D>();
        rg2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        localScale = GetComponent<Transform>().localScale;
        InputManager.gameplayControls.Player.Jump.performed += Jump;
        InputManager.gameplayControls.Player.Move.performed += Move;
    }

    private void OnDestroy() {
        InputManager.gameplayControls.Player.Jump.performed -= Jump;
        InputManager.gameplayControls.Player.Move.performed -= Move;
    }

    private void Move(InputAction.CallbackContext ctx) {
        moveDirection = ctx.ReadValue<Vector2>();
    }

    private void Jump(InputAction.CallbackContext ctx) {
        if (collisions.OnGround() && !onWallClimb) {
            rg2d.gravityScale = defaultGravityScale;
            rg2d.velocity = new Vector2(rg2d.velocity.x, 18);
            animator.SetTrigger("JumpTrigger");
            actionsInProgress.onJump = true;
        }
    }

    private void FixedUpdate() {
        SetVelocity();
    }

    private void ManageResetPositionAfterAnimations() {
        if (resetTransformAfterJumpClimb) {
            if (facingDirection > 0) {
                transform.position = new Vector3((int)transform.position.x + 1.5f, Mathf.Round(transform.position.y + 0.8f), transform.position.z);
            } else {
                transform.position = new Vector3((int)transform.position.x - 0.5f, Mathf.Round(transform.position.y + 0.8f), transform.position.z);
            }
            actionsInProgress.onClimbJump = false;
            resetTransformAfterJumpClimb = false;
        }

        if (resetTransformAfterClimb) {
            if (facingDirection > 0) {
                transform.position = new Vector3((int)transform.position.x + 1.5f, (int)transform.position.y + 3, transform.position.z);
            } else {
                transform.position = new Vector3((int)transform.position.x - 0.5f, (int)transform.position.y + 3, transform.position.z);
            }
            resetTransformAfterClimb = false;
            onWallClimb = false;
        }
    }

    private void ManageBooleans() {

        if (!collisions.OnGround()) {
            onFallTime += Time.deltaTime;
        } else {
            onFallTime = 0;
        }

        if (collisions.OnGround()) {
            canDoubleJump = true;
        } else {
            canDoubleJump = false;
        }

        if (moveDirection.x > 0 || moveDirection.x < 0) {
            hMove = true;
        } else {
            hMove = false;
        }

        if (moveDirection.y > 0 || moveDirection.y < 0) {
            vMove = true;
        } else {
            vMove = false;
        }

        if (moveDirection.y < 0 && collisions.OnGround() && currentSpeed == 0) {
            onCrouch = true;
        } else {
            onCrouch = false;
        }

        if (collisions.OnGround() && !collisions.IsTopCollision() && !collisions.IsMiddleCollision() && collisions.IsBottomCollision()) {
            canClimbJump = true;
        } else {
            canClimbJump = false;
        }

    }

    void Update() {

        // respect order of functions calls !!!
        ManageBooleans();
        ManageClimbJump();
        ManageGrab();
        ManageResetPositionAfterAnimations();
        SetLocalScale();
        DetectPickableItemsInArea();
        SetAnimators();
        if (!onGrab && !actionsInProgress.onJump) {
            if (!collisions.OnGround()) {
                rg2d.gravityScale = defaultGravityScale;
            } else {
                rg2d.gravityScale = 1;
            }
        }
        /*if (wallGrab) {
            rg2d.gravityScale = 0;
            rg2d.velocity = new Vector2(rg2d.velocity.x, 0);
            float y = Input.GetAxis("Vertical");
            float speedModifier = y > 0 ? 0.35f : 1;
            rg2d.velocity = new Vector2(rg2d.velocity.x, y * (speed * speedModifier));
        } else {
            rg2d.gravityScale = defaultGravityScale;
        }*/

    }

    private void SetAnimators() {
        animator.SetFloat("Speed", currentSpeed);
        /*if (!actionsInProgress.onJump || !actionsInProgress.onClimbJump) {
            animator.SetBool("OnGround", collisions.OnGround());
        } else {
            animator.SetBool("OnGround", true);
        }*/
        animator.SetBool("OnFall", onFallTime > 0.3f ? true : false);
        animator.SetBool("OnGround", collisions.OnGround());
        animator.SetBool("WallClimb", onWallClimb);
        animator.SetBool("WallGrab", onGrab);
        animator.SetBool("Crouch", onCrouch);
        animator.SetBool("ClimbJump", actionsInProgress.onClimbJump);
        animator.SetBool("Hmove", hMove);
    }

    private void SetVelocity() {
        currentSpeed = 0;
        if (onWallClimb || actionsInProgress.onClimbJump || onGrab)
            return;
        Vector2 targetVelocity;
        if (onCrouch) {
            onCrouch = false;
        }
        if (hMove) {
            currentSpeed = Mathf.Abs(moveDirection.x * 25f);
            targetVelocity = new Vector2(moveDirection.x * currentSpeed, rg2d.velocity.y);
        } else {
            targetVelocity = new Vector2(0, rg2d.velocity.y);
        }
        rg2d.velocity = Vector2.SmoothDamp(rg2d.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }

    private void SetLocalScale() {
        if (onWallClimb || actionsInProgress.onClimbJump || onGrab)
            return;
        if (moveDirection.x > 0) {
            transform.localScale = new Vector2(localScale.x, localScale.y);
            facingDirection = 1;
        }
        if (moveDirection.x < 0) {
            transform.localScale = new Vector2(-localScale.x, localScale.y);
            facingDirection = -1;
        }
    }

    private void ManageGrab() {
        if (actionsInProgress.onClimbJump || onWallClimb)
            return;
        if (collisions.CanGrab() && collisions.OnHandWall() && !collisions.OnGround()) {
            canGrab = true;
        } else {
            canGrab = false;
        }
        if (!onGrab && !actionsInProgress.stopGrab) {
            if (canGrab && hMove) {
                onGrab = true;
                rg2d.gravityScale = 0;
                rg2d.velocity = new Vector2(rg2d.velocity.x, 0);
                if (facingDirection > 0) {
                    transform.position = new Vector3((int)transform.position.x + 1, (int)transform.position.y + 0.5f, transform.position.z);
                } else {
                    transform.position = new Vector3((int)transform.position.x, (int)transform.position.y + 0.5f, transform.position.z);
                }
                animator.SetTrigger("WallGrabTrigger");
            }
        } else {
            /*if (hMove && !actionsInProgress.stopGrab) {
                if (!canGrab) {
                    StartCoroutine(StopGrab());
                }
            }*/
            if (vMove && !actionsInProgress.stopGrab) {
                if (moveDirection.y > 0) {
                    onWallClimb = true;
                    // to Do penser à checker si le perso peut ou non passer !!!!!!!!!!!!!!!!!!
                }
                if (moveDirection.y < 0) {
                    StartCoroutine(StopGrab());
                }
            }
        }
    }

    private IEnumerator StopGrab() {
        actionsInProgress.stopGrab = true;
        rg2d.gravityScale = 1;
        yield return new WaitForSeconds(0.2f);
        onGrab = false;
        actionsInProgress.stopGrab = false;
    }

    private void ManageClimbJump() {
        if (actionsInProgress.onClimbJump || onWallClimb) {
            return;
        }
        if (hMove && canClimbJump && (moveDirection.x > 0 && facingDirection > 0 || moveDirection.x < 0 && facingDirection < 0)) {
            actionsInProgress.onClimbJump = true;
        }
    }

    public void TriggerAnimationClimbJumpFinished() {
        rg2d.gravityScale = defaultGravityScale;
        resetTransformAfterJumpClimb = true;
    }

    public void TriggerJumpFinished() {
        actionsInProgress.onJump = false;
    }


    /**
     * @param { resetTransformAfterClime }
     * pour reset la postion du player a zéro apres la fin de l'anim.
     * En effet seule la position de l'anim a bougée pas celle tu container Player!
     * Cela évite aussi de voir l'effet de clignotement lors de la remise a zéro du container
     */
    public void TriggerClimbAnimationFinished() {
        StartCoroutine(StopGrab());
        resetTransformAfterClimb = true;

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Pickable")) {
            Item itemToAdd = collision.GetComponent<Item>();
            if (InventoryManager.instance.AddItem(itemToAdd)) {
                itemToAdd.Destroy();
            }
        }
    }

    private void DetectPickableItemsInArea() {
        Collider2D[] itemsCollider = Physics2D.OverlapCircleAll(transform.position, 1f, (1 << 14));

        for (int i = 0; i < itemsCollider.Length; i++) {
            if (itemsCollider[i].CompareTag("Pickable")) {
                bool canAddItem = InventoryManager.instance.CanAddItem(itemsCollider[i].GetComponent<Item>());
                Attractor attractor = itemsCollider[i].GetComponent<Attractor>();
                if (canAddItem) {
                    attractor.Setup(transform, 5f);
                } else {
                    attractor.Reset();
                }
            }
        }
    }
}