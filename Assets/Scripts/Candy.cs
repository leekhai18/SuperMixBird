using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Init()
    {
        transform.position = new Vector3(Random.Range(0, 2.5f), Random.Range(-2.5f, 3.5f), 0);
    }
}
