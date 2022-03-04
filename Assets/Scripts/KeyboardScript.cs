using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class KeyboardScript : MonoBehaviour
{
    public TMP_InputField textField;

    public void AlphabetFunction()
    {
        textField.text += EventSystem.current.currentSelectedGameObject.name;
        Vibrator.Vibrate(100);
        // Handheld.Vibrate();
    }

    public void BackSpace()
    {
        if (textField.text.Length > 0) textField.text = textField.text.Remove(textField.text.Length - 1);
    }

}