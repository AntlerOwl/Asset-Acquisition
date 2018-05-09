using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraPatrol : MonoBehaviour {

    //patrol related
    public GameObject currentNode;
    public float waitTimer;

    //References
    private CameraZone cz;
    private NavMeshAgent agent;

	// Use this for initialization
	void Start () {

        agent = gameObject.GetComponent<NavMeshAgent>();
        cz = gameObject.GetComponent<CameraZone>();

        MoveToNextNode();
		
	}
	
	// Update is called once per frame
	void Update () {

        if(agent.remainingDistance <= 0.5f)
        {
         
            if(waitTimer > 0)
            {
                
                waitTimer = waitTimer - Time.deltaTime;
                agent.isStopped = true;

            }
            else
            {
                currentNode = currentNode.GetComponent<PatrolNode>().nextNode[Random.Range(0, currentNode.GetComponent<PatrolNode>().nextNode.Length)];

                MoveToNextNode();
            }

        }
		
	}

    void MoveToNextNode()
    {
        agent.isStopped = false;
        agent.SetDestination(currentNode.transform.position);
        waitTimer = currentNode.GetComponent<PatrolNode>().waitTime;


    }
}
