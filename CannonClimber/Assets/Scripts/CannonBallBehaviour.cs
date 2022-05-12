using UnityEngine;

public class CannonBallBehaviour : MonoBehaviour
{
    public float ballSpeed = 2.5f;        //how fast the cannon ball will go
    public GameObject explosionAnim;    //explosion prefab for when it hits

    void Update()
    {
        BallMove();
    }

    private void BallMove()
    {
        transform.localPosition -= new Vector3(ballSpeed, 0, 0) * Time.deltaTime;
        //add rotation?
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInChildren<InGameUI>().TakeDamage();
            //need to add player dmg
            Debug.Log("Player");
            if(FindObjectOfType<GameManager>().stageLevel == 1)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(5f,4f);
            }
        }

        else if (collision.gameObject.tag == "Block")
        {
            Debug.Log("Kicked");
            FindObjectOfType<PlayerBehaviour>().jumpCount = 0;
        }
        Detonate();
    }

    public void Detonate()
    {
        GameObject explosion = Instantiate(explosionAnim, this.transform.position, Quaternion.identity, transform.parent);
        Destroy(this.gameObject);
    }
}
