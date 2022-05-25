using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCannonLevel : MonoBehaviour
{
    private CannonBehaviour cannon;
    private bool isMoving;
    public float currLocX;
    public float speed = 1f;
    private float currSpeed;
    // Start is called before the first frame update
    void Start()
    {
        cannon = GetComponentInChildren<CannonBehaviour>();
        isMoving = false;
        currLocX = -3.5f;
        currSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        StayOnScreen();
        RandomLocation();
        MoveToLocAndFire();
    }

    private void StayOnScreen()
    {
        this.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -3f);
    }

    private void RandomLocation()
    {
        if (!isMoving)
        {
            isMoving = true;
            currLocX = Random.Range(-4, 4) + 0.5f;
        }
    }

    private void MoveToLocAndFire()
    {
        if (isMoving)
        {
            float distance = currLocX - cannon.gameObject.transform.position.x;
            if (distance < -0.025)
            {
                cannon.gameObject.transform.position += (new Vector3(-currSpeed * Time.deltaTime,0,0));
            } 
            else if (distance > 0.025)
            {
                cannon.gameObject.transform.position += (new Vector3(currSpeed * Time.deltaTime,0,0));
            }
            else
            {
                if (currSpeed != 0)
                {
                    StartCoroutine(StopMoving());
                }
            }
            
        }
    }

    private IEnumerator StopMoving()
    {
        if(Random.Range(0f,1f) > 0.5f)
        {
            cannon.SetOnOff(true);
        }
        currSpeed = 0;
        yield return new WaitForSeconds(4f);
        isMoving = false;
        currSpeed = speed;
    }
}
