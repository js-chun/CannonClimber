using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject cam;
    private PlayerBehaviour player;
    private GameManager gm;

    void Start()
    {
        cam = FindObjectOfType<Camera>().gameObject;
        player = FindObjectOfType<PlayerBehaviour>();
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        CamMove();
    }

    private void CamMove()
    {
        if(cam != null)
        {
            if(gm.stageLevel > 3)
            {
                if(player != null)
                {
                    if (player.GetGroundCheck())
                    {
                        float currentHeight = this.transform.position.y;
                        if (currentHeight > gm.GetPeakHeight()) { gm.SetPeakHeight(currentHeight); }
                    }
                }
                Vector2 dist = new Vector2(0, gm.GetPeakHeight()-cam.transform.position.y) * Time.deltaTime * 0.9f;
                cam.transform.Translate(dist);
            }
        }
    }


}
