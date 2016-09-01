using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof (NavMeshAgent))]
public class Guard : MonoBehaviour {

    public static bool caughtPlayer;

    private NavMeshAgent agent;
    

    private GameObject playerObject;
    public Transform jailTarget;
    public float waitBeforePatrolResumes;

    public int guardState;


	// Use this for initialization
	void Start () {

        waitBeforePatrolResumes = 1.0f;

        agent = gameObject.GetComponent<NavMeshAgent>();
        

        playerObject = GameObject.FindGameObjectWithTag("Player");

        caughtPlayer = false;

        guardState = 1;
	}
	
	// Update is called once per frame
	void Update () {

        switch (guardState)
        {
            //Patrolling state
            case 1:
                Patrolling();
                break;

            //Patrolling while the alarm is going off-state
            case 2:
                PatrollingWhileAlarm();
                break;

            //Chasing the Player
            case 3:
                ChasingPlayer();
                break;

            //Moving to target area 
            case 4:
                MovingToTargetArea();
                break;

            //Searching target area
            case 5:
                SearchingTheArea();
                break;

            //Default state
            default:
                break;
        }

        

        if (waitBeforePatrolResumes > 0)
        {
            waitBeforePatrolResumes = waitBeforePatrolResumes - Time.deltaTime;

        }

        //if(waitBeforePatrolResumes < 1)
        //{
        //    agent.Resume();
        //}

        if (waitBeforePatrolResumes < 1 && waitBeforePatrolResumes > 0 && caughtPlayer == true)
        {
            SendPlayerToJail();
            
            agent.speed = 3.5f;
            gameObject.GetComponent<GuardPatrol>().GotoNextPoint();
            caughtPlayer = false;                       
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player" && PlayerController.playerCaught == false)
        {
            agent.Stop();
            agent.ResetPath();
            
            caughtPlayer = true;
            PlayerController.playerCaught = true;

            gameObject.GetComponent<AILines>().speech.text = gameObject.GetComponent<AILines>().caughtLines[Random.Range(0,gameObject.GetComponent<AILines>().caughtLines.Length)];

            //FieldofView Variables:
            gameObject.GetComponent<FieldOfView>().isChasing = false;
            FieldOfView.isSpotting = false;

            waitBeforePatrolResumes = 3.75f;

            //gameObject.GetComponent<AILines>().speech.text = "";
            
            //SendPlayerToJail();
            

            

            

        }
    }

    void SendPlayerToJail()
    {
        playerObject.transform.position = jailTarget.transform.position;
        //waitBeforePatrolResumes = 3.75f;

        //agent.ResetPath();
        //gameObject.GetComponent<GuardPatrol>().GotoNextPoint();
        
        caughtPlayer = false;

    }
    

    void Patrolling()
    {
        print("State change patrolling");
    }


    void PatrollingWhileAlarm()
    {
        print("State Change Patrolling with Alarm");
    }


    void ChasingPlayer()
    {
        print("I'm chasing now!");
    }

    
    void MovingToTargetArea()
    {

    }


    void SearchingTheArea()
    {

    }
}
