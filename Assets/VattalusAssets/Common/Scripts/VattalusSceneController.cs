using System;
using Unity.Collections;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VattalusSceneController : VattalusUnitySingleton<VattalusSceneController>
{
    // This script acts as a central hub for all other important scripts in the demo scene and can be accessed easily from all other scripts
    public bool ExteriorOnlyDemoMode = false;


    //Events
    [HideInInspector]
    public UnityEvent PilotSeatEnter;
    [HideInInspector]
    public UnityEvent PilotSeatExit;
    [HideInInspector]
    public UnityEvent<CameraModes> CameraChanged;
    [HideInInspector]
    public UnityEvent PlayerEntersShip;
    [HideInInspector]
    public UnityEvent PlayerExitsShip;


    [Header("Player / Camera / Spaceship")]
    //Player related variables
    public VattalusFirstPersonCamera firstPersonController;

    //Different camera behaviour types
    public enum CameraModes
    {
        Player,
        ShipOrbit
    }
    private CameraModes cameraMode;
    public CameraModes GetCamMode { get { return cameraMode; } }

    //orbit camera that rotates around the spaceship
    public VattalusOrbitCamera orbitCameraController;

    //Spaceship related variables
    public VattalusSpaceshipController spaceshipController;


    [Header("Inputs")]
    public KeyCode cameraKey = KeyCode.C;
    public KeyCode hideUIKey = KeyCode.Tab;

    [Header("UI References")]
    public GameObject reticle;
    public GameObject KeyPromptsParent;
    public GameObject UI_Background;

    [Space]
    // references to the UI elements of key prompts
    public VattalusKeyPrompt interactPrompt;
    public VattalusKeyPrompt standUpPrompt;

    [Space]
    //ship controls prompts
    public VattalusKeyPrompt hologramPrompt;
    public VattalusKeyPrompt landingGearPrompt;
    public VattalusKeyPrompt rampPrompt;

    [Space]
    public VattalusKeyPrompt shipControlsPrompt;
    public Text pitchDownPrompt;
    public Text pitchUpPrompt;
    public Text yawLeftPrompt;
    public Text yawRightPrompt;
    public Text rollLeftPrompt;
    public Text rollRightPrompt;
    public Text acceleratePrompt;
    public Text deceleratePrompt;

    [Space]
    public VattalusKeyPrompt cameraPrompt;
    public VattalusKeyPrompt hideUIPrompt;

    void Start()
    {
        Cursor.visible = false;

        //Check important references and throw warnings for you in case you forgot something
        if (firstPersonController == null) Debug.Log("color=#FF0000>VattalusAssets: [SceneController] Missing reference to first person camera controller</color>");
        if (orbitCameraController == null) Debug.Log("color=#FF0000>VattalusAssets: [SceneController] Missing reference to orbit camera controller</color>");
        if (spaceshipController == null) Debug.Log("color=#FF0000>VattalusAssets: [SceneController] Missing reference to the ship controller</color>");

        if (interactPrompt == null) Debug.Log("<color=#FF0000>VattalusAssets: [SceneController] Missing reference to UI component: Interaction key prompt</color>");
        if (standUpPrompt == null) Debug.Log("<color=#FF0000>VattalusAssets: [SceneController] Missing reference to UI component: Stamp up key prompt</color>");
        if (hologramPrompt == null) Debug.Log("<color=#FF0000>VattalusAssets: [SceneController] Missing reference to UI component: Hologram key prompt</color>");
        if (landingGearPrompt == null) Debug.Log("<color=#FF0000>VattalusAssets: [SceneController] Missing reference to UI component: Landing gear key prompt</color>");
        if (rampPrompt == null) Debug.Log("<color=#FF0000>VattalusAssets: [SceneController] Missing reference to UI component: ramp key prompt</color>");
        if (cameraPrompt == null) Debug.Log("<color=#FF0000>VattalusAssets: [SceneController] Missing reference to UI component: camera key prompt</color>");

        if (shipControlsPrompt == null) Debug.Log("<color=#FF0000>VattalusAssets: Missing reference to UI component: ship controls prompts</color>");


        //set the key prompt values on the UI
        if (spaceshipController != null)
        {
            if (hologramPrompt != null) hologramPrompt.UpdateKeyPromptTexts("Hologram", spaceshipController.hologramKey.ToString());
            if (landingGearPrompt != null) landingGearPrompt.UpdateKeyPromptTexts("Landing Gear", spaceshipController.landingGearKey.ToString());

            if (pitchDownPrompt != null) pitchDownPrompt.text = spaceshipController.pitchDown.ToString();
            if (pitchUpPrompt != null) pitchUpPrompt.text = spaceshipController.pitchUp.ToString();
            if (yawLeftPrompt != null) yawLeftPrompt.text = spaceshipController.yawLeft.ToString();
            if (yawRightPrompt != null) yawRightPrompt.text = spaceshipController.yawRight.ToString();
            if (rollLeftPrompt != null) rollLeftPrompt.text = spaceshipController.rollLeftInputKey.ToString();
            if (rollRightPrompt != null) rollRightPrompt.text = spaceshipController.rollRightInputKey.ToString();
            if (acceleratePrompt != null) acceleratePrompt.text = spaceshipController.accelerateInputKey.ToString();
            if (deceleratePrompt != null) deceleratePrompt.text = spaceshipController.decelerateInputKey.ToString();
        }

        //Initialize

        //Initialize UI
        ShowUI(false);
        ShowUI(true);

        if (spaceshipController != null && orbitCameraController != null && orbitCameraController.targetTransform == null) orbitCameraController.targetTransform = spaceshipController.transform;
        if (cameraPrompt != null) cameraPrompt.UpdateKeyPromptTexts("Camera", cameraKey.ToString());
        if (hideUIPrompt != null) hideUIPrompt.UpdateKeyPromptTexts("Hide Controls", hideUIKey.ToString());

        if (ExteriorOnlyDemoMode)
        {
            //SetPlayerControl(ControlModeTypes.Seated);
            SetCameraMode(CameraModes.ShipOrbit);
            if (spaceshipController != null) spaceshipController.enableMovement = true;
        }
        else
        {
            if (firstPersonController != null) firstPersonController.SetPlayerControl(VattalusFirstPersonCamera.ControlModeTypes.Walking);
            SetCameraMode(CameraModes.Player);
        }
    }

    void Update()
    {
        //When pressing the camera key, switch between camera modes
        if (spaceshipController != null && spaceshipController.PlayerInPilotSeat && Input.GetKeyDown(cameraKey))
        {
            CameraModes newCamMode = CameraModes.Player;
            if (cameraMode == CameraModes.Player) newCamMode = CameraModes.ShipOrbit;
            if (cameraMode == CameraModes.ShipOrbit) newCamMode = CameraModes.Player;

            SetCameraMode(newCamMode);
        }

        #region UI
        //When seated, always show a special UI hint to stand up
        if (firstPersonController != null)
        {
            standUpPrompt.gameObject.SetActive(firstPersonController.currentlyOccupiedSeat != null);
            interactPrompt.gameObject.SetActive(firstPersonController.lookingAtInteractable != null);
        }


        //Update UI prompts when sitting in a seat
        if (firstPersonController != null && firstPersonController.currentlyOccupiedSeat != null)
        {
            string standUpPromptText = firstPersonController.currentlyOccupiedSeat.isActivated ? firstPersonController.currentlyOccupiedSeat.deactivateText : firstPersonController.currentlyOccupiedSeat.activateText;
            if (string.IsNullOrEmpty(standUpPromptText)) standUpPromptText = "Stand Up";

            standUpPrompt.UpdateKeyPromptTexts(standUpPromptText, firstPersonController.standUpKey.ToString(), firstPersonController.currentlyOccupiedSeat.CanInteract);
        }

        //Update UI prompt when looking at an interactable
        if (firstPersonController != null && firstPersonController.lookingAtInteractable != null)
        {
            string interactionPromptText = firstPersonController.lookingAtInteractable.isActivated ? firstPersonController.lookingAtInteractable.deactivateText : firstPersonController.lookingAtInteractable.activateText;
            if (string.IsNullOrEmpty(interactionPromptText)) interactionPromptText = "Interact";

            interactPrompt.UpdateKeyPromptTexts(interactionPromptText, firstPersonController.interactionKey.ToString(), firstPersonController.lookingAtInteractable.CanInteract);
        }

        if (spaceshipController != null)
        {
            //Show ship control specific UI prompts only when player is in the pilot seat (or when in exterior demo mode
            if (UI_Background != null) UI_Background.SetActive(spaceshipController.PlayerInPilotSeat || ExteriorOnlyDemoMode);

            if (hologramPrompt != null) hologramPrompt.gameObject.SetActive((spaceshipController.PlayerInPilotSeat || ExteriorOnlyDemoMode) && spaceshipController.hologram != null);
            if (landingGearPrompt != null) landingGearPrompt.gameObject.SetActive((spaceshipController.PlayerInPilotSeat || ExteriorOnlyDemoMode) && spaceshipController.landingGearList != null && spaceshipController.landingGearList.Count > 0);
            if (rampPrompt != null) rampPrompt.gameObject.SetActive(spaceshipController.PlayerInPilotSeat && spaceshipController.ramp != null);
            if (cameraPrompt != null) cameraPrompt.gameObject.SetActive(spaceshipController.PlayerInPilotSeat && orbitCameraController != null);
            if (hideUIPrompt != null) hideUIPrompt.gameObject.SetActive(spaceshipController.PlayerInPilotSeat || ExteriorOnlyDemoMode);

            if (shipControlsPrompt != null) shipControlsPrompt.gameObject.SetActive(spaceshipController.PlayerInPilotSeat || ExteriorOnlyDemoMode);

            if (spaceshipController.PlayerInPilotSeat || ExteriorOnlyDemoMode)
            {
                if (landingGearPrompt != null) landingGearPrompt.UpdateKeyPromptTexts("Landing Gear", spaceshipController.landingGearKey.ToString(), !spaceshipController.IsLandingGearAnimating());
                if (rampPrompt != null) rampPrompt.UpdateKeyPromptTexts("Ramp", spaceshipController.rampKey.ToString(), spaceshipController.ramp ? spaceshipController.ramp.CanInteract : true);
            }
        }

        //disable reticle when in orbit camera
        if (reticle != null) reticle.SetActive(cameraMode == CameraModes.Player);


        //Hide UI
        if (Input.GetKeyDown(hideUIKey) && KeyPromptsParent != null)
        {
            ShowUI(!KeyPromptsParent.activeSelf);
        }
        #endregion

#if !UNITY_EDITOR
                if (Input.GetKeyDown(KeyCode.Escape))
                    Application.Quit();
#endif
    }

    private void ShowUI(bool state)
    {
        if (KeyPromptsParent != null) KeyPromptsParent.SetActive(state);
    }

    //switches between the different camera modes (currently first person, and orbit cameras)
    public void SetCameraMode(CameraModes newMode)
    {
        cameraMode = newMode;

        if (firstPersonController != null) firstPersonController.gameObject.SetActive(cameraMode == CameraModes.Player);
        if (orbitCameraController != null) orbitCameraController.gameObject.SetActive(cameraMode != CameraModes.Player);

        //notify the ship controller of the camera switch
        if (CameraChanged != null) CameraChanged.Invoke(newMode);
    }
}
