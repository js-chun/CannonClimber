using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int stageLevel = 0;
    public int maxLives = 3;
    public int score;

    private Vector2 spawnLoc;

    private void Awake()
    {
        int sessionCount = FindObjectsOfType<GameManager>().Length;
        if (sessionCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        score = 0;
    }

    void Update()
    {

    }

    public void setSpawnLoc(float locX, float locY)
    {
        spawnLoc = new Vector2(locX, locY);
    }

}
