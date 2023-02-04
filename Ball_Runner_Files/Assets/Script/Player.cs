using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private bool isGrounded = true;
    [SerializeField]
    private float speed = 12f;
    [SerializeField]
    private float Jumpspeed = 8f;
    private float x;
    private float z;
    public Joystick joystick;
    [SerializeField] private AudioSource PlayerDiedSoundEffect;
    [SerializeField] private AudioSource CollectionSoundEffect;
    [SerializeField] private Text ScoreText;
    private int Coins = 0;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Inputs();
    }

    void FixedUpdate()
    {
        Move();
    
        if(rb.position.y < -1f)
        {
           PlayerManager.gameOver = true;
        }
    }
    
    private void Inputs()
    {
        x = joystick.Horizontal;
        z = joystick.Vertical;
    }

    private void Move()
    {
        rb.AddForce(new Vector3(x,0,z) * speed);
    }
    
   public void Jump()
   {
     if (isGrounded)
        {
            rb.AddForce(Vector3.up * Jumpspeed, ForceMode.Impulse);
            isGrounded = false;
        }
   }

    void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.tag == "Ground")
            {
                isGrounded = true;
            }

            if(other.gameObject.tag == "Obstacle")
            {
                PlayerManager.gameOver = true;
                PlayerDiedSoundEffect.Play();
            }
        }

        void OnTriggerEnter(Collider collision) 
    {
        if(collision.tag == "Coins")
        {
            CollectionSoundEffect.Play();
            Coins++;
            ScoreText.text = "Score: " + Coins;
            Destroy(collision.gameObject);
        }    
    }
}
