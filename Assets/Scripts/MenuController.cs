using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject AboutMenu
    ;
    void Start()
    {
        MainMenu.SetActive(true);
        AboutMenu.SetActive(false);
    }

    public void StartBtn()
    {
        Debug.Log("StartBtnClick");
        SceneManager.LoadScene("asteroide");
    }

    public void ContinueBtn()
    {
        Debug.Log("ContinueBtnClick");
    }

    public void AboutBtn()
    {
        Debug.Log("AboutBtnClick");
        MainMenu.SetActive(false);
        AboutMenu.SetActive(true);
    }

    public void BackBtn()
    {
        Debug.Log("BackBtnClick");
        MainMenu.SetActive(true);
        AboutMenu.SetActive(false);
    }

    public void QuitBtn()
    {
        Debug.Log("QuitBtnClick");
        Application.Quit();
    }
}
