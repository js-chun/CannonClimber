using System.Collections;
using UnityEngine;

public class SpearLevel : MonoBehaviour
{
    public TrapBehaviour[] left;
    public TrapBehaviour[] right;
    public float spearRestTime;

    private bool trapActive;
    private bool started;

    private TrapBehaviour[] first;
    private TrapBehaviour[] second;

    void Start()
    {
        RandomLeftRight();
        trapActive = true;
        started = false;
    }


    void Update()
    {
        Spears();
    }

    private void RandomLeftRight()
    {
        bool leftFirst = true;
        if (Random.Range(0f, 1f) <= 0.5f) { leftFirst = false; }
        if (leftFirst)
        {
            first = left;
            second = right;
        }
        else
        {
            first = right;
            second = left;
        }
    }

    private void Spears()
    {
        StartCoroutine(SpearsUpDown());
    }

    private IEnumerator SpearsUpDown()
    {
        if (trapActive)
        {
            trapActive = false;
            if (!started)
            {
                SpearsUp(first);
                SpearsDown(second);
                started = true;
            }
            yield return new WaitForSeconds(spearRestTime);
            SpearsUp(second);
            SpearsDown(first);
            yield return new WaitForSeconds(spearRestTime);
            SpearsUp(first);
            SpearsDown(second);
            trapActive = true;
        }
        
    }

    private void SpearsUp(TrapBehaviour[] spears)
    {
        foreach (TrapBehaviour s in spears)
        {
            s.SpearsUp();
        }
    }

    private void SpearsDown(TrapBehaviour[] spears)
    {
        foreach (TrapBehaviour s in spears)
        {
            s.SpearsDown();
        }
    }
}
