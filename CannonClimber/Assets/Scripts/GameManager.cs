using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;   //Player prefab to respawn in case Player falls
    private PauseOverlay pauseOverlay;

    public int stageLevel = 0;  //Stage Level to determine certain events
    public int maxLives = 3;    //Maximum lives the Player has
    public int coconutBuff;     //How many kicks the Player has
    public bool wineBuff;       //If Player currently has wine buff
    public int score;           //Score of Player in game session
    private bool pauseGame;

    public int playerFloors;    //What floor the Player has hit
    public int createdFloors;   //What floor level the map has generated
    private float peakHeight;
    private Vector3 spawnLoc;   //Spawn location of Player in case they fall

    //Make sure there is one GameManager object at a time
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
        ResetGame();
    }

    void Update()
    {
        if (stageLevel > 2 && stageLevel < 99)
        {
            if (Input.GetKeyDown("p"))
            {
                pauseGame = !pauseGame;
            }
            PauseGame();
        }
    }

    public void ResetGame()
    {
        score = 0;
        peakHeight = 0f;
        coconutBuff = 0;
        wineBuff = false;
        playerFloors = 0;
        createdFloors = 5;
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
            wineBuff = false;
            Instantiate(player, spawnLoc, Quaternion.identity);

            if (pauseOverlay == null) { pauseOverlay = FindObjectOfType<PauseOverlay>(); }
            if (pauseOverlay != null) { pauseOverlay.FindPlayer(); }

            FindObjectOfType<PlayerBehaviour>().SetInvincible(true);
            FindObjectOfType<PlayerBehaviour>().SetJustSpawned(true);
        }
        else
        {
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator GameOver()
    {
        stageLevel = 99;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("EndScene");
    }

    //Set maximum height
    public void SetPeakHeight(float y) { peakHeight = y; }

    //Get the maximum height
    public float GetPeakHeight() { return peakHeight; }

    public void SetPaused(bool paused) { pauseGame = paused; }

    //If game is paused, stops time and activates pause overlay
    private void PauseGame()
    {
        if (pauseGame)
        {
            if (pauseOverlay == null) { pauseOverlay = FindObjectOfType<PauseOverlay>(); }
            Time.timeScale = 0f;
            if (pauseOverlay != null)
            {
                pauseOverlay.SetMaskOn(true);
                pauseOverlay.SetButtonsOn(true);
            }
        }
        else
        {
            Time.timeScale = 1f;
            if (pauseOverlay != null)
            {
                pauseOverlay.SetMaskOn(false);
                pauseOverlay.SetButtonsOn(false);
            }
        }
    }
    

}
