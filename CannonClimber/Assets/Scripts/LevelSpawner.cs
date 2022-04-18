using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class LevelSpawner : MonoBehaviour
{
    public Tilemap lvlMap;
    public int tileCount; //will be private later on

    public Tilemap bgdMap;
    public Tile bgdLeftTile;
    public Tile bgdMidTile;
    public Tile bgdRightTile;

    public Tilemap boundMap;
    public Tile bndLeftTile;
    public Tile bndRightTile;
    void Start()
    {
        
    }

    void Update()
    {
        addBackground();
        addBoundary();
    }

    private void onTriggerEnter2D(Collider2D collision)
    {
        tileCount++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        tileCount--;
    }

    private void addBackground()
    {
        if (tileCount == 0)
        {
            Tile newTile;
            for (int i = -4; i < 4; i++){
                if (i == -4) { newTile = bgdLeftTile; }
                else if (i == 3) { newTile = bgdRightTile; }
                else { newTile = bgdMidTile; }

                int y = (int)this.transform.position.y;

                bgdMap.SetTile(new Vector3Int(i,y,0), newTile);
            }
        }
    }

    private void addBoundary()
    {
        if (tileCount == 0)
        {
            Tile newTile;
            for (int i = -5; i < 5; i+=9)
            {
                if (i == -5) { newTile = bndLeftTile; }
                else{ newTile = bndRightTile; }

                int y = (int)this.transform.position.y;

                boundMap.SetTile(new Vector3Int(i, y, 0), newTile);
            }
        }
    }
}
