using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    public static Player instance;
    private Rigidbody2D rg2d;
    private Animator animator;
    private PlayerCollision collisions;

    private float speed = 10f;
    private Vector2 m_Velocity = Vector2.zero;
    private float m_MovementSmoothing = .05f;

    private Vector2 moveDirection;
    private Vector3 localScale;
    // toDo remettre tout en private apres juste pour debug!
    public bool canDoubleJump;
    public bool vMove;
    public bool hMove;
    public bool isGrab;
    public bool canGrab; // se suspendre à un mur
    public bool canClimb; // enjember un mur
    public bool onWallClimb;
    public int facingDirection;
    private bool stopVelocity;
    private bool stopLocalScale;
    private Transform playerTransform;

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
        if (collisions.OnGround()) {
            rg2d.velocity = new Vector2(rg2d.velocity.x, 7);
            animator.SetTrigger("JumpTrigger");
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

        if (collisions.OnGround()) {
            canDoubleJump = true;
        } else {
            if (!canDoubleJump) {
                // Debug.Log("DOUBLE JUMP");
            }
        }

        OnGrab();
        SetLocalScale();
        SetVelocity();
    }

    void Update() {

        if (collisions.OnGround()) {
            onWallClimb = false;
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

        if (collisions.CanGrab() && collisions.OnHandWall() && collisions.OnGround()) {
            canClimb = true;
        } else {
            canClimb = false;
        }

        // OnGrab();

        // SetLocalScale();

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
        animator.SetFloat("Speed", Mathf.Abs(moveDirection.x * 20f));
        animator.SetBool("OnGround", collisions.OnGround());
        animator.SetBool("WallClimb", onWallClimb);
        animator.SetBool("WallGrab", isGrab);
    }

    private void SetVelocity() {
        if (stopVelocity || isGrab || onWallClimb)
            return;
        Vector2 targetVelocity;
        if (hMove) {
            targetVelocity = new Vector2(moveDirection.x * speed, rg2d.velocity.y);
        } else {
            targetVelocity = new Vector2(0, rg2d.velocity.y);
        }
        rg2d.velocity = Vector2.SmoothDamp(rg2d.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }

    private void SetLocalScale() {
        if (stopLocalScale)
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

    private void SetGrab(bool grad) {
        if (grad) {
            isGrab = true;
            rg2d.gravityScale = 0;
            rg2d.velocity = new Vector2(rg2d.velocity.x, 0);
            Debug.Log(facingDirection);
            if (facingDirection > 0) {
                playerTransform.position = new Vector3((int)playerTransform.position.x + 0.5f, (int)playerTransform.position.y + 0.5f);
            } else {
                playerTransform.position = new Vector3((int)playerTransform.position.x, (int)playerTransform.position.y + 0.5f);
            }
        } else {
            isGrab = false;
            rg2d.gravityScale = 1;
        }
        onWallClimb = false;
        stopVelocity = false;
        stopLocalScale = false;
    }

    private void OnGrab() {
        if (onWallClimb)
            return;
        if (collisions.CanGrab() && collisions.OnHandWall() && !collisions.OnGround()) {
            canGrab = true;
        } else {
            canGrab = false;
        }
        if (!isGrab) {
            if (canGrab && hMove && moveDirection.x == facingDirection) {
                SetGrab(true);
            }
        } else {
            if (isGrab && !canGrab) {
                SetGrab(false);
                return;
            }
            if (hMove) {
                if (moveDirection.x == facingDirection) {
                    // Debug.Log("GRIND");
                }
                if (moveDirection.x != facingDirection) {
                    SetGrab(false);
                }
            }
            if (vMove) {
                if (moveDirection.y > 0) {
                    onWallClimb = true;
                    stopVelocity = true;
                    stopLocalScale = true;
                    // to Do penser à checker si le perso peut ou non passer!!!
                }
                if (moveDirection.y < 0) {
                    SetGrab(false);
                }
            }
        }
    }

    // call by animation
    public void TriggerClimbAnimationFinished() {
        SetGrab(false);
        if (facingDirection > 0) {
            playerTransform.position = new Vector3((int)playerTransform.position.x + 1.5f, (int)playerTransform.position.y + 3f);
        } else {
            // playerTransform.position = new Vector3((int)playerTransform.position.x - 0.5f, (int)playerTransform.position.y + 3f);
        }
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