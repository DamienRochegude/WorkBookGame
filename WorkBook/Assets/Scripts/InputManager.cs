using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InputManager : MonoBehaviour
{
    private Dictionary<string, KeyCode> buttonKeys;

    void OnEnable()
    {
        buttonKeys = new Dictionary<string, KeyCode>();

        buttonKeys["Up"] = KeyCode.UpArrow;
        buttonKeys["Down"] = KeyCode.DownArrow;
        buttonKeys["Left"] = KeyCode.LeftArrow;
        buttonKeys["Right"] = KeyCode.RightArrow;
    }

    public bool GetButtonDown(string buttonName)
    {
        if (buttonKeys.ContainsKey(buttonName) == false)
        {
            Debug.LogError("InputManager::GetButtonDown -- No button found: " + buttonName);
            return false;
        }

        return Input.GetKeyDown(buttonKeys[buttonName]);
    }

    public string[] GetButtonNames()
    {
        return buttonKeys.Keys.ToArray();
    }

    public string GetKeyNameForButton(string buttonName)
    {
        if (buttonKeys.ContainsKey(buttonName) == false)
        {
            Debug.LogError("InputManager::GetKeyNameForButton -- No button found: " + buttonName);
            return "N/A";
        }

        return buttonKeys[buttonName].ToString();
    }

    public void SetButtonForKey(string buttonName, KeyCode keyCode)
    {
        buttonKeys[buttonName] = keyCode;
    }
    
}
