using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject mainSection;
    public GameObject aboutSection;

    void Start()
    {
        mainSection.SetActive(true);
        aboutSection.SetActive(false);
    }

    public void StartBtn()
    {
        Debug.Log("StartBtnClick");
        SceneManager.LoadScene("asteroide");
    }

    public void AboutBtn()
    {
        Debug.Log("AboutBtnClick");
        mainSection.SetActive(false);
        aboutSection.SetActive(true);
    }

    public void BackBtn()
    {
        Debug.Log("BackBtnClick");
        mainSection.SetActive(true);
        aboutSection.SetActive(false);
    }

    public void QuitBtn()
    {
        Debug.Log("QuitBtnClick");
        Application.Quit();
    }
}
