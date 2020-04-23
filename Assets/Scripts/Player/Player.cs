using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    public static Player instance;
    private Rigidbody2D rg2d;
    private Animator animator;

    private float speed = 10f;
    private Vector3 m_Velocity = Vector3.zero;
    private float m_MovementSmoothing = .05f;
    //private static int maxHealth = 75;

    private float getAxis;
    private Vector3 localScale;
    private bool onGround;
    private bool jump = false;
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private float currentPosY;
    [SerializeField] private LayerMask m_WhatIsGround;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        // currentPosY = transform.position.y;
        //toolbar = GameObject.FindGameObjectWithTag("InventoryToolbar").GetComponent<InventoryToolbar>();
        rg2d = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        localScale = gameObject.GetComponent<Transform>().localScale;
        InputManager.gameplayControls.Player.Move.performed += SetGetAxis;
        InputManager.gameplayControls.Player.Jump.performed += Jump;
    }

    private void OnDestroy() {
        InputManager.gameplayControls.Player.Move.performed -= SetGetAxis;
        InputManager.gameplayControls.Player.Jump.performed -= Jump;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Pickable")) {
            Item itemToAdd = collision.GetComponent<Item>();
            if (InventoryManager.instance.AddItem(itemToAdd)) {
                itemToAdd.Destroy();
            }
        }
    }

    private void SetGetAxis(InputAction.CallbackContext ctx) {
        this.getAxis = ctx.ReadValue<float>();
    }

    private void Jump(InputAction.CallbackContext ctx) {
        if (onGround) {
            jump = true;
            rg2d.velocity = new Vector2(rg2d.velocity.x, 7);
            // animator.SetTrigger("Jump");
            animator.SetBool("Jump", true);
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
        // currentPosY = transform.position.y;
        onGround = false;
        animator.SetBool("OnGround", onGround);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject != gameObject) {
                onGround = true;
                animator.SetBool("OnGround", true);
                jump = false;
            }
        }
        /*if (jump && onGround) {
            animator.SetBool("Jump", false);
            jump = false;
        }*/
    }

    void Update() {
        onGround = false;
        // calculer sur la pos Y ?
        if (rg2d.velocity.y > 0 && rg2d.velocity.y < 3) {
            Debug.Log(rg2d.velocity.y);
            animator.SetBool("Jump", false);
        }
        DetectPickableItemsInArea();

        int direction = 0;

        if (getAxis < -0.1f) {
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
            direction = -1;
        }

        if (getAxis > 0.1f) {
            transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
            direction = 1;
        }
        Vector3 targetVelocity = new Vector2(direction * 10f, rg2d.velocity.y);
        // And then smoothing it out and applying it to the character
        animator.SetFloat("Speed", Mathf.Abs(direction * 20f));
        rg2d.velocity = Vector3.SmoothDamp(rg2d.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        // limit speed of player
        if (rg2d.velocity.x > speed) {
            rg2d.velocity = new Vector2(speed, rg2d.velocity.y);
        }

        if (rg2d.velocity.x < -speed) {
            rg2d.velocity = new Vector2(-speed, rg2d.velocity.y);
        }

    }

    private void DetectPickableItemsInArea() {
        Collider2D[] itemsCollider = Physics2D.OverlapCircleAll(this.transform.position, 1f, (1 << 14));

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
