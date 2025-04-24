using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class grabObject : MonoBehaviour
{

    private XRGrabInteractable grabInteractable;
    public GameObject objectRetrouve;

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("object grab");
        gameObject.SetActive(false);
        objectRetrouve.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
