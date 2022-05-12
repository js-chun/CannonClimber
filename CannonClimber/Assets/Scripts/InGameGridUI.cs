using UnityEngine;
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
        StayOnScreen();
        ShowWineStatus();
        ShowScore();
        ShowCoconut();
        ShowWineStatus();
        ShowFloor();
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
        if (gm.wineBuff)
        {
            hue += Time.deltaTime * 1f;
            if (hue >= 1f) { hue = 0f; }
            wine.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(hue,1f,1f);
        }
        else
        {
            wine.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(0f,1f,0.1f);
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
        scoreTxt.text = "x" + gm.score;
    }

    private void ShowFloor()
    {
        floorTxt.text = gm.playerFloors.ToString();
    }
}
