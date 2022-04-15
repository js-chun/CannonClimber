using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private float moveLimit = 0.2f;
    private float itemSpeed = 0.5f;
    private Transform tf;
    public float minMove;
    public float maxMove;
    private bool goingUp;

    public int scoreBonus;
    public int lifeBonus;

    void Start()
    {
        tf = this.transform;
        minMove = tf.localPosition.y - moveLimit;
        maxMove = tf.localPosition.y + moveLimit;

        goingUp = true;
    }

    void Update()
    {
        itemMove();

    }

    //need to adjust for when moving
    private void itemMove()
    {
        if (goingUp)
        {
            tf.localPosition += new Vector3(0, itemSpeed * Time.deltaTime);
        }
        else
        {
            tf.localPosition -= new Vector3(0, itemSpeed * Time.deltaTime);
        }
        
        if (tf.localPosition.y >= maxMove)
        {
            goingUp = false;
        }
        else if(tf.localPosition.y <= minMove)
        {
            goingUp = true;
        }
    }

    //need to add a collider
}
