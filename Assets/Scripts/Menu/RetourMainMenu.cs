using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerToMainMenu : MonoBehaviour
{
    public float timerDuration = 5f; // Durée avant le chargement de la scène
    private float timer;

    void Start()
    {
        timer = timerDuration;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
