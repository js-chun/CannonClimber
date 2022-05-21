using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Class for controlling the UI banner at the top of the Game
public class InGameGridUI : MonoBehaviour
{
    private GameManager gm;

    public TextMeshProUGUI cocoTxt;     //Text for Coconut Charges
    public GameObject wine;             //Image of Wine Buff Status
    public TextMeshProUGUI scoreTxt;    //Text for Score
    public TextMeshProUGUI floorTxt;

    private float hue;

    void Start()
    {
        hue = 0f;
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        ShowWineStatus();
        ShowScore();
        ShowCoconut();
        ShowWineStatus();
        ShowFloor();
    }

    //Shows Wine Buff status
    //Greyed out if no buff. In colour if buffed
    private void ShowWineStatus()
    {
        if (gm.wineBuff)
        {
            hue += Time.deltaTime * 1f;
            if (hue >= 1f) { hue = 0f; }
            wine.GetComponent<Image>().color = Color.HSVToRGB(hue,1f,1f);
        }
        else
        {
            wine.GetComponent<Image>().color = Color.HSVToRGB(0f,1f,0.1f);
        }
    }

    //Updates text with current number of coconut charges
    private void ShowCoconut()
    {
        cocoTxt.text = gm.coconutBuff + "/3";
    }
    
    //Updates text with current score
    private void ShowScore()
    {
        string x ;
        if (gm.score < 10) { x = "x000"; }
        else if (gm.score < 100) { x = "x00"; }
        else if (gm.score < 1000) { x = "x0"; }
        else { x = "x"; }
        scoreTxt.text = x + gm.score;
    }

    private void ShowFloor()
    {
        string x;
        if (gm.playerFloors < 10) { x = "00"; }
        else if (gm.playerFloors < 100) { x = "0"; }
        else { x = ""; }
        floorTxt.text = x + gm.playerFloors.ToString();
    }
}
