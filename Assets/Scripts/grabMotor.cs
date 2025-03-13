using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class grabMotor : MonoBehaviour
{
    
    private XRGrabInteractable grabInteractable;

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Motor grab");
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}