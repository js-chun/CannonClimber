using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    private GameManager gm;
    public float movementSpd = 0.8f;
    private bool stopMoving;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        tileMove();
        if (gm.menuStage == 2)
        {
            StartCoroutine(comeToHalt());
        }
    }

    private void tileMove()
    {
        if (!stopMoving)
        {
            transform.localPosition -= new Vector3(movementSpd, 0, 0) * Time.deltaTime;
        }
    }

    private IEnumerator comeToHalt()
    {
        yield return new WaitForSeconds(1.5f);
        stopMoving = true;
    }
}
