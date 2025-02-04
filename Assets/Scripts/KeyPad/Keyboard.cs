using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;


public class Keyboard : MonoBehaviour
{
    public TMP_InputField inputField;

    public enum Scenes
    {
        asteroide,
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

    private string sceneName;


    public Transform spawnParent; // Where to spawn the cubes (e.g., an empty GameObject in the scene)
    [SerializeField] private ChangementScene spawn;
    public GameObject Cubes;


    void Start()
    {
        // Récupère le nom de la scène actuelle
        sceneName = SceneManager.GetActiveScene().name;

        // Tente de convertir le nom de la scène actuelle en une valeur de l'énumération Scenes
        if (System.Enum.TryParse(sceneName, out Scenes currentScene))
        {
            SceneToLoad = currentScene; // Assigne la valeur de l'énumération à SceneToLoad
            Debug.Log("SceneToLoad a été définie à : " + SceneToLoad);
        }
        else
        {
            Debug.LogWarning("La scène actuelle n'existe pas dans l'énumération Scenes !");
        }

        Debug.Log("Nom de la scène actuelle : " + sceneName);
    }


    public void InsertChar(string c)
    {        
        switch (c)
        {
            case "0":
                inputField.text = "Ceinture d'astéroïde";
                SceneToLoad = Scenes.asteroide;
             

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

    public void ResetKeypad()
    {
        inputField.text = string.Empty;
    }



    public void EnterCode()
    {
        // Appelle ToString() correctement avec des parenthèses
        if (sceneName != SceneToLoad.ToString())
        {
            Debug.Log("Les scènes sont différentes !");
            // Ajouter le code pour charger la nouvelle scène ici
            //SceneManager.LoadScene(SceneToLoad.ToString());
            spawn.SpawnObject();
            //Instantiate(Cubes, new Vector3(565, 2, 100), Quaternion.identity);
        }
    }


}
