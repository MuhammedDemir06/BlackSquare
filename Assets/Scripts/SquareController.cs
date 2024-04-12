using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour
{ 
    //Events
    public delegate void SquareEvent();
    public static SquareEvent StepSpawnControl;
    public static SquareEvent ScoreUpdateControl;
    public static SquareEvent PlayerLoseControl;
    public GameManager ManagerGame;

    [Header("Control")]
    [Range(1, 10)] [SerializeField] private float speed;
    [Range(1,10)][SerializeField] private float jumpPower;
    private float inputX;
    private Rigidbody2D rb2D;
    private BoxCollider2D coll2D;
    public bool IsGrounded;
    public static bool IsDead;
    [SerializeField] private LayerMask groundLayer;
    private AudioSource jumpSound;

    private void Start()
    {
        coll2D = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        jumpSound = GetComponent<AudioSource>();
        jumpSound.Stop();
    }
    private void GetInput()
    {
        if(ManagerGame.EditorMode)
          inputX = Input.GetAxis("Horizontal");
    }
    private void Move()
    {
        //  rb2D.velocity = new Vector2(inputX * speed *100 * Time.deltaTime, rb2D.velocity.y);
        transform.position += new Vector3(inputX * speed * Time.deltaTime, 0, 0);

        var newScale = transform.localScale;
        if (inputX > 0)
            newScale.x = 1f;
        else if(inputX<0)
            newScale.x = -1f;
        transform.localScale = newScale;

        //Ground
        IsGrounded = Physics2D.BoxCast(coll2D.bounds.center, coll2D.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);

            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
    }
    private void Jump()
    {
        if(IsGrounded)
                rb2D.velocity = Vector3.up * jumpPower;
    }
    private void FixedUpdate()
    {
        GetInput();
        Move();
    }
    //Trigggers
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Step")
        {
            if (StepSpawnControl!=null)
                StepSpawnControl();
            jumpSound.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Step")
        {
            collision.GetComponent<BoxCollider2D>().isTrigger = false;
            if(ScoreUpdateControl != null)
                ScoreUpdateControl();
        }
        if(collision.gameObject.tag=="Lose")
        {
            if (PlayerLoseControl != null)
                PlayerLoseControl();
            IsDead = true;
            rb2D.simulated = false;
        }
    }
    //Buttons
    public void LeftAndRightButtons(float newInput)
    {
        inputX = newInput;
    }
    public void JumpButton()
    {
        Jump();
    }
}
