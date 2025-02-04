using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    // Récupérer la référence InputActionReference (nécessite l'import de UnityEngine.InputSystem). BIEN PENSER A DEFINIR DANS L'INSPECTEUR
    [SerializeField] public InputActionReference inputActionReference_GameDefinedActions;
    public GameObject CanvaPauseMenu;
    
    // Start is called before the first frame update
    private void Start()
    {
        // On définit le callback qui sera appelé par le listener "performed" (qui s'exécute lors de l'utilisation de l'input)
        inputActionReference_GameDefinedActions.action.performed += ChangeState;
        CanvaPauseMenu.SetActive(false);
    }

    // fonction de callback (format spécifique: prend comme argument un CallbackContext d'InputAction)
    private void ChangeState(InputAction.CallbackContext obj){
        if (CanvaPauseMenu.activeSelf){
            CanvaPauseMenu.SetActive(false);
        }else{
            CanvaPauseMenu.SetActive(true);
        }
    }
}
