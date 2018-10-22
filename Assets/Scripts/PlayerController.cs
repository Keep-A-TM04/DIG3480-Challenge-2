using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;
    private bool facingRight = true;
    private int count;
    public Text countText;

    public float speed;
    public float jumpForce;

    private bool grounded;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    private Animator marioAnim;

    public GameObject shroom;
    private bool shroomActive;

    public AudioClip jump;
    public AudioClip coin;
    public AudioClip stomp;
    public AudioClip powerUp;
    public AudioClip clear;
    private AudioSource source;

	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        marioAnim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

        count = 0;
        SetCountText();
        shroomActive = false;
    }

    void Awake()
    {
        //source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            marioAnim.SetBool("Walking", true);
        }
        else
        {
            marioAnim.SetBool("Walking", false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            marioAnim.SetTrigger("Jump");
        }

        if (shroomActive)
        {
            shroom.SetActive(true);
        }
        else
        {
            shroom.SetActive(false);
        }
    }

    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Vector2 movement = new Vector2(moveHorizontal, 0);
        //rb2d.AddForce(movement * speed);
        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
        grounded = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        Debug.Log(grounded);

        if(facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if(facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void SetCountText()
    {
        countText.text = "Coins: " + count.ToString();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && grounded)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                rb2d.velocity = Vector2.up * jumpForce;
                source.PlayOneShot(jump, 1F);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            count++;
            source.PlayOneShot(coin, 1F);
            SetCountText();
        }

        if (other.gameObject.CompareTag("CoinBox"))
        {
            other.gameObject.SetActive(false);
            count++;
            source.PlayOneShot(coin, 1F);
            SetCountText();
        }

        if (other.gameObject.CompareTag("MushroomBox"))
        {
            other.gameObject.SetActive(false);
            shroomActive = true;
        }

        if (other.gameObject.CompareTag("Mushroom"))
        {
            shroomActive = false;
            source.PlayOneShot(powerUp, 1F);
        }

        if (other.gameObject.CompareTag("Goal"))
        {
            source.PlayOneShot(clear, 1F);
        }
    }

}
