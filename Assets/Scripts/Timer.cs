using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private Text timerText;
    private float startTime;
    public int totalTime = 0;
    

    void Awake() {
        
        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        
    }

	// Use this for initialization
	void Start () {
        
        startTime = Time.time;
        timerText.text = "Time: " + totalTime.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        float t = Time.time - startTime;
        
        totalTime = (int)t%60;
        
        timerText.text = "Time: " + totalTime.ToString();
	}
}
