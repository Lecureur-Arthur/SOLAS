using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class grabParabole : MonoBehaviour
{
    
    private XRGrabInteractable grabInteractable;
    public GameObject parabole;


    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Parabole grab");
        gameObject.SetActive(false);
        parabole.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
