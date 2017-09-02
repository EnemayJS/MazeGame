using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


    public int playerScore;
    public static GameManager instance = null;
    public int gameTime;
    public string playerName;


    private Text nameInput;
    private Text gameOverText;
    private GameObject gameOverBackground;
    private BoardManager boardScript;
    private GameObject mainMenu;



    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);


        //Get a component reference to the attached BoardManager script
       
        boardScript = GetComponent<BoardManager>();
        mainMenu = GameObject.Find("MainMenu");
        mainMenu.SetActive(false);

        gameOverBackground = GameObject.Find("GameOverBackground");
        gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();
        gameOverBackground.SetActive(false);

        //Call the InitGame function to initialize the first level 
        InitGame();
    }

   

    //Initializes the game for each level.
    void InitGame()
    {
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        boardScript.SetupScene();
        
    }

    public void GameOver()
    {
        gameTime = GetComponent<Timer>().totalTime;
        gameOverText.text = "Your result :" + playerScore + " coins in " + gameTime + " seconds!";
        gameOverBackground.SetActive(true);
        BoardManager.instance.enabled = false;
        enabled = false;
        
    }



}
