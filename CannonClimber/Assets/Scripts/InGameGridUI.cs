using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

//Class for controlling the UI banner at the top of the Game
public class InGameGridUI : MonoBehaviour
{
    private GameManager gm;

    public TextMeshProUGUI cocoTxt;     //Text for Coconut Charges
    public GameObject wine;             //Image of Wine Buff Status
    public TextMeshProUGUI scoreTxt;    //Text for Score
    public TextMeshProUGUI floorTxt;    //Text for Floor Player has hit
    public TextMeshProUGUI finalTxt;

    private float hue;                  //Float to represent hue for wine icon (rainbow color)

    public bool inGame;                 //true = during game, false = post-game

    void Start()
    {
        hue = 0f;
        gm = FindObjectOfType<GameManager>();
        if (!inGame)
        {
            ShowFinalCoins();
        }
    }

    void Update()
    {
        if (inGame)
        {
            ShowWineStatus();
            ShowCoins();
            ShowCoconut();
            ShowWineStatus();
            ShowFloor();
        }
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
    private string CalcCoins(int num)
    {
        string x = "";
        if (num < 10) { x = "000"; }
        else if (num < 100) { x = "00"; }
        else if (num < 1000) { x = "0"; }
        return x + num;
    }

    //Updates text with current score
    private void ShowCoins() { scoreTxt.text = CalcCoins(gm.score); }
    private string CalcFloor(int num)
    {
        string x = "";
        if (num < 10) { x = "00"; }
        else if (num < 100) { x = "0"; }
        return x + num;
    }

    //Updates text with current floor
    private void ShowFloor() { floorTxt.text = CalcFloor(gm.playerFloors); }

    private void ShowFinalCoins()
    {
        StartCoroutine(FinalCoins());
    }

    private IEnumerator FinalCoins()
    {
        float totalTime;
        string txt;
        int val;
        TextMeshProUGUI go;
        float t = 0.05f;

        for (int turn = 0; turn < 3; turn++)
        {
            totalTime = 0f;

            if (turn == 0)
            {
                txt = "TOTAL COINS: ";
                val = gm.score;
                go = scoreTxt;
            }
            else if (turn == 1)
            {
                txt = "FINAL FLOOR: ";
                val = gm.playerFloors;
                go = floorTxt;
            }
            else
            {
                txt = "TOTAL SCORE: ";
                val = gm.score + 10 * gm.playerFloors;
                go = finalTxt;
            }

            while (totalTime <= 1f)
            {
                yield return new WaitForSeconds(t);
                totalTime += t;
                go.text = txt + CalcCoins(Random.Range(0, 10000));
            }
            yield return new WaitForSeconds(t);
            go.text = txt + CalcCoins(val);
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(t);
                go.color = new Color(1f, 1f, 1f, 0f);
                yield return new WaitForSeconds(t);
                go.color = new Color(1f, 1f, 1f, 1f);
            }
        }

    }
}
