using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroides : MonoBehaviour
{
    public GameObject motor2;
    public GameObject salleJardin;
    public GameObject detailsSerre;
    public GameObject salleJardin2;
    public GameObject parabole;
    public GameObject cuisine;
    public GameObject cryo;
    public GameObject capsule;
    public GameObject panneauxSolaires;
    public GameObject motor;
    public GameObject battery;
    public GameObject citerne;
    public GameObject cube1PanneauxSolaires;
    public GameObject cube2PanneauxSolaires;
    public GameObject cube3PanneauxSolaires;

    // Start is called before the first frame update
    void Start()
    {
        salleJardin.SetActive(false);
        salleJardin2.SetActive(false);
        detailsSerre.SetActive(false);
        parabole.SetActive(false);
        cuisine.SetActive(false);
        cryo.SetActive(false);
        capsule.SetActive(false);
        panneauxSolaires.SetActive(false);  
        motor.SetActive(false);
        battery.SetActive(false);
        citerne.SetActive(false);
        motor2.SetActive(false);
        cube1PanneauxSolaires.SetActive(false);
        cube2PanneauxSolaires.SetActive(false);
        cube3PanneauxSolaires.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
