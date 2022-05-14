
using UnityEngine;

public class FloatTileSystem : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject stopper;
    public float speed;
    private bool movingUp;
    public bool completeStop;

    // Start is called before the first frame update
    void Start()
    {
        completeStop = false;
        movingUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!completeStop)
        {
            MoveUp();
        }
    }

    private void MoveUp()
    {
        if (movingUp)
        {
            foreach(GameObject t in tiles) { t.transform.position += new Vector3(0f, speed*Time.deltaTime, 0f); }
        }
    }

    public void SetFloors (int numFlr) 
    { 
        stopper.transform.localPosition = new Vector3(0f, numFlr * 3f, 0f);
    }

    public void SetMovingUp(bool playerIsOn) { movingUp = playerIsOn; }

}
