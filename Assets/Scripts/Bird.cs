using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bird : MonoBehaviour
{
    enum BirdState
    {
        begin,
        jump,
        die
    }
    BirdState currentState;


    [SerializeField]
    float velocityJumpY;
    [SerializeField]
    float velocityJumpX;


    int scaleX = 1;

    Rigidbody2D rb;

    Animator animator;

    [SerializeField]
    GameObject rendererForBeginAnim;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentState = BirdState.begin;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        if (currentState != BirdState.die)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentState == BirdState.begin)
                {
                    rendererForBeginAnim.SetActive(false);
                    GetComponent<SpriteRenderer>().enabled = true;
                    rb.bodyType = RigidbodyType2D.Dynamic;
                }

                animator.SetBool("IsJumping", true);
                currentState = BirdState.jump;
                rb.velocity = new Vector2(velocityJumpX, velocityJumpY);

                SoundManager.Instance.Play(SoundManager.Sounds.jump);

                StartCoroutine(Falling());
            }
        }
#else
        if (currentState != BirdState.die)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Debug.Log("Touched");
                }
            }
        }
#endif


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (scaleX == 1)
            {
                GameManager.Instance.FaceOutSpikesRight();
                GameManager.Instance.FaceInSpikesLeft();
            }
            else
            {
                GameManager.Instance.FaceOutSpikesLeft();
                GameManager.Instance.FaceInSpikesRight();
            }

            velocityJumpX = -1 * velocityJumpX;
            scaleX = -1 * scaleX;
            transform.DOScaleX(scaleX, 0);

            if (currentState != BirdState.die)
            {
                SoundManager.Instance.Play(SoundManager.Sounds.getScore);
                GameManager.Instance.Score++;
                if (GameManager.Instance.Score > PlayerPrefs.GetInt("bestScore", GameManager.Instance.BestScore))
                {
                    GameManager.Instance.BestScore = GameManager.Instance.Score;
                    PlayerPrefs.SetInt("bestScore", GameManager.Instance.BestScore);
                }
            }
        }

        if (collision.gameObject.CompareTag("Spike"))
        {
            Die();
        }
    }

    void Die()
    {
        if (currentState != BirdState.die)
        { 
            currentState = BirdState.die;
            SoundManager.Instance.Play(SoundManager.Sounds.die);
            animator.SetBool("IsDied", true);
            rb.velocity = new Vector2(velocityJumpX * 3, velocityJumpY);
            StartCoroutine(FaceOutThenDie());
        }
    }

    IEnumerator Falling()
    {
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("IsJumping", false);
    }

    IEnumerator FaceOutThenDie()
    {
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("IsFaceOut", true);
        GameManager.Instance.GameOver();
    }

    void SetActiveToFalse()
    {
        gameObject.SetActive(false);
        GameManager.Instance.FaceOutSpikesLeft();
        GameManager.Instance.FaceOutSpikesRight();
    }
}
