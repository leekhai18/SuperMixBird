using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text bestScoreText;

    [SerializeField]
    Text gamesPlayedText;


    // Use this for initialization
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {

    }

    public void Replay()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene);
    }

    void Init()
    {
        scoreText.text = GameManager.Instance.Score.ToString("00");
        bestScoreText.text = GameManager.Instance.BestScore.ToString("00");
        gamesPlayedText.text = GameManager.Instance.GamesPlayed.ToString("00");
    }
}
