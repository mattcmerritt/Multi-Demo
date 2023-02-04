using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public DialogueEntry activeEntry;
    public GameObject dialogueBox;
    public TMP_Text speaker;
    public TMP_Text message;
    public Image character;
    private Coroutine writingCoroutine;
    public GameObject characterSelectUI;
    public GameObject conversationUI;

    IEnumerator TypeText(string fullMessage)
    {
        char[] fullMessageChars = fullMessage.ToCharArray();
        foreach (char character in fullMessageChars) {
            message.SetText(message.text + character);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void StartDialogue()
    {
        speaker.text = activeEntry.speaker;
        message.text = "";
        character.sprite = activeEntry.img;

        characterSelectUI.SetActive(false);
        conversationUI.SetActive(true);
        dialogueBox.SetActive(true);

        // start the message typing
        writingCoroutine = StartCoroutine(TypeText(activeEntry.message));
    }

    // only works in editor
    // public void HandleCallback(string callback, string[] parameters)
    // {
    //     // load new line of dialogue
    //     if(callback == "next")
    //     {
    //         string[] newEntries = AssetDatabase.FindAssets(parameters[0], new[] {"Assets/DialogueEntries"}); // returns array of GUIDs
    //         if(newEntries.Length > 1)
    //         {
    //             Debug.LogError("DIALOGUE ERROR: There are multiple dialogue entries with the specified filename (" + parameters[0] + "). Please change them to be unique.");
    //         }
    //         else if(newEntries.Length < 1)
    //         {
    //             Debug.LogError("DIALOGUE ERROR: There are no dialogue entries with the specified filename (" + parameters[0] + "). Please change the parameter to a valid dialogue entry, or change the callback function.");
    //         }
    //         else
    //         {
    //             string assetPath = AssetDatabase.GUIDToAssetPath(newEntries[0]);
    //             activeEntry = AssetDatabase.LoadAssetAtPath<DialogueEntry>(assetPath);
    //             StartDialogue();
    //         }
    //     }
    // }

    public void HandleCallback(string callback, Object[] parameters)
    {
        // load new line of dialogue
        if(callback == "next")
        {
            activeEntry = (DialogueEntry) parameters[0];
            StartDialogue();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dialogueBox.activeSelf)
        {
            // if we are still typing out the message, autocomplete it
            if (activeEntry.message != message.text)
            {
                // stop the writing routine
                StopCoroutine(writingCoroutine);
                // complete the message
                message.SetText(activeEntry.message);
            }
            // else text is fully typed, so do callback
            else
            {
                HandleCallback(activeEntry.callback, activeEntry.parameters);
            }
        }
    }
}
