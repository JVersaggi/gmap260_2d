using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Score : MonoBehaviour
{

    private float timeLeft = 120;
    public int playerScore = 0;
    public GameObject timeLeftUI;
    public GameObject playerScoreUI;

    void Start ()
    {
        //just for testing
        DataManagement.datamanagement.LoadData();
    }

    //on every frame it will count down the time, and update and display the time and score
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timeLeftUI.gameObject.GetComponent<Text>().text = ("Time Left: " + (int)timeLeft);
        playerScoreUI.gameObject.GetComponent<Text>().text = ("Score: " + (int)playerScore);

        if (timeLeft < 0.1f)
        {
            SceneManager.LoadScene("prototype_1");
        }
    }

    //when the player runs into the end of the level, it will call the routine to count the score
    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.name == "EndLevel")
            {
            CountScore();
            DataManagement.datamanagement.SaveData ();
        }
        if (trig.gameObject.name == "Coin")
        {
            playerScore += 10;
            Destroy(trig.gameObject);
        }
    }

    //counts the score and adds the time bonus
    void CountScore ()
    {
        Debug.Log("Data says high score is currently " + DataManagement.datamanagement.highScore);
        playerScore = playerScore + (int)(timeLeft * 10);
        DataManagement.datamanagement.highScore = playerScore + (int)(timeLeft);
        DataManagement.datamanagement.SaveData();
        Debug.Log("Now that we have saved, data says high score is currently " + DataManagement.datamanagement.highScore);
    }
}
