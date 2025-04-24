using UnityEngine;
using UnityEngine.SceneManagement;

public class AtterrissageLune : MonoBehaviour
{
    private bool isPressed = false;

    void Update()
    {
        // Si isPressed est vrai, charger la scène "Lune"
        if (isPressed)
        {
            SceneManager.LoadScene("LuneSurface");  // Charge la scène "Lune"
        }
    }

    // Cette méthode est appelée lorsque le bouton est pressé
    public void StartLoadingMoonScene()
    {
        isPressed = true;  // Change isPressed à true, ce qui déclenche le chargement de la scène
    }

    public void StopLoading()
    {
        isPressed = false;  // Remet isPressed à false si tu veux annuler ou réinitialiser l'état
    }
}
