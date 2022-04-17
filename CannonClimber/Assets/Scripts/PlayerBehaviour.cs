using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private GameManager gm;
    private Rigidbody2D rigibody;
    private Animator anim;
    public float moveSpeed = 4f;
    public float jumpPower = 3.5f;
    public float jumpStart = 0.7f;
    public float jumpBuildRate = 0.4f;
    private float jumpPerc;
    private int jumpCount;
    private int moveStop;
    private bool checkGround;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        rigibody = GetComponent<Rigidbody2D>();
        moveStop = 1;
    }

    void Update()
    {
        if (gm.stageLevel>2)
        {
            Move();
            Jump();
        }
        else
        {
            menuLoad();
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
        if (checkGround)
        {
            if (jumpCount == 0)
            {
                if (Input.GetKeyDown("space")) { jumpPerc = jumpStart; }
                if (Input.GetKey("space"))
                {
                    if (rigibody.velocity.y == 0)
                    {
                        jumpPerc += Time.deltaTime * jumpBuildRate;
                        if (jumpPerc > 1) { jumpPerc = 1; }
                        moveStop = 0;
                        anim.SetInteger("JumpState", 1);
                }
                }
                else if (Input.GetKeyUp("space"))
                {
                    moveStop = 1;
                    jumpCount++;
                    Vector2 jumpFt = new Vector2(0, jumpPower * jumpPerc);
                    rigibody.velocity += jumpFt;
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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CannonBall")
        {
            Vector2 pushback = (this.transform.position - collision.transform.position) * collision.gameObject.GetComponent<CannonBallBehaviour>().getBallPower();
            Debug.Log(pushback);
            rigibody.velocity += pushback;
        }
        else if (collision.gameObject.tag == "Boundary")
        {
            //need to make it so player can't move in direction of Boundary
        }
    }

    private void menuLoad()
    {
        if (gm.stageLevel == 0)
        {
            anim.SetBool("isMoving", true);
        }
        else if (gm.stageLevel == 1)
        {
            StartCoroutine(haltChar());
        }
    }

    private IEnumerator haltChar()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("isMoving", false);
    }

    public void isGrounded(bool grounded)
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
        }
        else { 
            anim.SetBool("isGrounded", false);
            checkGround = false;
        }
    }
}
