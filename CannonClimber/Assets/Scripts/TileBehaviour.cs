using System.Collections;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    private GameManager gm;
    public float movementSpd = 0.8f;
    private bool stopMoving;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        TileMove();
        if (gm.stageLevel == 1)
        {
            StartCoroutine(ComeToHalt());
        }
    }

    private void TileMove()
    {
        if (!stopMoving)
        {
            transform.position -= new Vector3(movementSpd, 0, 0) * Time.deltaTime;
        }
    }

    private IEnumerator ComeToHalt()
    {
        yield return new WaitForSeconds(1.5f);
        stopMoving = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Boundary")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (gm.stageLevel > 2)
            {
                collision.gameObject.transform.position -= new Vector3(movementSpd, 0, 0) * Time.deltaTime;
            }
        }
    }
}
