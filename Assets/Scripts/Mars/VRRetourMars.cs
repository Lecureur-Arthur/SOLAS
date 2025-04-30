using UnityEngine;
using UnityEngine.Events;

public class VRRetourMars : MonoBehaviour
{
    public GameObject button;  // Le bouton lui-m�me (GameObject)
    public UnityEvent onPress;  // Ev�nement d�clench� quand le bouton est press�
    public UnityEvent onRelease;  // Ev�nement d�clench� quand le bouton est rel�ch�

    private GameObject presser;  // L'objet qui presse le bouton (ton contr�leur VR)
    private AudioSource sound;  // Audio pour l'effet de pression
    private bool isPressed = false;  // Indique si le bouton est press�

    public RetourVaisseauMars retourMars;  // R�f�rence au script RetourVaisseau

    void Start()
    {
        sound = GetComponent<AudioSource>();  // R�cup�re le composant AudioSource
        isPressed = false;

        // V�rifie si le script RetourVaisseau est bien assign�
        if (retourMars == null)
        {
            Debug.LogError("RetourVaisseau script is not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si le bouton n'a pas d�j� �t� press�
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0.003f, 0);  // D�place l�g�rement le bouton
            presser = other.gameObject;  // Le contr�leur VR qui a press� le bouton
            onPress.Invoke();  // D�clenche l'�v�nement de pression du bouton
            sound.Play();  // Joue le son de pression
            isPressed = true;  // Indique que le bouton a �t� press�

            // Appeler la m�thode StartLoadingMoonScene() du script RetourVaisseau
            if (retourMars != null)
            {
                retourMars.StartLoadingMoonScene();  // Change la sc�ne
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Lorsque l'objet quitte la zone du bouton (rel�chement du bouton)
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.015f, 0);  // R�initialise la position du bouton
            onRelease.Invoke();  // D�clenche l'�v�nement de rel�chement du bouton
            isPressed = false;  // Le bouton est rel�ch�
        }
    }

    // Fonction pour ajouter des sph�res dans la sc�ne (pour le test)
    public void SpawnSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.localPosition = new Vector3(0, 1, 2);
        sphere.AddComponent<Rigidbody>();
    }
}
