using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Nécessaire pour manipuler les scènes

public class PlayVoixOff : MonoBehaviour
{
    public AudioClip asteroide;
    public AudioClip mercure;
    public AudioClip venus;
    public AudioClip Terre;
    public AudioClip Lune;
    public AudioClip Mars;
    public AudioClip Jupiter;
    public AudioClip Saturne;
    public AudioClip Uranus;
    public AudioClip Neptune;

    public AudioClip woosh;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Obtenir le nom de la scène actuelle
        string currentScene = SceneManager.GetActiveScene().name;

        // Jouer le clip correspondant à la scène
        PlayAudioForScene(currentScene);
    }

    void PlayAudioForScene(string sceneName)
    {
        StartCoroutine(PlaySoundStartChargement(sceneName));
    }

    IEnumerator PlaySoundStartChargement(string sceneName)
    {

        audioSource.PlayOneShot(woosh);

        yield return new WaitForSeconds(0);
        // Utiliser un switch pour déterminer quel clip jouer en fonction de la scène
        AudioClip clipToPlay = null;

        switch (sceneName)
        {
            case "asteroide":
                clipToPlay = asteroide;
                break;
            case "Mercure":
                clipToPlay = mercure;
                break;
            case "Venus":
                clipToPlay = venus;
                break;
            case "Terre":
                clipToPlay = Terre;
                break;
            case "Lune":
                clipToPlay = Lune;
                break;
            case "Mars":
                clipToPlay = Mars;
                break;
            case "Jupiter":
                clipToPlay = Jupiter;
                break;
            case "Saturne":
                clipToPlay = Saturne;
                break;
            case "Uranus":
                clipToPlay = Uranus;
                break;
            case "Neptune":
                clipToPlay = Neptune;
                break;
        }

        if (clipToPlay != null)
        {
            yield return new WaitForSeconds(1);
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }
    }
}
