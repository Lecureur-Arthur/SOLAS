using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this script is used to combine 3 layers of sounds with separate intensities and decay speeds in order to achiev a more comlex sound
public class VattalusEngineSoundController : MonoBehaviour
{
    [Header("Low Layer")]
    public AudioSource lowAudio;
    public float lowLerpSpeed = 1f;
    public Vector2 lowVolumeInterval = new Vector2(0f, 1f);
    public Vector2 lowPitchInterval = new Vector2(0.5f, 1f);

    [Header("Mid Layer")]
    public AudioSource midAudio;
    public float midLerpSpeed = 1f;
    public Vector2 midVolumeInterval = new Vector2(0f, 1f);
    public Vector2 midPitchInterval = new Vector2(0.5f, 1f);


    [Header("High Layer")]
    public AudioSource highAudio;
    public float highLerpSpeed = 1f;
    public Vector2 highVolumeInterval = new Vector2(0f, 1f);
    public Vector2 highPitchInterval = new Vector2(0.5f, 1f);


    private float lowIntensity = 0f;
    private float midIntensity = 0f;
    private float highIntensity = 0f;


    void Start()
    {
        if (highAudio != null) highAudio.volume = 0f;
        if (lowAudio != null) lowAudio.volume = 0f;
    }

    //input (1f) controls the intensity of the sound effect
    public void SetInput(float input)
    {
        if (lowAudio != null)
        {
            lowIntensity = Mathf.Lerp(lowIntensity, Mathf.Clamp01(input), Time.deltaTime * lowLerpSpeed);

            lowAudio.volume = Mathf.Lerp(lowVolumeInterval.x, lowVolumeInterval.y, lowIntensity);
            lowAudio.pitch = Mathf.Lerp(lowPitchInterval.x, lowPitchInterval.y, lowIntensity);
        }

        if (midAudio != null)
        {
            midIntensity = Mathf.Lerp(midIntensity, Mathf.Clamp01(input), Time.deltaTime * midLerpSpeed);

            midAudio.volume = Mathf.Lerp(midVolumeInterval.x, midVolumeInterval.y, midIntensity);
            midAudio.pitch = Mathf.Lerp(midPitchInterval.x, midPitchInterval.y, midIntensity);
        }


        if (highAudio != null)
        {
            highIntensity = Mathf.Lerp(highIntensity, Mathf.Clamp01(input), Time.deltaTime * highLerpSpeed);

            highAudio.volume = Mathf.Lerp(highVolumeInterval.x, highVolumeInterval.y, highIntensity);
            highAudio.pitch = Mathf.Lerp(highPitchInterval.x, highPitchInterval.y, highIntensity);
        }
    }
}
