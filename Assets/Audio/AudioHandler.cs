using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour {

    bool canPlay;
    [Range(0,1)]
    public float waitTime = 0.2f;
    public AudioSource soundClip;

    private void Start()
    {
        canPlay = true;
    }

    public void PlayAudio(AudioClip clip)
    {

        if (canPlay)
            canPlay = false;
        GetComponent<AudioSource>().PlayOneShot(clip);

        StartCoroutine(Reset());

    }

    public bool CanPlayAudio()
    {
        return canPlay;
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(waitTime);
        canPlay = true;
    }

}
