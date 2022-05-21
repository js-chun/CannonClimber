using UnityEngine;


//Class for Pause Overlay so that cutout stays on Player
public class PauseOverlay : MonoBehaviour
{
    public GameObject overlay;      //Reference to child under pause overlay (gray overlay)
    public GameObject restartBtn;
    public GameObject menuBtn;

    private PlayerBehaviour player;
    private Vector2 loc;            //Initial location stored so child stays in place

    void Start()
    {
        loc = this.GetComponent<RectTransform>().position;
        player = FindObjectOfType<PlayerBehaviour>();
        Canvas canvas = FindObjectOfType<Canvas>();
        overlay.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, canvas.GetComponent<RectTransform>().rect.height);
        overlay.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, canvas.GetComponent<RectTransform>().rect.width);
    }

    void Update()
    {
        KeepOverlayCentered();
    }

    //Moves cutout onto Player
    //Keeps gray overlay (which is child of the cutout parent) centered
    private void KeepOverlayCentered()
    {
        if (player != null)
        {
            this.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(player.transform.position);
        }
        overlay.GetComponent<RectTransform>().position = loc;
    }

    //Turns Mask on or off for pauses/spawning
    public void SetMaskOn(bool onOrOff) { overlay.SetActive(onOrOff); }

    public void SetButtonsOn(bool onOrOff)
    {
        restartBtn.SetActive(onOrOff);
        menuBtn.SetActive(onOrOff);
    }

    //Used in GameManager so that it refers to spawned Player after they fall
    public void FindPlayer() { player = FindObjectOfType<PlayerBehaviour>(); }
}
