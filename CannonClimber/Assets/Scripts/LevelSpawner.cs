using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class LevelSpawner : MonoBehaviour
{
    private GameManager gm;
    private Collider2D col;
    public Collider2D compare;
    public Tilemap lvlMap;
    public int tileCount; //will be private later on
    public int levelCount; //will be private later on

    private int canOnLeft;
    private int canOnRight;
    private int difficulty;

    private bool lvlActive;

    public Tilemap bgdMap;
    public Tile bgdLeftTile;
    public Tile bgdMidTile;
    public Tile bgdRightTile;

    public Tilemap boundMap;
    public Tile bndLeftTile;
    public Tile bndRightTile;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        lvlActive = false;
        levelCount = 0;
        canOnLeft = 0;
        canOnRight = 0;
        difficulty = 2;
        col = GetComponent<Collider2D>();  
    }

    void Update()
    {
        boundMap.transform.Translate(new Vector2(0f, 0f));
        if (lvlActive)
        {
            showLevel();
        }
        generateLevel();
    }

    private void onTriggerEnter2D(Collider2D collision)
    {
        tileCount++;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision);
        tileCount = 2;
    }

    private void onCollisionEnter2D(Collider2D collision)
    {
        tileCount++;
    }

    //can't seem to get colliders to work
    private void OnTriggerExit2D(Collider2D collision)
    {
        //tileCount--;
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
        if (tileCount ==0)
        {
            addBackground();
            if(levelCount == 2)
            {
                addBoundary(Random.Range(1, difficulty));
            }
            else
            {
                addBoundary(0);
            }
        }
    }

    private void addBackground()
    {
        Tile newTile;
        for (int i = -4; i < 4; i++){
            if (i == -4) { newTile = bgdLeftTile; }
            else if (i == 3) { newTile = bgdRightTile; }
            else { newTile = bgdMidTile; }

            int y = (int)this.transform.position.y;

            bgdMap.SetTile(new Vector3Int(i,y,0), newTile);
            //tileCount++;
        }
    }

    private void addBoundary(int scenario)
    {
        int y = (int)this.transform.position.y;
        if(scenario == 0)
        {
            boundMap.SetTile(new Vector3Int(-5, y, 0), bndLeftTile);
            canOnLeft = 0;
            boundMap.SetTile(new Vector3Int(4, y, 0), bndRightTile);
            canOnRight = 0;
        }
        else if(scenario == 1)
        {
            boundMap.SetTile(new Vector3Int(4, y, 0), bndRightTile);
            canOnLeft = 1;
            canOnRight = 0;
        }
        else if(scenario == 2)
        {
            boundMap.SetTile(new Vector3Int(-5, y, 0), bndLeftTile);
            canOnLeft = 0;
            canOnRight = 1;
        }
        //tileCount += 2;
        levelCount++;
    }

    private void addLevel()
    {
        if(levelCount == 3)
        {
            levelCount = 0;

        }
    }
}
