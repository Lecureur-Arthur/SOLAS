using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Récupérer la référence InputActionReference (nécessite l'import de UnityEngine.InputSystem). BIEN PENSER A DEFINIR DANS L'INSPECTEUR
    [SerializeField] public InputActionReference inputActionReference_GameDefinedActions;
    private GameObject CanvaPauseMenu;

    private GameObject LocomotionSystem; 
    
    // Start is called before the first frame update
    private void Start()
    {
        // On définit le callback qui sera appelé par le listener "performed" (qui s'exécute lors de l'utilisation de l'input)
        inputActionReference_GameDefinedActions.action.performed += ChangeState;
        CanvaPauseMenu = GameObject.Find("MenuPause");
        LocomotionSystem = GameObject.Find("Locomotion System");
        CanvaPauseMenu.SetActive(false);
    }

    // fonction de callback (format spécifique: prend comme argument un CallbackContext d'InputAction)
    private void ChangeState(InputAction.CallbackContext obj){
        if (CanvaPauseMenu.activeSelf){
            // Desactiver le Canva de pause
            CanvaPauseMenu.SetActive(false);

            // Activer le locomotion systeme
            LocomotionSystem.SetActive(true);
        }else{
            // Activer le Canva de pause
            CanvaPauseMenu.SetActive(true);

            // Desactiver le locomotion system
            LocomotionSystem.SetActive(false);
        }
    }

    public void ReprendreBtn()
    {
        Debug.Log("Reprendre Btn Click");
        // Desactiver le Canva de pause
        CanvaPauseMenu.SetActive(false);

        // Activer le locomotion systeme
        LocomotionSystem.SetActive(true);
    }

    public void SauvegardeBtn()
    {
        Debug.Log("Sauvegarde Btn Click");        
    }

    public void QuitBtn()
    {
        Debug.Log("Quit Btn Click");
        SceneManager.LoadScene("MainMenu");
        
    }
}
