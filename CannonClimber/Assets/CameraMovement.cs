using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject cam;
    private GameManager gm;
    void Start()
    {
        cam = FindObjectOfType<Camera>().gameObject;
        gm = gameObject.GetComponent<GameManager>();
    }

    void Update()
    {
        camMove();
    }

    private void camMove()
    {
        if(cam != null)
        {
            if(gm.stageLevel > 1)
            {
                Vector2 dist = new Vector2(0, this.transform.position.y - cam.transform.position.y) * Time.deltaTime * 0.7f;
                cam.transform.Translate(dist);
            }
        }
    }


}
