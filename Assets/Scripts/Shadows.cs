using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public class Shadows : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetActiveFalse()
    {
        EasyObjectPool.Instance.ReturnObjectToPool(this.gameObject);
        gameObject.SetActive(false);
    }
}
