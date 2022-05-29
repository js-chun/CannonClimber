using UnityEngine;

//Class to assist with randomizing layout of next floor
public class FloorRandomizer : MonoBehaviour
{
    private GameManager gm;

    public TileGrid[] floors;       //Range of floor layouts to pick from
    public TileGrid botFloor;       //The floor layout that was just generated
    public TileGrid topFloor;       //The floor layout to be generated next
    public TileGrid initialFloor;   //Initial floor layout for start of random generation

    public GameObject cannon;       //Normal Cannon Gameobject to spawn for Cannon Floor
    public GameObject triCannon;    //Tri-Shot Cannon Gameobject to spawn for higher Cannon Floor
    public float fSpeed = 0.2f;     //Float for average speed of cannons

    public GameObject spikes;           //Spike Gameobject randomly generated for +30 floors
    public GameObject boxLevel;         //Moving Box Level prefab to generate
    public GameObject disappearLevel;   //Disappearing Tile Level prefab to generate
    public GameObject spearLevel;       //Spear Level prefab to generate (Takes up 3 floors)
    public GameObject floatTile;        //Floating Tile to go along with Spear Level
    public GameObject topCannonLevel;   //Top Cannon Level that gets activated

    public GameObject levelContainer;   //Container for levels to be instantiated in for organization

    private readonly int trapFloor = 30;
    private readonly int cannonFloor = 45;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        botFloor = initialFloor;
    }

    //Randomly selects the next floor layout
    //Makes sure that the next floor layout is accessible from the floor below
    //And can't be another spear level right after a spear level
    public void RandomFlr()
    {
        if(botFloor != null)
        {
            do
            {
                int testFlr = Random.Range(0, floors.Length);
                topFloor = floors[testFlr];
            } while (botFloor.CantGoUp(topFloor) || (botFloor.spearLevel && topFloor.spearLevel));
        }
    }

    //Sets the floor just generated after generation
    public void SetBotFloor()
    {
        botFloor = topFloor;
    }

    //Spawns Cannon (or Tri-Shot Cannon 45+ floors)
    //on the left if facing right
    //on the right if facing left
    public void SpawnCannon(float locX, float locY, bool faceRight)
    {
        GameObject can = cannon;

        if (gm.createdFloors > cannonFloor){
            int a = Random.Range(0, 2);
            if (a == 0) { can = cannon; }
            else if (a == 1) { can = triCannon; }
        }

        if (faceRight)
        {
            locX = -(locX);
        }

        GameObject newCan = Instantiate(can, new Vector2(locX, locY), Quaternion.identity, levelContainer.transform);
        newCan.GetComponent<CannonBehaviour>().fireSpeed = Random.Range(fSpeed - 0.08f, fSpeed + 0.08f);
        if (faceRight)
        {
            newCan.transform.localScale = new Vector2(-2f, 2f);
        }
    }

    //Spawns Moving Box Level
    //Moves left or right based on TileGrid layout used
    public void SpawnBoxLevel(float locY)
    {
        GameObject newBoxLevel = Instantiate(boxLevel, new Vector2(0f,locY + 1),Quaternion.identity, levelContainer.transform);
        if (topFloor.boxesLeft)
        {
            newBoxLevel.GetComponent<BoxSpawner>().boxMoveLeft = true;
        }
        else
        {
            newBoxLevel.GetComponent<BoxSpawner>().boxMoveLeft = false;
        }
    }

    //Spawns Disappearing Level
    public void SpawnDisappearLevel(float locY)
    {
        Instantiate(disappearLevel, new Vector2(0f, locY + 1), Quaternion.identity, levelContainer.transform);
    }

    //Spawns Spear Level with Floating Tiles
    //Sets the Spear Counter in Level Spawner to 3 so that it ignores generation of the 3 floors
    public void SpawnSpearLevel(float locY)
    {
        Instantiate(spearLevel, new Vector2(0f, locY + 1), Quaternion.identity, levelContainer.transform);
        Instantiate(floatTile, new Vector2(0f, locY + 1), Quaternion.identity, levelContainer.transform);
        FindObjectOfType<LevelSpawner>().spearCounter = 3;
    }

    //Spawns random spikes
    public void SpawnTraps(float locX, float locY)
    {
        if(gm.createdFloors > trapFloor)
        {
            float a = Random.Range(0f, 1f);
            if (a < 0.2f)
            {
                Instantiate(spikes, new Vector2(locX, locY), Quaternion.identity, levelContainer.transform);
            }
        }
    }

    //Turns on vertical cannon level
    public void TurnOnCannonLevel()
    {
        topCannonLevel.SetActive(true);
    }
}
