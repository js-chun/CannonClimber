using UnityEngine;

public class CannonBallBehaviour : MonoBehaviour
{
    public float ballSpeed = 2.5f;        //how fast the cannon ball will go
    public GameObject explosionAnim;    //explosion prefab for when it hits
    private float ballPower = 3f;

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
            //Vector2 pushback = (collision.transform.localPosition - this.transform.position) * ballPower;
            //collision.gameObject.GetComponent<Rigidbody2D>().velocity += pushback;
            //need to add player dmg
            Debug.Log("Player");
        }
        else if (collision.gameObject.tag == "Block")
        {
            Debug.Log("Kicked");
        }
        GameObject explosion = Instantiate(explosionAnim, this.transform.position, Quaternion.identity, transform.parent);
        Destroy(this.gameObject);
    }

    public void SetBallPower(float power)
    {
        ballPower = power;
    }

    public float GetBallPower()
    {
        return ballPower;
    }
}
