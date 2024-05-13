using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float hAxis;
    Vector2 direction;
    [SerializeField] Vector2 wallJumpingPower = new Vector2(8f, 16f);

    [SerializeField] float speed = 3;
    [SerializeField] float jumpPower = 3;
    [SerializeField] float jumpCount = 1;
    [SerializeField] float maxjump = 1;
    [SerializeField] float wallslidespeed = 2;
    float wallJumpingDirection;
    float wallJumpingTime = 0.2f;
    float wallJumpingCounter;
    [SerializeField] float wallJumpingDuration = 2f;
    [SerializeField] float dashingPower = 24f;
    float dashingTime = 0.2f;
    float dashingCooldown = 1f;
    [SerializeField] Vector2 wallchecksize;

    Rigidbody2D rb;
    Animator animator;

    [SerializeField] bool onGround = false;
    [SerializeField] bool onWall = false;
    bool isWallJumping;
    bool canDash = true;
    bool isDashing;

    [SerializeField] AudioClip[] audioClips;
    AudioSource audioSource;

    [SerializeField] Transform BG;
    [SerializeField] Transform wallCheck;

    [SerializeField] LayerMask wallLayer;

    [SerializeField] Lives liveScript;

    [SerializeField] TrailRenderer tr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        movement();
        jump();
        
        animations();
        wallSlide();
        wallJump();

        if (!isWallJumping)
        {
            facing();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true)
        {
            StartCoroutine(Dash());
        }
    }

    void movement()
    {
        //movement player obj
        hAxis = Input.GetAxis("Horizontal");
        direction = new Vector2(hAxis, 0);

        //Vector2 velocity = new Vector2(hAxis * speed, rb.velocity.y);
        //rb.velocity = velocity;
        transform.Translate(direction * Time.deltaTime * speed);

        if (onGround == false && isWallJumping)
        {
            if (hAxis != 0)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    void jump()
    {
        //spacebar velocity to rb yaxis
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount >= 1 && onGround == true)
        {
            rb.velocity = new Vector2(0, 1) * jumpPower;
            audioSource.clip = audioClips[1];
            audioSource.Play();
            
        }
        else if(Input.GetKeyDown(KeyCode.Space) && jumpCount >= 1 && onGround == false)
        {
            rb.velocity = new Vector2(0, 1) * (jumpPower*0.8f);
            audioSource.clip = audioClips[1];
            audioSource.Play();
            jumpCount -= 1;
        }

        //if (Input.GetKeyUp(KeyCode.Space) && jumpCount >= 1 && onGround == true && rb.velocity.y > 0)
        //{
        //    rb.velocity = new Vector2(0, 1) * (jumpPower * 0.2f);
        //    audioSource.clip = audioClips[1];
        //    audioSource.Play();
        //}
    }

    void facing()
    {
        //if right is right
        if (hAxis > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            BG.localScale = new Vector3(2, 2, 2);
        }
        //if left is left
        if (hAxis < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            BG.localScale = new Vector3(-2, 2, 2);
        }
    }

    void animations()
    {
        //if move it move
        animator.SetFloat("Moving", Mathf.Abs(hAxis));
        //if jump it jump
        animator.SetBool("OnGround", onGround);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            onGround = true;
            jumpCount = maxjump;
        }

        if (col.tag == "Enemy")
        {
            liveScript.reduceLives();
        }

        if (col.tag == "Collectible")
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }

        if (col.tag == "MGround")
        {
            onGround = true;
            this.transform.parent = col.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        //if trigger niggernt then cant jump
        if (col.tag == "Ground")
        {
            onGround = false;
            jumpCount -= 1;
        }

        if (col.tag == "MGround")
        {
            onGround = true;
            this.transform.parent = null;
        }
    }

    private bool isWalled()
    {
        //return Physics2D.OverlapCircle(wallCheck.position, wallchecksize, wallLayer);
        return Physics2D.OverlapBox(wallCheck.position, wallchecksize, 0f, wallLayer);
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(wallCheck.position, wallchecksize);
        Gizmos.DrawWireCube(wallCheck.position, wallchecksize);
    }

    private void wallSlide()
    {
        if (isWalled() && onGround == false)
        {
            onWall = true;
            jumpCount = (maxjump - 1f);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallslidespeed, float.MaxValue));
        }
        else
        {
            onWall = false;
        }
    }

    private void wallJump()
    {
        if (onWall == true)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(stopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                Vector3 localscale = transform.localScale;
                localscale.x *= -1f;
                BG.localScale = new Vector3(2, 2, 2);
                transform.localScale = localscale;
            }

            Invoke(nameof(stopWallJumping), wallJumpingDuration);
        }
    }

    void stopWallJumping()
    {
        isWallJumping = false;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
