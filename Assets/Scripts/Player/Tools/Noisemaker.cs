using UnityEngine;
using System.Collections;

public class Noisemaker : MonoBehaviour {


    public float thrust;
    public float spinSpeed;


    public GameObject panel;

    public GameObject box;

    public Vector3 endSize;

    private int activated;
    private int interactingWith;
    private bool playerInTrigger;

    private Vector3 sackPosition;
    private GameObject sack;
    private Rigidbody rb;
    private GameObject gc;

    // Use this for initialization
    void Start () {

        sack = GameObject.Find("sackPosition");
        box = this.gameObject.transform.GetChild(0).gameObject;

        activated = 0;
        rb = gameObject.GetComponent<Rigidbody>();
        gc = GameObject.Find("Game Control");
        panel.SetActive(false);
        interactingWith = 0;

        playerInTrigger = false;


    }
	
	// Update is called once per frame
	void Update () {

        sackPosition = sack.transform.position;

        if(playerInTrigger == true && Input.GetButtonDown("e"))
        {
            if(panel.activeInHierarchy == false) 
            {
                panel.SetActive(true);
                Player.busyInteracting = true;

            }

            else
            {
                panel.SetActive(false);
                Player.busyInteracting = false;
            }


        }


    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player" && Input.GetButton("Fire2") && activated == 0)
        {
            activated = 1;

            box.GetComponent<BoxCollider>().enabled = false;
            panel.SetActive(false);
            Player.busyInteracting = false;
            StartCoroutine(MoveObjectToSack(1.25f));

        }

        
        

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && activated == 0) 
        {
            playerInTrigger = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && activated == 0)
        {
            playerInTrigger = false;
        }

        if(other.gameObject.tag == "Player")
        {
            panel.SetActive(false);
            Player.busyInteracting = false;
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

        gc.GetComponent<PlayerToolsInventory>().noiseMakers++;

        Destroy(gameObject);
        yield return null;
    }

}
