using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    private PlayerBehaviour player;

    public GameObject jumpBottom;
    public GameObject jumpTop;
    public GameObject jumpBar;
    public GameObject slider;
    public float jpFlat;
    private float jpStart;
    private float jpPerc;
    private bool jpCanSee;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
        jpStart = player.jumpStart;
        jpFlat = jpStart;
        jpCanSee = false;
    }

    void Update()
    {
        UpdSlider();
        UpdSeeThru();
        KeepReflection();
    }

    void KeepReflection()
    {
        if (this.transform.parent.transform.localScale.x < 0)
        {
            this.transform.localScale = new Vector3(-1f, 1f,1f);
        }
        else if(this.transform.parent.transform.localScale.x > 0)
        {
            this.transform.localScale = new Vector3(1f, 1f,1f);
        }
    }

    private void UpdSlider()
    {
        float dist = jumpTop.transform.position.y - jumpBottom.transform.position.y;
        jpPerc = (jpFlat - jpStart) / (1 - jpStart);
        float toPos = dist * jpPerc + jumpBottom.transform.position.y;
        float fromPos = slider.transform.position.y;
        slider.transform.Translate(0, toPos - fromPos,0);
    }

    private void UpdSeeThru()
    {
        if (jpCanSee)
        {
            jumpBottom.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            jumpTop.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            jumpBar.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            slider.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        }
        else
        {
            jumpBottom.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            jumpTop.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            jumpBar.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            slider.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        }
    }

    public void SetSeeThru(bool seeOrNot) { jpCanSee = seeOrNot; }

    public void TakeDamage()
    {
        if (heart3.gameObject.activeSelf)
        {
            heart3.GetComponent<Animator>().SetTrigger("Hurt");
            StartCoroutine(removeHeart(3));
        }
        else if (heart2.gameObject.activeSelf)
        {
            heart2.GetComponent<Animator>().SetTrigger("Hurt");
            StartCoroutine(removeHeart(2));
        }
        else
        {
            heart1.GetComponent<Animator>().SetTrigger("Hurt");
            StartCoroutine(removeHeart(1));
        }
    }

    private IEnumerator removeHeart(int i)
    {
        yield return new WaitForSeconds(0.1f);
        if (i == 3) { heart3.SetActive(false); }
        else if (i == 2) { heart2.SetActive(false); }
        else { heart1.SetActive(false); }
    }
}
