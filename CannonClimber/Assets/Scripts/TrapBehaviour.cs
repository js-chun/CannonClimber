using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehaviour : MonoBehaviour
{
    public int trapType;
    public float restTime;
    public Sprite idleSpr;
    private bool trapActive;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        trapActive = true;
    }

    void Update()
    {
        Spikes();
    }

    private void Spikes()
    {
        if (trapType == 0)
        {
            StartCoroutine(SpikeAnim());
            if(this.GetComponent<SpriteRenderer>().sprite == idleSpr) { this.GetComponent<BoxCollider2D>().enabled = false; }
            else { this.GetComponent <BoxCollider2D>().enabled = true; }
        }
    }

    private IEnumerator SpikeAnim()
    {
        if (trapActive)
        {
            trapActive = false;
            yield return new WaitForSeconds(restTime);
            anim.SetTrigger("SpikesUp");
            yield return new WaitForSeconds(restTime);
            anim.SetTrigger("SpikesDown");
            trapActive = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponentInChildren<InGameUI>().TakeDamage();
            collision.GetComponent<Rigidbody2D>().velocity += new Vector2(0f, 3f);
        }    
    }
}
