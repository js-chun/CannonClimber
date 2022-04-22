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
        StartAnim();
    }

    private void StartAnim()
    {
        animTime = anim.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(DestroyParticle());
    }

    private IEnumerator DestroyParticle()
    {
        yield return new WaitForSeconds(animTime + 0.1f);
        Destroy(this.gameObject);
    }
}
