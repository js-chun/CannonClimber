using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private readonly float moveLimit = 0.2f;
    private readonly float itemSpeed = 0.5f;
    private Transform tf;

    private float minMove;
    private float maxMove;
    private bool goingUp;

    private GameManager gm;
    private PlayerBehaviour player;
    public GameObject linked;

    public int itemType;
    public int scoreBonus;
    public float rarity;

    void Start()
    {
        gm= FindObjectOfType<GameManager>();
        FindPlayer();
        tf = this.transform;

        minMove = tf.localPosition.y - moveLimit;
        maxMove = tf.localPosition.y + moveLimit;
       
        goingUp = true;
    }

    void Update()
    {
        ItemMove();
        if(player != null)
        {
            FindPlayer();
        }
    }

    public void FindPlayer()
    {
        player = FindObjectOfType<PlayerBehaviour>();
    }
    //need to adjust for when moving
    private void ItemMove()
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
            if (itemType == 0) { MapConsumed(); }
            else if (itemType == 1) { CoconutConsumed(); }
            else if (itemType == 2) { OrangeConsumed(); }
            else if (itemType == 3) { WineConsumed(); }
            else if (itemType == 4) { ScoreConsumed(); }
            Destroy(this.gameObject);
        }
    }

    private void MapConsumed()
    {
        gm.stageLevel++;
        if (gm.stageLevel  == 5)
        {
            FindObjectOfType<LevelSpawner>().SetLvl(true);
            FloorRandomizer fr = FindObjectOfType<FloorRandomizer>();
            fr.SpawnCannon(4.3f, 2.5f,true);
        }
    }

    private void CoconutConsumed()
    {
        gm.coconutBuff = 3;
    }

    private void OrangeConsumed()
    {

    }

    private void WineConsumed()
    {
        gm.wineBuff = true;
        player.CallWineInvincible();
    }

    private void ScoreConsumed()
    {
        gm.score += scoreBonus;
    }
}
