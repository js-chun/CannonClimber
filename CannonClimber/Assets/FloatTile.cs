using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatTile : MonoBehaviour
{
    private FloatTileSystem ftSys;
    void Start()
    {
        ftSys = GetComponentInParent<FloatTileSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ftSys.SetMovingUp(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ftSys.SetMovingUp(false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!ftSys.completeStop)
            {
                collision.transform.position += new Vector3(0f, ftSys.speed * Time.deltaTime, 0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TileStopper") { ftSys.completeStop = true; }
    }
}
