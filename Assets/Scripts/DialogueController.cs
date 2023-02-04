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
    public bool dialogueActive = false;

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
        dialogueActive = true;

        speaker.text = activeEntry.speaker;
        message.text = "";

        dialogueBox.SetActive(true);

        // start the message typing
        writingCoroutine = StartCoroutine(TypeText(activeEntry.message));
    }

    public void HandleCallback(string callback, string[] parameters)
    {
        // load new line of dialogue
        if(callback == "next")
        {
            AssetDatabase.FindAssets(parameters[0], new[] {"Assets/DialogueEntries"});
        }
    }

    void Update()
    {
        if (dialogueActive)
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
}
