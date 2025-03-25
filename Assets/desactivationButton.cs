using UnityEngine;

public class desactivationButton : MonoBehaviour
{

/*
    public GameObject myButton;  // Référence à ton GameObject
    private Renderer buttonRenderer;
    private Collider buttonCollider;

    void Start()
    {
        // Désactivation des interactions (Collider)
        buttonCollider = myButton.GetComponent<Collider>();
        if (buttonCollider != null)
        {
            buttonCollider.enabled = false;  // Désactiver le collider
        }

        // Griser le bouton
        buttonRenderer = myButton.GetComponent<Renderer>();
        if (buttonRenderer != null)
        {
            // Changer la couleur à gris
            buttonRenderer.material.color = Color.gray;

            // Ou changer la transparence
            Color currentColor = buttonRenderer.material.color;
            currentColor.a = 0.5f;  // Transparence à 50%
            buttonRenderer.material.color = currentColor;
        }
    }*/

    public GameObject myButton;  // Le GameObject dont tu veux changer la couleur
    public Color newColor = Color.green;  // Nouvelle couleur (exemple : vert)

    void Start()
    {
        // Accéder au Renderer du GameObject
        Renderer renderer = myButton.GetComponent<Renderer>();

        if (renderer != null)
        {
            // Modifier la couleur du matériau
            renderer.material.color = newColor;
        }
        else
        {
            Debug.LogWarning("Le GameObject ne possède pas de Renderer.");
        }
    }

}
