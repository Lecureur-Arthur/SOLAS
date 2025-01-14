using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Keyboard : MonoBehaviour
{
    public TMP_InputField inputField;

    public enum Scenes
    {
        Asteroide,
        Mercure,
        Venus,
        Terre,
        Lune,
        Mars,
        Jupiter,
        Saturne,
        Uranus,
        Neptune
    }

    public Scenes SceneToLoad;

    public void InsertChar(string c)
    {        
        switch (c)
        {
            case "0":
                inputField.text = "Ceinture d'astéroïde";
                SceneToLoad = Scenes.Asteroide;
                break;
            case "1":
                inputField.text = "Mercure";
                SceneToLoad = Scenes.Mercure;
                break;
            case "2":
                inputField.text = "Vénus";
                SceneToLoad = Scenes.Venus;
                break;
            case "3":
                inputField.text = "Terre";
                SceneToLoad = Scenes.Terre;
                break;
            case "4":
                inputField.text = "Lune";
                SceneToLoad = Scenes.Lune;
                break;
            case "5":
                inputField.text = "Mars";
                SceneToLoad = Scenes.Mars;
                break;
            case "6":
                inputField.text = "Jupiter";
                SceneToLoad = Scenes.Jupiter;
                break;
            case "7":
                inputField.text = "Saturne";
                SceneToLoad = Scenes.Saturne;
                break;
            case "8":
                inputField.text = "Uranus";
                SceneToLoad = Scenes.Uranus;
                break;
            case "9":
                inputField.text = "Neptune";
                SceneToLoad = Scenes.Neptune;
                break;
        }
    }

    public void EnterCode()
    {
        //Debug.Log("Button press and try to Load scene name : " + SceneToLoad.ToString());
        SceneManager.LoadScene(SceneToLoad.ToString());
    }

    public void ResetKeypad()
    {
        inputField.text = string.Empty;
    }
}
