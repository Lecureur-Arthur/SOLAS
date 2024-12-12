using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VattalusKeyPrompt : MonoBehaviour
{
    //This script is used to hold references to key prompts on the UI. It also has methods to update the Text

    public Text promptNameText;
    public Text promptKeyText;

    public void UpdateKeyPromptTexts(string text, string key, bool interactable = true)
    {
        if (promptNameText != null) promptNameText.text = text;
        if (promptKeyText != null) promptKeyText.text = key;

        promptKeyText.color = interactable ? Color.white : Color.grey;
        promptKeyText.color = interactable ? Color.white : Color.grey;
    }
}
