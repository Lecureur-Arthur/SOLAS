using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class grabBattery : MonoBehaviour
{
    
    private XRGrabInteractable grabInteractable;
    public GameObject battery;

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Battery grab");
        gameObject.SetActive(false);
        battery.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}