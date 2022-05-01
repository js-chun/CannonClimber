using UnityEngine;

public class FloorRandomizer : MonoBehaviour
{
    private GameManager gm;
    
    public TileGrid[] floors;
    public TileGrid botFloor;
    public TileGrid topFloor;
    public TileGrid initialFloor;

    public GameObject cannon;
    public float fSpeed = 0.2f;

    public GameObject boxLevel;
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
            } while (botFloor.CantGoUp(topFloor));
        }
    }

    public void SetBotFloor()
    {
        botFloor = topFloor;
    }

    public void SpawnCannon(float locX, float locY, bool faceRight)
    {
        if (faceRight)
        {
            locX = -(locX);
        }
        GameObject newCan = Instantiate(cannon, new Vector2(locX, locY), Quaternion.identity, transform);
        newCan.GetComponent<CannonBehaviour>().fireSpeed = Random.Range(fSpeed - 0.15f, fSpeed + 0.15f);
        if (faceRight)
        {
            newCan.transform.localScale = new Vector2(-2f, 2f);
        }
    }

    public void SpawnBoxLevel(float locY)
    {
        GameObject newBoxLevel = Instantiate(boxLevel, new Vector2(0f,locY + 1),Quaternion.identity, transform);
        if (topFloor.boxesLeft)
        {
            newBoxLevel.GetComponent<BoxSpawner>().boxMoveLeft = true;
        }
        else
        {
            newBoxLevel.GetComponent<BoxSpawner>().boxMoveLeft = false;
        }
    }
}
