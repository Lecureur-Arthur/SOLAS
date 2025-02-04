using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class grabBattery : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    public GameObject Battery1;
    public GameObject Battery2;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Grab Batterie");
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Baterie grab");
        Battery1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
