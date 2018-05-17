using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    [SerializeField]
    GameObject spikesLeft;

    [SerializeField]
    GameObject spikesRight;

    [SerializeField]
    GameObject candies;

    [SerializeField]
    GameObject scoreText;

    [SerializeField]
    GameObject gameOver;

    [SerializeField]
    GameObject ready;

    private int score;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            scoreText.GetComponent<Text>().text = score.ToString("00");
            SoundManager.Instance.Play(SoundManager.Sounds.getScore);
        }
    }

    private int bestScore;
    public int BestScore
    {
        get
        {
            return bestScore;
        }
        set
        {
            bestScore = value;
        }
    }

    private int gamesPlayed;
    public int GamesPlayed
    {
        get
        {
            return gamesPlayed;
        }
        set
        {
            gamesPlayed = value;
        }
    }

    // Use this for initialization
    void Start () {
        bestScore = PlayerPrefs.GetInt("bestScore", 0);
        gamesPlayed = PlayerPrefs.GetInt("gamesPlayed", 0);

        gamesPlayed++;
        PlayerPrefs.SetInt("gamesPlayed", gamesPlayed);
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

            if (rand == obj.transform.childCount / 2)
            {
                GameObject candy = candies.transform.GetChild(Random.Range(0, 2)).gameObject;

#pragma warning disable CS0618 // Type or member is obsolete
                if (!candy.active)
#pragma warning restore CS0618 // Type or member is obsolete
                {
                    candy.GetComponent<Candy>().Init();
                    candy.SetActive(true);
                }
            }

            obj.transform.GetChild(rand).gameObject.GetComponent<Image>().enabled = true;
            obj.transform.GetChild(rand).gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        }
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }

    public void Started()
    {
        ready.GetComponent<Animator>().SetBool("IsStarted", true);
    }
}
