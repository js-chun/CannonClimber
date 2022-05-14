using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;


//Class to remove tiles or to trigger respawning of Player
public class Shredder : MonoBehaviour
{

    private GameManager gm;

    public Tilemap lvlMap;      //Level tiles
    public Tilemap bgdMap;      //Background tiles
    public Tilemap boundMap;    //Boundary tiles
    public Tilemap introMap;    //Introduction tiles

    private bool tileExists;    //If tile exists or not

    private void Start()
    {
        gm= FindObjectOfType<GameManager>(); 
        tileExists = false;
    }

    //Checks tiles for the y coordinate based on background tiles
    private void Update()
    {
        if(bgdMap != null)
        {
            CheckTile();
            DestroyTiles();
        }   
    }

    //If Cannon, Cannonball, Item, etc hits the shredder (at the bottom), destroys them
    //If Player falls, destroys Player and respawns
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            StartCoroutine(RespawnChar());
        }
        else
        {
            if(collision.gameObject.tag != "SpearLevel")
            {
                Destroy(collision.gameObject);
            }
        }
    }

    //To set tiles on the same Y level as null
    private void DestroyTiles()
    {
        if (tileExists)
        {
            int y = (int)this.transform.position.y - 1;

            for (int x = -5; x < 5; x++)
            {
                if (x == -5 || x == 4)
                {
                    boundMap.SetTile(new Vector3Int(x,y,0),null);
                    introMap.SetTile(new Vector3Int(x,y,0),null);
                }
                else
                {
                    lvlMap.SetTile(new Vector3Int(x,y+1,0),null);
                    introMap.SetTile(new Vector3Int(x,y,0),null);
                }
                bgdMap.SetTile(new Vector3Int(x, y, 0), null);
            }
        }
    }

    //To check if background tile exists at the Y level (only checks X=0)
    private void CheckTile()
    {
        int y = (int)this.transform.position.y;
        if (bgdMap.GetTile(new Vector3Int(0, y, 0)) == null)
        {
            tileExists = false;
        }
        else
        {
            tileExists = true;
        } 
    }

    //To trigger Player respawning after a set duration
    private IEnumerator RespawnChar()
    {
        yield return new WaitForSeconds(1.5f);
        gm.SpawnNewChar();
    }
}
