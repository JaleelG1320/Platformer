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
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        health.text = "Health: " + healthValue.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
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
void SetHealhtText(){
    health.text = "Health: " + healthValue.ToString();
}
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.collider.tag == "Coin")
        {
            Destroy(collision.collider.gameObject);
            scoreValue +=1;
            score.text = "Score: "+ scoreValue.ToString();
            if (scoreValue >= 4)
        {
            winTextObject.SetActive(true);
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
            if (scoreValue >= 4)
        {
            winTextObject.SetActive(true);
            Destroy(gameObject);
        }
        }
    }
}
