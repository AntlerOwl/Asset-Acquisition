using UnityEngine;
using System.Collections;


public class ScoreObject : MonoBehaviour {

    public int value;
    public float thrust;
    public float spinSpeed;
    public string objectType;

    public Vector3 endSize;

    private int activated;
    
    private Vector3 sackPosition;
    private GameObject sack;
    private Rigidbody rb;


    public GameObject gC;


	// Use this for initialization
	void Start () {

        sack = GameObject.Find("sackPosition");
        
        activated = 0;
        rb = gameObject.GetComponent<Rigidbody>();
        gC = GameObject.Find("Game Control");
	}
	
	// Update is called once per frame
	void Update () {

        sackPosition = sack.transform.position;

    }

    void OnTriggerStay(Collider other)
    {

        if(other.gameObject.tag == "Player" && Input.GetButton("Fire2") && activated == 0)
        {
            activated = 1;
            
            gameObject.GetComponent<MeshCollider>().enabled = false;
            StartCoroutine(MoveObjectToSack(1.25f));
            
        }
        
    }

    public IEnumerator MoveObjectToSack(float duration)
    {
        rb.AddForce(new Vector3(0, 550, 0));
        yield return new WaitForSeconds(0.3f);

        var originalPosition = gameObject.transform.position;
        var originalScale = gameObject.transform.localScale;
        var targetScale = endSize;
        float progress = duration;

        

        while (progress > 0)
        {
            progress -= Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(sackPosition, originalPosition, progress);
            transform.Rotate(Vector3.left * spinSpeed * Time.deltaTime);
            transform.localScale = Vector3.Lerp(targetScale, originalScale, progress);

            yield return null;
        }
        ScoreManager.currentScore++;

        if(objectType == "GoldGoblet")
        {
            gC.GetComponent<PlayerInventory>().goldenGoblet++;
        }
        if(objectType == "GoldenPlate")
        {
            gC.GetComponent<PlayerInventory>().goldenPlate++;
        }
        
        gC.GetComponent<PlayerInventory>().cashTotal += value;
        Destroy(gameObject);
        yield return null;
    }

   
}
