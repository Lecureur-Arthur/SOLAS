using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VattalusPostProcessController : MonoBehaviour
{
    public PostProcessProfile profile;
    private DepthOfField dofSettings;

    void Start()
    {
        if (profile != null) profile.TryGetSettings<DepthOfField>(out dofSettings);
    }

    void Update()
    {
        //Dynamically adjust Depth of field focus distance onto the point at which the camera is looking at
        float objectDistance = 5f;
        RaycastHit hit;
        var cameraCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, Camera.main.nearClipPlane));
        if (Physics.Raycast(cameraCenter, Camera.main.transform.forward, out hit, 50f))
        {
            objectDistance = Vector3.Distance(hit.point, Camera.main.transform.position);
        }

        if (dofSettings != null)
        {
            float lerpSpeed = objectDistance > dofSettings.focusDistance.value ? 1f : 12f; //focus inward faster than outward
            dofSettings.focusDistance.value = Mathf.Lerp(dofSettings.focusDistance.value, objectDistance, lerpSpeed * Time.deltaTime);
        }
    }
}
