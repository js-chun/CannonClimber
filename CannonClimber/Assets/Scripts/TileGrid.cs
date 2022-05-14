using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//will have preset TileGrids and separate TileGrid spawner
//TileGrid spawner will do the checks to make sure the selected tilegrid has an opening for player to jump through
[CreateAssetMenu(menuName = "Floor Config")]
public class TileGrid : ScriptableObject
{
    public bool[] xTiles = new bool[8];     //each bool for if tile is occupied or not (left to right)

    public bool moveBoxes;      //if floor is moving box level
    public bool boxesLeft;      //if box is moving left
    public bool boxesRight;     //if box is moving right
    public bool spearLevel;

    //checks current TileGrid vs the one above to see if there's no where to jump to (incompatible)
    public bool CantGoUp(TileGrid aboveLevel)
    {
        int numJumpTo = 0;
        int numBotOccupied = 0;
        int numBotBlocked = 0;
        for (int i = 1; i < 9; i++)
        {
            if (GetOccupied(i))
            {
                numBotOccupied++;
                for (int j = -2; j < 3; j++)
                {
                    int checkPosition = i + j;
                    if (checkPosition > 0 && checkPosition < 9)
                    {
                        if (j == 0)
                        {
                            if (TileCompare(aboveLevel, checkPosition) == -1) { numBotBlocked++; }
                        }
                        if (aboveLevel.GetOccupied(checkPosition)) { numJumpTo++; }
                    }
                }
            }
        }
        if (numJumpTo == 0) { return true; }
        else
        {
            if (numBotOccupied == numBotBlocked) { return true; }
            else { return false; }
        }
    }

    //gets occupied for x tile as an integer (0 for none, or 1 for occupied)
    public int GetOccupiedInt(int x)
    {
        bool a = GetOccupied(x);
        if (a) { return 1; }
        else { return 0; }
    }

    //gets occupied x tile as true or false
    public bool GetOccupied(int x)
    {
        return xTiles[x - 1];
    }

    //sets oocupied for x tile
    public void SetOccupied(int x, bool hasTile)
    {
        xTiles[x - 1] = hasTile;
    }

    //compares x tile with x tile on above level to see if empty, blocked
    public int TileCompare(TileGrid aboveLevel, int x)
    {
        int state = this.GetOccupiedInt(x) - 2 * aboveLevel.GetOccupiedInt(x);
        return state;
        //0 means above and current both don't have tiles on X = EMPTY
        //-2 means above has a tile, current does not on X 
        //-1 means above and current both have tiles on X = BLOCKED
        //1 means above does not, current does have a tile on X
    }

    //returns what type of tile (visually) should be created for this if occupied
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
        //-1 is no tile
        //0 is solo
        //1 is left
        //2 is mid
        //3 is right
    }
}
