using UnityEngine;
using UnityEngine.SceneManagement;

public class RetourVaisseauTerre : MonoBehaviour
{
    private bool isPressed = false;

    void Update()
    {
    
        if (isPressed)
        {
            SceneManager.LoadScene("Terre");
        }
    }

    public void StartLoadingEarthScene()
    {
        isPressed = true;
    }

    public void StopLoading()
    {
        isPressed = false;
    }
}
