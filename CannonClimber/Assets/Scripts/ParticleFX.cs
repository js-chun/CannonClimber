using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFX : MonoBehaviour
{
    private Animator anim;
    private float animTime;
    void Start()
    {
        anim = GetComponent<Animator>();
        startAnim();
    }

    private void startAnim()
    {
        animTime = anim.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(destroyParticle());
    }

    private IEnumerator destroyParticle()
    {
        yield return new WaitForSeconds(animTime + 0.1f);
        Destroy(this.gameObject);
    }
}
