using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


//This script handles the functions specific to controlling the spaceship (inputs, movement, landing gear animations, etc)
public class VattalusSpaceshipController : MonoBehaviour
{
    [Tooltip("Should the landing gear be deployed at the start of the scene?")]
    public bool DeployLandingGearByDefault = false;

    //CONTROLS

    //List of input keys
    [Header("Spaceship Controls")]
    public KeyCode accelerateInputKey = KeyCode.LeftShift;
    public KeyCode decelerateInputKey = KeyCode.LeftControl;
    public KeyCode strafeLeftInputKey = KeyCode.A;
    public KeyCode strafeRightInputKey = KeyCode.D;
    public KeyCode moveUpInputKey = KeyCode.Space;
    public KeyCode moveDownInputKey = KeyCode.LeftAlt;
    public KeyCode rollLeftInputKey = KeyCode.Q;
    public KeyCode rollRightInputKey = KeyCode.E;
    public KeyCode pitchUp = KeyCode.S;
    public KeyCode pitchDown = KeyCode.W;
    public KeyCode yawLeft = KeyCode.LeftArrow;
    public KeyCode yawRight = KeyCode.RightArrow;

    [Space]
    public KeyCode landingGearKey = KeyCode.G;
    public KeyCode hologramKey = KeyCode.H;
    public KeyCode rampKey = KeyCode.J;


    [Header("Movement Variables")]
    public bool enableMovement = false;
    public float maxSpeed = 200f;
    public float maxRotation = 3f;

    public float fwdThrust = 10f;
    public float backThrust = 6f;

    public float verticalThrust = 6f;
    public float lateralThrust = 6f;
    public float rollThrust = 4f;
    public float pitchThrust = 5f;
    public float yawThrust = 5f;

    [Space]

    //important references
    [Header("Important Components")]
    public List<VattalusThrusterController> listOfThrusters = new List<VattalusThrusterController>();
    public List<VattalusInteractable> landingGearList = new List<VattalusInteractable>();
    public VattalusInteractable pilotSeat;
    public VattalusInteractable ramp;
    public VattalusInteractable hologram;
    [HideInInspector]
    public bool PlayerInPilotSeat = false;

    public Transform Joystick;
    public Transform ThrottleControl;

    private Vector3 joystickAngleTarget = new Vector3();
    private float throttleAngleTarget = 0f;

    [Header("Sound Effects")]
    public VattalusEngineSoundController IdlingSound;
    public VattalusEngineSoundController ThrustSound;
    public VattalusEngineSoundController RevThrustSound;
    public VattalusEngineSoundController ManeuverSound;


    //parameters that control the amount of shaking produced during maneuvering (this will influence camera shake, and other potential behaviours)
    [Header("Shaking")]
    public float accelerationShakeAmplitude = 0.2f;
    public float reverseShakeAmplitude = 0.12f;
    public float maneuveringShakeAmplitude = 0.06f;
    [Space]
    public float accelerationShakeFrequency = 40f;
    public float reversetionShakeFrequency = 60f;
    public float maneuveringShakeFrequency = 80f;
    [Space]
    public float shakeSmoothingSpeed = 2f; //how smoothly should we change the shake parameters
    private Vector2 shakeValues = new Vector2(0f, 1f); //[Amplitude, Frequency] final result of calculating the shake factors


    //player input values
    public float accelerationInput = 0f;
    [HideInInspector] public float strafeInput = 0f;
    [HideInInspector] public float upDownInput = 0f;
    [HideInInspector] public float rollInput = 0f;
    [HideInInspector] public float pitchInput = 0f;
    [HideInInspector] public float yawInput = 0f;

    public VattalusFakeSkyboxMovement fakeSkyboxController;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (DeployLandingGearByDefault) DeployLandingGear(true);
        shakeValues = new Vector2(0f, 1f);

        //Initialize References/Variables
        listOfThrusters = new List<VattalusThrusterController>();
        foreach (var ThrusterController in GetComponentsInChildren<VattalusThrusterController>())
        {
            listOfThrusters.Add(ThrusterController);
        }

        //reference checks
        if (listOfThrusters == null || listOfThrusters.Count == 0) Debug.Log("<color=#FF0000>VattalusAssets: [ShipController] List of thrusters null or empty.</color>");
        if (landingGearList == null || landingGearList.Count == 0) Debug.Log("<color=#FF0000>VattalusAssets: [ShipController] Landing gear references null or empty.</color>");
        if (pilotSeat == null) Debug.Log("<color=#FF0000>VattalusAssets: [ShipController] Missing pilot seat reference.</color>");
    }

    void Update()
    {
        //only enable spaceship controls when sitting in the pilot seat (or when in exterior demo mode)
        if (VattalusSceneController.Instance.ExteriorOnlyDemoMode) enableMovement = true;
        else enableMovement = PlayerInPilotSeat;
    }

    private void FixedUpdate()
    {
        //We set the command inputs based on keypresses (or other input methods). They will be processed later
        accelerationInput = 0f;
        strafeInput = 0f;
        upDownInput = 0f;
        rollInput = 0f;
        pitchInput = 0f;
        yawInput = 0f;

        //Get inputs
        if (enableMovement)
        {
            if (Input.GetKey(accelerateInputKey)) accelerationInput = 1f; if (Input.GetKey(decelerateInputKey)) accelerationInput -= 1f;
            if (Input.GetKey(strafeRightInputKey)) strafeInput = 1f; if (Input.GetKey(strafeLeftInputKey)) strafeInput -= 1f;
            if (Input.GetKey(moveUpInputKey)) upDownInput = 1f; if (Input.GetKey(moveDownInputKey)) upDownInput -= 1f;
            if (Input.GetKey(rollRightInputKey)) rollInput = 1f; if (Input.GetKey(rollLeftInputKey)) rollInput -= 1f;
            if (Input.GetKey(pitchUp)) pitchInput = 1f; if (Input.GetKey(pitchDown)) pitchInput -= 1f;
            if (Input.GetKey(yawRight)) yawInput = 1f; if (Input.GetKey(yawLeft)) yawInput -= 1f;


            //Add movement force
            if (rb != null)
            {
                ////MOVEMENT   (Actual ship movement is not implemented, as it would be outside the scope of this demo, in order to achieve the desired effect, I instead chose to rotate the whole environment around the ship to fake movement)
                //              the code below is a very basic implementation of physics movement using forces applied to the rigidbody
                if (enableMovement)
                {
                    //Apply move speed
                    //if (accelerationInput > 0f) rb.AddForce(transform.forward * accelerationInput * fwdThrust * Time.deltaTime);
                    //if (accelerationInput < 0f) rb.AddForce(transform.forward * accelerationInput * backThrust * Time.deltaTime);
                    //if (upDownInput != 0f) rb.AddForce(transform.up * upDownInput * verticalThrust * Time.deltaTime);
                    //if (strafeInput != 0f) rb.AddForce(transform.right * strafeInput * lateralThrust * Time.deltaTime);

                    ////ROTATION
                    //rb.AddRelativeTorque(
                    //    pitchInput * -pitchThrust * Time.deltaTime,
                    //    yawInput * yawThrust * Time.deltaTime,
                    //    rollInput * -rollThrust * Time.deltaTime
                    //);
                }

            }
        }

        //Apply thruster effects
        if (listOfThrusters != null)
        {
            foreach (var thruster in listOfThrusters)
            {
                if (thruster == null) continue;

                //by default, all thrusters will decay
                thruster.SetThrust(0f);

                //Depending on which input is given to the spaceship, enable thrust effects on thrusters whose directions correspond with given input
                if (accelerationInput > 0 && thruster.usedForAcceleration) thruster.SetThrust(accelerationInput);
                if (accelerationInput < 0 && thruster.usedForDeceleration) thruster.SetThrust(-accelerationInput);

                if (strafeInput > 0 && thruster.usedForStrafeRight) thruster.SetThrust(strafeInput);
                if (strafeInput < 0 && thruster.usedForStrafeLeft) thruster.SetThrust(-strafeInput);

                if (upDownInput > 0 && thruster.usedForMoveUp) thruster.SetThrust(upDownInput);
                if (upDownInput < 0 && thruster.usedForMoveDown) thruster.SetThrust(-upDownInput);

                if (rollInput > 0 && thruster.usedForRollRight) thruster.SetThrust(rollInput);
                if (rollInput < 0 && thruster.usedForRollLeft) thruster.SetThrust(-rollInput);

                if (pitchInput > 0 && thruster.usedForPitchUp) thruster.SetThrust(pitchInput);
                if (pitchInput < 0 && thruster.usedForPitchDown) thruster.SetThrust(-pitchInput);

                if (yawInput > 0 && thruster.usedForYawRight) thruster.SetThrust(yawInput);
                if (yawInput < 0 && thruster.usedForYawLeft) thruster.SetThrust(-yawInput);
            }
        }

        //Move Joystick / Throttle control
        if (Joystick != null)
        {
            joystickAngleTarget = new Vector3(
                Mathf.Lerp(25f, -25f, (pitchInput + 1f) / 2f),
                Mathf.Lerp(-20f, 20f, (rollInput + 1f) / 2f),
                Mathf.Lerp(-19f, 19f, (yawInput + 1f) / 2f));

            Joystick.localRotation = Quaternion.Lerp(Joystick.transform.localRotation, Quaternion.Euler(joystickAngleTarget), 5f * Time.deltaTime);
        }

        if (ThrottleControl != null)
        {
            throttleAngleTarget = Mathf.Lerp(throttleAngleTarget, Mathf.Lerp(-33f, 33f, (accelerationInput + 1f) / 2f), 5f * Time.deltaTime);
            ThrottleControl.localRotation = Quaternion.Euler(throttleAngleTarget, 0f, 0f);
        }

        //handle interaction inputs
        if (Input.GetKeyDown(landingGearKey)) DeployLandingGear();
        if (Input.GetKeyDown(rampKey) && ramp != null) ramp.Interact();
        if (Input.GetKeyDown(hologramKey) && hologram != null) hologram.Interact();

        //Handle sound effects
        if (IdlingSound != null) IdlingSound.SetInput(enableMovement ? 1f - Mathf.Abs(accelerationInput) : 0f); //idling sound will play when player in pilot seat but not accelerating
        if (ThrustSound != null) ThrustSound.SetInput(Mathf.Clamp01(Mathf.Max(accelerationInput)));
        if (RevThrustSound != null) RevThrustSound.SetInput(Mathf.Clamp01(-accelerationInput));
        float maneuverintensity = Mathf.Clamp01(Mathf.Abs(strafeInput) + Mathf.Abs(upDownInput) + Mathf.Abs(rollInput) + Mathf.Abs(pitchInput) + Mathf.Abs(yawInput));
        if (ManeuverSound != null) ManeuverSound.SetInput(maneuverintensity);

        //Handle shaking
        float shakeAmplitude = 0f;
        shakeAmplitude += Mathf.Lerp(0f, accelerationShakeAmplitude, Mathf.Clamp01(accelerationInput));
        shakeAmplitude += Mathf.Lerp(0f, reverseShakeAmplitude, Mathf.Clamp01(-accelerationInput));
        shakeAmplitude += Mathf.Lerp(0f, maneuveringShakeAmplitude, Mathf.Clamp01(maneuverintensity));

        float shakeFreq = 1f;
        shakeFreq += Mathf.Lerp(0f, accelerationShakeFrequency, Mathf.Clamp01(accelerationInput));
        shakeFreq += Mathf.Lerp(0f, reversetionShakeFrequency, Mathf.Clamp01(-accelerationInput));
        shakeFreq += Mathf.Lerp(0f, maneuveringShakeFrequency, Mathf.Clamp01(maneuverintensity - Mathf.Abs(accelerationInput) * 0.65f));

        shakeValues = Vector2.Lerp(shakeValues, new Vector2(shakeAmplitude, shakeFreq), shakeSmoothingSpeed * Time.deltaTime);

        VattalusCameraShake.Shake(shakeValues.x, shakeValues.y);

        //update the fake skybox
        if (fakeSkyboxController != null) fakeSkyboxController.MoveFakeSkybox(pitchInput, pitchThrust, yawInput, yawThrust, rollInput, rollThrust);
    }

    private void DeployLandingGear(bool instant = false)
    {
        if (landingGearList != null)
            foreach (var landingGear in landingGearList)
            {
                landingGear.Interact(instant);
            }
    }

    public bool IsLandingGearDeployed()
    {
        if (landingGearList != null && landingGearList.Count > 0)
        {
            return landingGearList[0].isActivated;
        }

        return false;
    }

    public bool IsLandingGearAnimating()
    {
        if (landingGearList != null && landingGearList.Count > 0)
        {
            return landingGearList[0].IsAnimating;
        }

        return false;
    }
}
