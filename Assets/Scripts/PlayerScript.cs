using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
private Rigidbody2D rd2d;
public float speed; 
public Text scoreText;
private int scoreValue;
public GameObject winTextObject;
public GameObject loseTextObject;
private int lives;
public Text livesText;
private bool levelTwo;
private bool resetLives;
public AudioSource musicSource;
public AudioClip musicClip1;
Animator anim;
public bool facingRight;
public float horizontalValue;
private bool isOnGround;
public Transform groundcheck;
public float checkRadius;
public LayerMask allGround;
private bool playerDead;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        scoreValue = 0;
        scoreText.text = "Score: " + scoreValue.ToString ();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        lives = 3;
        levelTwo = false;
         musicSource.clip = musicClip1;
        musicSource.Play();
        anim = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        { 
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
        }  
        { 
         isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
    }
    }   
    
    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
    {
       scoreValue += 1; 
       scoreText.text = "Score: " + scoreValue.ToString();
        Destroy(collision.collider.gameObject);
    }
      else if(collision.collider.tag == "Enemy")
    {  
      lives -= 1; 
      livesText.text = lives.ToString();
      Destroy(collision.collider.gameObject);
      }
      
    }
    
void Update()
{
    if (scoreValue >= 8)
    { 
    winTextObject.SetActive(true); 
}
{ 
    livesText.text = "Lives: " + lives.ToString();
}
if (lives <= 0)
{ 
    loseTextObject.SetActive(true);
}
if (scoreValue >= 8)
        {
         musicSource.clip = musicClip1;
          musicSource.Stop();

         }
         if (Input.GetKeyDown(KeyCode.RightArrow))
{
anim.SetInteger("State", 5);
}
         if (Input.GetKeyDown(KeyCode.LeftArrow))
{
anim.SetInteger("State", 5);
} 
if (Input.GetKeyUp(KeyCode.RightArrow))
{
anim.SetInteger("State", 0);
}
         if (Input.GetKeyUp(KeyCode.LeftArrow))
{
anim.SetInteger("State", 0);
} 
if (isOnGround == true)
{
    anim.SetBool("air", false);
}
else {
    anim.SetBool("air", true);
}
 {
        move();
        properFlip();
    }
}
    private void OnCollisionStay2D(Collision2D collision)
{
if (collision.collider.tag == "Ground" && isOnGround)
{
if (Input.GetKey(KeyCode.UpArrow))
{
rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
}
}
if (Input.GetKey("escape"))
{
Application.Quit();
}
      if (scoreValue == 4 && !levelTwo)
      { 
      levelTwo = true;
      transform.position = new Vector2(64.0f, -1.0f);
      }
      if (scoreValue == 4 && !resetLives)
    {
        resetLives = true;
lives = 3;
    }
    if (lives == 0 && !playerDead)
        { playerDead = true;
gameObject.SetActive(false); 

        }
    
}
    void move()
    {
        horizontalValue = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        //transform.Translate(new Vector3()
    }
 
    void properFlip()
    {
        if((horizontalValue < 0 && facingRight) || (horizontalValue > 0 && !facingRight))
        {
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}

