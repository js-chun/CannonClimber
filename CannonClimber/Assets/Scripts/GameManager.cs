using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public int stageLevel = 0;
    public int maxLives = 3;
    public int coconutBuff;
    public int score;
    
    private float peakHeight;
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
        peakHeight = 0f;
        coconutBuff = 0;
    }

    void Update()
    {

    }

    public void SetSpawnLoc(float locX, float locY)
    {
        spawnLoc = new Vector2(locX, locY);
    }

    public void SpawnNewChar()
    {
        if (maxLives > 0)
        {
            Instantiate(player, spawnLoc, Quaternion.identity);
        }
    }

    public void SetPeakHeight(float y) { peakHeight = y; }

    public float GetPeakHeight() { return peakHeight; }
}
