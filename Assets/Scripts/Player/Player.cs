using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    private float speed = 10f;
    [SerializeField]
    private readonly float jumpForces = 200f;
    private Vector3 m_Velocity = Vector3.zero;
    // [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private float m_MovementSmoothing = .05f;
    //private static int maxHealth = 75;

    // References
    public Rigidbody2D rg2d;
    private Animator anim;

    private float getAxis;

    /*public Color colorStart = Color.black;
    public Color colorEnd = Color.white;
    public float duration = 1.0F;*/

    public static Player instance;

    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    void Start() {
        //toolbar = GameObject.FindGameObjectWithTag("InventoryToolbar").GetComponent<InventoryToolbar>();
        rg2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        InputManager.gameplayControls.Player.Move.performed += SetGetAxis;
        InputManager.gameplayControls.Player.Jump.performed += Jump;
    }

    private void OnDestroy() {
        InputManager.gameplayControls.Player.Move.performed -= SetGetAxis;
        InputManager.gameplayControls.Player.Jump.performed -= Jump;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Pickable")) {
            Item itemToAdd = collision.GetComponent<Item>();

            if(InventoryManager.instance.AddItem(itemToAdd)) {
                itemToAdd.Destroy();
            }
        }
    }

    private void SetGetAxis(InputAction.CallbackContext ctx) {
        this.getAxis = ctx.ReadValue<float>();
    }

    private void Jump(InputAction.CallbackContext ctx) {
        rg2d.AddForce(Vector2.up * jumpForces);
    }

    void Update() {
        /*float lerp = Mathf.PingPong(Time.time, duration) / duration;
        RenderSettings.skybox.SetColor("_Tint", Color.Lerp(colorStart, colorEnd, lerp));*/

        this.DetectPickableItemsInArea();

        int direction = 0;

        if(getAxis < -0.1f) { 
            transform.localScale = new Vector3(-1, 1, 1);
            direction = -1;
        }

        if(getAxis > 0.1f) {
            transform.localScale = new Vector3(1, 1, 1);
            direction = 1;
        }



        Vector3 targetVelocity = new Vector2(direction * 10f, rg2d.velocity.y);
        // And then smoothing it out and applying it to the character
        rg2d.velocity = Vector3.SmoothDamp(rg2d.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        // limit speed of player
        if(rg2d.velocity.x > speed) {
            rg2d.velocity = new Vector2(speed, rg2d.velocity.y);
        }

        if(rg2d.velocity.x < -speed) {
            rg2d.velocity = new Vector2(-speed, rg2d.velocity.y);
        }

    }

    private void DetectPickableItemsInArea() {
        Collider2D[] itemsCollider = Physics2D.OverlapCircleAll(this.transform.position, 1f, (1 << 14));

        for(int i = 0; i < itemsCollider.Length; i++) {
            if(itemsCollider[i].CompareTag("Pickable")) {
                bool canAddItem = InventoryManager.instance.CanAddItem(itemsCollider[i].GetComponent<Item>());
                Attractor attractor = itemsCollider[i].GetComponent<Attractor>();

                if(canAddItem) {
                    attractor.Setup(this.transform, 5f);
                } else {
                    attractor.Reset();
                }

            }
        }
    }

}
