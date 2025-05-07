using UnityEngine;
using UnityEngine.Events;

public class VRRetourTerre : MonoBehaviour
{
    public GameObject button; 
    public UnityEvent onPress;  
    public UnityEvent onRelease;  

    private GameObject presser;  
    private AudioSource sound;  
    private bool isPressed = false;  

    public RetourVaisseauTerre retourTerre;  

    void Start()
    {
        sound = GetComponent<AudioSource>();  
        isPressed = false;

        
        if (retourTerre == null)
        {
            Debug.LogError("RetourVaisseau script is not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0.003f, 0);  
            presser = other.gameObject; 
            onPress.Invoke();  
            sound.Play();  
            isPressed = true; 

           
            if (retourTerre != null)
            {
                retourTerre.StartLoadingEarthScene();  
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
      
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.015f, 0);  
            onRelease.Invoke();  
            isPressed = false;  
        }
    }

    public void SpawnSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.localPosition = new Vector3(0, 1, 2);
        sphere.AddComponent<Rigidbody>();
    }
}
