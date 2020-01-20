using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

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

    public void InitControls() {
        InputManager.controls.Player.Move.performed += ctx => this.SetGetAxis(ctx.ReadValue<float>());
        InputManager.controls.Player.Jump.performed += ctx => this.Jump();
    }

    void Start() {
        //toolbar = GameObject.FindGameObjectWithTag("InventoryToolbar").GetComponent<InventoryToolbar>();
        rg2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Pickable")) {
            Item itemToAdd = collision.GetComponent<Item>();

            if(InventoryManager.instance.AddItem(itemToAdd)) {
                itemToAdd.Destroy();
            }
        }
    }

    private void SetGetAxis(float value) {
        this.getAxis = value;
    }

    private void Jump() {
        rg2d.AddForce(Vector2.up * jumpForces);
    }

    void Update() {
        /*float lerp = Mathf.PingPong(Time.time, duration) / duration;
        RenderSettings.skybox.SetColor("_Tint", Color.Lerp(colorStart, colorEnd, lerp));*/

        this.DetectPickableItemsInArea();


        if(getAxis < -0.1f) { 
            transform.localScale = new Vector3(-1, 1, 1);
            // this.transform.position = new Vector2(this.transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, this.transform.position.y);
        }

        if(getAxis > 0.1f) {
            transform.localScale = new Vector3(1, 1, 1);
            // this.transform.position = new Vector2(this.transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, this.transform.position.y);
        }

        Vector3 targetVelocity = new Vector2(getAxis * 10f, rg2d.velocity.y);
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
