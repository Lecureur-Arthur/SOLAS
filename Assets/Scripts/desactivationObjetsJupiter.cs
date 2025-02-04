using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class descativationObjetsJupiter : MonoBehaviour
{

	public GameObject salle_jardin;
	public GameObject DetailsSerre;
	public GameObject salle_jardin_2;
	public GameObject Battery;
	public GameObject Citerne_complete;
	public GameObject Parabole;
	public GameObject cuisine;
	public GameObject cryo;
	public GameObject capsules;
	public GameObject panneauxSolaires;


    // Start is called before the first frame update
    void Start()
    {
        salle_jardin.SetActive(false);
		DetailsSerre.SetActive(false);
		salle_jardin_2.SetActive(false);
		Battery.SetActive(false);
		Citerne_complete.SetActive(false);
		Parabole.SetActive(false);
		cuisine.SetActive(false);
		cryo.SetActive(false);
		capsules.SetActive(false);
		panneauxSolaires.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}