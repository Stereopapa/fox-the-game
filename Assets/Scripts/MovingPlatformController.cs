using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private float startPositionX;
    private bool isMovingRight = false;

    private Rigidbody2D rigidbody;

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
        if (isMovingRight)
        {
            MoveRight();
            if (!(startPositionX + moveRange > this.transform.position.x))
            {
                isMovingRight = false;
            }
        }
        else
        {
            MoveLeft();
            if (!(startPositionX - moveRange < this.transform.position.x))
            {
                isMovingRight = true;
            }
        }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startPositionX = this.transform.position.x;
    }


    private void MoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }

    private void MoveLeft()
    {
        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }

}
