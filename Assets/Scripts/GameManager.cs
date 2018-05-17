﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    [SerializeField]
    GameObject spikesLeft;

    [SerializeField]
    GameObject spikesRight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FaceInSpikesLeft()
    {
        RandomizeSpikes(spikesLeft);
        spikesLeft.transform.DOLocalMoveX(-1.1f, 0.5f);
    }

    public void FaceOutSpikesLeft()
    {
        spikesLeft.transform.DOLocalMoveX(-1.8f, 0.5f);
    }

    public void FaceInSpikesRight()
    {
        RandomizeSpikes(spikesRight);
        spikesRight.transform.DOLocalMoveX(3.9f, 0.5f);
    }

    public void FaceOutSpikesRight()
    {
        spikesRight.transform.DOLocalMoveX(4.6f, 0.5f);
    }

    void RandomizeSpikes(GameObject obj)
    {
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            obj.transform.GetChild(i).gameObject.GetComponent<Image>().enabled = false;
            obj.transform.GetChild(i).gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }

        int rand;
        for (int i = 0; i < 5; i++)
        {
            rand = Random.Range(0, obj.transform.childCount);

            obj.transform.GetChild(rand).gameObject.GetComponent<Image>().enabled = true;
            obj.transform.GetChild(rand).gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        }
    }
}