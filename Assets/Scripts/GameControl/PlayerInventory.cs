using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

    public int cashTotal;
    public int goldenGoblet;
    public int goldenPlate;

    public Text cashText;
    public Text gobletText;
    public Text plateText;

	// Use this for initialization
	void Start () {


        goldenGoblet = 0;


	}
	
	// Update is called once per frame
	void Update () {

        cashText.text = cashTotal.ToString();
        gobletText.text = goldenGoblet.ToString();
        plateText.text = goldenPlate.ToString();

	}

}
