using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venus : MonoBehaviour
{
    public GameObject motor2;
    public GameObject cryo;
    public GameObject capsule;
    public GameObject panneauxSolaire;

    // Start is called before the first frame update
    void Start()
    {
        cryo.SetActive(false);
        capsule.SetActive(false);
        panneauxSolaire.SetActive(false);  
        motor2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}