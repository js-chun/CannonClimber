using UnityEngine;

public class FloorRandomizer : MonoBehaviour
{
    
    public TileGrid[] floors;
    public TileGrid botFloor;
    public TileGrid topFloor;
    public TileGrid initialFloor;

    public GameObject cannon;
    public GameObject triCannon;
    public float fSpeed = 0.2f;

    public GameObject boxLevel;

    public GameObject levelContainer;
    void Start()
    {
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
        GameObject can = cannon;

        if (FindObjectOfType<GameManager>().createdFloors > 30){
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
}
