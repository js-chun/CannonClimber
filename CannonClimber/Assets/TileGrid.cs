using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//will have preset TileGrids and separate TileGrid spawner
//TileGrid spawner will do the checks to make sure the selected tilegrid has an opening for player to jump through
public class TileGrid : MonoBehaviour
{
    private bool x1;
    private bool x2;
    private bool x3;
    private bool x4;
    private bool x5;
    private bool x6;
    private bool x7;
    private bool x8;

    public TileGrid(bool x1T, bool x2T, bool x3T, bool x4T, bool x5T, bool x6T, bool x7T, bool x8T)
    {
        x1 = x1T;
        x2 = x2T;
        x3 = x3T;
        x4 = x4T;
        x5 = x5T;
        x6 = x6T;
        x7 = x7T;
        x8 = x8T;
    }

    public int tileCompare(TileGrid aboveLevel, int x)
    {
        int state = this.getOccupiedInt(x) - 2 * aboveLevel.getOccupiedInt(x);
        return state;
        //0 means above and current both don't have tiles on X = EMPTY
        //-2 means above has a tile, current does not on X 
        //-1 means above and current both have tiles on X = BLOCKED
        //1 means above does not, current does have a tile on X
    }

    public bool cantGoUp(TileGrid aboveLevel)
    {
        int numBlocked = 0;
        int numOccupied = 0;
        for (int i = 1; i < 9; i++)
        {
            if (getOccupied(i))
            {
                numOccupied++;
                if(tileCompare(aboveLevel, i) == -1)
                {
                    numBlocked++;
                }
            }
        }
        if (numBlocked == numOccupied) { return true; }
        else { return false; }
    }

    public int getOccupiedInt(int x)
    {
        bool a = getOccupied(x);
        if (a) { return 1; }
        else { return 0; }
    }

    public bool getOccupied(int x)
    {
        if (x == 1) { return x1; }
        else if (x == 2) { return x2; }
        else if (x == 3) { return x3; }
        else if (x == 4) { return x4; }
        else if (x == 5) { return x5; }
        else if (x == 6) { return x6; }
        else if (x == 7) { return x7; }
        else if (x == 8) { return x8; }
        else { return false; }
    }

    public void setOccupied(int x, bool hasTile)
    {
        if (x == 1) { x1 = hasTile; }
        else if (x == 2) { x2 = hasTile; }
        else if (x == 3) { x3 = hasTile; }
        else if (x == 4) { x4 = hasTile; }
        else if (x == 5) { x5 = hasTile; }
        else if (x == 6) { x6 = hasTile; }
        else if (x == 7) { x7 = hasTile; }
        else if (x == 8) { x8 = hasTile; }
    }
}
