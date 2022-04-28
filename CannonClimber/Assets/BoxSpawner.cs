using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject box;
    public float spawnTimer = 1.5f;

    private bool canSpawn;

    private void Start()
    {
        canSpawn = true;
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SpawnBoxes());
    }

    private IEnumerator SpawnBoxes()
    {
        if (canSpawn)
        {
            canSpawn = false;
            yield return new WaitForSeconds(spawnTimer);
            Instantiate(box, this.transform.position, Quaternion.identity, transform.parent);
            canSpawn = true;
        }
    }
}
