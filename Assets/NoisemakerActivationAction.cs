using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisemakerActivationAction : MonoBehaviour {

    public float timeToTranslate;
    public float duration;

    [HideInInspector]
    public Transform finalTransform;
    public Transform startTransform;

	// Use this for initialization
	void Start () {

        finalTransform.position = new Vector3(0.96f, 4.47f, -0.4f);
        finalTransform.localScale = new Vector3(2.5f, 2.5f, 2.5f);

        startTransform.position = new Vector3(0.0f, 0.0f, 0.0f);
        startTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public IEnumerator NoiseMakerActivates()
    {
        float progress = timeToTranslate;

        var startPosition = startTransform.position;
        var startSize = startTransform.localScale;
        var startRotation = startTransform.rotation;

        var endPosition = finalTransform.position;
        var endLocalScale = finalTransform.localScale;
        var endRotation = finalTransform.rotation;

        while (progress > 0)
        {
            progress -= Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(endPosition, startPosition, progress);
            gameObject.transform.localScale = Vector3.Lerp(endLocalScale, startSize, progress);

            yield return null;

        }
        
        yield return null;
    }
}
