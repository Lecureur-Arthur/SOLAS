using UnityEngine;
using UnityEngine.SceneManagement;  // Nécessaire pour gérer les scènes

public class SceneChanger : MonoBehaviour
{
    // Cette méthode sera appelée lorsque le bouton UI est pressé
    public void LoadScene(string sceneName)
    {
       SceneManager.LoadScene(sceneName);
    }
}
