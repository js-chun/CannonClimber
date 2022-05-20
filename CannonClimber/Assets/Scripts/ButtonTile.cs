using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTile : MonoBehaviour
{
    public Sprite activeSp;
    public Sprite inactiveSp;
    private bool active;

    public FloatTileSystem ftSys;
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        ActiveShow();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") { active = true; }
        if (ftSys != null) { ftSys.movingUp = true; }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") { active = false; }
        if (ftSys != null) { ftSys.movingUp = false; }
    }

    private void ActiveShow()
    {
        if (active) { GetComponent<SpriteRenderer>().sprite = activeSp; }
        else { GetComponent<SpriteRenderer>().sprite = inactiveSp; }
    }

    public bool IsButtonActive() { return active; }
}
