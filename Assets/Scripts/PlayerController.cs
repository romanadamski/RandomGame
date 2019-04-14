using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Animator anim;
    Rigidbody2D rb;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask WhatIsGround;
    public float jumpHeight;
    public float moveSpeed;
    public bool grounded;
    float jumpTimeCounter;
    public float jumpTime;
    bool jump;
    bool longJump;
    int jumpCounter = 0;

    Quaternion turnRight;
    Quaternion turnLeft;
    float playerHeight;
    float playerWidth;

    float bottomBorder;
    float topBorder;
    float leftBorder;
    float rightBorder;
    void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        jump = false;
        bottomBorder = -5.34f;
        topBorder = 5.34f;
        leftBorder = -9;
        rightBorder = 9;
        playerWidth = GetComponent<SpriteRenderer>().size.x * transform.localScale.x;
        playerHeight = GetComponent<SpriteRenderer>().size.y * transform.localScale.y;

        SetDirections();
    }
    void SetDirections()
    {
        turnRight = transform.rotation;
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y + 180, rot.z);
        turnLeft = Quaternion.Euler(rot);
    }
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        if (rb.velocity.magnitude < .01)
        {
            rb.velocity = Vector3.zero;
        }
        FixPosition();
    }
    // Update is called once per frame
    void Update() {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", grounded);
        if (grounded)
            jump = false;
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && grounded)
        {
            longJump = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(0, jumpHeight);
            jumpCounter++;
        }
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && longJump /*&& grounded*/)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(0, jumpHeight);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                longJump = false;
            }
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.rotation = turnLeft;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.rotation = turnRight;
        }
        if (!Input.anyKey)
            rb.velocity = new Vector2(rb.velocity.x * 0.95f, rb.velocity.y);
    }
    void FixPosition()
    {
        if (transform.position.y <= bottomBorder)
        {
            transform.position = new Vector3(transform.position.x, topBorder, transform.position.z);
            rb.velocity = new Vector2(0,1);
        }
        if (transform.position.x <= leftBorder)
        {
            transform.position = new Vector3(rightBorder, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= rightBorder)
        {
            transform.position = new Vector3(leftBorder, transform.position.y, transform.position.z);
        }
    }
}
