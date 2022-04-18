using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private float moveLimit = 0.2f;
    private float itemSpeed = 0.5f;
    private Transform tf;
    private float minMove;
    private float maxMove;
    private bool goingUp;

    private GameManager gm;
    public GameObject linked;

    public int iType; //0 = map,
    public int scoreBonus;
    public int lifeBonus;

    void Start()
    {
        gm= FindObjectOfType<GameManager>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (iType == 0) { mapConsumed(); }
            Destroy(this.gameObject);
        }
    }

    private void mapConsumed()
    {
        gm.stageLevel++;
        if (gm.stageLevel  == 4)
        {
            if(linked != null) 
            { 
                linked.SetActive(true);
                linked.GetComponent<LevelBehaviour>().setLvl(true);
            }
        }
    }

}
