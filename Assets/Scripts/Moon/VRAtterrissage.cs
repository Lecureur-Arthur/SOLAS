using UnityEngine;
using UnityEngine.Events;

public class VRAtterrissage : MonoBehaviour
{
    public GameObject button;  // Le bouton lui-même (GameObject)
    public UnityEvent onPress;  // Evénement déclenché quand le bouton est pressé
    public UnityEvent onRelease;  // Evénement déclenché quand le bouton est relâché

    private GameObject presser;  // L'objet qui presse le bouton (ton contrôleur VR)
    private AudioSource sound;  // Audio pour l'effet de pression
    private bool isPressed = false;  // Indique si le bouton est pressé

    public AtterrissageLune atterrissage;  // Référence au script RetourVaisseau

    void Start()
    {
        sound = GetComponent<AudioSource>();  // Récupère le composant AudioSource
        isPressed = false;

        // Vérifie si le script RetourVaisseau est bien assigné
        if (atterrissage == null)
        {
            Debug.LogError("RetourVaisseau script is not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si le bouton n'a pas déjà été pressé
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0.003f, 0);  // Déplace légèrement le bouton
            presser = other.gameObject;  // Le contrôleur VR qui a pressé le bouton
            onPress.Invoke();  // Déclenche l'événement de pression du bouton
            sound.Play();  // Joue le son de pression
            isPressed = true;  // Indique que le bouton a été pressé

            // Appeler la méthode StartLoadingMoonScene() du script RetourVaisseau
            if (atterrissage != null)
            {
                atterrissage.StartLoadingMoonScene();  // Change la scène
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Lorsque l'objet quitte la zone du bouton (relâchement du bouton)
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.015f, 0);  // Réinitialise la position du bouton
            onRelease.Invoke();  // Déclenche l'événement de relâchement du bouton
            isPressed = false;  // Le bouton est relâché
        }
    }

    // Fonction pour ajouter des sphères dans la scène (pour le test)
    public void SpawnSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.localPosition = new Vector3(0, 1, 2);
        sphere.AddComponent<Rigidbody>();
    }
}
