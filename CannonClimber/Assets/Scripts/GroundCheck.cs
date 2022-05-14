using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private PlayerBehaviour player;
    private GameManager gm;
    public InGameGridUI gameUI;

    void Start()
    { 
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerBehaviour>(); 
    }


    void Update()
    {
        
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
        if (gm.stageLevel == 2)
        {
            player.GetComponent<Rigidbody2D>().velocity += new Vector2(-1.5f, 5f);
            player.GetComponent<Animator>().SetTrigger("hitDead");
            gm.stageLevel = 3;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
         player.IsGrounded(false);
        
    }
}
