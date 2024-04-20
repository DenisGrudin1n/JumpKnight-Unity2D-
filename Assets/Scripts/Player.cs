using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MainMenu;
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D bodyCollider2D;
    private BoxCollider2D feetCollider2D;
    private float jumpValue = 0f;
    private float runSpeed = 4f;

    private Animator playerAnimator;
    [SerializeField] private List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    private string currentState;
    private string playerIdle = "Idle";
    private string playerRun = "Run";
    private string playerJump = "Jump";
    private string playerSit = "Sit";

    [SerializeField] private AudioSource GameMusic;
    [SerializeField] private AudioSource jumpSound;

    [SerializeField] private GameObject transparentPanel;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponentInChildren<Animator>();
        bodyCollider2D = GetComponent<CapsuleCollider2D>();
        feetCollider2D = GetComponent<BoxCollider2D>();
        if (needToTurnMusic) GameMusic.Play();
        Time.timeScale = 1;
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        bool isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(0.9f, 0.4f), 0f, 1 << 8) && feetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (jumpValue == 0f && isGrounded)
        {
            rb.velocity = new Vector2(moveInput * runSpeed, rb.velocity.y);

            if (moveInput == 0f)
            {
                PlayAnim(playerIdle);
            }

            else
            {
                foreach (var sprite in sprites)
                {
                    if (moveInput == -1f)
                    {
                        sprite.flipX = false;
                    }
                    else
                    {
                        sprite.flipX = true;
                    }
                }
                PlayAnim(playerRun);
            }        
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {         
            jumpValue += 0.6f; //for build 0.6f, for unity 0.3f;

            if (jumpValue < 25f)
            {
                PlayAnim(playerSit);
            }

            else
            {
                jumpValue = 25f;
                rb.velocity = new Vector2(moveInput * runSpeed * 1.85f, jumpValue);

                PlayAnim(playerJump);
                if (needToTurnSound)
                {
                    jumpSound.Play();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (jumpValue < 10f)
            {
                jumpValue = 10f;
            }

            if (isGrounded)
            {
                if (jumpValue > 10f)
                {
                    rb.velocity = new Vector2(moveInput * runSpeed * 1.85f, jumpValue);
                }
                else
                {
                    rb.velocity = new Vector2(moveInput * runSpeed * 1.25f, jumpValue);
                }

                PlayAnim(playerJump);
                if (needToTurnSound)
                {
                    jumpSound.Play();
                }
            }
        }

        if (!isGrounded)
        {
            jumpValue = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transparentPanel.SetActive(!transparentPanel.activeSelf);
            Time.timeScale = Math.Abs(Time.timeScale - 1);
        }
    }
    
    void PlayAnim(string newState)
    {
        if (currentState == newState) return;
        playerAnimator.Play(newState);
        currentState = newState;
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Door"))
        {
            SceneManager.LoadScene("EndGame");
        }
    }

    public void ResumeButton()
    {
        transparentPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LeaveGameButton()
    {
        Application.Quit();
    }
}
