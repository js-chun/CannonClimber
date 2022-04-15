using UnityEngine;

public class CannonBallBehaviour : MonoBehaviour
{
    public float ballSpeed = 3f;        //how fast the cannon ball will go
    public GameObject explosionAnim;    //explosion prefab for when it hits
    private float ballPower = 3f;

    void Update()
    {
        ballMove();
    }

    private void ballMove()
    {
        transform.localPosition -= new Vector3(ballSpeed, 0, 0) * Time.deltaTime;
        //add rotation?
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject explosion = Instantiate(explosionAnim, this.transform.position, Quaternion.identity, transform.parent);
            Destroy(this.gameObject);

            //need to add player dmg
        }   
    }

    public void setBallPower(float power)
    {
        ballPower = power;
    }

    public float getBallPower()
    {
        return ballPower;
    }
}
