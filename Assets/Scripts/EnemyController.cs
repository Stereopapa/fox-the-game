using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private bool isFacingRight = false;
    private float startPositionX;
    private bool isMovingRight = false;

    private Rigidbody2D rigidbody;
    private Collider2D collider2d;

    [Header("Movement parameters")]
    [Range(0.01f, 20.0f)][SerializeField] private float moveSpeed = 0.1f;
    [Range(0.1f, 20.0f)][SerializeField] public float moveRange = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Eagle position x: " + this.transform.position.x 
        //    + " MovingRight: " + isMovingRight 
        //    + " FacingRight: " + isFacingRight);       
        if (isMovingRight)
        {
            MoveRight();
            if (!(startPositionX + moveRange > this.transform.position.x))
            {
                Flip();
                isMovingRight = false;
            }
        }
        else
        {
            MoveLeft();
            if(!(startPositionX - moveRange < this.transform.position.x))
            {
                Flip();
                isMovingRight = true;
            }
        }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider2d = GetComponent<Collider2D>();
        startPositionX = this.transform.position.x;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x = -theScale.x;
        transform.localScale = theScale;
    }

    private void MoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }

    private void MoveLeft()
    {
        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (transform.position.y < col.gameObject.transform.position.y)
            {
                animator.SetBool("isDead", true);
                collider2d.enabled = false;
                // Debug.Log("Killed an enemy");
                StartCoroutine(KillOnAnimationEnd());
            }
        }
        
    }

    IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
