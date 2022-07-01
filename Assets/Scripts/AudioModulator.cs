using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioModulator : MonoBehaviour
{
    [Range(1, 20000)]
    public float Frequency = 0;
    public float SampleRate = 44100;

    public float NoteC = 523.25f;
    public float NoteD = 587.33f;
    public float NoteE = 659.26f;
    public float NoteF = 698.46f;
    public float NoteG = 783.99f;


    float _phase = 0;
    AudioSource _audioSource;

    void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = true;
        _audioSource.spatialBlend = 0; // 2D audio
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            Frequency = NoteC;
        else if (Input.GetKey(KeyCode.Alpha2))
            Frequency = NoteD;
        else if (Input.GetKey(KeyCode.Alpha3))
            Frequency = NoteE;
        else if (Input.GetKey(KeyCode.Alpha4))
            Frequency = NoteF;
        else if (Input.GetKey(KeyCode.Alpha5))
            Frequency = NoteG;
        else
        {
            Frequency = 0;
        }
    }


    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i += channels)
        {
            _phase += 2 * Mathf.PI * Frequency / SampleRate;

            data[i] = Mathf.Sin(_phase);

            if (_phase >= 2 * Mathf.PI)
            {
                _phase -= 2 * Mathf.PI;
            }
        }
    }
    public float CreateSine(int timeIndex, float frequency, float sampleRate)
    {
        return Mathf.Sin(2 * Mathf.PI * timeIndex * frequency / sampleRate);
    }
}
