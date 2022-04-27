using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameGridUI : MonoBehaviour
{
    private PlayerBehaviour player;
    private GameManager gm;

    public GameObject wine;

    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        StayOnScreen();
        ShowWineStatus();
    }

    private void StayOnScreen()
    {
        Vector3 camLoc = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -5f);
        this.transform.position = camLoc;
    }

    private void ShowWineStatus()
    {
        //if for wine enabled
        //wine.GetComponent<SpriteRenderer>().color = Color.white;
        //else
        //wine.GetComponent<SpriteRenderer>().material.color = Color.gray;
    }
}
