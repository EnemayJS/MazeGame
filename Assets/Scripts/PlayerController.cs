using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {

    public float speed;
    public static int score = 0;
    private Text scoreText;
    
    bool facingRight = true;
    Vector2 direction = Vector2.zero;

    int targetX = 1;
    int targetY = 1;

    int currentX = 1;
    int currentY = 1;

    // Use this for initialization
    void Start () {
       
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.text = "Score: 0";
    }

    void Update()
    {
        bool targetReached = transform.position.x == targetX && transform.position.y == targetY;

        currentX = Mathf.FloorToInt(transform.position.x);
        currentY = Mathf.FloorToInt(transform.position.y);
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        
        if (direction.x > 0)
        {



            if (BoardManager.instance.GetMazeGridCell(currentX + 1, currentY) && targetReached)
            {
                targetX = currentX + 1;
                targetY = currentY;
            }
        }
        else if (direction.x < 0)
        {
           

            if (BoardManager.instance.GetMazeGridCell(currentX - 1, currentY) && targetReached)
            {
                targetX = currentX - 1;
                targetY = currentY;
                
            }
        }
        else if (direction.y > 0)
        {
           
            if (BoardManager.instance.GetMazeGridCell(currentX, currentY + 1) && targetReached)
            {
                targetX = currentX;
                targetY = currentY + 1;
               
            }
        }
        else if (direction.y < 0)
        {
            

            if (BoardManager.instance.GetMazeGridCell(currentX, currentY - 1) && targetReached)
            {
                targetX = currentX;
                targetY = currentY - 1;
                
            }
        }


        if (direction.x > 0 && !facingRight)
        {
            Flip();
           
        }
        else if (direction.x < 0 && facingRight)
        {
            Flip();
           
        }

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, targetY), speed * Time.deltaTime);

       
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Coin")
        {
            score++;
            Destroy(other.gameObject);
            BoardManager.instance.GenerateCoin();
            scoreText.text = "Score: " + score.ToString();
            CheckScore();
        }
        if (other.tag == "Zombie")
        {
            GameManager.instance.playerScore = score;
            GameManager.instance.GameOver();
            PlayerController.Destroy(this);
        }
        if (other.tag == "Mummy")
        {
            GameManager.instance.playerScore = 0;
            GameManager.instance.GameOver();
            PlayerController.Destroy(this);
        }


    }

  

    private void CheckScore()
    {
        if (score == 5)
        {

            BoardManager.instance.GenerateZombie();
        }
        else if (score == 10)
        {

            BoardManager.instance.GenerateMummy();
        }
        else if (score > 20) {
            
            Enemy[] enemies = FindObjectsOfType <Enemy>();
            foreach (Enemy enemy in enemies)
            {
                enemy.GetComponent<Enemy>().walkSpeed += (enemy.GetComponent<Enemy>().walkSpeed * 5 / 100);
               
            }
              
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}

