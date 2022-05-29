using UnityEngine;

public class CannonBallTopBehaviour : MonoBehaviour
{
    private CannonBallBehaviour cb;
    public GameObject scoreFx;
    public int addScore;

    void Start()
    {
        cb = GetComponentInParent<CannonBallBehaviour>();
        addScore = 5;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Grounded")
    //    {
    //        Debug.Log("Top");
    //        collision.gameObject.GetComponentInChildren<InGameUI>().CantTakeDam();
    //        collision.gameObject.GetComponent<PlayerBehaviour>().jumpCount = 0;
    //        collision.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(0, 5f);
    //        cb.Detonate();
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Grounded")
        {
            FindObjectOfType<InGameUI>().CantTakeDam();
            collision.gameObject.GetComponentInParent<PlayerBehaviour>().jumpCount = 0;
            collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity += new Vector2(0, 10f);
            FindObjectOfType<GameManager>().score += addScore;
            GameObject scoreEffect = Instantiate(scoreFx, this.transform.position, Quaternion.identity);
            scoreEffect.GetComponent<ParticleFX>().SetScore(addScore);
            cb.Detonate();
        }
    }
}
