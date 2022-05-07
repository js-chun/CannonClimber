using UnityEngine;

public class PauseOverlay : MonoBehaviour
{
    public GameObject overlay;
    private PlayerBehaviour player;
    private Vector2 loc;


    void Start()
    {
        loc = this.GetComponent<RectTransform>().position;
        player = FindObjectOfType<PlayerBehaviour>();
    }


    void Update()
    {
        if(player != null)
        {
            this.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(player.transform.position);
        }
        overlay.GetComponent<RectTransform>().position = loc;
    }

    public void SetMaskOn(bool onOrOff)
    {
        overlay.SetActive(onOrOff);
    }

    public void FindPlayer()
    {
        player = FindObjectOfType<PlayerBehaviour>();
    }
}
