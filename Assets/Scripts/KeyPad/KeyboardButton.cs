using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class KeyboardButton : MonoBehaviour
{
    private Keyboard keyboard;
    private TextMeshProUGUI buttonText;

    // Start is called before the first frame update
    void Start()
    {
        // Trouver dynamiquement le Keyboard dans les parents ou la scène
        keyboard = FindObjectOfType<Keyboard>();
        if (keyboard == null)
        {
            Debug.LogError("Keyboard non trouvé dans la scène.");
            return;
        }

        // Récupérer le TextMeshProUGUI dans les enfants
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText == null)
        {
            Debug.LogError("TextMeshProUGUI non trouvé dans les enfants de " + gameObject.name);
            return;
        }

        // Assigner le nom de l'objet au texte
        NameToButtonText();

        // Vérification de la longueur du texte avant d'ajouter un listener
        if (buttonText.text.Length == 1)
        {
            // Récupérer le ButtonVR dans les enfants
            ButtonVR buttonVR = GetComponentInChildren<ButtonVR>();
            if (buttonVR != null)
            {
                // S'assurer que onRelease est initialisé
                if (buttonVR.onRelease == null)
                {
                    buttonVR.onRelease = new UnityEvent();
                }

                // Ajouter un listener avec des vérifications null
                buttonVR.onRelease.AddListener(delegate
                {
                    if (keyboard != null && buttonText != null)
                    {
                        keyboard.InsertChar(buttonText.text);
                    }
                    else
                    {
                        Debug.LogError("keyboard ou buttonText est null au moment de l'appel du listener.");
                    }
                });
            }
            else
            {
                Debug.LogError("ButtonVR non trouvé dans les enfants de " + gameObject.name);
            }
        }
        else if (buttonText.text == "Enter")
        {
            // Ajouter un listener pour le bouton "Enter"
            ButtonVR buttonVR = GetComponentInChildren<ButtonVR>();

            if (buttonVR != null)
            {
                if (buttonVR.onRelease == null) 
                {
                    buttonVR.onRelease = new UnityEvent();
                }

                // Ajouter un listener pour verifier le code quand "Enter" est presse
                buttonVR.onRelease.AddListener(delegate
                {
                    keyboard.EnterCode();
                });
            }
            else
            {
                Debug.LogError("ButtonVR non trouvé dans les enfants de " + gameObject.name);
            }
        }
        else if (buttonText.text == "Del")
        {
            // Ajouter un listener pour le bouton "Del" (delete)
            ButtonVR buttonVR = GetComponentInChildren<ButtonVR>();
            if (buttonVR != null)
            {
                if (buttonVR.onRelease == null)
                {
                    buttonVR.onRelease = new UnityEvent();
                }

                // Ajouter un listener pour supprimer uin caractere lorsque "Del" est pressé
                buttonVR.onRelease.AddListener(delegate
                {
                    keyboard.DeleteChar();
                });
            }
            else
            {
                Debug.LogError("ButtonVR non trouvé dans les enfants de " + gameObject.name);
            }
        }
    }

    // Méthode pour assigner le nom de l'objet au texte du TextMeshPro
    public void NameToButtonText()
    {
        buttonText.text = gameObject.name;
    }
}
