using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearTile : MonoBehaviour
{
    private bool invisible;
    private float alpha;
    private float speed = 1f;

    public bool actsAlone;
    public float waitTime;
    void Start()
    {
        invisible = true;
        alpha = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Visibility();
        Colliding();

        if (actsAlone)
        {
            StartCoroutine(ToggleOnOff());
        }
    }

    public void SetInvisible(bool onOrOff) { invisible = onOrOff; }

    private void Visibility()
    {
        if (invisible)
        {
            if(alpha > 0f)
            {
                alpha -= speed * Time.deltaTime;
            }
        }
        else
        {
            if (alpha < 1f)
            {
                alpha += speed * Time.deltaTime;
            }
        }
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
        SpriteRenderer[] srChild = GetComponentsInChildren<SpriteRenderer>();
        if (srChild != null)
        {
            foreach(SpriteRenderer sr in srChild) { sr.color = new Color(1f, 1f, 1f, alpha); }
        }
    }
    
    private void Colliding()
    {
        BoxCollider2D[] boxChild = GetComponentsInChildren<BoxCollider2D>();
        if (alpha <= 0.5f)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            if (boxChild != null)
            {
                foreach (BoxCollider2D bc in boxChild) { bc.enabled = false; }
            }
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
            if (boxChild != null)
            {
                foreach (BoxCollider2D bc in boxChild) { bc.enabled = true; }
            }
        }
    }

    private IEnumerator ToggleOnOff()
    {
        if (invisible)
        {
            yield return new WaitForSeconds(waitTime);
            invisible = false;
            yield return new WaitForSeconds(waitTime);
            invisible = true;
        }
    }

}
