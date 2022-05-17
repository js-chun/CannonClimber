using UnityEngine;
using System.Collections;

public class SFXPlayer : MonoBehaviour
{
    private AudioSource audio;
    private float localVolume = 0.1f;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(PlaySoundOnce());
    }

    private IEnumerator PlaySoundOnce()
    {
        audio.volume = localVolume;
        yield return new WaitForSeconds(audio.clip.length);
        Destroy(this.gameObject);
    }
}
