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
        TileMove();
        if (gm.stageLevel == 1)
        {
            StartCoroutine(ComeToHalt());
        }
    }

    private void TileMove()
    {
        if (!stopMoving)
        {
            transform.localPosition -= new Vector3(movementSpd, 0, 0) * Time.deltaTime;
        }
    }

    private IEnumerator ComeToHalt()
    {
        yield return new WaitForSeconds(1.5f);
        stopMoving = true;
    }
}
