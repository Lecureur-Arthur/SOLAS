using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terre : MonoBehaviour
{
    public GameObject motor2;
    public GameObject salleJardin;
    public GameObject detailsSerre;
    public GameObject salleJardin2;
    public GameObject cuisine;
    public GameObject cryo;
    public GameObject capsule;
    public GameObject panneauxSolaire;

    // Start is called before the first frame update
    void Start()
    {
        salleJardin.SetActive(false);
        salleJardin2.SetActive(false);
        detailsSerre.SetActive(false);
        cuisine.SetActive(false);
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