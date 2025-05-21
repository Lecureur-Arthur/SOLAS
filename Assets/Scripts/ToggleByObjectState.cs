using System.Collections.Generic;
using UnityEngine;

public class ToggleByObjectStateList : MonoBehaviour
{
    [Header("Liste des objets dont on vérifie l'état")]
    public List<GameObject> conditionObjects;

    [Header("Objet à désactiver ou activer")]
    public GameObject toDisable;

    [Tooltip("Désactiver 'toDisable' si AU MOINS UN objet de la liste est actif")]
    public bool disableIfAnyConditionActive = true;

    void Update()
    {
        if (conditionObjects == null || conditionObjects.Count == 0 || toDisable == null)
            return;

        bool anyActive = false;
        foreach (GameObject obj in conditionObjects)
        {
            if (obj != null && obj.activeInHierarchy)
            {
                anyActive = true;
                break;
            }
        }

        if (disableIfAnyConditionActive && anyActive)
        {
            if (toDisable.activeSelf)
                toDisable.SetActive(false);
        }
        else if (!disableIfAnyConditionActive && !anyActive)
        {
            if (toDisable.activeSelf)
                toDisable.SetActive(false);
        }
        else
        {
            if (!toDisable.activeSelf)
                toDisable.SetActive(true);
        }
    }
}
