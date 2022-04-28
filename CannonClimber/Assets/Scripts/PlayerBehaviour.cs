using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private GameManager gm;
    private Rigidbody2D rigibody;
    private Animator anim;
    private InGameUI jumpSlider;

    public GameObject kickCollider;
    public Sprite kickTimer_1;
    public Sprite kickTimer_2;
    public Sprite kickTimer_3;
    public GameObject jumpFx;
    public GameObject jumpSpwn;

    public float moveSpeed = 4f;     //move speed of player
    public float jumpPower;          //how high a jump is
    public float jumpStart = 0.7f;   //lowest % a jump can be
    public float jumpBuildRate;      //how fast jump % builds

    private float jumpPerc;          //jump % of current jump
    private int jumpCount;           //player 1 jump only
    private int moveStop;            //stops user left/right while jumping
    private bool checkGround;        //check if player on the ground
    private bool chargingJump;
    private float groundSpawn;

    public bool invincible;
    public bool showInvincible;
    public bool justSpawned;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        rigibody = GetComponent<Rigidbody2D>();
        jumpSlider = FindObjectOfType<InGameUI>();
        moveStop = 1;
        groundSpawn = 0;
        chargingJump = false;

    }

    void Update()
    {
        if (gm.stageLevel > 3) 
        {
            Move();
            Jump();
            Kick();
            KickColliderCheck();
            Invincible();
        }
        else if (gm.stageLevel > 2 ) { Move(); }
        else{ MenuLoad(); }

    }

    private void Move()
    {
        float inAir = 1f;
        if (!checkGround) { inAir = 0.9f; }
        float moveFt = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime * moveStop * inAir;
        if (moveFt != 0)
        {
            if(gm.stageLevel == 3) { gm.stageLevel = 4; }
            anim.SetBool("isMoving", true);
            if (moveFt > 0)
            {
                this.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            }
            else if (moveFt < 0)
            {
                this.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            }
            this.transform.Translate(moveFt, 0, 0);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    private void Jump()
    {
        //need to update jump behaviour
        if (checkGround)
        {
            if (jumpCount == 0)
            {
                if (Input.GetKeyDown("space")) { jumpPerc = jumpStart; }
                if (Input.GetKey("space"))
                {
                    if (rigibody.velocity.y == 0)
                    {
                        chargingJump = true;
                        jumpSlider.SetSeeThru(true);
                        jumpPerc += Time.deltaTime * jumpBuildRate;
                        if (jumpPerc > 1) { jumpPerc = 1; }
                        jumpSlider.jpFlat = jumpPerc;
                        moveStop = 0;

                        anim.SetInteger("JumpState", 1);
                }
                }
                else if (Input.GetKeyUp("space"))
                {
                    chargingJump = false;
                    jumpSlider.SetSeeThru(false);
                    moveStop = 1;
                    Vector2 jumpFt = new Vector2(0, jumpPower * jumpPerc);
                    rigibody.velocity += jumpFt;
                    Instantiate(jumpFx, jumpSpwn.transform.position, Quaternion.identity, transform.parent);
                    jumpCount++;
                }
            }
        }
        if (!checkGround)
        {
            if(rigibody.velocity.y > 0)
            {
                anim.SetInteger("JumpState", 2);
            }
            else if (rigibody.velocity.y < 0)
            {
                anim.SetInteger("JumpState", 3);
            }
            if(jumpCount == 1)
            {
                if (Input.GetKeyDown("space"))
                {
                    rigibody.velocity = new Vector2(0, 0);
                    rigibody.velocity += new Vector2(0, jumpPower * 0.6f);
                    Instantiate(jumpFx, jumpSpwn.transform.position, Quaternion.identity, transform.parent);
                    jumpCount++;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CannonBall")
        {
            //Vector2 pushback = (this.transform.position - collision.transform.position) * collision.gameObject.GetComponent<CannonBallBehaviour>().GetBallPower();
            //rigibody.velocity += pushback;
        }
        else if (collision.gameObject.tag == "Boundary")
        {
            //need to make it so player can't move in direction of Boundary
        }
    }

    private void MenuLoad()
    {
        if (gm.stageLevel == 0)
        {
            anim.SetBool("isMoving", true);
        }
        else if (gm.stageLevel == 1)
        {
            StartCoroutine(HaltChar());
        }
    }

    private IEnumerator HaltChar()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("isMoving", false);
    }

    public void IsGrounded(bool grounded)
    {
        if (grounded) 
        {
            if (!checkGround)
            {
                anim.SetInteger("JumpState",4);
            }
            anim.SetBool("isGrounded", true);
            checkGround = true;
            jumpCount = 0;
            groundSpawn += Time.deltaTime;

            if (groundSpawn > 2f)
            {
                gm.SetSpawnLoc(this.transform.position.x, this.transform.position.y);
                groundSpawn = 0;
            }
            
        }
        else { 
            anim.SetBool("isGrounded", false);
            checkGround = false;
        }
    }


    private void Kick()
    {
        if (gm.coconutBuff > 0)
        {
            if (Input.GetKeyDown("f"))
            {
                if (!chargingJump)
                {
                    Instantiate(jumpFx, jumpSpwn.transform.position, Quaternion.identity, transform.parent);
                    anim.SetTrigger("Kick");
                    if (transform.localScale.x < 0) 
                    {
                        rigibody.velocity = new Vector2(0f, 0f);
                        rigibody.velocity += new Vector2(-1f, 4f);
                    }
                    else if(transform.localScale.x > 0) 
                    {
                        rigibody.velocity = new Vector2(0f, 0f);
                        rigibody.velocity += new Vector2(1f, 4f);
                    }
                }
            }
        }
    }

    private void KickColliderCheck()
    {
        if(this.GetComponent<SpriteRenderer>().sprite == kickTimer_1 ||
            this.GetComponent<SpriteRenderer>().sprite == kickTimer_2 ||
            this.GetComponent<SpriteRenderer>().sprite == kickTimer_3)
        {
            kickCollider.SetActive(true);
        }
        else
        {
            kickCollider.SetActive(false);
        }
    }

    public bool GetGroundCheck() { return checkGround; }

    private void Invincible()
    {
        if (invincible)
        {
            gameObject.layer = 14;
            StartCoroutine(InvincibleShow());
            if (justSpawned)
            {
                if (Input.GetKeyDown("space") || Input.GetAxis("Horizontal") > 0 || (gm.coconutBuff > 0 && Input.GetKeyDown("f")))
                {
                    invincible = false;
                    justSpawned = false;
                }
            }
        }
        else
        {
            gameObject.layer = 6;
        }
    }

    private IEnumerator InvincibleShow()
    {
        if (showInvincible)
        {
            showInvincible = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.15f);
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.15f);
            showInvincible = true;
        }
    }

    public void SetInvincible(bool onOrOff)
    {
        if (onOrOff)
        {
            invincible = true;
            showInvincible = true;
        }
        else
        {
            invincible = false;
            showInvincible = false;
        }
        
    }

    public IEnumerator DelayOffInvincible()
    {
        yield return new WaitForSeconds(2f);
        showInvincible = false;
        invincible = false;
    }
}
