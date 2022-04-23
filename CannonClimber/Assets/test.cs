using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class test : MonoBehaviour
{
    public TileGrid bot;
    public TileGrid top;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInChildren<InGameUI>().TakeDamage();
        }
    }
}
