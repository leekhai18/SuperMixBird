using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bird : MonoBehaviour
{
    enum BirdState
    {
        normal,
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
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentState = BirdState.normal;
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        if (currentState != BirdState.die)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentState = BirdState.jump;
                rb.velocity = new Vector2(velocityJumpX, velocityJumpY);
                animator.SetBool("IsJumping", true);
                StartCoroutine(JumpingToNormal());
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
            currentState = BirdState.die;
            animator.SetBool("IsDied", true);
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
        }

        if (collision.gameObject.CompareTag("Spike"))
        {
            currentState = BirdState.die;
            animator.SetBool("IsDied", true);
        }
    }

    IEnumerator JumpingToNormal()
    {
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("IsJumping", false);
        currentState = BirdState.normal;
    }
}
