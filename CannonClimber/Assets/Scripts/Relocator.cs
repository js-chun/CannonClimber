using UnityEngine;

public class Relocator : MonoBehaviour
{
    public GameObject toLocation;

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.position = toLocation.transform.position;
    }
}
