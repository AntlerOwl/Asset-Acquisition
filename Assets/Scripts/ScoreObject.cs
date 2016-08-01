using UnityEngine;
using System.Collections;


public class ScoreObject : MonoBehaviour {

    public int score;
  

	// Use this for initialization
	void Start () {
        score = 1;
	}
	
	// Update is called once per frame
	void Update () { 

      

	}

    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player")
        {
            ScoreManager.currentScore ++; 
            Destroy(gameObject);
        }
        
    }
}
