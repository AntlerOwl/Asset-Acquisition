using UnityEngine;
using UnityEngine.AI;
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
                    agent.isStopped = true;
                    rotateToLookPoint();
                }

                if(lookPointType == "double")
                {
                    if(rotatedToTheFirstPoint == false)
                    {
                        agent.isStopped = true;
                        rotateToLookPointDouble();
                    }
                    else
                    {
                        rotateToLookPointDouble2();
                    }
                }
            }

            if(waitTimer > 0)
            {
                waitTimer = waitTimer - Time.deltaTime;
            }
            else
            {
                currentNode = currentNode.GetComponent<PatrolNode>().nextNode[Random.Range(0, currentNode.GetComponent<PatrolNode>().nextNode.Length)];
               
                moveToNextNode();                
                
            }

        }

	}

    public void moveToNextNode()
    {
        agent.isStopped = false;
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
        Vector3 direction = (nodeLookPoint.position - transform.position);

        
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

    }

    public void rotateToLookPointDouble()
    {
        
        Vector3 direction = (nodeLookPoint.position - transform.position);

        
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        if (Quaternion.Angle(transform.rotation, lookRotation) < 5)
        {
            rotatedToTheFirstPoint = true;
        }
    }


    public void rotateToLookPointDouble2()
    {
        
        Vector3 direction = (nodeLookPoint2.position - transform.position);

        
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        if (Quaternion.Angle(transform.rotation, lookRotation) < 5)
        {

        }
    }
}
