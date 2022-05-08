using System.Collections;
using UnityEngine;

//Class to control Player Movements and Invincibility
public class PlayerBehaviour : MonoBehaviour
{
    private GameManager gm;
    private Rigidbody2D rigibody;
    private Animator anim;
    private InGameUI jumpSlider;
    private PauseOverlay pauseOverlay;

    public GameObject kickCollider; //Kick Collision to turn on or off
    public Sprite kickTimer_1;      //Sprites to determine when Kick Collision is active
    public Sprite kickTimer_2;
    public Sprite kickTimer_3;
    public GameObject kickFx;       //Particle prefab for when user is kicking
    public GameObject jumpFx;       //Particle prefab for when user is jumping
    public GameObject jumpSpwn;     //Where jump particles are instantiated
    private bool kicked;

    public float moveSpeed = 4f;     //Move speed of Player

    //below are for charge jump method
    public float jumpPower;          //Max jump Height of Player
    public float jumpStart = 0.7f;   //Min jump % of Player
    public float jumpBuildRate;      //Build up rate of jump %
    private bool chargingJump;       //If Player is currently charging jump
    private float jumpPerc;          //Current jump %

    //below is for alternative no charge jump method
    public float jumpPowerAlt = 8f;

    public int jumpCount;           //Player jumpt count for double jump
    private int moveStop;            //Stops Player from moving left/right while charging jump
    private bool checkGround;        //If Player is currently on ground
    private float previousHeight;    //Previous max height of Player

    private bool invincible;         //If Player should currently be invincible or not
    private bool showInvincible;     //If Player is showing as Invincible
    private bool justSpawned;        //If Player just spawned
    private float wineHue;           //Colour hue value of player (used for wine buff)

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        rigibody = GetComponent<Rigidbody2D>();
        jumpSlider = FindObjectOfType<InGameUI>();
        pauseOverlay = FindObjectOfType<PauseOverlay>();

        moveStop = 1;
        previousHeight = 0;
        chargingJump = false;
        wineHue = 0f;

    }

    void Update()
    {
        //Stage Level 4 when Player is actually in the levels (has moved after hitting ground)
        //Stage Level 3 when Player first hits ground
        //Stage Level 1-2 when Player Object is in Menu (not controllable)
        if (gm.stageLevel > 3)
        {
            Move();
            //Jump();
            JumpAlternative();
            Kick();
            KickColliderCheck();
            Invincible();
        }
        else if (gm.stageLevel > 2) { Move(); }
        else { MenuLoad(); }

    }

    //When Player is in Menu (non-controllable
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

    //When Play is clicked on menu, Player stops moving
    private IEnumerator HaltChar()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("isMoving", false);
    }


    //To control Player movement (left and right)
    //If in air, less left and right
    private void Move()
    {
        float inAir = 1f;
        if (!checkGround) { inAir = 0.9f; }
        float moveFt = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime * moveStop * inAir;
        if (moveFt != 0)
        {
            if (gm.stageLevel == 3) { gm.stageLevel = 4; }
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

    //To control Player movement (jumps - up and down)
    //Shows jump slider when charging
    //Player can double jump
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
            if (rigibody.velocity.y > 0)
            {
                anim.SetInteger("JumpState", 2);
            }
            else if (rigibody.velocity.y < 0)
            {
                anim.SetInteger("JumpState", 3);
            }
            if (jumpCount == 1)
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

    //To control Player movement (jumps - up and down)
    //Non-Charge - no jump slider
    //Player can double jump
    private void JumpAlternative()
    {
        if (!checkGround)
        {
            if (rigibody.velocity.y > 0)
            {
                anim.SetInteger("JumpState", 2);
            }
            else if (rigibody.velocity.y < 0)
            {
                anim.SetInteger("JumpState", 3);
            }
        }

            //need to update jump behaviour
        if (jumpCount == 0)
        {
            if (Input.GetKeyDown("space"))
            {
                rigibody.velocity = new Vector2(0, 0);
                rigibody.velocity += new Vector2(0, jumpPowerAlt);
                Instantiate(jumpFx, jumpSpwn.transform.position, Quaternion.identity, transform.parent);
                jumpCount = 1;
            }
        }
        else if (jumpCount == 1)
        {
            if (Input.GetKeyDown("space"))
            {
                rigibody.velocity = new Vector2(0, 0);
                rigibody.velocity += new Vector2(0, jumpPowerAlt * 0.85f);
                Instantiate(jumpFx, jumpSpwn.transform.position, Quaternion.identity, transform.parent);
                jumpCount++;
            }
        }
    }

    //To set when Player is on the ground
    //If Player reaches new max height and is grounded, set as new spawn location
    public void IsGrounded(bool grounded)
    {
        if (grounded)
        {
            if (!checkGround)
            {
                anim.SetInteger("JumpState", 4);
            }
            anim.SetBool("isGrounded", true);
            checkGround = true;
            jumpCount = 0;

            if (this.transform.position.y > previousHeight)
            {
                previousHeight = this.transform.position.y;
                gm.SetSpawnLoc(this.transform.position.x, previousHeight);
            }

        }
        else {
            anim.SetBool("isGrounded", false);
            checkGround = false;
        }
    }


    //When Player has Coconut buffs, can kick while not charging jumps
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
                    kicked = true;
                    if (transform.localScale.x < 0)
                    {
                        rigibody.velocity = new Vector2(0f, 0f);
                        rigibody.velocity += new Vector2(-1f, 4f);
                    }
                    else if (transform.localScale.x > 0)
                    {
                        rigibody.velocity = new Vector2(0f, 0f);
                        rigibody.velocity += new Vector2(1f, 4f);
                    }
                }
            }
        }
    }

    //Turns on Kick Collider (to block Cannon damage) during Kick animation
    private void KickColliderCheck()
    {
        if (this.GetComponent<SpriteRenderer>().sprite == kickTimer_1 ||
            this.GetComponent<SpriteRenderer>().sprite == kickTimer_2 ||
            this.GetComponent<SpriteRenderer>().sprite == kickTimer_3)
        {
            kickCollider.SetActive(true);
            if (this.GetComponent<SpriteRenderer>().sprite == kickTimer_1)
            {
                if (kicked)
                {
                    Instantiate(kickFx, kickCollider.transform.position, Quaternion.identity, transform.parent);
                    kicked = false;
                }
            }
        }
        else
        {
            kickCollider.SetActive(false);
        }
    }

    //To check Player invincibility and trigger flashes and function
    //Rainbow color while wine buff invincibility
    //If the Player just spawned, moving will cancel invincibility
    //Otherwise, for a set amount of time
    private void Invincible()
    {
        if (invincible)
        {
            gameObject.layer = 14;
            if (!gm.wineBuff)
            {
                StartCoroutine(InvincibleShow());
            }
            else
            {
                wineHue += Time.deltaTime * 1f;
                if (wineHue >= 1f) { wineHue = 0f; }
                this.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(wineHue, 1f, 1f);
            }

            if (justSpawned)
            {
                if (pauseOverlay != null) { pauseOverlay.SetMaskOn(true); }
                if (!checkGround) { rigibody.gravityScale = 0f; }

                if (Input.GetKeyDown("space") || 
                    Input.GetAxis("Horizontal") != 0 || 
                    (gm.coconutBuff > 0 && Input.GetKeyDown("f")))
                {
                    if (pauseOverlay != null) { pauseOverlay.SetMaskOn(false); }
                    rigibody.gravityScale = 1.9f;
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

    //On how invincibility shows on Player
    //Player flashes during invincibility
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

    //Calls wine buff invincibility (called from item)
    public void CallWineInvincible()
    {
        StartCoroutine(WineInvincible());
    }

    //Wine buff wears off after 5 seconds
    private IEnumerator WineInvincible()
    {
        invincible = true;
        yield return new WaitForSeconds(5f);
        gm.wineBuff = false;
        invincible = false;
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    //Set duration of invincibility for when Player takes damage before it turns off
    public IEnumerator DelayOffInvincible()
    {
        yield return new WaitForSeconds(2f);
        showInvincible = false;
        invincible = false;
    }

    //To set if Player is invincible
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

    //To get if Player is on ground or not
    public bool GetGroundCheck() { return checkGround; }

    //To set if Player just spawned
    public void SetJustSpawned(bool isSpawn) { justSpawned = isSpawn; }

    public bool GetJustSpawned() {return justSpawned; }
}
