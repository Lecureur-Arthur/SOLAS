using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VattalusInteractable : MonoBehaviour
{
    public bool debug = false;
    // The script handles logic related to interactions with various objects (open doors, switch lights occupy seats etc)

    private VattalusSceneController sceneController;

    // on/off states for various functionality (door open/closed, switch on/off, seat occupied/unoccupied etc)
    public bool defaultState = false; //defailt activated state. We might want some interactables to start off as activated
    private bool activated;
    public bool isActivated { get { return activated; } }

    // texts shown on UI to activate/deactivate interactable
    public string activateText = "Open";
    public string deactivateText = "Close";

    public AudioSource activateSoundEffect;
    public AudioSource deactivateSoundEffect;

    [Tooltip("when this interactable is activated, it will trigger other linked interactables. Useful for linking multiple doors, lights etc together")]
    public List<VattalusInteractable> linkedInteractables = new List<VattalusInteractable>();
    [Tooltip("when set to true, the linked interactables will conform to this one's new state")]
    public bool conformLinked = false;

    //Animation variables. Objects can be animated using 2 methods: using an animator component and animation file, OR via script (give it the delta position/rotation and anim duration)
    //animate using anim file
    [Header("Animation controlled by animation clip")]
    public Animation animationComponent;

    //animate using script
    [Header("Animation controlled by script via parameters")]
    public Vector3 rotationAnimation = Vector3.zero;
    public Vector3 positionAnimation = Vector3.zero;
    public float animationDuration = 2f;

    private Quaternion initialRotation; //save initial rotation and position in order to reset when needed
    private Vector3 initialPosition;
    private Vector3 targetPos;
    //bunch of internal variables to handle the script-based animations
    private AnimationCurve animCurve;
    private float animStartTime;
    private float animCompleteTime = -1f; //timestamp when the animation is complete, and the object becomes interactable again

    [Header("Seat-type interactable")]
    public bool isSeat = false;
    public Transform seatCameraAnchor = null;
    public Vector2 seatCamAngleConstraints = new Vector2(90f, 90f);

    [Header("Additional Effects")]
    public bool toggleParticles = false; //if true, the script will find all particle systems in its list of child gameobjects, and it will toggle them
    private ParticleSystem[] particleSystems = null;

    [Header("Event Callbacks")]
    public UnityEvent onAnimationStartEvent;
    public UnityEvent onAnimationEndEvent;
    public UnityEvent onActivateEvent;
    public UnityEvent onDeactivateEvent;



    void Start()
    {
        //find the sceneController script
        sceneController = FindObjectOfType<VattalusSceneController>();
        if (sceneController == null) Debug.Log("color=#FF0000>VattalusAssets: Interactable object did not find reference to the VattalusSceneController. Make sure to include it in the scene</color>");

        activated = defaultState;
        animCompleteTime = -1f;


        //initialize the animation clip (if its the case)
        if (animationComponent == null) animationComponent = GetComponent<Animation>();

        initialRotation = transform.localRotation;
        initialPosition = transform.localPosition;
        targetPos = initialPosition + positionAnimation;

        //initialize sound effects
        if (activateSoundEffect != null) activateSoundEffect.gameObject.SetActive(false);
        if (deactivateSoundEffect != null) deactivateSoundEffect.gameObject.SetActive(false);

        //find all particle systems under this gameobject
        if (toggleParticles)
        {
            particleSystems = GetComponentsInChildren<ParticleSystem>(true);
            //initialize state of particle systems
            foreach (var particles in particleSystems)
            {
                if (activated) particles.Play(); else particles.Stop();
            }
        }

        InitializeAnimCurve();
    }


    //this method is called when the player interacts with this object
    public void Interact(bool instant = false, bool force = false) //instant will snap to the the end of the animation, force will ignore the "canInteract" limitation
    {
        //ignore if object cannot be interacted with right now
        if (!CanInteract && !force) return;

        activated = !activated;

        //determine if interactions activates/deactivates the object, whatever that might mean (open/close door, switch on/off lights etc)
        if (animationComponent != null && animationComponent.clip != null)
        {
            //if the animation is handled by an animation file
            if (activated == true)
            {
                //Activate/Open/Deploy
                if (animationComponent != null)
                {
                    animationComponent[animationComponent.clip.name].speed = 1f;
                    animationComponent[animationComponent.clip.name].time = instant ? animationComponent.clip.length : 0f;
                }
            }
            else
            {
                //Deactivate/Close/Shut
                if (animationComponent != null)
                {
                    animationComponent[animationComponent.clip.name].speed = -1f;
                    animationComponent[animationComponent.clip.name].time = instant ? 0f : animationComponent.clip.length;
                }
            }

            animCompleteTime = instant ? Time.time + 0.1f : Time.time + animationComponent.clip.length;
            animationComponent.Play();
        }
        else
        {
            //if the animation is handled via script
            animStartTime = instant ? Time.time - animationDuration : Time.time;
            animCompleteTime = Time.time + animationDuration + 0.1f;
        }

        //toggle game objects and particles
        if (toggleParticles)
        {
            foreach (var particles in particleSystems)
            {
                if (activated) particles.Play(); else particles.Stop();
            }
        }

        //trigger callback functions
        TriggerCallbacks();

        //play sound effects
        if (!instant)
        {
            if (activateSoundEffect != null) activateSoundEffect.gameObject.SetActive(activated);
            if (deactivateSoundEffect != null) deactivateSoundEffect.gameObject.SetActive(!activated);
        }

        //trigger the callback method when starting the animation
        if (onAnimationStartEvent != null) onAnimationStartEvent.Invoke();


        //toggle the linked interactables as well
        if (linkedInteractables != null)
        {
            foreach (var linked in linkedInteractables)
            {
                if (linked != null && conformLinked && linked.isActivated != this.isActivated) linked.Interact();
            }
        }
    }

    public void InteractForced() { Interact(false, true); } //Regardless of the state of this interactable, force it to change state
    public void InteractForceToState(bool newState) //Change state by force to the desired state
    {
        if (activated == newState) return;
        InteractForced();
    }

    // can the player interact with the object? certain actions can temporarily block interactions (ex: while an animation is playing)
    public bool CanInteract { get { return !IsAnimating; } }

    public bool IsAnimating { get { return Time.time <= animCompleteTime; } }

    void Update()
    {
        //handle the script animation
        if (IsAnimating && (rotationAnimation != Vector3.zero || positionAnimation != Vector3.zero))
        {
            transform.localPosition = Vector3.Lerp(
                activated ? initialPosition : targetPos,
                activated ? targetPos : initialPosition,
                animCurve.Evaluate(Time.time - animStartTime));

            Quaternion targetRot = initialRotation * Quaternion.Euler(rotationAnimation);

            transform.localRotation = Quaternion.Slerp(
                activated ? initialRotation : targetRot,
                activated ? targetRot : initialRotation,
                animCurve.Evaluate(Time.time - animStartTime));
        }

        //check for animation end in order to trigger the method callback
        if (!IsAnimating && Time.time - animCompleteTime < Time.deltaTime && onAnimationEndEvent != null) onAnimationEndEvent.Invoke();
    }

    //to simplify the scene editor and to avoid having to set the animation curve for each interactable object, i decided to build a generic animation curve via keyframes in the script.
    private void InitializeAnimCurve()
    {
        animCurve = new AnimationCurve();
        animCurve.AddKey(new Keyframe(0, 0));
        animCurve.AddKey(new Keyframe(animationDuration, 1));
    }

    private void TriggerCallbacks()
    {
        if (activated && onActivateEvent != null) onActivateEvent.Invoke();
        if (!activated && onDeactivateEvent != null) onDeactivateEvent.Invoke();
    }
}
