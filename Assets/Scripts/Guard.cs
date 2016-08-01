using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof (NavMeshAgent))]
public class Guard : MonoBehaviour {

    public static bool caughtPlayer;

    private NavMeshAgent agent;
    public Text speechBubble;

    private GameObject playerObject;
    public Vector3 jailTarget;
    private float waitBeforePatrolResumes;


	// Use this for initialization
	void Start () {

        waitBeforePatrolResumes = 1.0f;

        agent = gameObject.GetComponent<NavMeshAgent>();
        

        playerObject = GameObject.FindGameObjectWithTag("PlayerGroup");

        caughtPlayer = false;
	}
	
	// Update is called once per frame
	void Update () {


        if(waitBeforePatrolResumes > 0)
        {
            waitBeforePatrolResumes = waitBeforePatrolResumes - Time.deltaTime;

        }

        if(waitBeforePatrolResumes < 1)
        {
            agent.Resume();
        }

        
	
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player" && PlayerController.playerCaught == false)
        {
            agent.Stop();
            PlayerController.playerCaught = true;
            speechBubble.text = AILines.caughtLines[Random.Range(0, AILines.caughtLines.Length)];
            print("You've been caught!");
            SendPlayerToJail();
            caughtPlayer = true;
            if(waitBeforePatrolResumes > 0 && waitBeforePatrolResumes < 1)
            {
                
                agent.Resume();
                print("Resuming after having caught Player!");
            }

        }
    }

    void SendPlayerToJail()
    {
        playerObject.transform.position = jailTarget;
        waitBeforePatrolResumes = 3.75f;

    }
    
}
