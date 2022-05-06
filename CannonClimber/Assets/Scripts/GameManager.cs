using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;   //Player prefab to respawn in case Player falls
    public int stageLevel = 0;  //Stage Level to determine certain events
    public int maxLives = 3;    //Maximum lives the Player has
    public int coconutBuff;     //How many kicks the Player has
    public bool wineBuff;       //If Player currently has wine buff
    public int score;           //Score of Player in game session
    

    private float peakHeight;
    private Vector3 spawnLoc;


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
        wineBuff = false;
    }

    void Update()
    {

    }

    //Set Spawn Location of Player in case they need to respawn
    public void SetSpawnLoc(float locX, float locY)
    {
        spawnLoc = new Vector3(locX, locY, -5f);
    }

    //If Player has more than 0 lives, will respawn character
    //Character will be invincible at spawn location
    public void SpawnNewChar()
    {
        if (maxLives > 0)
        {
            Instantiate(player, spawnLoc, Quaternion.identity);
            FindObjectOfType<PlayerBehaviour>().SetInvincible(true);
            FindObjectOfType<PlayerBehaviour>().SetJustSpawned(true);
        }
    }

    //Set maximum height
    public void SetPeakHeight(float y) { peakHeight = y; }

    //Get the maximum height
    public float GetPeakHeight() { return peakHeight; }

}
