using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioNames
{
    MUSIC_01,
    CLICK_01
}

public class AudioManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.OnPlayAudio += PlayAudio;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayAudio -= PlayAudio;
    }

    private void PlayAudio(AudioNames audioName)
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            var child = this.transform.GetChild(i).gameObject;
            if (child.name == audioName.ToString())
            {
                var audio = child.GetComponent<AudioSource>();
                audio.Stop();
                audio.Play();
            }
        }
    }
}
