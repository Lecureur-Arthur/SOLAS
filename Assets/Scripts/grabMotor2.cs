using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class grabMotor2 : MonoBehaviour
{
    
    private XRGrabInteractable grabInteractable;
    public GameObject motor2;

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Motor2 grab");
        gameObject.SetActive(false);
        motor2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}