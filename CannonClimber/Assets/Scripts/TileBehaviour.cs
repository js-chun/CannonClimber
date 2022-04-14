using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    public float movementSpd = 0.8f;

    void Start()
    {
        
    }

    void Update()
    {
        tileMove();
    }

    private void tileMove()
    {
        transform.localPosition -= new Vector3(movementSpd, 0, 0) * Time.deltaTime;
    }
}
