using UnityEngine;

public class CannonBallBehaviour : MonoBehaviour
{
    public float ballSpeed = 3f;        //how fast the cannon ball will go
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
            //need to add player dmg
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
