using UnityEngine;

public class FloorRandomizer : MonoBehaviour
{
    public TileGrid[] floors;
    public TileGrid botFloor;
    public TileGrid topFloor;

    void Start()
    {
        Debug.Log(floors.Length);
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

    public void setBotFloor()
    {
        botFloor = topFloor;
    }
}
