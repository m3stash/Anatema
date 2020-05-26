using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    public static Player instance;
    private Rigidbody2D rg2d;
    private Animator animator;
    private PlayerCollision collisions;

    private float speed = 9f;
    private float m_MovementSmoothing = .05f;
    private Vector2 m_Velocity = Vector2.zero;

    private Vector2 moveDirection;
    private Vector3 localScale;
    // toDo remettre tout en private apres juste pour debug!
    public bool vMove;
    public bool hMove;
    private Transform playerTransform;
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
    public bool onClimbJump;
    public bool onJump;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        playerTransform = GetComponentsInParent<Transform>()[1];
        collisions = GetComponent<PlayerCollision>();
        //toolbar = GameObject.FindGameObjectWithTag("InventoryToolbar").GetComponent<InventoryToolbar>();
        // rg2d = gameObject.GetComponent<Rigidbody2D>();
        rg2d = GetComponentInParent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        localScale = gameObject.GetComponent<Transform>().localScale;
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
        if (collisions.OnGround() && !onWallClimb && !onClimbJump) {
            rg2d.gravityScale = 1;
            rg2d.velocity = new Vector2(rg2d.velocity.x, 7);
            animator.SetTrigger("JumpTrigger");
            onJump = true;
        }
        /*if (Input.GetButtonDown("Jump") && grounded) {
            velocity.y = jumpTakeOffSpeed;
        } else if (Input.GetButtonUp("Jump")) {
            if (velocity.y > 0) {
                velocity.y = velocity.y * 0.5f;
            }
        }*/
        // animator.SetBool("grounded", grounded);
        // animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
    }

    private void FixedUpdate() {
        SetVelocity();
    }

    private void ManageResetPositionAfterAnimations() {
        if (resetTransformAfterJumpClimb) {
            if (facingDirection > 0) {
                playerTransform.position = new Vector3((int)playerTransform.position.x + 1.5f, Mathf.Round(playerTransform.position.y + 1), playerTransform.position.z);
            } else {
                playerTransform.position = new Vector3((int)playerTransform.position.x - 0.5f, Mathf.Round(playerTransform.position.y + 1), playerTransform.position.z);
            }
            resetTransformAfterJumpClimb = false;
            onClimbJump = false;
        }

        if (resetTransformAfterClimb) {
            if (facingDirection > 0) {
                playerTransform.position = new Vector3((int)playerTransform.position.x + 1.5f, (int)playerTransform.position.y + 3, playerTransform.position.z);
            } else {
                playerTransform.position = new Vector3((int)playerTransform.position.x - 0.5f, (int)playerTransform.position.y + 3, playerTransform.position.z);
            }
            resetTransformAfterClimb = false;
            onWallClimb = false;
        }
    }

    private void ManageBooleans() {

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
        ManageClimbJump();
        ManageGrab();
    }

    void Update() {

        if (!onGrab && !onJump) {
            if (!collisions.OnGround()) {
                rg2d.gravityScale = 3;
            } else {
                rg2d.gravityScale = 1;
            }
        }

        // respect order of functions calls !!!
        ManageBooleans();
        ManageResetPositionAfterAnimations();
        SetLocalScale();
        DetectPickableItemsInArea();
        SetAnimators();
        /*if (wallGrab) {
            rg2d.gravityScale = 0;
            rg2d.velocity = new Vector2(rg2d.velocity.x, 0);
            float y = Input.GetAxis("Vertical");
            float speedModifier = y > 0 ? 0.35f : 1;
            rg2d.velocity = new Vector2(rg2d.velocity.x, y * (speed * speedModifier));
        } else {
            rg2d.gravityScale = 3;
        }*/

    }

    private void SetAnimators() {
        animator.SetFloat("Speed", currentSpeed);
        animator.SetBool("OnGround", collisions.OnGround());
        animator.SetBool("WallClimb", onWallClimb);
        animator.SetBool("WallGrab", onGrab);
        animator.SetBool("Crouch", onCrouch);
        animator.SetBool("ClimbJump", onClimbJump);
    }

    private void SetVelocity() {
        currentSpeed = 0;
        if (onWallClimb || onClimbJump || onGrab)
            return;
        Vector2 targetVelocity;
        if (onCrouch) {
            onCrouch = false;
        }
        if (hMove) {
            currentSpeed = Mathf.Abs(moveDirection.x * 20f);
            targetVelocity = new Vector2(moveDirection.x * speed, rg2d.velocity.y);
        } else {
            targetVelocity = new Vector2(0, rg2d.velocity.y);
        }
        rg2d.velocity = Vector2.SmoothDamp(rg2d.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }

    private void SetLocalScale() {
        if (onWallClimb || onClimbJump || onGrab)
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

    private void StopGrab() {
        onGrab = false;
        rg2d.gravityScale = 1;
    }

    private void ManageGrab() {
        if (onClimbJump || onWallClimb)
            return;
        if (collisions.CanGrab() && collisions.OnHandWall() && !collisions.OnGround()) {
            canGrab = true;
        } else {
            canGrab = false;
        }
        if (!onGrab) {
            if (canGrab && hMove) {
                onGrab = true;
                rg2d.gravityScale = 0;
                rg2d.velocity = new Vector2(rg2d.velocity.x, 0);
                if (facingDirection > 0) {
                    playerTransform.position = new Vector3((int)playerTransform.position.x + 1, (int)playerTransform.position.y + 0.5f, playerTransform.position.z);
                } else {
                    playerTransform.position = new Vector3((int)playerTransform.position.x, (int)playerTransform.position.y + 0.5f, playerTransform.position.z);
                }
                animator.SetTrigger("WallGrabTrigger");
            }
        } else {
            if (hMove) {
                if (!canGrab) {
                    StopGrab();
                }
            }
            if (vMove) {
                if (moveDirection.y > 0) {
                    onWallClimb = true;
                    // to Do penser à checker si le perso peut ou non passer !!!!!!!!!!!!!!!!!!
                }
                if (moveDirection.y < 0) {
                    StopGrab();
                }
            }
        }
    }

    private void ManageClimbJump() {
        if (onClimbJump || onWallClimb) {
            return;
        }
        if (hMove && canClimbJump) {
            onClimbJump = true;
        }
    }

    public void TriggerAnimationClimbJumpFinished() {
        resetTransformAfterJumpClimb = true;
    }

    public void TriggerJumpFinished() {
        onJump = false;
    }


    /**
     * @param { resetTransformAfterClime }
     * pour reset la postion du player a zéro apres la fin de l'anim.
     * En effet seule la position de l'anim a bougée pas celle tu container Player!
     * Cela évite aussi de voir l'effet de clignotement lors de la remise a zéro du container
     */
    public void TriggerClimbAnimationFinished() {
        StopGrab();
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
                    attractor.Setup(this.transform, 5f);
                } else {
                    attractor.Reset();
                }

            }
        }
    }

}