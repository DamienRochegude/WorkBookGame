using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KeybindDialogBox : MonoBehaviour
{
    [SerializeField] private GameObject keyList;
    [SerializeField] private GameObject keyItemPrefab;

    [SerializeField] private InputManager inputManager;

    private string buttonToRebind = null;
    private Dictionary<string, Text> buttonToLabel;

    private void Start()
    {

        string[] buttonNames = inputManager.GetButtonNames();
        buttonToLabel = new Dictionary<string, Text>();

        //foreach(string btn in buttonNames)
        for(int i = 0; i < buttonNames.Length; i++)
        {
            string btn;
            btn = buttonNames[i];

            GameObject go = (GameObject)Instantiate(keyItemPrefab);
            go.transform.SetParent(keyList.transform);
            go.transform.localScale = Vector3.one;

            Text buttonNameText = go.transform.Find("Button Name").GetComponent<Text>();
            buttonNameText.text = btn;

            Text keyNameText = go.transform.Find("Button/Key Name").GetComponent<Text>();
            keyNameText.text = inputManager.GetKeyNameForButton(btn);
            buttonToLabel[btn] = keyNameText;

            Button keyBindButton = go.transform.Find("Button").GetComponent<Button>();
            keyBindButton.onClick.AddListener( () => { StartRebindFor(btn); } );
        }
    }

    private void Update()
    {
        if(buttonToRebind != null)
        {
            if(Input.anyKeyDown)
            {
                Array kcs = Enum.GetValues(typeof(KeyCode));

                foreach(KeyCode kc in Enum.GetValues(typeof(KeyCode)))
                {
                    if(Input.GetKeyDown(kc))
                    {
                        inputManager.SetButtonForKey(buttonToRebind, kc);
                        buttonToLabel[buttonToRebind].text = kc.ToString();
                        buttonToRebind = null;
                        break;
                    }
                }
            }
        }
    }

    void StartRebindFor(string buttonName)
    {
        Debug.Log("StartRebindFor: " + buttonName);

        buttonToRebind = buttonName;
    }
}
