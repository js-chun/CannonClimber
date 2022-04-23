using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class test : MonoBehaviour
{
    public TileGrid bot;
    public TileGrid top;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(bot.CantGoUp(top)); //should be true
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
