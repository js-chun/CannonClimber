using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelSpawner : MonoBehaviour
{
    private GameManager gm;
    public Tilemap lvlMap;
    public int tileCount; //will be private later on
    public int levelCount; //will be private later on

    private bool canLeft;
    private bool canMidLeft;
    private bool canMidRight;
    private bool canRight;

    private int difficulty;

    private bool lvlActive;

    public Tilemap bgdMap;
    public Tile bgdLeftTile;
    public Tile bgdMidTile;
    public Tile bgdRightTile;

    public Tilemap boundMap;
    public Tile bndLeftTile;
    public Tile bndRightTile;

    public bool tileIsSet;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        lvlActive = false;
        levelCount = 0;
        difficulty = 2;

    }

    void Update()
    {
        if (lvlActive)
        {
            showLevel();
        }
        checkTile();
        generateLevel();
    }

    private void showLevel()
    {
        if (lvlMap != null)
        {
            lvlMap.gameObject.SetActive(true);
            float transparency = lvlMap.color.a;
            transparency += 0.7f * Time.deltaTime;
            if (transparency > 1) { transparency = 1; }

            Color newCol = new Color(255, 255, 255, transparency);
            lvlMap.color = newCol;
        }
        if (boundMap != null)
        {
            boundMap.SetTile(new Vector3Int(-5, 2, 0), null);
        }
    }

    public void setLvl(bool isActive) { lvlActive = isActive; }

    private void generateLevel()
    {
        if (!tileIsSet)
        {
            addBackground();
            levelCount++;

            if (levelCount == 3)
            {
                addBoundary(Random.Range(1, difficulty + 1));
                levelCount = 0;
            }
            else if (levelCount == 2)
            {
                //addLevel
                addBoundary(0);
            }
            else { addBoundary(0); }
        }
    }

    private void addBackground()
    {
        Tile newTile;
        for (int i = -4; i < 4; i++)
        {
            if (i == -4) { newTile = bgdLeftTile; }
            else if (i == 3) { newTile = bgdRightTile; }
            else { newTile = bgdMidTile; }

            int y = (int)this.transform.position.y;

            bgdMap.SetTile(new Vector3Int(i, y, 0), newTile);
        }
    }

    private void addBoundary(int scenario)
    {
        int y = (int)this.transform.position.y;
        if(scenario == 0)
        {
            boundMap.SetTile(new Vector3Int(-5, y, 0), bndLeftTile);
            boundMap.SetTile(new Vector3Int(4, y, 0), bndRightTile);
        }
        else if(scenario == 1)
        {
            boundMap.SetTile(new Vector3Int(4, y, 0), bndRightTile);
        }
        else if(scenario == 2)
        {
            boundMap.SetTile(new Vector3Int(-5, y, 0), bndLeftTile);
        }
    }

    private void addLevel(int scenario)
    {
        int y = (int)this.transform.position.y;
        if(scenario == 0)
        {

        }
        else if (scenario == 1)
        {

        }
        else if(scenario == 2)
        {

        }
    }

    private void checkTile()
    {
        int y = (int)this.transform.position.y;
        if(bgdMap.GetTile(new Vector3Int(0,y,0)) == null)
        {
            tileIsSet = false;
        }
        else
        {
            tileIsSet = true;
        }
    }
}
