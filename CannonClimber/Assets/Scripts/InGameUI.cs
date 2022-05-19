using System.Collections;
using UnityEngine;

//Class for UI on Player (Lives and Jump Slider)
public class InGameUI : MonoBehaviour
{
    private GameManager gm;             
    private PlayerBehaviour player;

    //For Jump Slider
    public GameObject jumpBottom;   //Bottom of the Jump Slider
    public GameObject jumpTop;      //Top of the Jump Slider
    public GameObject jumpBar;      //Middle bar of the Jump Slider
    public GameObject slider;       //Slider indicator (filler)
    public float jpFlat;            //Flat jump power
    private float jpStart;          //Start % for jump power
    private float jpPerc;           //Flat jump power as %
    private bool jpCanSee;          //If Jump Slider is visible or not

    //For Lives
    public GameObject heartContainer;   //Grouped hearts
    public GameObject heart1;           //Heart object for 1st life
    public GameObject heart2;           //Heart object for 2nd life
    public GameObject heart3;           //Heart object for 3rd life
    private bool canTakeDamage;         //Whether Player can take damage or not

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerBehaviour>();
        jpStart = player.jumpStart;
        jpFlat = jpStart;
        jpCanSee = false;
        canTakeDamage = true;
        UpdateRemovedHearts();
    }

    void Update()
    {
        UpdSlider();
        UpdSeeThru();
        KeepReflection();
    }

    //Keeps UI one way regardless of Player direction
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

    //Updates Jump Slider bar to reflect Jump Power
    private void UpdSlider()
    {
        float dist = jumpTop.transform.position.y - jumpBottom.transform.position.y;
        jpPerc = (jpFlat - jpStart) / (1 - jpStart);
        float toPos = dist * jpPerc + jumpBottom.transform.position.y;
        float fromPos = slider.transform.position.y;
        slider.transform.Translate(0, toPos - fromPos,0);
    }

    //Keeps Jump Slider visible or not
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

    //Used in PlayerBehaviour to set whether Jump Slider is visible or not
    public void SetSeeThru(bool seeOrNot) { jpCanSee = seeOrNot; }

    //Takes damage if the Player can take damage
    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            if(gm.maxLives > 0)
            {
                gm.maxLives--;
                UpdateRemovedHearts();
                player.SetInvincible(true);
                StartCoroutine(player.DelayOffInvincible());
            }
        }
        else { canTakeDamage = true; }
    }

    private void UpdateRemovedHearts()
    {
        if(gm.maxLives < 3) { removeHeart(3); }
        if(gm.maxLives < 2) { removeHeart(2); }
        if(gm.maxLives < 1) { removeHeart(1); } 
    }

    private void removeHeart(int i)
    {
        if (i == 3) { heart3.GetComponent<Animator>().SetTrigger("Hurt"); }
        else if (i == 2) { heart2.GetComponent<Animator>().SetTrigger("Hurt"); }
        else { heart1.GetComponent<Animator>().SetTrigger("Hurt"); }
    }

    public void addHeart(int i)
    {
        if (i==3) { heart3.GetComponent<Animator>().SetTrigger("Add"); }
        else if (i == 2) { heart2.GetComponent<Animator>().SetTrigger("Add"); }
        else { heart1.GetComponent<Animator>().SetTrigger("Add"); }
    }

    public void HideHearts(bool hide)
    {
        heartContainer.SetActive(!hide);
    }

    public void CantTakeDam()
    {
        StartCoroutine(NoDamWindow());
    }

    private IEnumerator NoDamWindow()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.1f);
        canTakeDamage = true;
    }

}
