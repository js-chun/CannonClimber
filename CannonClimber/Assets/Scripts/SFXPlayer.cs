using UnityEngine;
using System.Collections;

public class SFXPlayer : MonoBehaviour
{
    private AudioSource sfx;
    private float localVolume = 0.1f;

    private void Awake()
    {
        sfx = GetComponent<AudioSource>();
        StartCoroutine(PlaySoundOnce());
    }

    private IEnumerator PlaySoundOnce()
    {
        sfx.volume = localVolume;
        yield return new WaitForSeconds(sfx.clip.length);
        Destroy(this.gameObject);
    }
}
