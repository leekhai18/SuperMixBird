using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ready : MonoBehaviour {
    [SerializeField]
    Text bestScoreText;

    [SerializeField]
    Text gamesPlayedText;

    // Use this for initialization
    void Start () {
        Init();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Init()
    {
        bestScoreText.text = GameManager.Instance.BestScore.ToString("00");
        gamesPlayedText.text = GameManager.Instance.GamesPlayed.ToString("00");
    }
}
