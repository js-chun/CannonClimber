using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehaviour : MonoBehaviour
{
    public GameObject CannonBall;
    public GameObject BallPoint;

    private bool isFiring;
    private int numFired;
    public float delayTime = 1f;

    void Start()
    {
        isFiring = false;
        numFired = 0;
    }

    void Update()
    {
        Fire();
    }
    
    private void Fire()
    {
        if (isFiring == true)
        {
            if (numFired == 0)
            {
                GameObject fireBall = Instantiate(CannonBall, BallPoint.transform.position, Quaternion.identity, transform);
                numFired += 1;
                StartCoroutine(delayOnFire());
            }
        }
    }

    private IEnumerator delayOnFire()
    {
        yield return new WaitForSeconds(delayTime);
        numFired -= 1;
    }

    public void setFireOnOff(bool enemyThere)
    {
        if (enemyThere) { isFiring = true;}
        else { isFiring = false; }
    }
}
