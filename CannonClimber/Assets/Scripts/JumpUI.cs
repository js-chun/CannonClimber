using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpUI : MonoBehaviour
{
    private PlayerBehaviour player;

    public GameObject bottom;
    public GameObject top;
    public GameObject bar;
    public GameObject slider;

    public float flat;
    private float start;
    private float perc;
    private bool canSee;

    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
        start = player.jumpStart;
        flat = start;
        canSee = false;
    }

    void Update()
    {
        updateSlider();
        updSeeThru();
    }

    private void updateSlider()
    {
        float dist = top.transform.position.y - bottom.transform.position.y;
        perc = (flat - start) / (1 - start);
        float toPos = dist * perc + bottom.transform.position.y;
        float fromPos = slider.transform.position.y;
        slider.transform.Translate(0, toPos - fromPos, 0);
    }

    private void updSeeThru()
    {
        if (canSee)
        {
            bottom.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            top.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            bar.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            slider.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        }
        else
        {
            bottom.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            top.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            bar.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            slider.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        }
    }

    public void setSeeThru(bool seeOrNot) { canSee = seeOrNot; }
}
