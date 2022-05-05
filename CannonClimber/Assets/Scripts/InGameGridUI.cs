using UnityEngine;
using TMPro;

//Class for controlling the UI banner at the top of the Game
public class InGameGridUI : MonoBehaviour
{
    private GameManager gm;

    public TextMeshProUGUI cocoTxt;     //Text for Coconut Charges
    public GameObject wine;             //Image of Wine Buff Status
    public TextMeshProUGUI scoreTxt;    //Text for Score

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        StayOnScreen();
        ShowWineStatus();
        ShowScore();
        ShowCoconut();
    }

    //Keeps banner on screen at the top relevant to Camera
    private void StayOnScreen()
    {
        Vector3 camLoc = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -5f);
        this.transform.position = camLoc;
    }

    //Shows Wine Buff status
    //Greyed out if no buff. In colour if buffed
    private void ShowWineStatus()
    {
        //if for wine enabled
        //wine.GetComponent<SpriteRenderer>().color = Color.white;
        //else
        //wine.GetComponent<SpriteRenderer>().material.color = Color.gray;
    }

    //Updates text with current number of coconut charges
    private void ShowCoconut()
    {
        cocoTxt.text = gm.coconutBuff + "/3";
    }
    
    //Updates text with current score
    private void ShowScore()
    {
        scoreTxt.text = "x" + gm.score;
    }
}
