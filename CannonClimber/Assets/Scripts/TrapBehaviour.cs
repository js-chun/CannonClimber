using System.Collections;
using UnityEngine;

public class TrapBehaviour : MonoBehaviour
{
    public int trapType;        //Trap type. Spike = 0, Spear = 1
    public float spikeRestTime; //Time between traps
    public Sprite idleSpr;      //Sprite on which trigger is not active
    private Animator anim;

    private bool spikeActive;    //Spike Trap is active or not

    void Start()
    {
        anim = GetComponent<Animator>();
        spikeActive = true;
    }

    void Update()
    {
        TriggerOnOff();
        Spikes();
    }

    private void TriggerOnOff()
    {
        if (trapType == 0)
        {
            if (this.GetComponent<SpriteRenderer>().sprite == idleSpr) { this.GetComponent<BoxCollider2D>().enabled = false; }
            else { this.GetComponent<BoxCollider2D>().enabled = true; }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponentInChildren<InGameUI>().TakeDamage();
            if (trapType == 0)
            {
                collision.GetComponent<Rigidbody2D>().velocity += new Vector2(0f, 3f);
            }
        }
    }

    private void Spikes()
    {
        if (trapType == 0) { StartCoroutine(SpikeAnim()); }
    }

    private IEnumerator SpikeAnim()
    {
        if (spikeActive)
        {
            spikeActive = false;
            yield return new WaitForSeconds(spikeRestTime);
            anim.SetTrigger("SpikesUp");
            yield return new WaitForSeconds(spikeRestTime);
            anim.SetTrigger("SpikesDown");
            spikeActive = true;
        }
    }

    public void SpearsUp()
    {
        if (trapType == 1) { anim.SetTrigger("SpearsUp"); }
    }

    public void SpearsDown()
    {
        if (trapType == 1) { anim.SetTrigger("SpearsDown"); }
    }

}
