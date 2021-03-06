using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    private GameManager gm;
    public float waitTime = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void PlayGame()
    {
        gm.ResetGame();
        gm.maxLives = 2;
        StartCoroutine(LoadGame());
    }

    public void PlayGameFromMenu()
    {
        gm.stageLevel = 1;
        gm.maxLives = 3;
        gm.ResetGame();
        StartCoroutine(LoadGame());
    }

    public void PlayGameFromPause()
    {
        gm.SetPaused(false);
        PlayGame();
    }


    private IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(waitTime);
        gm.stageLevel = 2;
        SceneManager.LoadScene("GameScene");
    }

    public void GoToMenu()
    {
        gm.SetPaused(false);
        StartCoroutine(LoadMenu());
    }

    private IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(waitTime);
        gm.stageLevel = 0;
        SceneManager.LoadScene("MenuScene");
    }
}
