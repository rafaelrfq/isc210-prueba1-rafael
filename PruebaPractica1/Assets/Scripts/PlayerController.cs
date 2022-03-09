using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpSpeed = 15f;

    private Rigidbody2D rigidbody2D;
    private Animator anim;
    private bool ground;
    private Vector3 deltaPos = new Vector3();
    private Vector3 currentSpeed = new Vector3();

    private Vector2 movementDirection;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ground = true;
    }


    void Update()
    {
        HandleLookingDirection();
        Move();
        if (Input.GetKey(KeyCode.Space) && ground)
            Jump();
        if(!ground)
        {
            // Yf = Y0 + V0y * T + (aT2)/2
            deltaPos.y = currentSpeed.y * Time.deltaTime + Physics.gravity.y * Mathf.Pow(Time.deltaTime, 2) / 2;
            gameObject.transform.Translate(deltaPos);
            currentSpeed += Physics.gravity * Time.deltaTime;
        }
        anim.SetBool("Ground", ground);
    }

    private void HandleLookingDirection()
    {
            if(Input.GetAxis("Horizontal") > 0.01f)
            {
                this.transform.localScale = new Vector3(2, 2, 2);
            } 
            else if (Input.GetAxis("Horizontal") < -0.01f)
            {
                this.transform.localScale = new Vector3(-2, 2, 2);
            }
    }
    private void Jump()
    {
        anim.SetTrigger("Jump");

        // caida libre
        deltaPos = new Vector3();
        currentSpeed = new Vector3();

        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed);
        ground = false;
    }

    private void Move()
    {
        anim.SetBool("Walk", Input.GetAxis("Horizontal") != 0);
        rigidbody2D.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rigidbody2D.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            print("Piso");
            ground = true;
        }

        if(collision.gameObject.tag == "Moneda")
        {
            GameManager.monedas += 1;
            Debug.Log("Cantidad Monedas: " + GameManager.monedas);
            Destroy(collision.gameObject);
        }
    }

    public bool canAttack()
    {
        return Input.GetAxis("Horizontal") == 0 && ground;
    }

}
