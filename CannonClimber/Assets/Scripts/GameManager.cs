using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isMenuState;
    public int stageLevel;

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
        //isMenuState = true;
        stageLevel = 0;
    }

    void Update()
    {
        
    }
}
