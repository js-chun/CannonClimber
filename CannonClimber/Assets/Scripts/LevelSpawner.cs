using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelSpawner : MonoBehaviour
{
    private FloorRandomizer fr;
    private GameManager gm;

    private int levelCount;         //float to keep track of what should be generated for y coordinate

    private bool lvlActive;         //Activates the levels once initial map is consumed

    public Tilemap lvlMap;          //Level TileMap
    public Tile lvlSoloTile;        //Level Tile - Single
    public Tile lvlLeftTile;        //Level Tile - Left End of Multi
    public Tile lvlMidTile;         //Level Tile - Middle of Multi
    public Tile lvlRightTile;       //Level Tile - Right End of Multi

    public Tilemap bgdMap;          //Background TileMap
    public Tile bgdLeftTile;        //Background Tile - Left Edge
    public Tile bgdMidTile;         //Background Tile - Middle
    public Tile bgdRightTile;       //Background Tile - Right Edge

    public Tilemap boundMap;        //Boundary TileMap
    public Tile bndLeftTile;        //Boundary Tile - Left End
    public Tile bndRightTile;       //Boundary Tile - Right End

    private bool tileIsSet;         //To check if top tile is set or not

    public GameObject fIndContainer;    //Container to add instances of floor indicators
    public GameObject floorInd;         //Prefab for spawning floor indicators
    private float floorIndY;            //float to keep track of Y coordinate for floor indicator

    public GameObject itemContainer;
    public GameObject scoreOne;     //Score Level 1 Item
    public GameObject scoreTwo;     //Score Level 2 Item
    public GameObject scoreThree;   //Score Level 3 Item
    public GameObject orangeItem;   //Orange Item (for +1 HP)
    public GameObject wineItem;     //Wine Item (for temp invincibility)
    public GameObject coconutItem;  //Coconut Item for kicks

    private Dictionary<GameObject, float> itemRarity = new Dictionary<GameObject, float>();

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        fr = FindObjectOfType<FloorRandomizer>();
        lvlActive = false;
        levelCount = 0;

        AddItemRarities();

        floorIndY = 9.5f;
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

    //To show level intially - activates and fades in
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

    //To trigger level showing - Used in MapConsumed in Item script
    public void SetLvl(bool isActive) { lvlActive = isActive; }

    //Generates each floor
    private void GenerateLevel()
    {
        if (!tileIsSet)
        {
            AddBackground();
            levelCount++;

            if (levelCount == 3)
            {
                AddBoundary(Random.Range(0, 3));
                levelCount = 0;
            }
            else if (levelCount == 2)
            {
                AddItems((int)this.transform.position.y);
                AddLevel();
                AddBoundary(0);
            }
            else { AddBoundary(0); }
        }
    }

    //Adds Background tiles for y height
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

    //Adds Boundary tiles for y height
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
            fr.SpawnCannon(4.3f,y + 0.5f,true);
        }
        else if(scenario == 2)
        {
            boundMap.SetTile(new Vector3Int(-5, y, 0), bndLeftTile);
            fr.SpawnCannon(4.3f, y + 0.5f, false);
        }
    }

    //Takes randomized level from FloorRandomizer and adds setup as Level tiles for y height
    //Adds floor indicator trigger object with correct floor level
    private void AddLevel()
    {
        fr.RandomFlr();
        if (fr.topFloor.moveBoxes)
        {
            fr.SpawnBoxLevel((int)this.transform.position.y);
        }
        else
        {
            for (int i = -4; i < 4; i++)
            {
                int typeTile = fr.topFloor.TypeOfTile(i);
                if (typeTile != -1)
                {
                    Tile newTile;
                    if (typeTile == 0) { newTile = lvlSoloTile; }
                    else if (typeTile == 1) { newTile = lvlLeftTile; }
                    else if (typeTile == 2) { newTile = lvlMidTile; }
                    else { newTile = lvlRightTile; }
                    int y = (int)this.transform.position.y;
                    lvlMap.SetTile(new Vector3Int(i, y, 0), newTile);

                    fr.SpawnTraps(i, y);
                }
            }
        }
        fr.SetBotFloor();
        
        floorIndY += 3f;
        GameObject fI = Instantiate(floorInd, new Vector2(0f, floorIndY), Quaternion.identity, fIndContainer.transform);
        fI.GetComponent<FloorIndicator>().floorNumber = ++gm.createdFloors;
    }

    private void AddItems(float y)
    {
        
        float a;
        for (int i = -4; i < 4; i++)
        {
            a = Random.Range(0f,1f);
            if (a <= itemRarity[scoreOne])
            {
                Instantiate(scoreOne, new Vector2(i + 0.5f, y-0.25f), Quaternion.identity, itemContainer.transform);
            }
            else if (a <= itemRarity[scoreTwo])
            {
                Instantiate(scoreTwo, new Vector2(i + 0.5f, y - 0.25f), Quaternion.identity, itemContainer.transform);
            }
            else if (a <= itemRarity[scoreThree])
            {
                Instantiate(scoreThree, new Vector2(i + 0.5f, y - 0.25f), Quaternion.identity, itemContainer.transform);
            }
            else if (a <= itemRarity[orangeItem])
            {
                Instantiate(orangeItem, new Vector2(i + 0.5f, y - 0.25f), Quaternion.identity, itemContainer.transform);
            }
            else if (a <= itemRarity[wineItem])
            {
                Instantiate(wineItem, new Vector2(i + 0.5f, y - 0.25f), Quaternion.identity, itemContainer.transform);
            }
            else if (a <= itemRarity[coconutItem])
            {
                Instantiate(coconutItem, new Vector2(i + 0.5f, y - 0.25f), Quaternion.identity, itemContainer.transform);
            }
        }
    }

    //To store cumulative rarities for items at the start of game
    private void AddItemRarities()
    {
        float flatRarity = scoreOne.GetComponent<Item>().rarity;
        itemRarity.Add(scoreOne, flatRarity);

        flatRarity += scoreTwo.GetComponent<Item>().rarity;
        itemRarity.Add(scoreTwo, flatRarity);

        flatRarity += scoreThree.GetComponent<Item>().rarity;
        itemRarity.Add(scoreThree, flatRarity);

        flatRarity += orangeItem.GetComponent<Item>().rarity;
        itemRarity.Add(orangeItem, flatRarity);

        flatRarity += wineItem.GetComponent<Item>().rarity;
        itemRarity.Add(wineItem, flatRarity);

        flatRarity += coconutItem.GetComponent<Item>().rarity;
        itemRarity.Add(coconutItem, flatRarity);
    }

    //Checks if current y height has tiles or not (just checks a background tile)
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
