using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 10f;
    [SerializeField]
    private readonly float jumpForces = 200f;
    private InventoryToolbar toolbar;
    private Vector3 m_Velocity = Vector3.zero;
    // [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private float m_MovementSmoothing = .05f;
    //private static int maxHealth = 75;

    // References
    public Rigidbody2D rg2d;
    private Animator anim;

    /*public Color colorStart = Color.black;
    public Color colorEnd = Color.white;
    public float duration = 1.0F;*/

    void Start()
    {
        //toolbar = GameObject.FindGameObjectWithTag("InventoryToolbar").GetComponent<InventoryToolbar>();
        rg2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Pickable"))
        {
            Item itemToAdd = collision.collider.GetComponent<Item>();
            if (InventoryManager.instance.AddItem(itemToAdd))
            {
                itemToAdd.Destroy();
            }
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
    }

    void Update()
    {
        /*float lerp = Mathf.PingPong(Time.time, duration) / duration;
        RenderSettings.skybox.SetColor("_Tint", Color.Lerp(colorStart, colorEnd, lerp));*/

        // It's just an example of item manager to create a dirt block at specific position
        if (Input.GetKeyDown(KeyCode.T))
        {
            ItemManager.instance.CreateItem(4, ItemStatus.ACTIVE, this.transform.position + this.transform.TransformDirection(new Vector3(1, 1, 0)));
        }


        var getAxis = Input.GetAxis("Horizontal");

        if (getAxis < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            // this.transform.position = new Vector2(this.transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, this.transform.position.y);
        }

        if (getAxis > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            // this.transform.position = new Vector2(this.transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, this.transform.position.y);
        }

        Vector3 targetVelocity = new Vector2(getAxis * 10f, rg2d.velocity.y);
        // And then smoothing it out and applying it to the character
        rg2d.velocity = Vector3.SmoothDamp(rg2d.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        if (Input.GetButtonDown("Jump"))
        {
            rg2d.AddForce(Vector2.up * jumpForces);
        }

        // limit speed of player
        if (rg2d.velocity.x > speed)
        {
            rg2d.velocity = new Vector2(speed, rg2d.velocity.y);
        }

        if (rg2d.velocity.x < -speed)
        {
            rg2d.velocity = new Vector2(-speed, rg2d.velocity.y);
        }

    }

}
