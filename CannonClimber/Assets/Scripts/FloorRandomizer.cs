using UnityEngine;

public class FloorRandomizer : MonoBehaviour
{
    private GameManager gm;

    public TileGrid[] floors;
    public TileGrid botFloor;
    public TileGrid topFloor;
    public TileGrid initialFloor;

    public GameObject cannon;
    public GameObject triCannon;
    public float fSpeed = 0.2f;

    public GameObject spikes;
    public GameObject boxLevel;
    public GameObject spearLevel;
    public GameObject floatTile;

    public GameObject levelContainer;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        botFloor = initialFloor;
    }

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

    public void SetBotFloor()
    {
        botFloor = topFloor;
    }

    public void SpawnCannon(float locX, float locY, bool faceRight)
    {
        GameObject can = cannon;

        if (gm.createdFloors > 45){
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

    public void SpawnSpearLevel(float locY)
    {
        Instantiate(spearLevel, new Vector2(0f, locY + 1), Quaternion.identity, levelContainer.transform);
        Instantiate(floatTile, new Vector2(0f, locY + 1), Quaternion.identity, levelContainer.transform);
        FindObjectOfType<LevelSpawner>().spearCounter = 3;
    }

    public void SpawnTraps(float locX, float locY)
    {
        if(gm.createdFloors > 30)
        {
            float a = Random.Range(0f, 1f);
            if (a < 0.2f)
            {
                Instantiate(spikes, new Vector2(locX, locY), Quaternion.identity, levelContainer.transform);
            }
        }
    }
}
