using UnityEngine;

public class FloorIndicator : MonoBehaviour
{
    public int floorNumber;     //floor number for the floor indicator
    private bool visited;       //bool for whether Player has already hit this level
    private GameManager gm;


    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        visited = false;
    }

    void Update()
    {
        CheckStart();
    }

    //For initial start, to make sure triggers are not on until Player is in charge of character
    private void CheckStart()
    {
        if (gm.stageLevel > 2)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    //if Player is in Floor Indicator, checks if they are grounded
    //if floor is higher than the previous number, updates record
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.GetComponent<PlayerBehaviour>().GetGroundCheck())
            {
                if (!visited && gm.playerFloors < floorNumber)
                {
                    gm.playerFloors = floorNumber;
                    visited = true;
                }
            }
        }
    }

    //Destroys Floor Indicator based on whether Player exits the trigger
    //If record is equivalent or higher, destroys
    //Otherwise gets destroyed by shredder at the bottom
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(visited || gm.playerFloors >= floorNumber)
            {
                Destroy(this.gameObject);
            }
        }
        if(collision.gameObject.tag == "Shredder")
        {
            Destroy(this.gameObject);
        }
    }
}
