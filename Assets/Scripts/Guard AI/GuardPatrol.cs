using UnityEngine;
using System.Collections;

public class GuardPatrol : MonoBehaviour {

    public GameObject[] points;
    private int destpoint = 0;
    private NavMeshAgent agent;
    private float nodeStopTime;
    public GameObject currentNode;
    private Transform nodeLookPoint;


	// Use this for initialization
	void Start () {

        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;

        GotoNextPoint();

       
	}
	
	// Update is called once per frame
	void Update () {
        //Choose the next destination point when the agent gets close to the current destination


        if (agent.remainingDistance < 0.125f)
        {

            
            

            //var patrolObject = currentNode.GetComponent<PatrolNodes>();
                   
            if(nodeLookPoint != null)
            {
                gameObject.transform.LookAt(nodeLookPoint);
            }

            if (nodeStopTime > 0)
            {
                agent.ResetPath();               
                
                nodeStopTime = nodeStopTime - Time.deltaTime;
            }
            if (nodeStopTime <= 0)
            {
                agent.Resume();
                GotoNextPoint();
            }
                        
        }




	
	}

    public void GotoNextPoint()
    {
        //returns if no points have been set up
        if (points.Length == 0)
            return;

        //Set agent to go to current point
        agent.destination = points[destpoint].transform.position;

        //Choose the next point in the array as destination
        //Cycle if needed
        destpoint = (destpoint + 1) % points.Length;

        //Get the current node
        currentNode = points[destpoint -1];

        nodeStopTime = currentNode.GetComponent<PatrolNodes>().waitTime;
        nodeLookPoint = currentNode.GetComponent<PatrolNodes>().lookPoint;


        





    }
}
