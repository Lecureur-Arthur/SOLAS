using UnityEngine;
using UnityEngine.SceneManagement;  // N�cessaire pour g�rer les sc�nes

public class SceneChanger : MonoBehaviour
{
    // Cette m�thode sera appel�e lorsque le bouton UI est press�
    public void LoadScene(string sceneName)
    {
       SceneManager.LoadScene(sceneName);
    }
}
