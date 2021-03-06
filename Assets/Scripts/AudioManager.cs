using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioNames
{
    MUSIC_01,
    MUSIC_02,
    CLICK_01
}

public class AudioManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.OnPlayAudio += PlayAudio;
        GameEvents.OnStopCurrentMusic += StopCurrentMusic;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayAudio -= PlayAudio;
        GameEvents.OnStopCurrentMusic -= StopCurrentMusic;
    }

    private void StopCurrentMusic()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            var child = this.transform.GetChild(i).gameObject;
            if (child.name.ToLower().Contains("music"))
            {
                var audio = child.GetComponent<AudioSource>();
                audio.Stop();
            }
        }
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
