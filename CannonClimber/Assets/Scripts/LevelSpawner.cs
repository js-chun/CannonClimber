using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelSpawner : MonoBehaviour
{
    private FloorRandomizer fr;
    public int tileCount;           //will be private later on
    public int levelCount;          //will be private later on

    private int difficulty;         //Used to set difficulty of levels later on

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

    public GameObject itemContainer;
    public GameObject scoreOne;             //Score Level 1 Item
    private float scoreOneRate;     //Rate at which score level 1 item occurs
    public GameObject scoreTwo;             //Score Level 2 Item
    private float scoreTwoRate;     //Rate at which score level 1 item occurs
    public GameObject scoreThree;           //Score Level 3 Item
    private float scoreThreeRate;   //Rate at which score level 1 item occurs

    void Start()
    {
        fr = FindObjectOfType<FloorRandomizer>();
        lvlActive = false;
        levelCount = 0;
        difficulty = 2;
        scoreOneRate = 0.15f;
        scoreTwoRate = 0.05f;
        scoreThreeRate = 0.01f;
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
                AddBoundary(Random.Range(0, difficulty + 1));
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
                }
            }
        }
        fr.SetBotFloor();
    }

    private void AddItems(float y)
    {
        float a;
        for (int i = -4; i < 4; i++)
        {
            a = Random.Range(0f,1f);
            if (a <= scoreThreeRate)
            {
                Instantiate(scoreThree, new Vector2(i + 0.5f, y-0.25f), Quaternion.identity, itemContainer.transform);
            }
            else if (a <= scoreTwoRate)
            {
                Instantiate(scoreTwo, new Vector2(i + 0.5f, y - 0.25f), Quaternion.identity, itemContainer.transform);
            }
            else if (a <= scoreOneRate)
            {
                Instantiate(scoreOne, new Vector2(i + 0.5f, y - 0.25f), Quaternion.identity, itemContainer.transform);
            }
        }
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
