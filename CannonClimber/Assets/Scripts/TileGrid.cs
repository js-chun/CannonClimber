using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//will have preset TileGrids and separate TileGrid spawner
//TileGrid spawner will do the checks to make sure the selected tilegrid has an opening for player to jump through
[CreateAssetMenu(menuName = "Floor Config")]
public class TileGrid : ScriptableObject
{
    public bool x1;
    public bool x2;
    public bool x3;
    public bool x4;
    public bool x5;
    public bool x6;
    public bool x7;
    public bool x8;

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

    public bool CantGoUp(TileGrid aboveLevel)
    {
        int numJumpTo = 0;

        for (int i = 1; i < 9; i++)
        {
            if (GetOccupied(i))
            {
                for (int j = -2; j < 3; j++)
                {
                    int checkPosition = i + j;
                    if (checkPosition > 0 || checkPosition < 9)
                    {
                        if (aboveLevel.GetOccupied(checkPosition)) { numJumpTo++; }
                    }
                }
            }
        }
        if(numJumpTo == 0) { return true; }
        else { return false; }
    }

    public int GetOccupiedInt(int x)
    {
        bool a = GetOccupied(x);
        if (a) { return 1; }
        else { return 0; }
    }

    public bool GetOccupied(int x)
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

    public void SetOccupied(int x, bool hasTile)
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

    public int TileCompare(TileGrid aboveLevel, int x)
    {
        int state = this.GetOccupiedInt(x) - 2 * aboveLevel.GetOccupiedInt(x);
        return state;
        //0 means above and current both don't have tiles on X = EMPTY
        //-2 means above has a tile, current does not on X 
        //-1 means above and current both have tiles on X = BLOCKED
        //1 means above does not, current does have a tile on X
    }

    public int TypeOfTile (int x)
    {
        x += 5;     //starts -4 in game
        if (GetOccupied(x))
        {
            if (x == 1)
            {
                if (GetOccupied(x + 1)) { return 2; }
                else { return 3; }
            }
            else if (x == 8)
            {
                if (GetOccupied(x - 1)) { return 2; }
                else { return 1; }
            }
            else
            {
                bool left = GetOccupied(x - 1);
                bool right = GetOccupied(x + 1);
                if (left && right) { return 2; }
                else if (left) { return 3; }
                else if (right) { return 1; }
                else { return 0; }
            }
        }
        return -1;
    }
}
