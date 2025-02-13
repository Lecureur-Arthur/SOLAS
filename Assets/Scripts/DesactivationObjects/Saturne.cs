using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saturne : MonoBehaviour
{
    public GameObject capsule;
    public GameObject panneauxSolaire;

    // Start is called before the first frame update
    void Start()
    {
        capsule.SetActive(false);
        panneauxSolaire.SetActive(false);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}