using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VattalusThrusterController : MonoBehaviour
{
    //This script handles the visual effects of the directional thrusters
    //It has variables to determine which movement type this thruster contributes to when maneuvering the ship. The ship controller will then selectively enable thrusters based on the desired movement.


    [Header("Check the movement types for which you want this thruster to be enabled")]
    //list of movements the thruster contributes to
    public bool usedForAcceleration = false;
    public bool usedForDeceleration = false;
    public bool usedForStrafeLeft = false;
    public bool usedForStrafeRight = false;
    public bool usedForMoveUp = false;
    public bool usedForMoveDown = false;
    public bool usedForRollLeft = false;
    public bool usedForRollRight = false;
    public bool usedForPitchUp = false;
    public bool usedForPitchDown = false;
    public bool usedForYawLeft = false;
    public bool usedForYawRight = false;

    [Space]

    private float currentThrust = 0f;
    private float thrustValue = 0f;
    [Header("Thrust effect buildup and decay speeds")]
    public float thrustIncreaseSpeed = 2f;
    public float thrustDecaySpeed = 1f;

    [Header("Effect references")]
    public Light lightComponent = null;
    private float lightMaxIntensity = 1f;
    public List<ParticleSystem> particles = new List<ParticleSystem>();
    public Renderer engineGlowMesh = null;
    private Color glowColor;

    public bool enableFlicker;
    public float flickerSpeed = 1f;
    public float flickerIntensity = 1f;
    private float flickerFactor = 1f;


    void Start()
    {
        currentThrust = 0f;
        particles = new List<ParticleSystem>();
        particles = GetComponentsInChildren<ParticleSystem>(true).ToList();

        if (lightComponent != null) lightMaxIntensity = lightComponent.intensity;
        if (engineGlowMesh != null) glowColor = engineGlowMesh.material.GetColor("_Color");
    }

    void Update()
    {
        //we want the thruster effect to to increase and decrease with different speeds
        float lerpSpeed = thrustValue > currentThrust ? thrustIncreaseSpeed : thrustDecaySpeed;

        //smoothly adjust the current thrust value towards the desired value. This variable will then drive the various effects
        currentThrust = Mathf.Lerp(currentThrust, thrustValue, lerpSpeed * Time.deltaTime);

        //update effect

        //update flicker
        if (currentThrust > 0.1f && enableFlicker)
        {
            float flickerMinThreshold = Mathf.Min(0.1f, 0.75f - flickerIntensity);
            float flickerMaxThreshold = 0.75f + (flickerIntensity / 2f);
            flickerFactor = Mathf.Lerp(flickerFactor, Random.Range(flickerMinThreshold, flickerMaxThreshold), flickerSpeed);
        }
        else
        {
            flickerFactor = 1f;
        }

        //particles
        if (particles != null)
        {
            foreach (var particleElement in particles)
            {
                ParticleSystem.EmissionModule particleEmission = particleElement.emission;
                ParticleSystem.MainModule particleMain = particleElement.main;

                if (currentThrust > 0.1f)
                {
                    particleEmission.enabled = true;
                    particleMain.startColor = new Color(particleMain.startColor.color.r, particleMain.startColor.color.g, particleMain.startColor.color.b, currentThrust * flickerFactor);
                }
                else
                {
                    //disable particle system if thrust level is low
                    particleEmission.enabled = false;
                }
            }
        }

        //light component
        if (lightComponent != null)
        {
            lightComponent.intensity = currentThrust * lightMaxIntensity * flickerFactor;
        }

        //glow mesh
        if (engineGlowMesh != null)
        {
            engineGlowMesh.material.SetColor("_Color", new Color(glowColor.r, glowColor.g, glowColor.b, currentThrust));
            engineGlowMesh.material.SetColor("_EmissionColor", new Color(glowColor.r, glowColor.g, glowColor.b) * Mathf.Max(0.01f, currentThrust) * 10f);
        }
    }

    public void SetThrust(float thrustVal)
    {
        thrustValue = thrustVal;
    }
}
