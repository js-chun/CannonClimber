using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cannon" || collision.gameObject.tag == "CannonBall") 
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {

        }
    }

}
