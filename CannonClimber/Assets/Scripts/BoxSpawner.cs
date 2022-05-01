using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject box;
    public GameObject boxSpawner;
    public float spawnTimer = 1.5f;
    public bool boxMoveLeft = false;
    private bool canSpawn;

    private void Start()
    {
        canSpawn = true;
    }
 
    // Update is called once per frame
    void Update()
    {
        LeftOrRight();
        StartCoroutine(SpawnBoxes());
    }

    private IEnumerator SpawnBoxes()
    {
        if (canSpawn)
        {
            canSpawn = false;
            yield return new WaitForSeconds(spawnTimer);
            GameObject boxTile = Instantiate(box, boxSpawner.transform.position, Quaternion.identity, transform.parent);
            boxTile.GetComponent<TileBehaviour>().moveLeft = boxMoveLeft;
            canSpawn = true;
        }
    }

    private void LeftOrRight()
    {
        if (boxMoveLeft)
        {
            this.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            this.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
