using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class NoiseTrigger : MonoBehaviour {

    Guard guard;
    NavMeshAgent agent;
    private GameObject player;
    private float fadeTimer;
    private Material mat;
    

    Player playerScript;


    // Use this for initialization
    void Start() {

        player = GameObject.FindGameObjectWithTag("Player");

        fadeTimer = 0.0f;

        mat = GetComponent<Renderer>().material;

       
    }

    // Update is called once per frame
    void Update()
    {

        
        fadeTimer = fadeTimer - Time.deltaTime;

        if (fadeTimer < 0)
        {
            StartCoroutine(FadeOut(8.5f));
        }


        if (player.GetComponent<Player>().playerMoving == true && fadeTimer != 0.175f)
        {

            StartCoroutine(FadeIn(8.5f));
            fadeTimer = 0.175f;

        }

    }



    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Guard" && player.GetComponent<Player>().playerMoving == true && other.gameObject.GetComponent<Guard>().guardState != 3)
        {
            other.gameObject.GetComponent<Guard>().guardState = 7;
            other.gameObject.GetComponent<Guard>().soundSourceNode.transform.position = player.transform.position;


            if(other.gameObject.GetComponent<Guard>().currentlySearchingSound == true)
            {
                other.gameObject.GetComponent<Guard>().soundSourceNode.transform.position = player.transform.position;
                other.gameObject.GetComponent<Guard>().currentlySearchingSound = false;
                other.gameObject.GetComponent<Guard>().soundTargetSet = true;
                other.gameObject.GetComponent<Guard>().hasWaitedBeforeMovingToSoundSource = false;             
                other.gameObject.GetComponent<NavMeshAgent>().Resume();
                other.gameObject.GetComponent<Guard>().waitingAfterHavingHeardSound = 0.0f;
                other.gameObject.GetComponent<Guard>().currentlySearchingSound = false;
            }

        }

    }
    

    public IEnumerator FadeOut(float duration)
    {


        while (mat.color.a > 0.085)
        {
            Color newColor = mat.color;
            newColor.a -= Time.deltaTime / duration;
            mat.color = newColor;
            yield return null;
        }

    }

    public IEnumerator FadeIn(float duration)
    {


        while (mat.color.a < 1.0)
        {
            Color newColor = mat.color;
            newColor.a += Time.deltaTime / duration;
            mat.color = newColor;
            yield return null;
        }

    }

    
}
