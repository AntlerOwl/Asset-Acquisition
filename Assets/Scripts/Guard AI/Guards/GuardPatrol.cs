﻿using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class GuardPatrol : MonoBehaviour {

    public GameObject[] points;
    private int destpoint = 0;
    private NavMeshAgent agent;
    public float nodeStopTime;

    public GameObject currentNode;
    private Transform nodeLookPoint;
    private Transform nodeLookPoint2;
    private string lookPointType;
    public bool hasLookedAtOneLookPoint;
    public bool beingSearchedFor;

    public float rotationSpeed;


	// Use this for initialization
	void Start () {

        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;
               
	}
	
	// Update is called once per frame
	void Update () {
        //Choose the next destination point when the agent gets close to the current destination


        if (agent.remainingDistance < 0.125f && beingSearchedFor != true && gameObject.GetComponent<Guard>().guardState == 1)
        {                     

            //var patrolObject = currentNode.GetComponent<PatrolNodes>();                   
            if(nodeLookPoint != null)
            {
                if(lookPointType == "single")
                {
                    LookAtLookPointSingle();
                }
                if(lookPointType == "double")
                {
                    if (hasLookedAtOneLookPoint == false)
                    {
                        LookAtLookPointDouble();
                    }
                    
                    else
                    {
                        LookAtLookPointDouble2();
                    }
                }
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


        hasLookedAtOneLookPoint = false;
        nodeStopTime = currentNode.GetComponent<PatrolNodes>().waitTime;
        nodeLookPoint = currentNode.GetComponent<PatrolNodes>().lookPoint;
        nodeLookPoint2 = currentNode.GetComponent<PatrolNodes>().lookPoint2;
        lookPointType = currentNode.GetComponent<PatrolNodes>().lookPointType;
    }

    public void LookAtLookPointSingle()
    {
        Vector3 direction = (nodeLookPoint.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    public void LookAtLookPointDouble()
    {
        Vector3 direction = (nodeLookPoint.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        if(Quaternion.Angle(transform.rotation, lookRotation) < 5)
        {
            hasLookedAtOneLookPoint = true;
        }
    }

    public void LookAtLookPointDouble2()
    {
        Vector3 direction = (nodeLookPoint2.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        if (Quaternion.Angle(transform.rotation, lookRotation) < 5)
        {

        }
    }

    public void GoBackOnPatrol()
    {
        //returns if no points have been set up
        if (points.Length == 0)
            return;

        //Set agent to go to current point
        agent.destination = points[destpoint].transform.position;

        //Choose the next point in the array as destination
        //Cycle if needed
        destpoint = (destpoint) % points.Length;

        //Get the current node
        currentNode = points[destpoint - 1];

        nodeStopTime = currentNode.GetComponent<PatrolNodes>().waitTime;
        nodeLookPoint = currentNode.GetComponent<PatrolNodes>().lookPoint;

    }    
}
