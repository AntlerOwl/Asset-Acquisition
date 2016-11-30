using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int currentScore;
    public Text[] scoreBoard;
    


	// Use this for initialization
	void Start () {
        currentScore = 0;
	}
	
	// Update is called once per frame
	void Update () {

        scoreBoard[0].text = currentScore.ToString();
        scoreBoard[1].text = currentScore.ToString();

    }
}
