using UnityEngine;
using UnityEngine.SceneManagement;

public class AtterrissageTerre : MonoBehaviour
{
    private bool isPressed = false;

    void Update()
    {
        // Si isPressed est vrai, charger la sc�ne "Terre"
        if (isPressed)
        {
            SceneManager.LoadScene("TerreSurface");
        }
    }

    // Cette m�thode est appel�e lorsque le bouton est press�
    public void StartLoadingTerreScene()
    {
        isPressed = true;  // Change isPressed � true, ce qui d�clenche le chargement de la sc�ne
    }

    public void StopLoading()
    {
        isPressed = false;  // Remet isPressed � false si tu veux annuler ou r�initialiser l'�tat
    }
}
