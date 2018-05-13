using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bird : MonoBehaviour
{
    [SerializeField]
    float velocityJumpY;
    [SerializeField]
    float velocityJumpX;

    int scaleX = 1;

    Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = new Vector2(velocityJumpX, velocityJumpY);
            //transform.DOMoveX(transform.position.x + Time.deltaTime * speedX, 1);
        }
       
#else
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("Touched");
            }
        }
#endif
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            velocityJumpX = -1 * velocityJumpX;
            scaleX = -1 * scaleX;
            transform.DOScaleX(scaleX, 0);
        }
    }
}
