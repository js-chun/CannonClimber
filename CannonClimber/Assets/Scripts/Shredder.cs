using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Shredder : MonoBehaviour
{

    private GameManager gm;

    public Tilemap lvlMap;
    public Tilemap bgdMap;
    public Tilemap boundMap;
    public Tilemap introMap;

    private bool tileExists;

    private void Start()
    {
        gm= FindObjectOfType<GameManager>(); 
        tileExists = false;
    }

    private void Update()
    {
        if(bgdMap != null)
        {
            CheckTile();
            DestroyTiles();
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cannon" || collision.gameObject.tag == "CannonBall") 
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            StartCoroutine(RespawnChar());
        }
    }

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

    private IEnumerator RespawnChar()
    {
        yield return new WaitForSeconds(1.5f);
        gm.SpawnNewChar();
    }
}
