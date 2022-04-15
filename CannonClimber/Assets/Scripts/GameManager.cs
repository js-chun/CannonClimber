using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isMenuState;
    public int menuStage;

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
        menuStage = 1;
    }

    void Update()
    {
        
    }
}
