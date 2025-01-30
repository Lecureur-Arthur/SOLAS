using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDoor : MonoBehaviour
{

    public GameObject salle;
    public GameObject colliderDisable;

    // Start is called before the first frame update
    void Start()
    {
        if (!salle.activeSelf)
        {
            colliderDisable.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
