using UnityEngine;


//script that handles the orbit behaviour of the camera when the player is sitting in the pilot seat
public class VattalusOrbitCamera : MonoBehaviour
{
    public Transform targetTransform;
    public Transform cameraParent;
    public Camera cameraComponent;

    public float cameraSensitivity = 90;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    public float orbitDistance = 0f;
    public Vector2 orbitDistanceLimits = new Vector2(10f, 60f);
    [Tooltip("While rotating around an object, the focus point will move forward/back to keep the object more centered")]
    public float baseShiftAmount = 5f;

    [Header("Camera Shake and Sway")]
    [Tooltip("The amount of camera shake applied to this camera. To disable shaking, set to 0")]
    public float shakeIntensity = 1f;
    [Tooltip("The amount of camera sway applied to this camera. To disable swaying, set to 0")]
    public float swayIntensity = 1f;


    void Start()
    {
        //set starting orbit distance
        if (orbitDistance < orbitDistanceLimits.x) orbitDistance = orbitDistanceLimits.x;
    }


    void Update()
    {
        //Read mouse input
        rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -90, 90);
        orbitDistance += -Input.mouseScrollDelta.y;
        orbitDistance = Mathf.Clamp(orbitDistance, orbitDistanceLimits.x, orbitDistanceLimits.y);

        //move the focus point of the camera to the target position
        if (targetTransform != null)
        {
            transform.rotation = targetTransform.rotation;
            if (cameraParent != null)
            {
                cameraParent.localRotation = Quaternion.Euler(-rotationY, rotationX, 0f);
            }


            transform.position = targetTransform.position;

            //add a position offset to the camera focus point depending on the angle of the camera, giving it a cool effect of keeping the ship more centered
            float baseShift = Mathf.Cos(rotationX * Mathf.Deg2Rad) * -baseShiftAmount;
            transform.position += targetTransform.forward * baseShift;
        }


        if (cameraComponent != null)
        {
            //calculate the distance factor, basically how close/far the current orbit distance is relative to orbit distance limits
            float distanceFactor = Mathf.Clamp01(orbitDistance / orbitDistanceLimits.y) * 0.8f;

            //Add camera shake
            cameraComponent.transform.localPosition = VattalusCameraShake.shakePosOffset * shakeIntensity * distanceFactor;
            cameraComponent.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(VattalusCameraShake.shakeRotOffset.eulerAngles * shakeIntensity), Quaternion.identity, distanceFactor);

            //Add camera sway
            cameraComponent.transform.localRotation *= VattalusCameraShake.GetSwayRotation(swayIntensity);

            //move the camera component based on orbit distance
            cameraComponent.transform.localPosition -= new Vector3(0f, 0f, orbitDistance); ;
        }
    }
}
