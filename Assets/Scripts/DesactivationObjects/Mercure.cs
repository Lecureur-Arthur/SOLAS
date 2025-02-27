using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mercure : MonoBehaviour
{
    public GameObject motor2;
    public GameObject cryo;
    public GameObject capsule;
    public GameObject panneauxSolaires;
    public GameObject cube1PanneauxSolaires;
    public GameObject cube2PanneauxSolaires;
    public GameObject cube3PanneauxSolaires;

    // Start is called before the first frame update
    void Start()
    {
        motor2.SetActive(false);
        cryo.SetActive(false);
        capsule.SetActive(false);
        panneauxSolaires.SetActive(false); 
        cube1PanneauxSolaires.SetActive(false);
        cube2PanneauxSolaires.SetActive(false);
        cube3PanneauxSolaires.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
