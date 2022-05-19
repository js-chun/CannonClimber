using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private PlayerBehaviour player;
    private GameManager gm;
    public GameObject landSfx;

    void Start()
    { 
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerBehaviour>(); 
    }


    void Update()
    {
        IgnoreCollisionOnInv();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!player.GetJustSpawned())
        {
            player.IsGrounded(true);
        }
        else
        {
            if (collision.gameObject.tag != "Box")
            {
                player.IsGrounded(true);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!player.GetGroundCheck()) 
        { 
            if (collision.gameObject.layer == 10 || collision.gameObject.tag == "Box")
            {
                Instantiate(landSfx, this.transform.position, Quaternion.identity, transform.parent); 
            }
        }
        if (gm.stageLevel == 2)
        {
            player.GetComponent<Rigidbody2D>().velocity += new Vector2(-1.5f, 5f);
            player.GetComponent<Animator>().SetTrigger("hitDead");
            gm.stageLevel = 3;
        }
        
        if (gm.stageLevel == 99)
        {
            player.GetComponent<Rigidbody2D>().velocity += new Vector2(1.5f, 5f);
            player.GetComponent<Animator>().SetTrigger("hitDead");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
         player.IsGrounded(false);
        
    }

    private void IgnoreCollisionOnInv()
    {
        if (player.GetInvincible())
        {
            Physics.IgnoreLayerCollision(9, 18, true);
        }
        else
        {
            Physics.IgnoreLayerCollision(9, 18, false);
        }
    }
}
