using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesactiveObjects : MonoBehaviour
{

    [Header("Boolean planet explore")]
    private Boolean asteroide = false;
    private Boolean mars = false;
    private Boolean jupiter = false;
    private Boolean lune = false;
    private Boolean terre = false;
    private Boolean mercure = false;
    private Boolean venus = false;
    private Boolean saturne = false;
    private Boolean uranus = false;
    private Boolean neptune = false;

    [Header("Current Scene Name")]
    private string currentScene;

    [Header("GameObject Collect")]
    private GameObject motorCollectAsteroide;
    private GameObject batteryCollectJupiter;
    private GameObject paraboleCollectLune;
    private GameObject citerneCollectMars;
    private GameObject motorCollectMercure;
    private GameObject cuisineCollectNeptune;
    private GameObject solarPanelCollectSaturne;
    private GameObject salleJardinCollectTerre;
    private GameObject capsuleCryoCollectUranus;
    private GameObject cryoCollectVenus;

    [Header("GameObject Grab")]
    private GameObject motorGrabAsteroide;
    private GameObject batteryGrabJupiter;
    private GameObject paraboleGrabLune;
    private GameObject citerneGrabMars;
    private GameObject motorGrabMercure;
    private GameObject cuisineGrabNeptune;
    private GameObject solarPanelGrabSaturne;
    private GameObject salleJardinGrabTerre;
    private GameObject capsuleCryoGrabUranus;
    private GameObject salleCryoGrabVenus;

    [Header("Robot Collect")]
    private GameObject petitRobotCollect;
    private GameObject robotCuistoCollect;

    // Start is called before the first frame update
    void Start()
    {
        // Get current scene name
        currentScene = SceneManager.GetActiveScene().name;

        // Get GemeObject collected
        motorCollectAsteroide = GameObject.Find("MotorCollectAsteroide");
        batteryCollectJupiter = GameObject.Find("BatteryCollectJupiter");
        paraboleCollectLune = GameObject.Find("ParaboleCollectLune");
        citerneCollectMars = GameObject.Find("CiterneCollectMars");
        motorCollectMercure = GameObject.Find("MotorCollectMercure");
        cuisineCollectNeptune = GameObject.Find("CuisineCollectNeptune");
        solarPanelCollectSaturne = GameObject.Find("SolarPanelCollectSaturne");
        salleJardinCollectTerre = GameObject.Find("SalleJardinCollectTerre");
        capsuleCryoCollectUranus = GameObject.Find("CapsuleCryoCollectUranus");
        cryoCollectVenus = GameObject.Find("CryoCollectVenus");

        // Get GameObject to grab
        motorGrabAsteroide = GameObject.Find("MotorGrabAsteroide");
        batteryGrabJupiter = GameObject.Find("BatteryGrabJupiter");
        paraboleGrabLune = GameObject.Find("ParaboleGrabLune");
        citerneGrabMars = GameObject.Find("CiterneGrabMars");
        motorGrabMercure = GameObject.Find("MotorGrabMercure");
        cuisineGrabNeptune = GameObject.Find("CuisineGrabNeptune");
        solarPanelGrabSaturne = GameObject.Find("SolarPanelGrabSaturne");
        salleJardinGrabTerre = GameObject.Find("SalleJardinGrabTerre");
        capsuleCryoGrabUranus = GameObject.Find("CapsuleCryoGrabUranus");
        salleCryoGrabVenus = GameObject.Find("SalleCryoGrabVenus");

        // Get Robot collected
        petitRobotCollect = GameObject.Find("PetitRobotCollect");
        robotCuistoCollect = GameObject.Find("RobotCuistoCollect");

        // Active Object Scene name
        desactiveAllObject();
        CheckIfActiveObjectToGrab();
        VisitedPlanet();
        CheckPlanetVisited();

    } 

    private void VisitedPlanet()
    {
        switch (currentScene)
        {
            case "asteroide":
                asteroide = true;
                break;

            case "Mars":
                mars = true;
                break;

            case "Jupiter":
                jupiter = true;
                break;

            case "Lune":
                lune = true;
                break;

            case "Terre":
                terre = true;
                break;

            case "Mercure":
                mercure = true;
                break;

            case "Venus":
                venus = true;
                break;

            case "Saturne":
                saturne = true;
                break;

            case "Uranus":
                uranus = true;
                break;

            case "Neptune":
                neptune = true;
                break;

        }
    }

    private void desactiveAllObject()
    {
        // SetActive false GemeObject collected
        motorCollectAsteroide.SetActive(false);
        batteryCollectJupiter.SetActive(false);
        paraboleCollectLune.SetActive(false);
        citerneCollectMars.SetActive(false);
        motorCollectMercure.SetActive(false);
        cuisineCollectNeptune.SetActive(false);
        solarPanelCollectSaturne.SetActive(false);
        salleJardinCollectTerre.SetActive(false);
        capsuleCryoCollectUranus.SetActive(false);
        cryoCollectVenus.SetActive(false);

        // SetActive false GemeObject grab
        motorGrabAsteroide.SetActive(false);
        batteryGrabJupiter.SetActive(false);
        paraboleGrabLune.SetActive(false);
        citerneGrabMars.SetActive(false);
        motorGrabMercure.SetActive(false);
        cuisineGrabNeptune.SetActive(false);
        solarPanelGrabSaturne.SetActive(false);
        salleJardinGrabTerre.SetActive(false);
        capsuleCryoGrabUranus.SetActive(false);
        salleCryoGrabVenus.SetActive(false);

        // SetActive false GemeObject robot
        petitRobotCollect.SetActive(false);
        robotCuistoCollect.SetActive(false);
    }

    private void activeObjectToGrab(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    private void CheckPlanetVisited()
    {
        switch (true)
        {
            case var _ when asteroide:
                activeObjectAfter_Asteroide();
                break;
            case var _ when mars:
                activeObjectAfter_Mars();
                break;
            case var _ when jupiter:
                activeObjectAfter_Jupiter();
                break;
            case var _ when lune:
                activeObjectAfter_Lune();
                break;
            case var _ when terre:
                activeObjectAfter_Terre();
                break;
            case var _ when mercure:
                activeObjectAfter_Mercure();
                break;
            case var _ when venus:
                activeObjectAfter_Venus();
                break;
            case var _ when saturne:
                activeObjectAfter_Saturne();
                break;
            case var _ when uranus:
                activeObjectAfter_Uranus();
                break;
            case var _ when neptune:
                activeObjectAfter_Neptune();
                break;
            default:
                // Rien à faire si aucun booléen n'est vrai
                break;
        }
    }

    private void CheckIfActiveObjectToGrab()
    {
        switch (false)
        {
            case var _ when asteroide:
                activeObjectToGrab(motorGrabAsteroide);
                break;
            case var _ when mars:
                activeObjectToGrab(citerneGrabMars);
                break;
            case var _ when jupiter:
                activeObjectToGrab(batteryGrabJupiter);
                break;
            case var _ when lune:
                activeObjectToGrab(paraboleGrabLune);
                break;
            case var _ when terre:
                activeObjectToGrab(salleJardinGrabTerre);
                break;
            case var _ when mercure:
                activeObjectToGrab(motorGrabMercure);
                break;
            case var _ when venus:
                activeObjectToGrab(salleCryoGrabVenus);
                break;
            case var _ when saturne:
                activeObjectToGrab(solarPanelGrabSaturne);
                break;
            case var _ when uranus:
                activeObjectToGrab(capsuleCryoGrabUranus);
                break;
            case var _ when neptune:
                activeObjectToGrab(cuisineGrabNeptune);
                break;
        }
    }

    private void activeObjectAfter_Asteroide()
    {
        // Rien a activer
    }

    private void activeObjectAfter_Mars()
    {
        motorCollectAsteroide.SetActive(true);
    }

    private void activeObjectAfter_Jupiter()
    {
        citerneCollectMars.SetActive(true);
    }

    private void activeObjectAfter_Lune()
    {
        batteryCollectJupiter.SetActive(true);
    }

    private void activeObjectAfter_Terre()
    {
        paraboleCollectLune.SetActive(true);
    }

    private void activeObjectAfter_Mercure()
    {
        salleJardinCollectTerre.SetActive(true);
    }

    private void activeObjectAfter_Venus()
    {
        motorCollectMercure.SetActive(true);
    }

    private void activeObjectAfter_Saturne()
    {
        cryoCollectVenus.SetActive(true);
    }

    private void activeObjectAfter_Uranus()
    {
        solarPanelCollectSaturne.SetActive(true);
    }

    private void activeObjectAfter_Neptune()
    {
        GameObject obj = GameObject.Find("CuisineGrabNeptune");
        if (obj != null && obj.activeInHierarchy)
        {
            // Le GameObject est actif
            capsuleCryoCollectUranus.SetActive(true);
        }
        else
        {
            // Le GameObject est désactivé
            capsuleCryoCollectUranus.SetActive(true);
            cuisineCollectNeptune.SetActive(true);
        }

    }

}
