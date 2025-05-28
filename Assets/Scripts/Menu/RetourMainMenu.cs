using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerToMainMenu : MonoBehaviour
{
    public float timerDuration = 5f; // Dur�e avant le chargement de la sc�ne
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
