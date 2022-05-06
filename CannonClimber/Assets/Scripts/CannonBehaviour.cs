using System.Collections;
using UnityEngine;

public class CannonBehaviour : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spRend;
    private GameManager gm;

    public float fireSpeed = 0.2f;
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
        MenuPlay();
        
        Fire();

        if (isFiring) { anim.SetBool("IsFiring", true); }
        else { anim.SetBool("IsFiring", false); }

    }

    //add a color for aggro and about to fire

    private void Fire()
    {
        if (isFiring == true)
        {
            anim.speed = fireSpeed;
            if (spRend.sprite == fireSprite)
            {
                if(numFired == 0)
                {
                    numFired += 1;
                    Vector3 loc = BallPoint.transform.position + new Vector3(0f, 0f, -5f);
                    GameObject fireBall = Instantiate(CannonBall, loc, Quaternion.identity, transform);
                    GameObject fireFx = Instantiate(LaunchFx, loc, Quaternion.identity, transform);
                }
            }
            else
            {
                numFired = 0;
            }
        }
    }

    public void SetFireOnOff(bool enemyThere)
    {
        if (enemyThere) { isFiring = true; }
        else { isFiring = false; }
    }

    private void MenuPlay()
    {
        if(gm.stageLevel == 1)
        {
            StartCoroutine(SwitchGravity());
        }
    }

    private IEnumerator SwitchGravity()
    {
        yield return new WaitForSeconds(2.5f);
        GetComponent<Rigidbody2D>().gravityScale = 1f;
    }
}
