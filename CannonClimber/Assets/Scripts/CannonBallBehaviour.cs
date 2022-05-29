using UnityEngine;

public class CannonBallBehaviour : MonoBehaviour
{
    private GameManager gm;
    public float ballSpeed = 2.5f;        //how fast the cannon ball will go
    public int addScore = 15;
    public GameObject explosionAnim;    //explosion prefab for when it hits
    public GameObject audioSfx;
    public GameObject scoreFx;
    

    void Update()
    {
        gm = FindObjectOfType<GameManager>();
        BallMove();
    }

    private void BallMove()
    {
        transform.localPosition -= new Vector3(ballSpeed, 0, 0) * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInChildren<InGameUI>().TakeDamage();
            if(gm.stageLevel == 1)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(5f,4f);
            }
        }
        else if (collision.gameObject.tag == "Block")
        {
            FindObjectOfType<PlayerBehaviour>().jumpCount = 0;
            gm.score += addScore;
            GameObject scoreEffect = Instantiate(scoreFx, transform.position, Quaternion.identity);
            scoreEffect.GetComponent<ParticleFX>().SetScore(addScore);
            
        }
        Detonate();
    }

    public void Detonate()
    {
        Instantiate(audioSfx, this.transform.position, Quaternion.identity, transform.parent);
        Instantiate(explosionAnim, this.transform.position, Quaternion.identity, transform.parent);
        Destroy(this.gameObject);
    }
}
