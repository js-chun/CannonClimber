using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehaviour : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spRend;
    private GameManager gm;

    public GameObject CannonBall;   //cannon ball prefab to shoot
    public GameObject BallPoint;    //where cannon ball instantiates + player detection range
    public Sprite fireSprite;       //sprite in animation when cannonball should launch
    public GameObject LaunchFx;     //launch particle prefab

    private bool isFiring;
    private int numFired;
    public float cannonPower = 3f;

    //need to add a controllable timer to cannon firing

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        spRend = GetComponent<SpriteRenderer>();
        isFiring = false;
        numFired = 0;
    }

    void Update()
    {
        menuPlay();
        
        Fire();

        if (isFiring) { anim.SetBool("IsFiring", true); }
        else { anim.SetBool("IsFiring", false); }

    }

    private void Fire()
    {
        if (isFiring == true)
        {
            if (spRend.sprite == fireSprite)
            {
                if(numFired == 0)
                {
                    numFired += 1;
                    Vector3 loc = BallPoint.transform.position + new Vector3(0f, 0f, -5f);
                    GameObject fireBall = Instantiate(CannonBall, loc, Quaternion.identity, transform);
                    fireBall.GetComponent<CannonBallBehaviour>().setBallPower(cannonPower);
                    GameObject fireFx = Instantiate(LaunchFx, loc, Quaternion.identity, transform);
                }
            }
            else
            {
                numFired = 0;
            }
        }
    }

    public void setFireOnOff(bool enemyThere)
    {
        if (enemyThere) { isFiring = true; }
        else { isFiring = false; }
    }

    private void menuPlay()
    {
        if(gm.stageLevel == 1)
        {
            StartCoroutine(switchGravity());
        }
    }

    private IEnumerator switchGravity()
    {
        yield return new WaitForSeconds(2.5f);
        GetComponent<Rigidbody2D>().gravityScale = 1f;
    }
}
