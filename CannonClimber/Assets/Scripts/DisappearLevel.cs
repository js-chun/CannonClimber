using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearLevel : MonoBehaviour
{
    public DisappearTile[] tiles;
    public List<DisappearTile> first;
    public List<DisappearTile> second;

    public float switchTime;
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        active = true;
        RandomAssign();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ControlTiles());
    }

    private void RandomAssign()
    {
        foreach(DisappearTile t in tiles)
        {
            if (Random.Range(0f,1f) < 0.5f) 
            {
                first.Add(t);
            }
            else { second.Add(t); }
        }
    }

    private IEnumerator ControlTiles()
    {
        if (active)
        {
            active = false;
            foreach(DisappearTile fTile in first) {
                Debug.Log("Running"); 
                fTile.SetInvisible(true); }
            foreach (DisappearTile sTile in second) { sTile.SetInvisible(false); }
            yield return new WaitForSeconds(switchTime);
            foreach (DisappearTile fTile in first) { fTile.SetInvisible(false); }
            foreach (DisappearTile sTile in second) { sTile.SetInvisible(true); }
            yield return new WaitForSeconds(switchTime);
            active = true;
        }
    }
}
