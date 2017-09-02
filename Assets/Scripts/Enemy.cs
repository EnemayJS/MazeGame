using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

   // public Transform destination;
    public float walkSpeed;

   

    Vector2 direction = Vector2.zero;
    int targetX ;
    int targetY ;

    int currentX = 1;
    int currentY = 1;

    // Use this for initialization
    void Start () {
        targetX = Mathf.FloorToInt(transform.position.x);
        targetY = Mathf.FloorToInt(transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.score < 20)
        {
            RandomMovement();
        }
        else
        {
            ChaseMovement();
        }
        
    }

    void RandomMovement()
    {
        bool targetReached = transform.position.x == targetX && transform.position.y == targetY;

        currentX = Mathf.FloorToInt(transform.position.x);
        currentY = Mathf.FloorToInt(transform.position.y);
        direction.x = Random.Range(-1, 2);
       
        direction.y = Random.Range(-1, 2);
       
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

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, targetY), walkSpeed * Time.deltaTime);
    }

    void ChaseMovement()
    {
        bool targetReached = transform.position.x == targetX && transform.position.y == targetY;

        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        currentX = Mathf.FloorToInt(transform.position.x);
        currentY = Mathf.FloorToInt(transform.position.y);

        if (Vector3.Distance(new Vector3 (transform.position.x+1,transform.position.y), playerPosition)<
            Vector3.Distance(transform.position, playerPosition))
        {

            if (BoardManager.instance.GetMazeGridCell(currentX + 1, currentY) && targetReached)
            {
                targetX = currentX + 1;
                targetY = currentY;
            }
            
            
        }
         if (Vector3.Distance(new Vector3(transform.position.x - 1, transform.position.y), playerPosition) <
            Vector3.Distance(transform.position, playerPosition))
        {

            if (BoardManager.instance.GetMazeGridCell(currentX - 1, currentY) && targetReached)
            {
                targetX = currentX - 1;
                targetY = currentY;
            }
        }
        if ((Vector3.Distance(new Vector3(transform.position.x, transform.position.y + 1), playerPosition) <
            Vector3.Distance(transform.position, playerPosition)) || (Vector3.Distance(new Vector3(transform.position.x, transform.position.y + 2), playerPosition) <
            Vector3.Distance(transform.position, playerPosition)))
        {

            if (BoardManager.instance.GetMazeGridCell(currentX, currentY + 1) && targetReached)
            {
                targetX = currentX;
                targetY = currentY + 1;
            }
        }
        if ((Vector3.Distance(new Vector3(transform.position.x, transform.position.y - 1), playerPosition) <
            Vector3.Distance(transform.position, playerPosition))|| (Vector3.Distance(new Vector3(transform.position.x, transform.position.y - 2), playerPosition) <
            Vector3.Distance(transform.position, playerPosition)))
        {

            if (BoardManager.instance.GetMazeGridCell(currentX, currentY - 1) && targetReached)
            {
                targetX = currentX;
                targetY = currentY - 1;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, targetY), walkSpeed * Time.deltaTime);
    }

    
    
}



