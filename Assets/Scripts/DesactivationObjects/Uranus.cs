using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uranus : MonoBehaviour
{
    public GameObject capsule1;
    public GameObject capsule2;
    public GameObject capsule3;
    public GameObject capsule4;

    // Start is called before the first frame update
    void Start()
    {
        capsule1.SetActive(false);
        capsule2.SetActive(false);
        capsule3.SetActive(false);
        capsule4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}