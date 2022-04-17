using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject cam;
    private GameObject player;
    private GameManager gm;
    private float peakHeight;

    void Start()
    {
        cam = FindObjectOfType<Camera>().gameObject;
        player = FindObjectOfType<PlayerBehaviour>().gameObject;
        gm = FindObjectOfType<GameManager>();
        peakHeight = 0;
    }

    void Update()
    {
        camMove();
    }

    private void camMove()
    {
        if(cam != null)
        {
            if(gm.stageLevel > 3)
            {
                if(player != null)
                {
                    if (player.GetComponent<Rigidbody2D>().velocity.y == 0)
                    {
                        float currentHeight = this.transform.position.y;
                        if (currentHeight > peakHeight) { peakHeight = currentHeight; }
                    }
                }
                Vector2 dist = new Vector2(0, peakHeight-cam.transform.position.y) * Time.deltaTime * 0.7f;
                cam.transform.Translate(dist);
            }
        }
    }


}
