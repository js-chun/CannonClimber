using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    public string shredTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == shredTag)
        {
            Destroy(collision.gameObject);
        }
    }
}
