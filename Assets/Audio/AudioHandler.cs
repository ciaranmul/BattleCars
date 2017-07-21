using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour {

    bool canPlay;
    public AudioSource soundClip;

    public void PlayAudio(AudioClip clip)
    {

        if (canPlay)
            canPlay = false;
        GetComponent<AudioSource>().PlayOneShot(clip);

        StartCoroutine(Reset());

    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(.2f);
        canPlay = true;
    }

}
