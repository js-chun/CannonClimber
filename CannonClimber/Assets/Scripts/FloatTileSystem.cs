
using UnityEngine;

public class FloatTileSystem : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject stopper;
    
    public float speed;
    public bool movingUp;
    public bool completeStop;
    
    private float minY;

    // Start is called before the first frame update
    void Start()
    {
        completeStop = false;
        movingUp = false;

        minY = tiles[0].transform.position.y;
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
            foreach(GameObject t in tiles) 
            { 
                t.transform.position += new Vector3(0f, speed * Time.deltaTime, 0f);
                t.GetComponent<Animator>().SetBool("Moving", true);
                t.GetComponent<Animator>().SetBool("Up", true);
            }
        }
        else
        {
            foreach (GameObject t in tiles)
            { 
                if(t.transform.position.y > minY)
                {
                    t.transform.position -= new Vector3(0f, speed * Time.deltaTime * 0.3f, 0f);
                    t.GetComponent<Animator>().SetBool("Moving", true);
                    t.GetComponent<Animator>().SetBool("Up", false);
                }
                else
                {
                    t.GetComponent<Animator>().SetBool("Moving", false);
                }
                
            }
        }
    }

    public void SetFloors (int numFlr) 
    { 
        stopper.transform.localPosition = new Vector3(0f, numFlr * 3f, 0f);
    }

    public void SetMovingUp(bool playerIsOn) { movingUp = playerIsOn; }

}
