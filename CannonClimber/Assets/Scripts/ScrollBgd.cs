using UnityEngine;

//Class to scroll background image in the menu
public class ScrollBgd : MonoBehaviour
{
    private float startLoc = 0.2f;          //start x coordinate of bgd
    private float endLoc = -4.8f;           //end x coordinate of bgd

    public float scrollSpeed = 0.8f;        //speed at how fast to scroll

    void Update()
    {
        Scroll();
    }

    //To move the background grid for scrolling effect
    private void Scroll()
    {
        this.transform.Translate(-1 * scrollSpeed * Time.deltaTime, 0, 0);
        if(this.transform.position.x < endLoc) { this.transform.position = new Vector3(startLoc, 0, 10); }
    }
}
