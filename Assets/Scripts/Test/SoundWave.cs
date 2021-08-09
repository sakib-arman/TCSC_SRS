using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWave : MonoBehaviour
{
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        int sampleFreq = 44000;
        float frequency = 440;

        float[] samples = new float[44000];
        for (int i = 0; i < samples.Length; i++)
        {
            samples[i] = Mathf.Sin(Mathf.PI * 2 * i * frequency / sampleFreq);
        }
        AudioClip clip = AudioClip.Create("Test", samples.Length, 1, sampleFreq, false);
        clip.SetData(samples, 0);
        source.clip = clip;
        source.Play();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
