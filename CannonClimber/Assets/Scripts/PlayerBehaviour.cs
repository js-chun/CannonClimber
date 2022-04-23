using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private GameManager gm;
    private Rigidbody2D rigibody;
    private Animator anim;
    private InGameUI jumpSlider;

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

    public int coconutBuff;     //private later

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        rigibody = GetComponent<Rigidbody2D>();
        jumpSlider = FindObjectOfType<InGameUI>();
        moveStop = 1;
        groundSpawn = 0;
        chargingJump = false;

        coconutBuff = 0;
    }

    void Update()
    {
        if (gm.stageLevel>2)
        {
            Move();
            Jump();
            Kick();
        }
        else
        {
            MenuLoad();
        }
    }

    private void Move()
    {
        float inAir = 1f;
        if (!checkGround) { inAir = 0.7f; }
        float moveFt = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime * moveStop * inAir;
        if (moveFt != 0)
        {
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
            Vector2 pushback = (this.transform.position - collision.transform.position) * collision.gameObject.GetComponent<CannonBallBehaviour>().GetBallPower();
            rigibody.velocity += pushback;
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
                gm.setSpawnLoc(this.transform.position.x, this.transform.position.y);
                groundSpawn = 0;
            }
            
        }
        else { 
            anim.SetBool("isGrounded", false);
            checkGround = false;
        }
    }

    public void SetCocoBuff()
    {
        coconutBuff = 3;
    }

    private void Kick()
    {
        if (coconutBuff > 0)
        {
            if (Input.GetKeyDown("f"))
            {
                if (!chargingJump)
                {
                    anim.SetTrigger("Kick");
                }
            }
        }
    }
}
