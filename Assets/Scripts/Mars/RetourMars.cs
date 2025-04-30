using UnityEngine;
using UnityEngine.SceneManagement;

public class RetourVaisseauMars : MonoBehaviour
{
    private bool isPressed = false;

    void Update()
    {
        // Si isPressed est vrai, charger la sc�ne "Lune"
        if (isPressed)
        {
            SceneManager.LoadScene("Mars");  // Charge la sc�ne "Lune"
        }
    }

    // Cette m�thode est appel�e lorsque le bouton est press�
    public void StartLoadingMoonScene()
    {
        isPressed = true;  // Change isPressed � true, ce qui d�clenche le chargement de la sc�ne
    }

    public void StopLoading()
    {
        isPressed = false;  // Remet isPressed � false si tu veux annuler ou r�initialiser l'�tat
    }
}
