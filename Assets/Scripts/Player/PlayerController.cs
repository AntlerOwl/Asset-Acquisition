using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public static bool playerCaught;
    Vector3 velocity;
    Rigidbody myRigidbody;
    Player playerScript;


	// Use this for initialization
	void Start () {

        myRigidbody = GetComponent<Rigidbody>();
        
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
        
    }

    public void LookAt(Vector3 lookPoint)
    {
        if(playerCaught != true)
        {
            Vector3 heightPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
            transform.LookAt(heightPoint);
        }
       
    }

    public void FixedUpdate()
    {
        if(playerCaught != true)
        {
            myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);
            //playerScript.playerMoving = true;
            
        }
        

    }
}
