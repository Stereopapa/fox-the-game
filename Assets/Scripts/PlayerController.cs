using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    const float rayLenght = 1.5f;
    private bool isFacingRight = true;
    //private int keysFound = 0;
    const int keysNumber = 3;

    [Header("Movement parameters")]
    [Range(0.01f, 20.0f)] [SerializeField] private float moveSpeed = 0.1f;
    [Space(10)]
    [Range(0.01f, 20.0f)] [SerializeField] private float jumpForce = 6.0f;
    private Rigidbody2D rigidbody;
    public LayerMask groundLayer;
    private bool isLadder = false;
    private bool isClimbing = false;
    private float vertical = 0.0f;
    private Vector2 startPosition;

    //Audio
    private AudioSource source;
    [SerializeField] AudioClip bonusSound;
    [SerializeField] AudioClip enemyDefSound;
    [SerializeField] AudioClip keyFounSound;
    [SerializeField] AudioClip liveUpSound;
    [SerializeField] AudioClip liveDownSound;
    [SerializeField] AudioClip nEKeysSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip looseSound;
    [SerializeField] AudioClip leverSound;
    [SerializeField] AudioClip playerJumpSound;
    [SerializeField] AudioClip playerWalkSound;
    private float walkigSoundTimePassed;
    [SerializeField] private float walkingSoundSpeed = 0.5f;

    bool isGrounded()
    {
        
        return Physics2D.Raycast(this.transform.position, Vector2.down, rayLenght, groundLayer.value);
    }

    void Jump()
    {
        if (isGrounded())
        {
            source.PlayOneShot(playerJumpSound);
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        Debug.Log("jumping");
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.getGameState() != GameManager.GameState.GAME)  return;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            if (!isFacingRight)
            {
                Flip();
            }
            
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            if (isFacingRight)
            {
                Flip();
            }
            
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) Jump();

        // Debug.DrawRay(transform.position, rayLenght * Vector3.down, Color.white, 1, false);
        
        animator.SetBool("isGrounded", isGrounded());
        animator.SetBool("isWalking", isWalking());

        //walking sound
        walkigSoundTimePassed += Time.deltaTime;
        if (isWalking() && isGrounded() && walkigSoundTimePassed > walkingSoundSpeed){
            source.PlayOneShot(playerWalkSound);
            walkigSoundTimePassed = 0;
        }

        vertical = Input.GetAxis("Vertical");
        if (isLadder && Math.Abs(vertical) > 0) {
            isClimbing = true;
        }
    }

    void FixedUpdate()
    {
        if (isClimbing)
        {
            rigidbody.gravityScale = 0;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, vertical * moveSpeed);
        }
        else
        {
            rigidbody.gravityScale = 1;
        }
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ( col.tag == "FallLevel")
        {
            GameManager.Instance.DecLives();
            if (GameManager.Instance.GetLives() < 1) {
                source.PlayOneShot(looseSound, AudioListener.volume);
                GameManager.Instance.GameOver();
            }
            else source.PlayOneShot(liveDownSound, AudioListener.volume);
            transform.position = startPosition;
            
            //Debug.Log("Player below level!");
            //Debug.Log("Game Over");
        }

        if (col.CompareTag("Key"))
        {
            GameManager.Instance.AddKeys(col.GetComponent<Renderer>().material.color); //Ask Lecturer
            source.PlayOneShot(keyFounSound, AudioListener.volume);
            //Debug.Log("Key Found "+ keysFound);
            col.gameObject.SetActive(false);
        }

        if (col.CompareTag("Heart"))
        {
            GameManager.Instance.AddLives();
            source.PlayOneShot(liveUpSound, AudioListener.volume);
            //Debug.Log("heart found, lives " + lives);
            col.gameObject.SetActive(false);
        }

        if (col.tag == "Finish")
        {
            if (GameManager.Instance.GetKeysFound() == GameManager.Instance.keysTab.Length)
            {
                GameManager.Instance.AddPoints(100*GameManager.Instance.GetLives());
                source.PlayOneShot(winSound, AudioListener.volume);
                GameManager.Instance.LevelCompleted();
            }
            else
            {
                source.PlayOneShot(nEKeysSound, AudioListener.volume);
            }

        }

        if (col.CompareTag("Bonus"))
        {
            GameManager.Instance.AddPoints(1);
            source.PlayOneShot(bonusSound, AudioListener.volume);
            col.gameObject.SetActive(false);
        }

        if (col.tag == "Ladder")
        {
            Debug.Log("Ladder enter");
            isLadder = true;
        }

        if (col.tag == "Lever")
        {
            source.PlayOneShot(leverSound, AudioListener.volume * 3);
        }

        if (col.CompareTag("MovingPlatform"))
        {
            transform.SetParent(col.transform);
            Debug.Log("Stepped on moving platform");
        }


        if (col.CompareTag("Enemy"))
        {
            if(transform.position.y > col.gameObject.transform.position.y)
            {
                GameManager.Instance.AddPoints(1);
                GameManager.Instance.AddEnemiesKilled();
                source.PlayOneShot(enemyDefSound, AudioListener.volume);
                Debug.Log("Killed an enemy");
            }

            if (transform.position.y <= col.gameObject.transform.position.y)
            {
                GameManager.Instance.DecLives();
                if (GameManager.Instance.GetLives() < 1) // TODO game Over
                {
                    GameManager.Instance.GameOver();
                    source.PlayOneShot(looseSound, AudioListener.volume);
                }
                else
                {
                    source.PlayOneShot(liveDownSound, AudioListener.volume);
                    transform.position = startPosition;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Ladder")
        {
            Debug.Log("Ladder exit");
            isLadder = false;
            isClimbing = false;
        }

        if (col.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
            Debug.Log("Exited moving platform");
        }
    }

    private bool isWalking()
    {
        return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x = -theScale.x;
        transform.localScale = theScale;
    }

}
