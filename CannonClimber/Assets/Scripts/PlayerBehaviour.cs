using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private GameManager gm;
    private Animator anim;
    public float moveSpeed = 4f;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!gm.isMenuState)
        {
            canMove();
            canJump();
        }
        else
        {
            menuLoad();
        }
    }

    private void canMove()
    {
        float moveFt = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        if(moveFt != 0)
        {
            anim.SetBool("isMoving", true);
            if (moveFt > 0)
            {
                this.transform.localScale = new Vector3(2, 2, 1);
            }
            else if (moveFt < 0)
            {
                this.transform.localScale = new Vector3(-2, 2, 1);
            }
            this.transform.Translate(moveFt, 0, 0);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    private void canJump()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CannonBall")
        {
            Vector2 pushback = (this.transform.position - collision.transform.position) * collision.gameObject.GetComponent<CannonBallBehaviour>().getBallPower();
            Debug.Log(pushback);
            this.GetComponent<Rigidbody2D>().velocity += pushback;
        }
    }

    private void menuLoad()
    {
        if (gm.menuStage == 1)
        {
            anim.SetBool("isMoving", true);
        }
        else if (gm.menuStage == 2)
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
        if (grounded) { anim.SetBool("isGrounded", true); }
        else { anim.SetBool("isGrounded", false); }
    }
}
