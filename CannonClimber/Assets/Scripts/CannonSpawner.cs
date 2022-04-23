using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSpawner : MonoBehaviour
{
    public GameObject cannon;
    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void SpawnCannon(float locX, float locY, bool faceRight)
    {
        if (faceRight)
        {
            locX = -(locX);
        }
        GameObject newCan = Instantiate(cannon, new Vector2(locX, locY), Quaternion.identity, transform);
        if (faceRight) { newCan.transform.localScale = new Vector2(-2f, 2f); }
        Debug.Log(locX);
        Debug.Log(locY);
    }
}
