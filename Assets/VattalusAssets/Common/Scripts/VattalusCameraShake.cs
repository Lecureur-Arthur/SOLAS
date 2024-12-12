using UnityEngine;
using System.Collections;


//This script generates camera shake and camera sway information based on inputs. Our custom camera scripts then have to add the shake information

//Camera shake functionality is desiged to be called every frame with the desired camera shake parameters. When the Shake() method stops being called every frame, the intensity will slowly fade to 0

//As opposed to camera shake, swaying only needs to be called once with the desired parameters, and swaying will continue untill the Sway() method is called with new parameters (or with a 0 intensity parameter to stop)
public class VattalusCameraShake : MonoBehaviour
{
    public static Vector3 shakePosOffset = Vector3.zero;
    public static Quaternion shakeRotOffset = Quaternion.identity;
    public float shakeFadeSpeed = 1f;

    //shake parameters
    private static float shakeIntensity = 0f; //how far the camera is nudged while shaking
    private static float shakeFrequency = 1f; //how fast the camera is shaking

    //private variables to calculate smooth lerping of values
    private Vector3 shakePosTarget = Vector3.zero;
    private Quaternion shakeRotTarget = Quaternion.identity;
    private float nextShakeTime = 0f;


    //sway parameters
    public static Vector3 swayPosOffset = Vector3.zero;
    public static Vector2 currentSwayAngles = Vector2.zero;
    public static Quaternion GetSwayRotation(float multiplier)
    {
        return Quaternion.Euler(new Vector3(currentSwayAngles.x, currentSwayAngles.y, 0f) * multiplier);
    }

    public float swayFadeSpeed = 0.5f;

    //shake parameters
    public static float swayIntensity = 0.5f; //how far the camera is rotated while swaying
    private static float targetSwayIntensity = 0f; //the actual swayIntensity variable will slowly lerp towards this value
    public static float swayFrequency = 0.4f; //how fast the camera is swaying

    //private variables to calculate smooth lerping of values
    private Vector3 swayPosTarget = Vector3.zero;


    //for the swaying, we handle the x and y axis independently to get a more natural look.
    private Vector2 swayStartAngles = Vector2.zero;
    private Vector2 swayTargetAngles = Vector2.zero;
    private Vector2 swayStartTimes = Vector2.zero;
    private Vector2 swayDurationTimes = Vector2.zero;

    public AnimationCurve swayCurve;


    void Awake()
    {
        //shaking initializations
        shakeIntensity = 0f;
        shakePosOffset = Vector3.zero;
        shakeRotOffset = Quaternion.identity;

        targetSwayIntensity = 0f;
        swayPosOffset = Vector3.zero;

        nextShakeTime = 0f;

        //swaying initializations
        currentSwayAngles = Vector2.zero;

        swayStartAngles = Vector2.zero;
        swayTargetAngles = Vector2.zero;

        swayStartTimes = Vector2.zero;
        swayDurationTimes = Vector2.zero;

        SetSway(swayIntensity, swayFrequency);
    }

    void Update()
    {
        #region Shaking Calculations
        //recalculate shaking
        if (Time.realtimeSinceStartup > nextShakeTime)
        {
            //time to generate a new shake target
            shakePosTarget = Random.insideUnitSphere * shakeIntensity / 100f;

            Vector3 randomShakeAngle = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f) * 6f;
            shakeRotTarget = Quaternion.Euler(randomShakeAngle * shakeIntensity);

            //update the next shake time
            nextShakeTime = Time.realtimeSinceStartup + (1f / shakeFrequency) * Random.Range(0.85f, 1.15f);
        }

        shakePosOffset = Vector3.Lerp(shakePosOffset, shakePosTarget, shakeFrequency / Random.Range(1.1f, 2f) * Time.deltaTime);
        shakeRotOffset = Quaternion.Lerp(shakeRotOffset, shakeRotTarget, shakeFrequency / Random.Range(15f, 30f) * Time.deltaTime);

        //smoothly fade out the shaking
        shakeIntensity = Mathf.Lerp(shakeIntensity, 0f, shakeFadeSpeed * Time.deltaTime);
        #endregion


        #region Sway calculations
        //recalculate swaying

        //x angle
        if (Time.realtimeSinceStartup > swayStartTimes.x + swayDurationTimes.x)
        {
            //randomize new x angle
            float randomAngle = Random.Range(-1f, 1f);
            swayStartAngles.x = currentSwayAngles.x;
            swayTargetAngles.x = randomAngle * 10f * swayIntensity;

            swayStartTimes.x = Time.realtimeSinceStartup;
            swayDurationTimes.x = (1f / swayFrequency) * Random.Range(0.8f, 1.3f) * Mathf.Lerp(0.8f, 1.7f, Mathf.Abs(randomAngle));
        }

        //y angle
        if (Time.realtimeSinceStartup > swayStartTimes.y + swayDurationTimes.y)
        {
            //randomize new y angle
            float randomAngle = Random.Range(-1f, 1f);
            swayStartAngles.y = currentSwayAngles.y;
            swayTargetAngles.y = randomAngle * 10f * swayIntensity;

            swayStartTimes.y = Time.realtimeSinceStartup;
            swayDurationTimes.y = (1f / swayFrequency) * Random.Range(0.8f, 1.3f) * Mathf.Lerp(0.8f, 1.7f, Mathf.Abs(randomAngle));
        }

        //smoothly animate the sway angles
        currentSwayAngles = new Vector2(
            Mathf.Lerp(swayStartAngles.x, swayTargetAngles.x, swayCurve.Evaluate((Time.realtimeSinceStartup - swayStartTimes.x) / swayDurationTimes.x)),
            Mathf.Lerp(swayStartAngles.y, swayTargetAngles.y, swayCurve.Evaluate((Time.realtimeSinceStartup - swayStartTimes.y) / swayDurationTimes.y)));


        swayPosOffset = Vector3.Lerp(swayPosOffset, swayPosTarget, swayFrequency / Random.Range(1.1f, 2f) * Time.deltaTime);

        //smoothly fade the intensity of the swaying
        swayIntensity = Mathf.Lerp(swayIntensity, targetSwayIntensity, swayFadeSpeed * Time.deltaTime);
        #endregion

    }

    //This method is called to generate shaking (intensity is designed to work between 0 and 1, though it can work with higher values as well)
    public static void Shake(float intensity, float frequency)
    {
        shakeIntensity = Mathf.Max(shakeIntensity, intensity);
        shakeFrequency = Mathf.Max(shakeFrequency, frequency);
    }

    //this method is called onces to set the swaying, the script will then continously generate swaying values
    public static void SetSway(float intensity, float frequency, bool instant = false)
    {
        targetSwayIntensity = intensity;
        if (instant) swayIntensity = intensity;

        swayFrequency = frequency;
    }
}