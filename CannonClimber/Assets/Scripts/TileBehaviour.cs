using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    public float movementSpd = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tileMove();
    }

    private void tileMove()
    {
        transform.localPosition -= new Vector3(movementSpd, 0, 0) * Time.deltaTime;
    }
}
