using UnityEngine;

//Class for items
public class Item : MonoBehaviour
{
    private readonly float moveLimit = 0.2f;    //How much items move up or down from center
    private readonly float itemSpeed = 0.5f;    //How fast items move
    private Transform tf;                       //Center point

    private float minMove;          //Lowest point item moves
    private float maxMove;          //Highest point item moves
    private bool goingUp;           //Bool for when item is going up

    private GameManager gm;         
    public GameObject linked;       //Linked is specifically for Map to activate levels
    public GameObject particleFx;
    public GameObject scoreFx;

    public int itemType;            //Governs item type, 0 = map, 1 = coconut, 2 = orange, 3 = wine, 4 = score
    public int scoreBonus;          //How much points a score item gives
    public float rarity;
    
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
        ItemMove();
    }

    //Moves the item up and down
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


    //When Player collides with it, triggers methods based on what type of item
    //Destroys item after triggering method
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CreateFx();
            if (itemType == 0) { MapConsumed(); }
            else if (itemType == 1) { CoconutConsumed(); }
            else if (itemType == 2) { OrangeConsumed(); }
            else if (itemType == 3) { WineConsumed(); }
            else if (itemType == 4) { ScoreConsumed(); }
            Destroy(this.gameObject);
        }
    }


    //When map is used, starts the level
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

    //When coconut is used, gives player kick charges
    private void CoconutConsumed() { gm.coconutBuff = 3; }

    //When orange is used, gives player a life back
    private void OrangeConsumed()
    {
        if(gm.maxLives < 3)
        {
            gm.maxLives++;
        }
    }

    //When wine is used, gives player temporary wine buff
    private void WineConsumed()
    {
        FindObjectOfType<PlayerBehaviour>().CallWineInvincible();
    }

    //When score item is used, adds score
    private void ScoreConsumed()
    {
        gm.score += scoreBonus; 
        if(scoreFx != null)
        {
            GameObject fx = Instantiate(scoreFx, this.transform.position, Quaternion.identity);
            fx.GetComponent<ParticleFX>().setScore(scoreBonus);
        }
    }


    //To create a vfx when consumed by Player
    private void CreateFx()
    {
        if(particleFx != null)
        {
            Instantiate(particleFx, this.transform.position, Quaternion.identity);
        }
    }
}
