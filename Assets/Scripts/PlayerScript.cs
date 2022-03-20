using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text health;
   public GameObject winTextObject;
   public GameObject loseTextObject;
    private int scoreValue = 0;
    private int healthValue = 3;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;
    public AudioSource musicSource;
    
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        health.text = "Health: " + healthValue.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.collider.tag == "Coin")
        {
            Destroy(collision.collider.gameObject);
            scoreValue +=1;
            score.text = "Score: "+ scoreValue.ToString();
            if (scoreValue >= 8)
        {
            winTextObject.SetActive(true);
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
        if (scoreValue == 4)
        {
            transform.position = new Vector3(50.0f, 0.0f, 0.0f);
            healthValue = 3;
            health.text = "Health: "+ healthValue.ToString();
        }
        }
        else if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            healthValue -=1;
            health.text = "Health: " + healthValue.ToString();
            if (healthValue == 0)
            {
                loseTextObject.SetActive(true);
                 musicSource.clip = musicClipThree;
                musicSource.Play();
                Destroy(gameObject);

            }
        }

    }

    private void OnCollisionStay2D(Collision2D collision) 
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W)){
                rd2d.AddForce(new Vector2(0,3), ForceMode2D.Impulse);
            }

        }
    }
}
