
using UnityEngine;

public class FloatTile : MonoBehaviour
{
    private FloatTileSystem ftSys;
    public bool parent;
    public FloatTile[] childs;

    void Start()
    {
        ftSys = GetComponentInParent<FloatTileSystem>();
        if (!parent) { GetComponent<BoxCollider2D>().enabled = false; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (parent)
        {
            if (collision.gameObject.tag == "Player")
            {
                ftSys.movingUp = true;
                collision.gameObject.transform.parent = this.transform;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (parent)
        {
            if (collision.gameObject.tag == "Player")
            {
                ftSys.movingUp = false;
                collision.gameObject.transform.parent = null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (parent)
        {
            if (collision.gameObject.tag == "TileStopper")
            {
                ftSys.completeStop = true;
                this.GetComponent<Animator>().SetBool("Moving", false);
                foreach (FloatTile c in childs)
                {
                    c.GetComponent<Animator>().SetBool("Moving", false);
                }
            }
        }
    }

}
