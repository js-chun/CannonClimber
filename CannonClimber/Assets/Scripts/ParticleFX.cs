using System.Collections;
using TMPro;
using UnityEngine;

//Class for particles
public class ParticleFX : MonoBehaviour
{
    private Animator anim; 
    private float animTime;             //How long the animation time is for the fx

    public bool isScoreFX;              //If it is a +## score particle or not
    public TextMeshProUGUI plusScore;   //Text reference for score particle

    void Start()
    {
        if (!isScoreFX)
        {
            anim = GetComponent<Animator>();
            StartAnim();
        }
    }

    void Update()
    {
        if (isScoreFX) { moveUp(); }
    }


    //If particle has an animation, destroys the particle object after full animation
    private void StartAnim()
    {
        if(anim != null)
        {
            animTime = anim.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(DestroyParticleAnim());
        }
    }

    private IEnumerator DestroyParticleAnim()
    {
        yield return new WaitForSeconds(animTime + 0.1f);
        Destroy(this.gameObject);
    }

    //If particle is for +## score, then no animation. Destroys object after flat time
    private IEnumerator DestroyScore()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
    }

    //Moves score particle up
    private void moveUp()
    {
        if (isScoreFX)
        {
            this.transform.position += new Vector3(0f, 1f * Time.deltaTime,0f);
        }
    }

    //Called from Item class to set score number and destory score object when item is consumed
    public void setScore(int score)
    {
        if(plusScore != null)
        {
            plusScore.text = "+" + score.ToString();
            StartCoroutine(DestroyScore());
        }
    }
}
