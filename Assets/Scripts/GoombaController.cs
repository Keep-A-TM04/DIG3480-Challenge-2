using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour {

    private Animator anim;

    public float speed;

    public LayerMask isGround;

    public Transform wallHitBox;

    private bool wallHit;

    public float wallHitHeight;
    public float wallHitWidth;

    public AudioClip stomp;
    private AudioSource source;

    void Start () {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    void FixedUpdate () {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        wallHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isGround);
        if(wallHit == true)
        {
            speed = speed * -1;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            source.PlayOneShot(stomp, 1F);
            Destroy(gameObject, 0.25f);
        }
    }
}
