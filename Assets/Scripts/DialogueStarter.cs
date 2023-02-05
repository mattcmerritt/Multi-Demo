using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    private DialogueController dc;
    public DialogueEntry entry;
    public GameObject questionsBox;

    void Start()
    {
        dc = GameObject.FindObjectOfType<DialogueController>();
        questionsBox = GameObject.Find("Questions Box");
    }

    public void RunDialogueEntry()
    {
        dc.HandleCallback("next", new Object[] {entry});

        dc.questionsUp = false;

        // destroy children questions buttons
        foreach (Transform t in questionsBox.transform)
        {
            Destroy(t.gameObject);
        }
    }
}
