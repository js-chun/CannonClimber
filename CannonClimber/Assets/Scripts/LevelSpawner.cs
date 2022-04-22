using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelSpawner : MonoBehaviour
{
    private GameManager gm;
    private FloorRandomizer fr;
    public int tileCount; //will be private later on
    public int levelCount; //will be private later on

    private int difficulty;

    private bool lvlActive;

    public Tilemap lvlMap;
    public Tile lvlSoloTile;
    public Tile lvlLeftTile;
    public Tile lvlMidTile;
    public Tile lvlRightTile;

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
        fr = FindObjectOfType<FloorRandomizer>();
        lvlActive = false;
        levelCount = 0;
        difficulty = 2;
    }

    void Update()
    {
        if (lvlActive)
        {
            ShowLevel();
        }
        CheckTile();
        GenerateLevel();
    }

    private void ShowLevel()
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

    public void SetLvl(bool isActive) { lvlActive = isActive; }

    private void GenerateLevel()
    {
        if (!tileIsSet)
        {
            AddBackground();
            levelCount++;

            if (levelCount == 3)
            {
                AddBoundary(Random.Range(1, difficulty + 1));
                levelCount = 0;
            }
            else if (levelCount == 2)
            {
                //addLevel
                AddBoundary(0);
            }
            else { AddBoundary(0); }
        }
    }

    private void AddBackground()
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

    private void AddBoundary(int scenario)
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

    private void AddLevel(int scenario)
    {
        int y = (int)this.transform.position.y;
        fr.RandomFlr();

    }

    private void CheckTile()
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
