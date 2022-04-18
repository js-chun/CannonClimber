using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelBehaviour : MonoBehaviour
{
    //private GameManager gm;
    private Tilemap tmap;
    private bool lvlActive;

    void Start()
    {
        //gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (lvlActive)
        {
            showLevel();
        }
    }

    private void showLevel()
    {
        if(tmap == null) { tmap = this.GetComponent<Tilemap>(); }
        else if(tmap != null)
        {
            float transparency = tmap.color.a;
            transparency += 0.7f * Time.deltaTime;
            if (transparency > 1) { transparency = 1; }

            Color newCol = new Color(255, 255, 255, transparency);
            tmap.color = newCol;
        }
    }

    public void setLvl(bool isActive) { lvlActive = isActive; }

}
