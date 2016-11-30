using UnityEngine;
using System.Collections;

public class GuardPatrolv2 : MonoBehaviour {

    public GameObject currentNode;

    public float waitTimer;
    public bool beingSearchedFor;

    private Transform nodeLookPoint;
    private Transform nodeLookPoint2;
    private string lookPointType;
    public float rotationSpeed;
    private bool rotatedToTheFirstPoint;

    private NavMeshAgent agent;
    private Guard grd;


	// Use this for initialization
	void Start () {

        agent = GetComponent<NavMeshAgent>();
        grd = GetComponent<Guard>();
        

    }
	
	// Update is called once per frame
	void Update () {


        if(grd.guardState == 1 && agent.remainingDistance <= 0.25f && beingSearchedFor != true)
        {

            //Start rotation if there's a lookpoint//
            if(nodeLookPoint != null)
            {
                if(lookPointType == "single")
                {
                    agent.Stop();
                    rotateToLookPoint();
                }

                if(lookPointType == "double")
                {
                    if(rotatedToTheFirstPoint == false)
                    {
                        agent.Stop();
                        rotateToLookPointDouble();
                    }
                    else
                    {
                        rotateToLookPointDouble2();
                    }
                }
            }

            //Wait timer for the node wait time//
            if(waitTimer <= 0)
            {
                currentNode = currentNode.GetComponent<PatrolNode>().nextNode[Random.Range(0, currentNode.GetComponent<PatrolNode>().nextNode.Length)];
               
                moveToNextNode();                
                
            }
            if(waitTimer > 0)
            {

                waitTimer = waitTimer - Time.deltaTime;

            }
        }

	}

    public void moveToNextNode()
    {
        agent.Resume();
        agent.SetDestination(currentNode.transform.position);

        waitTimer = currentNode.GetComponent<PatrolNode>().waitTime;
        lookPointType = currentNode.GetComponent<PatrolNode>().lookNodeType;
        nodeLookPoint = currentNode.GetComponent<PatrolNode>().lookNode;
        nodeLookPoint2 = currentNode.GetComponent<PatrolNode>().lookNode2;

        rotatedToTheFirstPoint = false;
    }


    //Rotation methods for lookpoints//

    public void rotateToLookPoint()
    {
        Vector3 direction = (nodeLookPoint.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    public void rotateToLookPointDouble()
    {
        Vector3 direction = (nodeLookPoint.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        if (Quaternion.Angle(transform.rotation, lookRotation) < 5)
        {
            rotatedToTheFirstPoint = true;
        }
    }


    public void rotateToLookPointDouble2()
    {
        Vector3 direction = (nodeLookPoint2.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        if (Quaternion.Angle(transform.rotation, lookRotation) < 5)
        {

        }
    }
}
