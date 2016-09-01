using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AILines : MonoBehaviour {

    public string[] lvl1GuardLines = new string[9];
    public string[] spottedLines = new string[4];
    public string[] caughtLines = new string[6];
    public Vector2 secondsBetweenTalkingMinMax;

    float nextTalkTime;
    float nextChaseTalkTime;
    

    public Text speech;
    private float textTimer;

	// Use this for initialization
	void Start () {

        lvl1GuardLines[0] = "Cold out tonight";
        lvl1GuardLines[1] = "Tonight is the night!";
        lvl1GuardLines[2] = "Come out Fisher, wherever you are!";
        lvl1GuardLines[3] = "Turtles all the way down!";
        lvl1GuardLines[4] = "Never doubt it.";
        lvl1GuardLines[5] = "Looking forward to supper";
        lvl1GuardLines[6] = "No one is getting past me.";
        lvl1GuardLines[7] = "Don't forget to change this voice line!";
        lvl1GuardLines[8] = "I miss my wife.";
        lvl1GuardLines[9] = "Gotta remember to pick up my lottery winnings";

        spottedLines[0] = "I see you!";
        spottedLines[1] = "Hey, stop right there!";
        spottedLines[2] = "You're not supposed to be here!";
        spottedLines[3] = "Stop immediately!";
        spottedLines[4] = "Who are you? Come here!";

        caughtLines[0] = "Got you now!";
        caughtLines[1] = "You're not getting away this time!";
        caughtLines[2] = "Thought we wouldn't find you, huh?";
        caughtLines[3] = "Gotcha!";
        caughtLines[4] = "Not this time, thief!";
        caughtLines[5] = "You cannot fool me!";
        caughtLines[6] = "Nothing gets past me!";


        nextTalkTime = Random.Range(secondsBetweenTalkingMinMax.x, secondsBetweenTalkingMinMax.y);
        nextChaseTalkTime = 4.0F;
       

    }
	
	// Update is called once per frame
	void Update () {

        if (Time.time > nextTalkTime)
        {
            float secondsBetweenTalk = Random.Range(secondsBetweenTalkingMinMax.x, secondsBetweenTalkingMinMax.y);
            nextTalkTime = Time.time + secondsBetweenTalk;

            

            speech.text = lvl1GuardLines[Random.Range(0, lvl1GuardLines.Length)];

            if(speech.text.Length <= 15 && speech.text.Length > 5)
            {
                textTimer = Random.Range(5, 6);
            }
            else
            {
                textTimer = Random.Range(7, 9);
            }

            
               
        }

        if(gameObject.GetComponent<FieldOfView>().isChasing == true && Time.time > nextChaseTalkTime)
        {
            
            nextChaseTalkTime = Time.time + nextChaseTalkTime;

            speech.text = spottedLines[Random.Range(0, spottedLines.Length)];

        }

       



        if (textTimer > 0)
        {
            textTimer = textTimer - Time.deltaTime;
        }

        if (textTimer < 0)
        {
            speech.text = "  ";
        }

	}
}
