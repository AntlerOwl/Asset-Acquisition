using UnityEngine;
using System.Collections;

public class ItemSpawn : MonoBehaviour {

    public GameObject[] itemSpawns;
    public GameObject item;

    private int itemsToSpawn = 3;


    // Use this for initialization
    void Start () {


        itemSpawns = GameObject.FindGameObjectsWithTag("itemspawn");

        for (int i = 0; i < itemsToSpawn; i++ )
        {
            SpawnItemAtLocation();
        }

    }

    // Update is called once per frame
    void Update () {
	
	}

    void SpawnItemAtLocation()
    {
        Vector3 spawnPosition = itemSpawns[Random.Range(0, itemSpawns.Length)].transform.position;
        GameObject newItem = (GameObject)Instantiate(item, spawnPosition, Quaternion.Euler(Vector3.forward)); 
    }

    
}


