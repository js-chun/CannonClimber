using System.Collections;
using UnityEngine;


//Class for moving boxes
public class TileBehaviour : MonoBehaviour
{
    private GameManager gm;
    public float movementSpd = 0.8f;    //how fast the boxes move
    private bool stopMoving;            //if boxes need to stop moving
    public bool moveLeft = false;       //if boxes should go left or right


    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    //Tiles are moving except in menu at stage level 1 (after clicking play) where it stops
    void Update()
    {
        TileMove();
        if (gm.stageLevel == 1)
        {
            StartCoroutine(ComeToHalt());
        }
    }

    //For box movements. Moving left or right
    private void TileMove()
    {
        if (!stopMoving)
        {
            if(!moveLeft)
            {
                transform.position += new Vector3(movementSpd, 0, 0) * Time.deltaTime;
            }
            else
            {
                transform.position -= new Vector3(movementSpd, 0, 0) * Time.deltaTime;
            }
        }
    }


    //For delayed stop in menu
    private IEnumerator ComeToHalt()
    {
        yield return new WaitForSeconds(1.5f);
        stopMoving = true;
    }


    //When box collides with the boundary, it gets destroyed
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Boundary" || collision.gameObject.tag == "Shredder")
        {
            Destroy(this.gameObject);
        }
    }

    //When box is colliding with Player, will move the Player along with the box
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (gm.stageLevel > 2)
            {
                if (!collision.gameObject.GetComponent<PlayerBehaviour>().GetJustSpawned())
                {
                    if (!moveLeft)
                    {
                        collision.gameObject.transform.position += new Vector3(movementSpd, 0, 0) * Time.deltaTime;
                    }
                    else
                    {
                        collision.gameObject.transform.position -= new Vector3(movementSpd, 0, 0) * Time.deltaTime;
                    }
                }
            }
        }
    }

}
