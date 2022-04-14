using UnityEngine;

public class CannonBallBehaviour : MonoBehaviour
{
    public float ballSpeed = 3f;
    void Start()
    {
        
    }

    void Update()
    {
        ballMove();
    }

    //method for instantiating explosion animation

    private void ballMove()
    {
        transform.localPosition -= new Vector3(ballSpeed, 0, 0) * Time.deltaTime;
        //add rotation?
    }
}
