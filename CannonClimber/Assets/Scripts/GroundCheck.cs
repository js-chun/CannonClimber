using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerBehaviour plyr;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(':');
        plyr.isGrounded(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        plyr.isGrounded(false);
    }
}
