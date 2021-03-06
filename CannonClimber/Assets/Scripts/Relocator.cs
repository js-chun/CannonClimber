using UnityEngine;

public class Relocator : MonoBehaviour
{
    private GameManager gm;
    public GameObject toLocation;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gm.stageLevel == 1)
        {
            Destroy(collision.gameObject);
        }
        else
        {
            collision.transform.position = toLocation.transform.position;
        }
    }

}
